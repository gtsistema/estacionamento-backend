using Gp.Domain.Interface.Repositories;
using Gp.Domain.Models;
using Gp.Infra.Context;
using Gp.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Infra.Repositories
{
    public class OrcamentoRepositories : BaseRepositories<Orcamento>, IOrcamentoRepositories
    {
        public OrcamentoRepositories(GpContext context) : base(context)
        {
        }
    }
}
