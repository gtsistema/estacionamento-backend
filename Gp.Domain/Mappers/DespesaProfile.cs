using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Input.Despesa;
using Gp.Domain.Models;
using Gp.Domain.Output;
using Gp.Domain.Shared;

namespace Gp.Domain.Mappers
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            CreateMap<DespesaPostInput, Despesa>();
            CreateMap<DespesaPutInput, Despesa>();
            CreateMap<Despesa, DespesaPostOutput>();
            CreateMap<Despesa, DespesaDto>();

            // Despesa Lancamento
            CreateMap<DespesaLancamentoPostInput, DespesaLancamento>();
        }
    }
}
