using AutoMapper;
using Estac.Domain.Input.ContaBancaria;
using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Models;
using Estac.Domain.Output.Estacionamento;
using Microsoft.Extensions.Options;

namespace Estac.Domain.Mappers
{
    public class EstacionamentoProfile : Profile
    {
        public EstacionamentoProfile()
        {
            // Map POST input to domain model
            CreateMap<EstacionamentoPostInput, Estacionamento>()
               .ForMember(dest => dest.ContasBancarias, opt => opt.MapFrom(src => src.ContaBancaria))
               .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa));


            CreateMap<EstacionamentoPutInput, Estacionamento>()
               .ForMember(dest => dest.ContasBancarias, opt => opt.MapFrom(src => src.ContaBancaria))
               .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa));

            CreateMap<Estacionamento, EstacionamentoOutput>()
              .ForMember(dest => dest.ContaBancaria, opt => opt.MapFrom(src => src.ContasBancarias))
              .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa));

            CreateMap<ContaBancariaInput, ContaBancaria>();

            CreateMap<ContaBancaria, ContaBancariaOutput>();

        }
    }
}
