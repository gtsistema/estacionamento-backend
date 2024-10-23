
namespace Gp.Domain.Output
{
    public class ErrorServices : IErrorServices
    {
        public void GraveErroERetornaOCrodigo(Exception ex, string codigoErro, object obj)
        {
            //_unitOfWork.Rollback();

            //codigoErro = $"{_config.LogConfiguration.LogCodePrefix}-{StringHelper.RandomString(5)}";

            //_httpContextAccessor.HttpContext.Items.TryGetValue("IdentityAccess", out var _identityAccess);
            //IdentityAccess identityAccess = (IdentityAccess)_identityAccess;

            //var descricao = ex.ListExeption(obj, identityAccess, _httpContextAccessor);

            //if (identityAccess != null)
            //{
            //    var logErro = new LogErro(identityAccess.CodigoExportacao, codigoErro, descricao.Replace("\\", ""));

            //    _externalServices.PostLogErro<LogErro>(logErro);
            //}
        }
    }
}
