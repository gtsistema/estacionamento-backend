using AutoMapper;
using Estac.Domain.Input.ConfiguracaoCobranca;
using Estac.Domain.Models;
using Estac.Domain.Output.ConfiguracaoCobranca;

namespace Estac.Domain.Mappers
{
    public class ConfiguracaoCobrancaProfile : Profile
    {
        public ConfiguracaoCobrancaProfile()
        {
            CreateMap<ConfiguracaoCobrancaPostInput, ConfiguracaoCobranca>();
            CreateMap<ConfiguracaoCobrancaPutInput, ConfiguracaoCobranca>();
            CreateMap<ConfiguracaoCobrancaRegraInput, ConfiguracaoCobrancaRegra>();

            CreateMap<ConfiguracaoCobranca, ConfiguracaoCobrancaOutput>()
                .ForMember(dest => dest.TransportadoraNome,
                    opt => opt.MapFrom(src => src.Transportadora != null
                        ? src.Transportadora.Descricao
                        : null))
                .ForMember(dest => dest.EstacionamentoNome,
                    opt => opt.MapFrom(src => src.Estacionamento != null
                        ? src.Estacionamento.Descricao
                        : null));

            CreateMap<ConfiguracaoCobrancaRegra, ConfiguracaoCobrancaRegraOutput>();
        }
    }
}
