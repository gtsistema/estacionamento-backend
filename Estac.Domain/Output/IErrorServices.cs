
namespace Estac.Domain.Output
{
    public interface IErrorServices
    {
        void GraveErroERetornaOCrodigo(Exception ex, string codigoErro, object obj);
    }
}
