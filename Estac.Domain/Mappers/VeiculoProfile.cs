using AutoMapper;
using Estac.Domain.Input.Veiculo;
using Estac.Domain.Input.VeiculoModelo;
using Estac.Domain.Models;
using Estac.Domain.Output.Veiculo;

namespace Estac.Domain.Mappers.Auth
{
    public class VeiculoProfile : Profile
    {
        public VeiculoProfile()
        {
            CreateMap<VeiculoPostInput, Veiculo>();
            CreateMap<VeiculoPutInput, Veiculo>();
            CreateMap<Veiculo, VeiculoOutput>();
            CreateMap<VeiculoDetalheInput, VeiculoDetalhe>();
            CreateMap<VeiculoMarcaInput, VeiculoMarca>();

            CreateMap<VeiculoModeloPostInput, VeiculoModelo>()
             .ForMember(dest => dest.VeiculoMarca, opt => opt.MapFrom(src => src.VeiculoMarca));

            CreateMap<VeiculoModeloPutInput, VeiculoModelo>()
             .ForMember(dest => dest.VeiculoMarca, opt => opt.MapFrom(src => src.VeiculoMarca));

            CreateMap<VeiculoModeloInput, VeiculoModelo>()
             .ForMember(dest => dest.VeiculoMarca, opt => opt.MapFrom(src => src.VeiculoMarca));

        }
    }
}
