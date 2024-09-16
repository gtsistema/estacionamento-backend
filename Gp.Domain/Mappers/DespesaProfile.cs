using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Input;
using Gp.Domain.Models;
using Gp.Domain.Output;
using Gp.Domain.Shared;

namespace Gp.Domain.Mappers
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            CreateMap<DespesaGetDto, Despesa>();
            CreateMap<DespesaPostInput, Despesa>();
            CreateMap<DespesaPutInput, Despesa>();

            CreateMap<Despesa, DespesaPostOutput>();
        }
    }
}
