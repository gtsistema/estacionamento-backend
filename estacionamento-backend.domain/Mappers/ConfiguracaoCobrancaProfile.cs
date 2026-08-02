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
            CreateMap<ConfiguracaoAgendamento, ConfiguracaoAgendamentoOutput>()
                .ForMember(dest => dest.TransportadoraId,
                    opt => opt.MapFrom(src => src.ConfiguracaoCobranca != null
                        ? src.ConfiguracaoCobranca.TransportadoraId
                        : 0))
                .ForMember(dest => dest.EstacionamentoId,
                    opt => opt.MapFrom(src => src.ConfiguracaoCobranca != null
                        ? src.ConfiguracaoCobranca.EstacionamentoId
                        : 0));

            // O agendamento é derivado da própria configuração de cobrança, nunca recebido do cliente.
            CreateMap<ConfiguracaoCobrancaPostInput, ConfiguracaoCobranca>()
                .ForMember(dest => dest.ConfiguracaoAgendamento, opt => opt.Ignore());

            CreateMap<ConfiguracaoCobrancaPutInput, ConfiguracaoCobranca>()
                .ForMember(dest => dest.ConfiguracaoAgendamento, opt => opt.Ignore());

            CreateMap<ConfiguracaoCobranca, ConfiguracaoCobrancaOutput>()
                .ForMember(dest => dest.TransportadoraNome,
                    opt => opt.MapFrom(src => src.Transportadora != null
                        ? src.Transportadora.Descricao
                        : null))
                .ForMember(dest => dest.EstacionamentoNome,
                    opt => opt.MapFrom(src => src.Estacionamento != null
                        ? src.Estacionamento.Descricao
                        : null))
                .ForMember(dest => dest.ConfiguracaoAgendamento,
                    opt => opt.MapFrom(src => src.ConfiguracaoAgendamento));
        }
    }
}
