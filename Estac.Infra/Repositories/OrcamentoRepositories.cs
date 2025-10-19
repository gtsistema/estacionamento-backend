using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Infra.Repositories
{
    public class OrcamentoRepositories : BaseRepositories<Orcamento>, IOrcamentoRepositories
    {
        public OrcamentoRepositories(GpContext context) : base(context)
        {
        }
    }
}
