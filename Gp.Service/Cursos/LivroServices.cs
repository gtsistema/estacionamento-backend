using Gp.Domain.Auth;
using Gp.Domain.Input.Auth;
using Gp.Domain.Output.Auth;
using Gp.Domain.Output;
using Gp.Domain.Interface.Services.Cursos;
using Gp.Domain.Interface.Repositories.Cursos;
using Gp.Domain.Models;
using Gp.Domain.Input.Cursos;
using Gp.Domain.Input;
using AutoMapper;
using Gp.Domain.Output.Cursos;

namespace Gp.Service.Cursos
{
    public class LivroServices : ILivroServices
    {
        private readonly ILivroRepositories _repositories;
        public LivroServices(ILivroRepositories _repositories)
        {
            this._repositories  = _repositories;
        }

        public async Task<ServicesResult> ObterTodosAsync(FilterInput input)
        {
            try
            {
                return new ServicesResult(await _repositories.GetPageAsync(input));
            }
            catch (Exception ex)
            {
                return new ServicesResult("exception", ex.Message);
            }
        }

        public async Task<ServicesResult> ObterAsync(Livro dto)
        {
            try
            {
                return new ServicesResult(await _repositories.SelectAsync(dto.Id));
            }
            catch (Exception ex)
            {
                return new ServicesResult("exception", ex.Message);
            }
        }

        public async Task<ServicesResult> NovoAsync(LivroInput dto)
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

                return new ServicesResult(await _repositories.InsertAsync(livro));
            }

            catch (Exception ex)
            {
                return new ServicesResult("exception", ex.Message);
            }
        }

        public async Task<ServicesResult> ExcluirAsync(Livro dto)
        {
            try
            {
                if(!await _repositories.ExistAsync(dto.Id))
                {
                    return new ServicesResult("ID", "Livro não encontrado!");
                }

                return new ServicesResult(await _repositories.DeleteAsync(dto.Id));
            }

            catch (Exception ex)
            {
                return new ServicesResult("exception", ex.Message);
            }
        }
    }
}
