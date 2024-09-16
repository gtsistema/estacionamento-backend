using Gp.Domain.Interface.Services.Cursos;
using Gp.Domain.Interface.Repositories.Cursos;
using Gp.Domain.Models;
using Gp.Domain.Input.Cursos;
using Gp.Domain.Input;
using Microsoft.AspNetCore.Mvc;
using Gp.Service.Extensions;
using Gp.Domain.Output;

namespace Gp.Service.Cursos
{
    public class LivroServices : ServiceResult<Livro>, ILivroServices
    {
        private readonly ILivroRepositories _repositories;

        public LivroServices(ILivroRepositories repositories,
                             IErrorApplication _errorApplication
                             ) : base(_errorApplication)
        {
            _repositories  = repositories;
        }

        public async Task<ActionResult> ObterTodosAsync(FilterInput input)
        {
            try
            {
                return await RetornOk(await _repositories.GetPageAsync(input));
            }
            catch (Exception ex)
            {
                return await RetornNo("exception", ex.Message, statusCode: 500);
            }
        }

        public async Task<ActionResult> ObterAsync(Livro dto)
        {
            try
            {
                return await RetornOk(await _repositories.SelectAsync(dto.Id));
            }
            catch (Exception ex)
            {
                return await RetornNo("exception", ex.Message, statusCode: 500);
            }
        }

        public async Task<ActionResult> NovoAsync(LivroInput dto)
        {
            try
            {
                Livro livro = new Livro()
                {
                    Autor = dto.Autor,
                    DataLancamento = DateTime.Now,
                    Descricao = dto.Descricao,
                    Valor = dto.Valor
                };

                return await RetornOk(await _repositories.InsertAsync(livro));
            }

            catch (Exception ex)
            {
                return await RetornNo("exception", ex.Message, statusCode: 500);
            }
        }

        public async Task<ActionResult> ExcluirAsync(Livro dto)
        {
            try
            {
                if(!await _repositories.ExistAsync(dto.Id))
                {
                    return await RetornNo("ID", "Livro não encontrado!");
                }

                return await RetornOk(await _repositories.DeleteAsync(dto.Id));
            }

            catch (Exception ex)
            {
                return await RetornNo("exception", ex.Message, statusCode: 500);
            }
        }

        public async Task<IEnumerable<Livro>> GetallTeste()
        {
            return await _repositories.SelectAllAsync();
        }
    }
}
