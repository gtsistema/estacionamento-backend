using Gp.Domain.Models;

namespace Gp.Domain.Interface.Repositories
{
    public interface IBaseRepositories<T> where T : Base
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(long id);
        Task<T> SelectAsync(long id);
        Task<IQueryable<T>> SelectAllAsync();
        Task<bool> ExistAsync(long id);
        long LastCodeTable();
        Task<T> InsertNoSaveChangesAsync(T item);
        Task SaveChangesAsync();
        Task<long?> GetIdByDescricaoAsync(string descricao);
    }
}
