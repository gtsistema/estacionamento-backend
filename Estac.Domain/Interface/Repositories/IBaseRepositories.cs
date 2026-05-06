using Estac.Domain.Models.Base;

namespace Estac.Domain.Interface.Repositories
{
    public interface IBaseRepositories<T> where T : BaseInt
    {
        Task<T> Gravar(T item);
        Task<T> Alterar(T item);
        Task<bool> Excluir(int id);
        Task<T> Selecionar(int id);
        Task<IQueryable<T>> SelectAllAsync();
        Task<bool> Existe(int id);
        int LastCodeTable();
        Task<T> InsertNoSaveChangesAsync(T item);
        Task SaveChangesAsync();
        Task<int?> GetIdByDescricaoAsync(string descricao);
    }
}
