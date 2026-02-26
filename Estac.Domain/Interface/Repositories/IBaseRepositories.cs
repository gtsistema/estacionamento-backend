using Estac.Domain.Models.Base;

namespace Estac.Domain.Interface.Repositories
{
    public interface IBaseRepositories<T> where T : BaseInt
    {
        Task<T> Gravar(T item);
        Task<T> Alterar(T item);
        Task<bool> Excluir(long id);
        Task<T> Selecionar(long id);
        Task<IQueryable<T>> SelectAllAsync();
        Task<bool> Existe(long id);
        long LastCodeTable();
        Task<T> InsertNoSaveChangesAsync(T item);
        Task SaveChangesAsync();
        Task<long?> GetIdByDescricaoAsync(string descricao);
    }
}
