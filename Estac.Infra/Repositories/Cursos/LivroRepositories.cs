using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input;
using Estac.Domain.Interface.Repositories.Cursos;
using Estac.Domain.Models;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories.Cursos
{
    public class LivroRepositories : BaseRepositories<Livro>, ILivroRepositories
    {
        private DbSet<Livro> _dataset;
        private readonly IMapper _mapper;

        public LivroRepositories(GpContext context, IMapper mapper) : base(context)
        {
            _dataset = context.Set<Livro>();
            _mapper = mapper;
        }

        public async Task<PagedResult<Livro>> GetPageAsync(FilterInput input)
        {
            return await _dataset.AsNoTracking()
                .ToPagedSort(input.Sort, input.Propriedade).Result
                .GetPaged(input.NumeroPagina, input.TamanhoPagina);
        }
    }
}
