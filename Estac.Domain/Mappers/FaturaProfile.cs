using AutoMapper;
using Estac.Domain.Input.Fatura;
using Estac.Domain.Models;
using Estac.Domain.Output.Fatura;

namespace Estac.Domain.Mappers
{
    public class FaturaProfile : Profile
    {
        public FaturaProfile()
        {
            CreateMap<FaturaPostInput, Fatura>()
                .ForMember(dest => dest.Itens, opt => opt.Ignore());
            CreateMap<FaturaPutInput, Fatura>()
                .ForMember(dest => dest.Itens, opt => opt.Ignore());

            CreateMap<Fatura, FaturaOutput>()
                .ForMember(dest => dest.TransportadoraNome,
                    opt => opt.MapFrom(src => src.Transportadora != null ? src.Transportadora.Descricao : null))
                .ForMember(dest => dest.EstacionamentoNome,
                    opt => opt.MapFrom(src => src.Estacionamento != null ? src.Estacionamento.Descricao : null))
                .ForMember(dest => dest.ValorEmAberto,
                    opt => opt.MapFrom(src => src.ValorEmAberto));
        }
    }
}
