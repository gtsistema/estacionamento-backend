using AutoMapper;
using Estac.Domain.Input.ContaBancaria;
using Estac.Domain.Input.Transportadora;
using Estac.Domain.Models;
using Estac.Domain.Output.Transportadora;

namespace Estac.Domain.Mappers
{
    public class TransportadoraProfile : Profile
    {
        public TransportadoraProfile()
        {
            // Map POST input to domain model
            CreateMap<TransportadoraPostInput, Transportadora>()
               .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.PessoaJuridica));

            CreateMap<TransportadoraPutInput, Transportadora>()
               .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.PessoaJuridica));

            CreateMap<Transportadora, TransportadoraOutput>()
              .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa));

            CreateMap<ContaBancariaInput, ContaBancaria>();

            CreateMap<ContaBancaria, ContaBancariaOutput>();

        }
    }
}
