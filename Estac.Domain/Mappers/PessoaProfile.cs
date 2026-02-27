using AutoMapper;
using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Input.PessoaContato;
using Estac.Domain.Models;
using Estac.Domain.Output.Pessoa;

namespace Estac.Domain.Mappers.Auth
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<PessoaInput, Pessoa>()
                .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos));

            CreateMap<Pessoa, PessoaOutput>()
               .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos))
               .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos));

            CreateMap<PessoaEnderecoInput, PessoaEndereco>();
            CreateMap<PessoaEndereco, PessoaEnderecoOutput>();

            CreateMap<PessoaContatoInput, PessoaContato>();
            CreateMap<PessoaContato, PessoaContatoOutput>();
        }
    }
}
