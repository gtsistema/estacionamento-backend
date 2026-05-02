using AutoMapper;
using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Input.PessoaContato;
using Estac.Domain.Models;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Output.Pessoa;
using Estac.Domain.Shared;

namespace Estac.Domain.Mappers.Auth
{
    public class MotoristaProfile : Profile
    {
        public MotoristaProfile()
        {
            CreateMap<MotoristaPostInput, Motorista>()
                .ForMember(d => d.Pessoa, opt => opt.MapFrom(s => s.Pessoa));

            CreateMap<MotoristaPutInput, Motorista>()

                .ForMember(d => d.Pessoa, opt => opt.MapFrom(s => s.Pessoa));

            CreateMap<Motorista, MotoristaOutput>()
                .ForMember(d => d.PessoaFisica, opt => opt.MapFrom(s => s.Pessoa));

            CreateMap<MotoristaSearchOutput, MotoristaOutput>()
                .ForMember(d => d.PessoaFisica, opt => opt.Ignore());

            CreateMap<Pessoa, PessoaMotorista>()
                .ForMember(d => d.Cpf, opt => opt.MapFrom(s => s.Documento))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email));

            CreateMap<Veiculo, VeiculoVinculoResumoOutput>()
                .ForMember(d => d.Placa, opt => opt.MapFrom(s => VeiculoPlacaHelper.FormatarExibicao(s.Placa)));

            CreateMap<PessoaEndereco, PessoaEnderecoInput>();

            CreateMap<PessoaContato, PessoaContatoInput>();

            CreateMap<Pessoa, PessoaMotoristaOutput>()
              .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NomeRazaoSocial))
              .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Documento))
              .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo))
              .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
              .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos));
        }
    }
}
