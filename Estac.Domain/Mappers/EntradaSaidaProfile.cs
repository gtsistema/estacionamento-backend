using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.EntradaSaida;
using Estac.Domain.Models;
using Estac.Domain.Output.EntradaSaida;

namespace Estac.Domain.Mappers
{
    public class EntradaSaidaProfile : Profile
    {
        public EntradaSaidaProfile()
        {
            CreateMap<EntradaSaidaPostInput, Estac.Domain.Models.EntradaSaida>();
            CreateMap<EntradaSaidaPutInput, Estac.Domain.Models.EntradaSaida>();
            CreateMap<Estac.Domain.Models.EntradaSaida, EntradaSaidaOutput>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDescription()));
            CreateMap<EntradaSaidaSuspensao, EntradaSaidaSuspensaoOutput>();
        }
    }
}
