using Gp.Domain.Models;

namespace Gp.Domain.Interface.Repositories
{
    public interface IBaseRepositories<T> where T : Base
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
        Task<T> SelectAsync(int id);
        Task<IQueryable<T>> SelectAllAsync();
        Task<bool> ExistAsync(int id);
        int LastCodeTable();
    }
}
