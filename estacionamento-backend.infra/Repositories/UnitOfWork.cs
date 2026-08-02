using Estac.Domain.Interface.Repositories;
using Estac.Infra.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Estac.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IdentityContext _identityContext;
        private readonly GtsContext _gtsContext;

        private IDbContextTransaction? _identityTransaction;
        private IDbContextTransaction? _gtsTransaction;

        public UnitOfWork(
            IdentityContext identityContext,
            GtsContext gtsContext)
        {
            _identityContext = identityContext;
            _gtsContext = gtsContext;
        }

        public async Task BeginTransactionAsync()
        {
            if (_identityTransaction is not null || _gtsTransaction is not null)
                throw new InvalidOperationException("Já existe uma transação em andamento para este escopo.");

            _identityTransaction = await _identityContext.Database.BeginTransactionAsync();
            _gtsTransaction = await _gtsContext.Database.BeginTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _identityContext.SaveChangesAsync();
            await _gtsContext.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            EnsureTransactionStarted();

            await SaveChangesAsync();

            await _identityTransaction!.CommitAsync();
            await _gtsTransaction!.CommitAsync();

            await DisposeTransactionsAsync();
        }

        public async Task RollbackAsync()
        {
            if (_identityTransaction is null && _gtsTransaction is null)
                return;

            if (_identityTransaction is not null)
                await _identityTransaction.RollbackAsync();

            if (_gtsTransaction is not null)
                await _gtsTransaction.RollbackAsync();

            await DisposeTransactionsAsync();
        }

        public void Dispose()
        {
            _identityTransaction?.Dispose();
            _gtsTransaction?.Dispose();
            _identityTransaction = null;
            _gtsTransaction = null;
        }

        private void EnsureTransactionStarted()
        {
            if (_identityTransaction is null || _gtsTransaction is null)
                throw new InvalidOperationException("Nenhuma transação foi iniciada.");
        }

        private async Task DisposeTransactionsAsync()
        {
            if (_identityTransaction is not null)
                await _identityTransaction.DisposeAsync();

            if (_gtsTransaction is not null)
                await _gtsTransaction.DisposeAsync();

            _identityTransaction = null;
            _gtsTransaction = null;
        }
    }
}
