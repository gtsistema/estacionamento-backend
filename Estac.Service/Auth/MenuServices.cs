using AutoMapper;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output;
using Estac.Domain.Output.Auth;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class MenuServices : ServiceResult<MenuOutput>, IMenuServices
    {
        private readonly IMenuRepositories _repositories;
        private readonly IMapper _mapper;

        public MenuServices(IErrorServices _errorServices,
                               IMenuRepositories repositories, IMapper mapper) : base(_errorServices)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            try
            {
                var result = await _repositories.SelecionarPorIdCompleto(id);

                if (result is null)
                    return await RetornNo(false, "Registro não encontrado.");

                return await RetornOk(_mapper.Map<MenuOutput>(result));
            }
            catch(Exception ex)
            {   
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Buscar()
        {
            var result = await _repositories.Buscar();

            return await RetornOk(result);
        }

        public async Task<ActionResult> Gravar(MenuCreateInput input)
        {
            try
            {
                //var validations = MenuPostInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<Module>(input);

                await _repositories.Gravar(result);

                return await RetornOk(result);
            }
            catch (Exception ex) 
            {
                return await RetornNo(false, ex.Message);
            }
          
        }

        public async Task<ActionResult> Alterar(MenuCreateInput input)
        {
            try
            {
                //var validations = MenuPutInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<Module>(input);

                var module = await _repositories.SelecionarPorId(result.Id);

                await _repositories.Atualizar(module);

                return await RetornOk(result);
            }
            catch (Exception ex) 
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Excluir(int id)
        {
            var result = await _repositories.Existe(id);

            if (!result)
                return await RetornNo(false, "Produto não localizado na base de dados!");

            var estacionamento = await _repositories.Selecionar(id);

            await _repositories.Excluir(id);

            return await RetornOk(true);
        }

        public async Task<ActionResult> OrganizarMenus(MenuOrganizacaoInput input)
        {
            if (input?.Menus == null || !input.Menus.Any())
                await RetornOk(false, "Não foi encontrado!");

            var menusAjustados = AjustarSequenciaMenus(input.Menus);

            var subMenusAjustados = AjustarSequenciaSubMenus(input.Menus);

            await _repositories.AtualizarOrdem(
                menusAjustados,
                subMenusAjustados
            );

            return await RetornOk(true);
        }

        private List<MenuOrdemInput> AjustarSequenciaMenus(List<MenuOrdemInput> menus)
        {
            return menus
                .OrderBy(x => x.Ordem)
                .Select((item, index) => new MenuOrdemInput
                {
                    Id = item.Id,
                    Ordem = item.Ordem 
                })
                .ToList();
        }

        private List<SubMenuOrdemInput> AjustarSequenciaSubMenus(List<MenuOrdemInput> menus)
        {
            var resultado = new List<SubMenuOrdemInput>();

            foreach (var menu in menus)
            {
                if (menu.SubMenus == null || !menu.SubMenus.Any())
                    continue;

                var ajustados = menu.SubMenus
                    .OrderBy(x => x.Ordem)
                    .Select((sub, index) => new SubMenuOrdemInput
                    {
                        Id = sub.Id,
                        Ordem = sub.Ordem
                    });

                resultado.AddRange(ajustados);
            }

            return resultado;
        }
    }
}
