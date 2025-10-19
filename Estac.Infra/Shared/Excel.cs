using Estac.Infra.Interface;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Infra.Shared
{
    public class Excel : IExcel
    {
        public Excel()
        {
        }

        public DataTable LerPlanilha(string caminhoArquivo, string nomePlanilha)
        {
            using (var package = new ExcelPackage(new FileInfo(caminhoArquivo)))
            {
                var planilha = package.Workbook.Worksheets[nomePlanilha];
                if (planilha == null)
                    throw new Exception($"A planilha '{nomePlanilha}' não foi encontrada.");

                var dataTable = new DataTable();

                // Ler cabeçalhos (primeira linha)
                for (int col = 1; col <= planilha.Dimension.End.Column; col++)
                {
                    dataTable.Columns.Add(planilha.Cells[1, col].Text);
                }

                // Ler linhas de dados
                for (int row = 2; row <= planilha.Dimension.End.Row; row++)
                {
                    var novaLinha = dataTable.NewRow();
                    for (int col = 1; col <= planilha.Dimension.End.Column; col++)
                    {
                        novaLinha[col - 1] = planilha.Cells[row, col].Text;
                    }
                    dataTable.Rows.Add(novaLinha);
                }

                return dataTable;
            }
        }
    }
}
