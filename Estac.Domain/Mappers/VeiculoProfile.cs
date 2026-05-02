using AutoMapper;
using Estac.Domain.Input.Veiculo;
using Estac.Domain.Input.VeiculoModelo;
using Estac.Domain.Models;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Output.Pessoa;
using Estac.Domain.Output.Veiculo;

namespace Estac.Domain.Mappers.Auth
{
    public class VeiculoProfile : Profile
    {
        public VeiculoProfile()
        {
            CreateMap<VeiculoPostInput, Veiculo>()
             .ForMember(dest => dest.VeiculoModelo, opt => opt.MapFrom(src => src.VeiculoModelo))
             .ForMember(dest => dest.Motorista, opt => opt.MapFrom(src => src.VeiculoModelo));


            CreateMap<VeiculoPutInput, Veiculo>();

            CreateMap<Veiculo, VeiculoOutput>()
              .ForMember(dest => dest.Detalhe, opt => opt.MapFrom(src => src.VeiculoDetalhe))
              .ForMember(dest => dest.Motorista, opt => opt.MapFrom(src => src.Motorista))
              .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.VeiculoModelo));


            CreateMap<VeiculoDetalhe, VeiculoDetalheOutput>();

            CreateMap<VeiculoModelo, VeiculoModeloOutput>()
                .ForMember(dest => dest.VeiculoMarca, opt => opt.MapFrom(src => src.VeiculoMarca));

            CreateMap<VeiculoMarca, VeiculoMarcaOutput>();

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
