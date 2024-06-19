using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Models;
using Gp.Domain.Shared;

namespace Gp.Domain.Mappers
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            CreateMap<DespesaGetDto, Despesa>().ReverseMap();
        }
    }
}
