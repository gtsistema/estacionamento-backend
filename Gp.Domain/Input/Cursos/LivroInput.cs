using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Domain.Input.Cursos
{
    public class LivroInput
    {
        public string Autor { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataLancamento { get; set; } = DateTime.Now;
        public decimal Valor { get; set; }
    }
}
