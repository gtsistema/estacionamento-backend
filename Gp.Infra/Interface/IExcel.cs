using System.Data;

namespace Gp.Infra.Interface
{
    public interface IExcel
    {
        DataTable LerPlanilha(string caminhoArquivo, string nomePlanilha);
    }
}
