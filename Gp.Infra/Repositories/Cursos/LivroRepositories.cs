using AutoMapper;
using Gp.Domain.Extensions;
using Gp.Domain.Input;
using Gp.Domain.Interface.Repositories.Cursos;
using Gp.Domain.Models;
using Gp.Domain.Shared;
using Gp.Infra.Context;
using Gp.Infra.Migrations;
using Gp.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Gp.Infra.Repositories.Cursos
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

        public async Task<PagedQuery<Livro>> GetPageAsync(FilterInput input)
        {
            return await _dataset.AsNoTracking()
                .ToPagedSortAsync(input.Sort, input.Propriedade).Result
                .ToPagedQueryAsync(input.NumeroPagina, input.TamanhoPagina);
        }
    }
}
