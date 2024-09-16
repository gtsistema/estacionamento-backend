using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Api.RequestHttp.Interface
{
    public interface IService<T, Dto> where T : class where Dto : class
    {
        Task<ActionResult> Capturar(decimal id, string message = "Operação realizada com sucesso");

        Task<ActionResult> CapturarLong(long id, string message = "Operação realizada com sucesso");

        Task<ActionResult> ListarComPaginacao(FilterRequest filterRequest);

        Task<ActionResult> Listar();

        Task<ActionResult> CapturarPorCodigo(object codigo, string property);

        Task<ActionResult> CapturarPorDescricao(string descricao, string property);

        Task<ActionResult> Gravar(T entity);

        Task<ActionResult> Alterar(T entity);

        Task<ActionResult> Excluir(decimal id);

        Task<ActionResult> ExcluirLong(long id);

        Task<ActionResult> RetornNo(object data, IList<ValidationFailure> message, int statusCode, string errorCode);

        void Dispose();
    }
}
