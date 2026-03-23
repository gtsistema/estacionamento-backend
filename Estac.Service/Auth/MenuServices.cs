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

        public async Task<ActionResult> Buscar(MenuFilterInput filter)
        {
            var result = await _repositories.Paginar(filter);

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

                await _repositories.Alterar(result);

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
    }
}
