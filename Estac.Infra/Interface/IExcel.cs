using System.Data;

namespace Estac.Infra.Interface
{
    public interface IExcel
    {
        DataTable LerPlanilha(string caminhoArquivo, string nomePlanilha);
    }
}
