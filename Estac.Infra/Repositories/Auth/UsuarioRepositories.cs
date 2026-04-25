using Estac.Domain.Interface.Repositories.Auth;
using Estac.Domain.Interface.Repositories.Dapper;
using Estac.Domain.Output.Auth;
using Estac.Infra.Context;

namespace Estac.Infra.Repositories.Auth
{
    public class UsuarioRepositories : IUsuarioRepositories
    {
        private readonly IDapperRepositories _dapperRepositories;

        public UsuarioRepositories(IdentityContext context, IDapperRepositories dapperRepositories)
        {
            _dapperRepositories = dapperRepositories;
        }

        public async Task<IEnumerable<UsuarioOutput>> BuscarUsuariosGrid(int? usuarioId)
        {
            const string sql = @"
                SELECT
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.FullName AS Nome,
                    u.EstacionamentoId,
                    r.Name AS Role
                FROM dbo.[User] u
                OUTER APPLY (
                    SELECT TOP 1 r2.Name
                    FROM dbo.UserRole ur
                    INNER JOIN dbo.[Role] r2 ON r2.Id = ur.RoleId
                    WHERE ur.UserId = u.Id
                ) r
                WHERE (u.IsDeleted IS NULL OR u.IsDeleted = 0)
                    AND (@usuarioId IS NULL OR u.Id = @usuarioId)
                ORDER BY u.UserName;";

            var result = await _dapperRepositories.QueryAsync<UsuarioOutput>(sql, new { usuarioId });
            return result;
        }
    }
}
