using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories
{
    public class VeiculoMotoristaRepositories : BaseRepositoriesNone<VeiculoMotorista>, IVeiculoMotoristaRepositories
    {
        public VeiculoMotoristaRepositories(GtsContext context) : base(context)
        {
        }

        public async Task VincularAsync(int veiculoId, int motoristaId)
        {
            var existe = await _context.VeiculoMotoristas
                .AsNoTracking()
                .AnyAsync(x => x.VeiculoId == veiculoId && x.MotoristaId == motoristaId);

            if (existe)
                return;

            await _context.VeiculoMotoristas.AddAsync(new VeiculoMotorista
            {
                VeiculoId = veiculoId,
                MotoristaId = motoristaId
            });
        }
    }
}
