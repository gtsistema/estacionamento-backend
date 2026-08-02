using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.ContaBancaria;
using Estac.Domain.Input.Transportadora;
using Estac.Domain.Models;
using Estac.Domain.Output.Transportadora;
using System.Collections.Generic;

namespace Estac.Domain.Mappers
{
    public class TransportadoraProfile : Profile
    {
        public TransportadoraProfile()
        {
            // Map POST input to domain model
            CreateMap<TransportadoraPostInput, Transportadora>()
               .ForMember(dest => dest.ContasBancarias, opt => opt.MapFrom((src, _, __, ctx) =>
                    src.ContaBancaria == null
                        ? null
                        : new List<ContaBancaria> { ctx.Mapper.Map<ContaBancaria>(src.ContaBancaria) }))
               .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.PessoaJuridica))
               .ForMember(dest => dest.ResponsavelCpf, opt => opt.MapFrom(src => src.ResponsavelCpf.SomenteDigitos()));

            CreateMap<TransportadoraPutInput, Transportadora>()
               .ForMember(dest => dest.ContasBancarias, opt => opt.MapFrom((src, _, __, ctx) =>
                    src.ContaBancaria == null
                        ? null
                        : new List<ContaBancaria> { ctx.Mapper.Map<ContaBancaria>(src.ContaBancaria) }))
               .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.PessoaJuridica))
               .ForMember(dest => dest.ResponsavelCpf, opt => opt.MapFrom(src => src.ResponsavelCpf.SomenteDigitos()));

            CreateMap<Transportadora, TransportadoraOutput>()
              .ForMember(dest => dest.ContaBancaria, opt => opt.MapFrom(src => src.ContasBancarias))
              .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa))
              .ForMember(dest => dest.ResponsavelCpf, opt => opt.MapFrom(src => src.ResponsavelCpf.FormatarCpf()));

            CreateMap<ContaBancariaInput, ContaBancaria>()
                .ForMember(dest => dest.CpfCnpj, opt => opt.MapFrom(src => src.CpfCnpj.NormalizarCpfOuCnpj()));

            CreateMap<ContaBancaria, ContaBancariaOutput>()
                .ForMember(dest => dest.CpfCnpj, opt => opt.MapFrom(src => src.CpfCnpj.FormatarCpfOuCnpj()));

        }
    }
}
