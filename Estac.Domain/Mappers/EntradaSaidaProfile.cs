using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Movimento.Entrada;
using Estac.Domain.Models;
using Estac.Domain.Output.Movimento.EntradaSaida;

namespace Estac.Domain.Mappers
{
    public class EntradaSaidaProfile : Profile
    {
        public EntradaSaidaProfile()
        {
            CreateMap<EntradaPostInput, Estac.Domain.Models.EntradaSaida>()
                .ForMember(dest => dest.MotoristaId, opt => opt.MapFrom(src => src.Motorista != null && src.Motorista.Id.HasValue ? src.Motorista.Id.Value : 0))
                .ForMember(dest => dest.TransportadoraId, opt => opt.MapFrom(src => src.Transportadora != null && src.Transportadora.Id.HasValue ? src.Transportadora.Id.Value : (int?)null))
                .ForMember(dest => dest.VeiculoId, opt => opt.MapFrom(src => src.Veiculo != null && src.Veiculo.Id.HasValue ? src.Veiculo.Id.Value : 0))
                .ForMember(dest => dest.DataHoraEntrada, opt => opt.MapFrom(src => src.DataHoraEntrada ?? DateTime.UtcNow))
                .ForMember(dest => dest.Motorista, opt => opt.Ignore())
                .ForMember(dest => dest.Transportadora, opt => opt.Ignore())
                .ForMember(dest => dest.Veiculo, opt => opt.Ignore())
                .ForMember(dest => dest.EstacionamentoId, opt => opt.Ignore())
                .ForMember(dest => dest.Faturado, opt => opt.Ignore())
                .ForMember(dest => dest.DataFaturado, opt => opt.Ignore())
                .ForMember(dest => dest.Suspensoes, opt => opt.Ignore());
            CreateMap<Estac.Domain.Models.EntradaSaida, EntradaSaidaOutput>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDescription()));
            CreateMap<EntradaSaidaSuspensao, EntradaSaidaSuspensaoOutput>();
        }
    }
}
