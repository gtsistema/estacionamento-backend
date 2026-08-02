
namespace Estac.Domain.Interface.Repositories
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
