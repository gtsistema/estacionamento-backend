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

        Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, TResult>(
     string sql,
     Func<T1, T2, T3, T4, TResult> map,
     object? param = null,
     string splitOn = "Id,Id,Id,Id",
      IDbTransaction? transaction = null,
      CancellationToken cancellationToken = default);
    }
}
