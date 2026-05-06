using AutoMapper;
using Estac.Domain.Extensions;
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
               .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.PessoaJuridica))
               .ForMember(dest => dest.ResponsavelCpf, opt => opt.MapFrom(src => src.ResponsavelCpf.SomenteDigitos()));

            CreateMap<TransportadoraPutInput, Transportadora>()
               .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.PessoaJuridica))
               .ForMember(dest => dest.ResponsavelCpf, opt => opt.MapFrom(src => src.ResponsavelCpf.SomenteDigitos()));

            CreateMap<Transportadora, TransportadoraOutput>()
              .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa))
              .ForMember(dest => dest.ResponsavelCpf, opt => opt.MapFrom(src => src.ResponsavelCpf.FormatarCpf()));


            CreateMap<ContaBancaria, ContaBancariaOutput>()
                .ForMember(dest => dest.CpfCnpj, opt => opt.MapFrom(src => src.CpfCnpj.FormatarCpfOuCnpj()));

        }
    }
}
