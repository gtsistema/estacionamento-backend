using Dapper;
using Estac.Domain.Interface.Repositories.Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

public class DapperRepositories : IDapperRepositories
{
    private readonly string _connectionString;

    public DapperRepositories(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<T>(
            new CommandDefinition(
                sql,
                param,
                transaction,
                cancellationToken: cancellationToken));
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default)
    {
        using var connection = CreateConnection();

        try
        {

            return await connection.QueryFirstOrDefaultAsync<T>(
                new CommandDefinition(
                    sql,
                    param,
                    transaction,
                    cancellationToken: cancellationToken));

        }
        catch (Exception ex)
        {
           throw new Exception($"{connection.ConnectionString}");
        }
    }

    public async Task<int> ExecuteAsync(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default)
    {
        using var connection = CreateConnection();

        return await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                param,
                transaction,
                cancellationToken: cancellationToken));
    }

    public IDbConnection CreateConnection()
    {
        var conn = new SqlConnection(_connectionString);
        conn.Open();
        return conn;
    }
}