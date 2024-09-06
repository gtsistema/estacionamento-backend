
namespace Gp.Domain.Output
{
    public interface IErrorApplication
    {
        void GraveErroERetornaOCrodigo(Exception ex, string codigoErro, object obj);
    }
}
