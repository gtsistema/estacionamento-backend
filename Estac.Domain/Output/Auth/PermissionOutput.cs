using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Domain.Output.Auth
{
    public class PermissionOutput
    {
        public int Id { get; set; }
        public int Ordem { get; set; }
        public int SubMenuId { get; set; }
        public string Descricao { get; set; }
    }
}
