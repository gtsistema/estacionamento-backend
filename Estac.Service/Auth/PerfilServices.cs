using AutoMapper;
using Azure.Core;
using DocumentFormat.OpenXml.Office2010.Excel;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Repositories.Auth;
using Estac.Domain.Interface.Services.Auth;
using Estac.Domain.Models;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output;
using Estac.Domain.Output.Auth;
using Estac.Domain.Output.Motorista;
using Estac.Infra.Context;
using Estac.Service.Extensions;
using Estac.Service.Identity.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace Estac.Service.Auth
{
    public class PerfilServices : ServiceResult<PerfilOutput>, IPerfilServices
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationSignManager _signManager;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly GtsContext _context;
        private readonly BearerTokenSettings _bearerTokenSettings;
        private readonly UserManager<ApplicationUser> _identityUserManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IPerfilRepositories _perfilRepositories;

        public PerfilServices(IApplicationUserManager userManager,
               IApplicationSignManager signManager, ICurrentUser currentUser,
               IOptions<BearerTokenSettings> bearerTokenSettings,
               IMapper mapper,
               GtsContext context,
               IErrorServices _errorApplication,
               UserManager<ApplicationUser> _identityUserManager,
               IApplicationRoleManager _roleManager,
               IPerfilRepositories _perfilRepositories) : base(_errorApplication)
        {
            _bearerTokenSettings = bearerTokenSettings.Value;
            _userManager = userManager;
            _signManager = signManager;
            _currentUser = currentUser;
            _mapper = mapper;
            _context = context;
            this._identityUserManager = _identityUserManager;
            this._roleManager = _roleManager;
            this._perfilRepositories = _perfilRepositories;
        }

        public async Task<ActionResult> Buscar()
        {
            var roles = await _roleManager.ListAsync();
            return await RetornOk(_mapper.Map<IEnumerable<PerfilOutput>>(roles));
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return await RetornOk(_mapper.Map<PerfilOutput>(role));
        }

        public async Task<ActionResult> Gravar(PerfilCreateInput input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input?.Nome))
                    return await RetornNo(false, "Nome do perfil é obrigatório.");

                var exists = await _roleManager.RoleExistsAsync(input.Nome);

                if (exists)
                    return await RetornNo(false, "Perfil já existe.");
                
                var result = await _roleManager.CreateAsync(new ApplicationRole() {Name = input.Nome});




                if (!result.Succeeded)
                    return await RetornNo(false, result.Errors.Select(e => e.Description).ToString());

                return await RetornOk(result);
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Alterar(ApplicationRole input)
        {
            if (string.IsNullOrWhiteSpace(input?.Name))
                return await RetornNo(false, "Nome do role é obrigatório.");

            var result = await _roleManager.UpdateAsync(input);

            if (!result.Succeeded)
                return await RetornNo(false, string.Join(", ", result.Errors.Select(e => e.Description)));

            return await RetornOk(result);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
                return await RetornNo(false, "Role não encontrada.");

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
                return await RetornNo(false, string.Join(", ", result.Errors.Select(e => e.Description)));

            return await RetornOk(true);
        }

        public async Task<ActionResult> BuscarPerfilPermissaoUsuario(int usuarioId)
        {
            var resultado = await _perfilRepositories.BuscarPerfilPermissaoUsuario(usuarioId);

            return await RetornOk(resultado);
        }

        public async Task<ActionResult> Ordem(ApplicationRole input)
        {
            if (string.IsNullOrWhiteSpace(input?.Name))
                return await RetornNo(false, "Nome do role é obrigatório.");

            var result = await _roleManager.UpdateAsync(input);

            if (!result.Succeeded)
                return await RetornNo(false, string.Join(", ", result.Errors.Select(e => e.Description)));

            return await RetornOk(result);
        }
    }
}
