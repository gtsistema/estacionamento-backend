using AutoMapper;
using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Input.PessoaContato;
using Estac.Domain.Extensions;
using Estac.Domain.Models;
using Estac.Domain.Output.Pessoa;

namespace Estac.Domain.Mappers.Auth
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<PessoaInput, Pessoa>()
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Cnpj.SomenteAlfanumericos()))
                .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos));

            CreateMap<Pessoa, PessoaOutput>()
               .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
               .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Documento.FormatarCpf()))
               .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos))
               .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos));

            CreateMap<PessoaUsuarioInput, Pessoa>()
               .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Cpf.SomenteDigitos()))
               .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Nome))
               .ForMember(dest => dest.NomeRazaoSocial, opt => opt.MapFrom(src => src.Nome));


            CreateMap<PessoaEnderecoInput, PessoaEndereco>()
               .ForMember(dest => dest.Cep, opt => opt.MapFrom(src => src.Cep.SomenteDigitos()));

            CreateMap<PessoaEndereco, PessoaEnderecoOutput>();

            CreateMap<PessoaContatoInput, PessoaContato>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.PessoaId, opt => opt.MapFrom(src => src.PessoaId))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.SomenteDigitos()))
                .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone.SomenteDigitos()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Principal, opt => opt.MapFrom(src => src.Principal))
                .ForMember(dest => dest.Observacao, opt => opt.MapFrom(src => src.Observacao))
                .ForMember(dest => dest.Pessoa, opt => opt.Ignore());

            CreateMap<PessoaContato, PessoaContatoOutput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.PessoaId, opt => opt.MapFrom(src => src.PessoaId))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.FormatarCpf()))
                .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone.FormatarTelefone()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Principal, opt => opt.MapFrom(src => src.Principal))
                .ForMember(dest => dest.Observacao, opt => opt.MapFrom(src => src.Observacao));

            CreateMap<Pessoa, PessoaTransportadoraOutput>()
              .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Documento.FormatarCnpj()))
              .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(src => src.Descricao))
              .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos))
              .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos));

            CreateMap<Pessoa, PessoaEstacionamentoOutput>()
              .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Documento.FormatarCnpj()))
              .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(src => src.Descricao))
              .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos))
              .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos));
        }
    }
}
