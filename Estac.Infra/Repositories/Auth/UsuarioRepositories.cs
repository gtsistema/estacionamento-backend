using Estac.Domain.Interface.Repositories.Auth;
using Estac.Domain.Interface.Repositories.Dapper;
using Estac.Domain.Output.Auth;
using Estac.Domain.Output.Auth.Usuario;
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
                    r.Name AS Role,
                    e.Descricao AS Estacionamento
                FROM dbo.[User] u
                INNER JOIN gts.Estacionamento e ON e.Id = u.EstacionamentoId
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

        public async Task<UsuarioCadastroOutput> SelecionarUsuarioPorId(int id)
        {
            string query = $@"
					 SELECT 
                        p.Id AS PessoaId,
                        p.Descricao AS Nome,
                        CASE 
                            WHEN LEN(REPLACE(REPLACE(REPLACE(p.Documento, '.', ''), '-', ''), '/', '')) = 11
                            THEN STUFF(STUFF(STUFF(REPLACE(REPLACE(REPLACE(p.Documento, '.', ''), '-', ''), '/', ''), 4, 0, '.'), 8, 0, '.'), 12, 0, '-')
                            ELSE p.Documento
                        END AS Cpf,
                        t.Id AS TransportadoraId,
                        t.Descricao AS Transportadora,
                        e.Id AS EstacionamentoId,
                        e.Descricao AS Estacionamento,
                        u.Id AS UsuarioId,
                        u.Email,
                        u.UserName,
						r.Id as PerfilId,
						r.Name as Perfil
                    FROM gts.Pessoa p
                    INNER JOIN dbo.[User] u  ON p.Id = u.PessoaId
                    INNER JOIN dbo.[UserRole] ur  ON u.Id = ur.UserId
                    INNER JOIN dbo.[Role] r  ON ur.RoleId = r.Id
                    LEFT JOIN gts.Transportadora t ON t.Id = u.TransportadoraId
                    LEFT JOIN gts.Estacionamento e ON e.Id = u.EstacionamentoId
                    WHERE u.Id = @usuarioId";

            return await _dapperRepositories.QueryFirstOrDefaultAsync<UsuarioCadastroOutput>(query, new { usuarioId = id });
        }
    }
}
