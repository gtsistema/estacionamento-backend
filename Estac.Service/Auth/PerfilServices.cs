using AutoMapper;
using Azure.Core;
using DocumentFormat.OpenXml.Office2010.Excel;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Repositories;
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
            var roles = await _roleManager.ListAsync();
            return await RetornOk(await _perfilRepositories.BuscarPerfilPermissaoGrid(null));
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            return await RetornOk(await _perfilRepositories.BuscarPerfilPermissaoGrid(id));
        }

        public async Task<ActionResult> Gravar(PerfilCreateInput input)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                if (string.IsNullOrWhiteSpace(input?.Nome))
                    return await RetornNo(false, "Nome do perfil é obrigatório.");

                var exists = await _roleManager.RoleExistsAsync(input.Nome);

                if (exists)
                    return await RetornNo(false, "Perfil já existe.");

                await _perfilRepositories.AdicionarPerfilSimples(new ApplicationRole() { Name = input.Nome });

                var menus = _mapper.Map<List<Module>>(input.Menus);

                var perfil = await _perfilRepositories.BuscarPerfilPorId(input.Id);

                var rolePermissoesAdicionar = TratarRolePermissionGravar(menus, perfil.Id);

                await _perfilRepositories.AtualizarPermissoesDoPerfil(rolePermissoesAdicionar, null);

                await _unitOfWork.CommitAsync();

                return await RetornOk(_perfilRepositories.BuscarPerfilPermissaoGrid(input.Id));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Alterar(PerfilUpdateInput input)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                if (string.IsNullOrWhiteSpace(input?.Nome))
                    return await RetornNo(false, "Nome do role é obrigatório.");

                 _perfilRepositories.AtualizarPerfilSimples(new ApplicationRole() { Id = input.Id, Name = input.Nome });
               
                var menus = _mapper.Map<List<Module>>(input.Menus);

                await AtualizarPermissoesdoPerfil(input, menus, input.Id);

                await _unitOfWork.CommitAsync();

                return await RetornOk(_perfilRepositories.BuscarPerfilPermissaoGrid(input.Id));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return await RetornNo(false, ex.Message);
            }
        }

        private async Task AtualizarPermissoesdoPerfil(PerfilUpdateInput input, List<Module> menus, int perfilId)
        {
            var rolePermissoesAdicionar = TratarRolePermissionGravar(menus, perfilId);

            var rolePermissoesAnterior = await _perfilRepositories.BuscarRolePermissoes(input.Id);

            await _perfilRepositories.AtualizarPermissoesDoPerfil(rolePermissoesAdicionar, rolePermissoesAnterior);
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

        private List<RolePermission> TratarRolePermissionGravar(List<Module> menus, int roleId)
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
                        rolePermissions.Add(new RolePermission
                        {
                            ModuleId = menu.Id,
                            RoleId = roleId,
                            SubModuleId = subMenu.Id
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
    }
}
