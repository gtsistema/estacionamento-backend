using AutoMapper;
using Estac.Domain.Input.Veiculo;
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
            CreateMap<VeiculoModeloInput, VeiculoModelo>();

        }
    }
}
