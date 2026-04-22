using Estac.Domain.Interface.Repositories;
using Estac.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Estac.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityContext _identityContext;
        private readonly GtsContext _gtsContext;

        private IDbContextTransaction _transaction;

        public UnitOfWork(
            IdentityContext identityContext,
            GtsContext gtsContext)
        {
            _identityContext = identityContext;
            _gtsContext = gtsContext;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _gtsContext.Database.BeginTransactionAsync();

            await _identityContext.Database
                .UseTransactionAsync(_transaction.GetDbTransaction());
        }

        public async Task SaveChangesAsync()
        {
            await _identityContext.SaveChangesAsync();
            await _gtsContext.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
