using Gp.Domain.Interface.Services.Cursos;
using Gp.Domain.Interface.Repositories.Cursos;
using Gp.Domain.Models;
using Gp.Domain.Input.Cursos;
using Gp.Domain.Input;
using Microsoft.AspNetCore.Mvc;
using Gp.Domain.Output;
using Gp.Domain.Dtos.Curso;

namespace Gp.Service.Cursos
{
    public class LivroServices : ServicesResult<LivroDto>, ILivroServices
    {
        private readonly ILivroRepositories _repositories;
        public LivroServices(IErrorServices _errorServices,
                            ILivroRepositories _repositories) : base(_errorServices)
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
               return await RetornNo(ex.Message, "Erro", statusCode: 500);
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
                return await RetornNo(ex.Message, "Erro", statusCode: 500);
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
                return await RetornNo(ex.Message, "Erro", statusCode: 500);
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
                return await RetornNo(ex.Message, "Erro", statusCode: 500);
            }
        }
    }
}
