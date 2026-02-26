using AutoMapper;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Models;
using Estac.Domain.Output.Motorista;

namespace Estac.Domain.Mappers.Auth
{
    public class MotoristaProfile : Profile
    {
        public MotoristaProfile()
        {
            CreateMap<MotoristaPostInput, Motorista>();
            CreateMap<MotoristaPutInput, Motorista>();
            CreateMap<Motorista, MotoristaOutput>();
        }
    }
}
