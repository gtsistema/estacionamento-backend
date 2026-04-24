using Estac.Domain.Interface.Repositories;
using Estac.Infra.Context;
using System.Transactions;

namespace Estac.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IdentityContext _identityContext;
        private readonly GtsContext _gtsContext;

        private TransactionScope? _transactionScope;

        public UnitOfWork(
            IdentityContext identityContext,
            GtsContext gtsContext)
        {
            _identityContext = identityContext;
            _gtsContext = gtsContext;
        }

        public async Task BeginTransactionAsync()
        {
            if (_transactionScope is not null)
                throw new InvalidOperationException("Já existe uma transação em andamento para este escopo.");

            _transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            await Task.CompletedTask;
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
            _transactionScope!.Complete();
            _transactionScope.Dispose();
            _transactionScope = null;
        }

        public async Task RollbackAsync()
        {
            if (_transactionScope is null)
                return;

            _transactionScope.Dispose();
            _transactionScope = null;
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _transactionScope?.Dispose();
            _transactionScope = null;
        }

        private void EnsureTransactionStarted()
        {
            if (_transactionScope is null)
                throw new InvalidOperationException("Nenhuma transação foi iniciada.");
        }
    }
}
