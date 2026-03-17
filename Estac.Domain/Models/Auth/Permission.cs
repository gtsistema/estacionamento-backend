using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Domain.Models.Auth
{
    public class Permission
    {
        public int Ordem { get; set; }
        public int Id { get; set; }
        public int SubModuleId { get; set; }
        public string Acao { get; set; }
        public SubModule SubModule { get; set; }
    }
}