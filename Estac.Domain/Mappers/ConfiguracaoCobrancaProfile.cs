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
            CreateMap<ConfiguracaoAgendamentoInput, ConfiguracaoAgendamento>()
                .ForMember(dest => dest.ConfiguracaoCobrancaId, opt => opt.Ignore())
                .ForMember(dest => dest.ConfiguracaoCobranca, opt => opt.Ignore())
                .ForMember(dest => dest.UltimaExecucao, opt => opt.Ignore())
                .ForMember(dest => dest.ProximaExecucao, opt => opt.Ignore())
                .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore());

            CreateMap<ConfiguracaoAgendamento, ConfiguracaoAgendamentoOutput>();

            CreateMap<ConfiguracaoCobrancaPostInput, ConfiguracaoCobranca>()
                .ForMember(dest => dest.ConfiguracaoAgendamento, opt => opt.MapFrom(src => src.ConfiguracaoAgendamento));

            CreateMap<ConfiguracaoCobrancaPutInput, ConfiguracaoCobranca>()
                .ForMember(dest => dest.ConfiguracaoAgendamento, opt => opt.MapFrom(src => src.ConfiguracaoAgendamento));

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
