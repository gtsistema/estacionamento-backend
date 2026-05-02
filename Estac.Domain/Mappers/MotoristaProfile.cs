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
            CreateMap<Motorista, MotoristaOutput>()
                .ForMember(d => d.PessoaFisica, opt => opt.MapFrom(s => s.Pessoa));
            CreateMap<MotoristaSearchOutput, MotoristaOutput>()
                .ForMember(d => d.PessoaFisica, opt => opt.Ignore());
            CreateMap<Veiculo, VeiculoVinculoResumoOutput>();
        }
    }
}
