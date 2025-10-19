using FluentValidation.Results;
using Estac.Domain.Output;
using Estac.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service.Extensions
{
    public class ServiceResult<T> where T : class
    {
        protected readonly IErrorServices _errorServices;

        protected IList<ValidationFailure> _validation;

        public ServiceResult(IErrorServices errorServices)
        {
            _errorServices = errorServices;
            _validation = new List<ValidationFailure>();
        }

        public Task<ActionResult> RetornOk(object data, string message = "Operação realizada com sucesso")
        {
            return Task.FromResult(ResponseResult.GetResponse(sucess: true, message, data));
        }

        public Task<ActionResult> RetornNo(object data = null, string message = null, int statusCode = 400, string errorCode = null)
        {
            return Task.FromResult(ResponseResult.GetResponse(sucess: false, new string[1] { message }, data, statusCode, errorCode));
        }

        public Task<ActionResult> RetornNo(object data = null, IList<ValidationFailure> message = null, int statusCode = 400, string errorCode = null)
        {
            return Task.FromResult(ResponseResult.GetResponse(sucess: false, message, data, statusCode, errorCode));
        }

        protected async Task<object> GetObjectPages<Dto>(PagedResult<Dto> result) where Dto : class
        {
            return await Task.FromResult(new
            {
                PageCount = result.PageCount,
                Data = result.Results,
                RowCount = result.RowCount
            });
        }

        protected object GetObjectPages<Dto>(IQueryable<Dto> result) where Dto : class
        {
            return new
            {
                Data = result
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
