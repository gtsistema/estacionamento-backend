using AutoMapper;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories
{
    public class PessoaRepositories : BaseRepositories<Pessoa>, IPessoaRepositories
    {
        private DbSet<Pessoa> _dataset;
        private readonly IMapper _mapper;

        public PessoaRepositories(GtsContext context, IMapper _mapper) : base(context)
        {
            this._mapper = _mapper;
            _dataset = context.Set<Pessoa>();
        }
    }
}