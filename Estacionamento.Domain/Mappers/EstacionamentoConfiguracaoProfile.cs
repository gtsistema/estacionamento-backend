using AutoMapper;
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Models;
using Estac.Domain.Output.Estacionamento;

namespace Estac.Domain.Mappers
{
    public class EstacionamentoConfiguracaoProfile : Profile
    {
        public EstacionamentoConfiguracaoProfile()
        {
            CreateMap<EstacionamentoConfiguracao, EstacionamentoConfiguracaoOutput>()
                .ForMember(d => d.Nome, opt => opt.Ignore())
                .ForMember(d => d.UtcOffset, opt => opt.Ignore());

            CreateMap<EstacionamentoConfiguracaoPostInput, EstacionamentoConfiguracao>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.EstacionamentoId, opt => opt.Ignore())
                .ForMember(d => d.Cultura, opt => opt.Ignore())
                .ForMember(d => d.Ativo, opt => opt.Ignore())
                .ForMember(d => d.DataCriacao, opt => opt.Ignore())
                .ForMember(d => d.DataAtualizacao, opt => opt.Ignore())
                .ForMember(d => d.Estacionamento, opt => opt.Ignore());
        }
    }
}
