using Gp.Domain.Output;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Api.RequestHttp
{
    public class Service<T, Dto> : ServiceResult<T>, IApplication<T, Dto>, IDisposable where T : class where Dto : class
    {
        protected readonly IService<T> _service;

        protected readonly IUnitOfWork _unitOfWork;

        protected readonly IRepositoryReadOnly<T> _repositoryReadOnly;

        public Application(IService<T> service, IRepositoryReadOnly<T> repositoryReadOnly, IRepositoryLog repositoryLog, IUnitOfWork unitOfWork, IErrorApplication errorApplication)
            : base(errorApplication)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _repositoryReadOnly = repositoryReadOnly;
        }

        public virtual async Task<ActionResult> CapturarLong(long id, string message)
        {
            return await RetornOk(_repositoryReadOnly.CapturarLong<Dto>(id), message);
        }

        public virtual async Task<ActionResult> Capturar(decimal id, string message)
        {
            return await RetornOk(_repositoryReadOnly.Capturar<Dto>(id), message);
        }

        public virtual async Task<ActionResult> CapturarPorCodigo(object codigo, string property)
        {
            return await RetornOk(_repositoryReadOnly.CapturarPorCodigo<Dto>(codigo, property));
        }

        public virtual async Task<ActionResult> CapturarPorDescricao(string descricao, string property)
        {
            return await RetornOk(GetObjectPages(await _repositoryReadOnly.CapturarPorDescricao<Dto>(descricao, property)));
        }

        public virtual async Task<ActionResult> Listar()
        {
            return await RetornOk(GetObjectPages(await _repositoryReadOnly.Listar<T>()));
        }

        public virtual async Task<ActionResult> ListarComPaginacao(FilterRequest filterRequest)
        {
            if (filterRequest == null)
            {
                throw new Exception("Verifique as propriedades do filtro");
            }

            IQueryable<Expression<Func<T, bool>>> dynamicQueryMany = filterRequest.GetDynamicQueryMany<T>();
            return await RetornOk(GetObjectPages(await _repositoryReadOnly.ListarComPaginacao<Dto>(dynamicQueryMany, filterRequest.Page, filterRequest.PageSize)));
        }

        public virtual async Task<ActionResult> Alterar(T entity)
        {
            if (!(await _service.VerificaSeEntidadeExisteNoBanco(entity)))
            {
                return await RetornNo(entity, "objeto não existe no banco de dados", 500);
            }

            await _service.Alterar(entity);
            await _unitOfWork.Commit();
            return await RetornOk(entity, "Alterado com sucesso.");
        }

        public virtual async Task<ActionResult> Gravar(T entity)
        {
            await _service.Gravar(entity);
            await _unitOfWork.Commit();
            return await RetornOk(entity, "Cadastrado com sucesso.");
        }

        public virtual async Task<ActionResult> Excluir(decimal id)
        {
            T val = await _repositoryReadOnly.Capturar<T>(id);
            if (val == null)
            {
                return await RetornNo(val, "objeto não existe no banco de dados", 500);
            }

            await _service.Excluir(val);
            await _unitOfWork.Commit();
            return await RetornOk(true, "Excluído com sucesso.");
        }

        public virtual async Task<ActionResult> ExcluirLong(long id)
        {
            T val = await _repositoryReadOnly.CapturarLong<T>(id);
            if (val == null)
            {
                return await RetornNo(val, "objeto não existe no banco de dados", 500);
            }

            await _service.Excluir(val);
            await _unitOfWork.Commit();
            return await RetornOk(true, "Excluído com sucesso.");
        }
    }
}
