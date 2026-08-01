using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Extensions
{
    public static class DataExtesions
    {
        public static string ObterMesAtualString()
        {
            int mesAtual = DateTime.Now.Month;
            MesDoAno mesEnum = (MesDoAno)mesAtual;
            return mesEnum.GetDescription();
        }

        public static MesDoAno ObterMesAtualEnum()
        {
            int mesAtual = DateTime.Now.Month;
            MesDoAno mesEnum = (MesDoAno)mesAtual;
            return mesEnum;
        }
    }
}
