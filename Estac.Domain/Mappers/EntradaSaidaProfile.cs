using AutoMapper;
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
            CreateMap<Estac.Domain.Models.EntradaSaida, EntradaSaidaOutput>();
            CreateMap<EntradaSaidaSuspensao, EntradaSaidaSuspensaoOutput>();
        }
    }
}
