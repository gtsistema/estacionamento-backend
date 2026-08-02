using AutoMapper;
using Azure.Core;
using DocumentFormat.OpenXml.Office2010.Excel;
using Estac.Domain.Input;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Repositories.Auth;
using Estac.Domain.Interface.Services.Auth;
using Estac.Domain.Models;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output;
using Estac.Domain.Output.Auth;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Permission;
using Estac.Infra.Context;
using Estac.Infra.Repositories;
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
        private readonly IUnitOfWork _unitOfWork;

        public PerfilServices(IApplicationUserManager userManager,
               IApplicationSignManager signManager, ICurrentUser currentUser,
               IOptions<BearerTokenSettings> bearerTokenSettings,
               IMapper mapper,
               GtsContext context,
               IErrorServices _errorApplication,
               UserManager<ApplicationUser> _identityUserManager,
               IApplicationRoleManager _roleManager,
               IPerfilRepositories _perfilRepositories,
               IUnitOfWork unitOfWork) : base(_errorApplication)
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
            this._unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Buscar()
        {
            return await RetornOk(await _perfilRepositories.BuscarPerfilPermissaoGrid(null));
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            return await RetornOk(await _perfilRepositories.BuscarPerfilPermissaoGrid(id));
        }

        public async Task<ActionResult> Gravar(PerfilCreateInput input)
        {
            if (input is null)
                return await RetornNo(false, "Dados para cadastro do perfil são obrigatórios.");

            if (string.IsNullOrWhiteSpace(input.Nome))
                return await RetornNo(false, "Nome do perfil é obrigatório.");

            if (input.Menus is null)
                return await RetornNo(false, "Menus do perfil são obrigatórios.");

            if (!TemAoMenosUmMenuSelecionado(input.Menus))
                return await RetornNo(false, "Selecione ao menos um menu para o perfil.");

            var exists = await _roleManager.RoleExistsAsync(input.Nome);
            if (exists)
                return await RetornNo(false, "Perfil já existe.");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var perfil = new ApplicationRole { Name = input.Nome };

                await _perfilRepositories.AdicionarPerfilSimples(perfil);
                await _unitOfWork.SaveChangesAsync();

                var menus = _mapper.Map<List<Module>>(input.Menus) ?? new List<Module>();

                var rolePermissoesAdicionar = await TratarRolePermissionGravar(menus, perfil.Id);

                await _perfilRepositories.AtualizarPermissoesDoPerfil(rolePermissoesAdicionar, null);

                await _unitOfWork.CommitAsync();

                return await RetornOk(true);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Alterar(PerfilUpdateInput input)
        {
            if (input is null)
                return await RetornNo(false, "Dados para alteração do perfil são obrigatórios.");

            if (input.Id <= 0)
                return await RetornNo(false, "Id do perfil inválido.");

            if (string.IsNullOrWhiteSpace(input.Nome))
                return await RetornNo(false, "Nome do role é obrigatório.");

            if (input.Menus is null)
                return await RetornNo(false, "Menus do perfil são obrigatórios.");

            if (!TemAoMenosUmMenuSelecionado(input.Menus))
                return await RetornNo(false, "Selecione ao menos um menu para o perfil.");

            var perfilAtual = await _perfilRepositories.BuscarPerfilPorId(input.Id);
            if (perfilAtual is null)
                return await RetornNo(false, "Perfil não encontrado.");

            //if(perfilAtual.Padrao)
            //    return await RetornNo(false, "Perfil padrão não pode ser alterado.");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _perfilRepositories.AtualizarPerfilSimplesAsync(new ApplicationRole() { Id = input.Id, Name = input.Nome, EmpresaId = _currentUser.EmpresaId });
                var menus = _mapper.Map<List<Module>>(input.Menus) ?? new List<Module>();

                await AtualizarPermissoesdoPerfil(menus, input.Id);

                await _unitOfWork.CommitAsync();

                return await RetornOk(_perfilRepositories.BuscarPerfilPermissaoGrid(input.Id));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return await RetornNo(false, ex.Message);
            }
        }

        private async Task AtualizarPermissoesdoPerfil(List<Module> menus, int perfilId)
        {
            if (menus is null || !menus.Any())
            {
                await _perfilRepositories.AtualizarPermissoesDoPerfil(new List<RolePermission>(), await _perfilRepositories.BuscarRolePermissoes(perfilId));
                return;
            }

            var rolePermissoesAnterior = await _perfilRepositories.BuscarRolePermissoes(perfilId);

            await _perfilRepositories.AtualizarPermissoesDoPerfil(await TratarRolePermissionGravar(menus, perfilId), rolePermissoesAnterior);
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role is null)
                    return await RetornNo(false, "Role não encontrada.");

                var result = await _roleManager.DeleteAsync(role);

                if (!result.Succeeded)
                    return await RetornNo(false, string.Join(", ", result.Errors.Select(e => e.Description)));

                await AtualizarPermissoesdoPerfil(null, id);

                await _unitOfWork.CommitAsync();

                return await RetornOk(true);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return await RetornNo(false, ex.Message);
            }
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

        private async Task<List<RolePermission>> TratarRolePermissionGravar(List<Module> menus, int roleId)
        {
            List<RolePermission> rolePermissions = new List<RolePermission>();

            foreach (var menu in menus.Where(p => p.Selecionado))
            {
                if (menu.SubModules == null || !menu.SubModules.Any())
                {
                    rolePermissions.Add(new RolePermission
                    {
                        ModuleId = menu.Id,
                        RoleId = roleId,
                    });

                    continue;
                }

                foreach (var subMenu in menu.SubModules.Where(p => p.SelecionadoSub))
                {
                    if (subMenu.Permissions == null || !subMenu.Permissions.Any())
                    {

                        var permissaoVisualizar = await _perfilRepositories.BuscarPermissao(subMenu.Id, PermissionAcess.Perfil.Visualizar);

                        if(permissaoVisualizar is null)
                            throw new Exception($"Permissão de visualização não encontrada para o submódulo {subMenu.Descricao}.");

                        rolePermissions.Add(new RolePermission
                        {
                            ModuleId = menu.Id,
                            RoleId = roleId,
                            SubModuleId = subMenu.Id,
                            PermissionId = permissaoVisualizar.Id
                        });

                        continue;
                    }

                    foreach (var permissao in subMenu.Permissions.Where(p => p.SelecionadoPerm))
                    {
                        rolePermissions.Add(new RolePermission
                        {
                            RoleId = roleId,
                            SubModuleId = subMenu.Id,
                            PermissionId = permissao.Id,
                            ModuleId = menu.Id
                        });
                    }
                }
            }

            return rolePermissions;
        }

        private static bool TemAoMenosUmMenuSelecionado(IEnumerable<ModuloInput> menus)
        {
            return menus.Any(menu => menu != null && menu.Selecionado && menu.MenuId > 0);
        }

        public async Task<ActionResult> BuscarSimplicado()
        {
            return await RetornOk(await _perfilRepositories.BuscarSimplicado(_currentUser.EmpresaId));
        }
    }
}
