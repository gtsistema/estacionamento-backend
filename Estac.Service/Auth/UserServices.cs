using AutoMapper;
using Estac.Domain.Auth;
using Estac.Domain.Input.Auth;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Repositories.Auth;
using Estac.Domain.Interface.Services;
using Estac.Domain.Interface.Services.Auth;
using Estac.Domain.Models;
using Estac.Domain.Models.Auth;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.Auth;
using Estac.Infra.Repositories;
using Estac.Service.Extensions;
using Estac.Service.Identity.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Estac.Service.Auth
{
    public partial class UserServices : ServiceResult<ApplicationUser>, IUserServices
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationSignManager _signManager;
        private readonly IMapper _mapper;
        private readonly BearerTokenSettings _bearerTokenSettings;
        private readonly UserManager<ApplicationUser> _identityUserManager;
        private readonly IPessoaRepositories _pessoaRepositories;
        private readonly IPerfilRepositories _perfilRepositories;
        private readonly IMenuRepositories _menuRepositories;
        private readonly IUsuarioRepositories _usuarioRepositories;
        private readonly RoleManager<ApplicationRole> _identityRoleManager;
        private readonly EmailConfirmationSettings _emailConfirmation;
        private readonly IEmailSenderService _emailSender;
        private readonly ILogger<UserServices> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UserServices(
            IApplicationUserManager userManager,
            IApplicationSignManager signManager,
            IOptions<BearerTokenSettings> bearerTokenSettings,
            IOptions<EmailConfirmationSettings> emailConfirmation,
            IEmailSenderService emailSender,
            ILogger<UserServices> logger,
            IMapper mapper,
            IErrorServices errorApplication,
            UserManager<ApplicationUser> identityUserManager,
            IPessoaRepositories pessoaRepositories,
            IPerfilRepositories perfilRepositories,
            IMenuRepositories menuRepositories,
            IUsuarioRepositories usuarioRepositories,
            RoleManager<ApplicationRole> identityRoleManager,
            IUnitOfWork unitOfWork)
            : base(errorApplication)
        {
            _bearerTokenSettings = bearerTokenSettings.Value;
            _emailConfirmation = emailConfirmation.Value ?? new EmailConfirmationSettings();
            _emailSender = emailSender;
            _logger = logger;
            _userManager = userManager;
            _signManager = signManager;
            _mapper = mapper;
            _identityUserManager = identityUserManager;
            _pessoaRepositories = pessoaRepositories;
            _perfilRepositories = perfilRepositories;
            _menuRepositories = menuRepositories;
            _usuarioRepositories = usuarioRepositories;
            _identityRoleManager = identityRoleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> LoginAsync(LoginInput dto)
        {
            try
            {
                var signIn = await _signManager.PasswordSignInAsync(dto.UserName, dto.Password);
                if (!signIn.Succeeded)
                    return await RespostaLoginFalhouAsync(dto.UserName, signIn);

                var user = await _identityUserManager.FindByNameAsync(dto.UserName);
                return await RetornOk(await MontarLoginResponseAsync(user), Resources.Resources.MSG_OperacaoRealizadaSucesso);
            }
            catch (Exception ex)
            {
                return await RetornNo(ex, ex.Message);
            }
        }

        public async Task<ActionResult> RegisterAsync(RegisterInput dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Password))
                return await RetornNo(false, "Senha é obrigatória no cadastro.");

            var pessoa = _mapper.Map<Pessoa>(dto.Pessoa);
            pessoa.Email = dto.Email;
            pessoa.Ativo = true;

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                pessoa.AdicionarPapel(TipoPapel.Funcionario);
                await _pessoaRepositories.Gravar(pessoa);

                var user = _mapper.Map<ApplicationUser>(dto);
                user.PessoaId = pessoa.Id;
                user.EmailConfirmed = false;
                user.FullName = pessoa.NomeFantasia;

                var create = await _userManager.CreateAsync(user, dto.Password);

                if (!create.Succeeded)
                    return await RetornNo(false, PrimeiroErroCadastro(create));

                await _userManager.AddToRoleAsync(user, dto.Perfil.Name);

                await _unitOfWork.CommitAsync();

                var usuarioAtual = await _identityUserManager.FindByNameAsync(dto.UserName) ?? user;
                var resposta = await MontarRespostaPosRegistroAsync(pessoa, usuarioAtual);
                return await RetornOk(resposta, Resources.Resources.MSG_OperacaoRealizadaSucesso);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                return await RetornNo(ex, Resources.Resources.MSG_OperacaoComErro);
            }
        }

        public async Task<ActionResult> Buscar()
        {
            return await RetornOk(await _usuarioRepositories.BuscarUsuariosGrid(null));
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            if (id <= 0)
                return await RetornNo(false, "Id de usuário inválido.");

            var user = await _identityUserManager.FindByIdAsync(id.ToString());
            if (user is null || user.IsDeleted == true)
                return await RetornNo(false, "Usuário não encontrado.");

            return await RetornOk(await MontarUsuarioDetalheAsync(user));
        }

        public async Task<ActionResult> Alterar(int id, RegisterInput input)
        {
            var erroValidacao = ValidarInputAlteracao(id, input);
            if (erroValidacao != null)
                return await RetornNo(false, erroValidacao);

            var user = await _identityUserManager.FindByIdAsync(id.ToString());
            if (user is null || user.IsDeleted == true)
                return await RetornNo(false, "Usuário não encontrado.");

            var outro = await _identityUserManager.FindByNameAsync(input.UserName);
            if (outro is not null && outro.Id != id)
                return await RetornNo(false, "Nome de usuário já está em uso.");


            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var pessoa = await SincronizarPessoaDoUsuarioAsync(user, input);
                if (pessoa is null)
                    return await RetornNo(false, "Pessoa vinculada ao usuário não foi encontrada.");

                var identity = await AtualizarUsuarioIdentityAsync(user, input, pessoa);
                if (identity != null)
                    return identity;

                if (!string.IsNullOrWhiteSpace(input.Password))
                {
                    var senha = await _userManager.ChangePasswordAsync(user, input.Password);
                    if (!senha.Succeeded)
                        return await RetornNo(false, MensagemErrosIdentity(senha));
                }

                var role = await SincronizarPerfilAsync(user, input.Perfil.Name);

                await _unitOfWork.CommitAsync();

                if (role != null)
                    return role;

                var atualizado = await _identityUserManager.FindByIdAsync(id.ToString());
                return await RetornOk(await MontarUsuarioDetalheAsync(atualizado!));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                return await RetornNo(ex, Resources.Resources.MSG_OperacaoComErro);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return await RetornNo(false, "Id de usuário inválido.");

            var user = await _identityUserManager.FindByIdAsync(id.ToString());
            if (user is null || user.IsDeleted == true)
                return await RetornNo(false, "Usuário não encontrado.");

            try
            {
                int? pessoaId = user.PessoaId;
                var remocao = await _identityUserManager.DeleteAsync(user);
                if (!remocao.Succeeded)
                    return await RetornNo(false, MensagemErrosIdentity(remocao));

                if (pessoaId is int pid and > 0)
                    await _pessoaRepositories.Excluir(pid);

                return await RetornOk(Resources.Resources.MSG_OperacaoRealizadaSucesso);
            }
            catch (Exception ex)
            {
                return await RetornNo(ex, Resources.Resources.MSG_OperacaoComErro);
            }
        }

        public async Task<ActionResult> ConfirmarEmailAsync(ConfirmarEmailInput input)
        {
            if (input is null)
                return await RetornNo(false, "Dados inválidos.");

            var erros = (await _userManager.ConfirmEmailAsync(input.UserId, input.Token))?.ToList();
            if (erros is { Count: > 0 })
                return await RetornNo(false, string.Join(" ", erros));

            return await RetornOk(new { emailConfirmado = true }, "E-mail confirmado. Você já pode fazer login.");
        }

        public async Task<ActionResult> EsqueciSenhaAsync(EsqueciSenhaInput input)
        {
            if (input is null || string.IsNullOrWhiteSpace(input.Email))
                return await RetornNo(false, "E-mail é obrigatório.");

            return await ProcessarEsqueciSenhaAsync(input.Email.Trim());
        }

        public async Task<ActionResult> RedefinirSenhaAsync(RedefinirSenhaInput input)
        {
            if (input is null)
                return await RetornNo(false, "Dados inválidos.");
            if (input.NewPassword != input.ConfirmPassword)
                return await RetornNo(false, "Confirmação de senha não confere com a nova senha.");

            var user = await _userManager.FindByEmailAsync(input.Email.Trim());
            if (user is null || user.IsDeleted == true)
                return await RetornNo(false, "Não foi possível redefinir a senha. Verifique o link ou solicite um novo.");

            var erros = (await _userManager.ResetPasswordAsync(user, input.Token, input.NewPassword))?.ToList();

            if (erros is { Count: > 0 })
                return await RetornNo(false, string.Join(" ", erros));

            return await RetornOk(new { senhaAlterada = true }, "Senha alterada com sucesso. Você já pode fazer login.");
        }
    }
}
