using System;
using System.Collections.Generic;
using System.Data;

namespace Estac.Domain.Interface.Repositories.Dapper
{
    public interface IDapperRepositories
    {
        Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object? param = null,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default);

        Task<T?> QueryFirstOrDefaultAsync<T>(
            string sql,
            object? param = null,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default);

        Task<int> ExecuteAsync(
            string sql,
            object? param = null,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default);
    }
}
