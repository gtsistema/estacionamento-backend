using AutoMapper;
using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Extensions;
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
                .ForMember(d => d.Pessoa, opt => opt.MapFrom(s => s.PessoaFisica))
                .ForMember(d => d.Transportadora, opt => opt.Ignore())
                .ForMember(d => d.VeiculoMotoristas, opt => opt.Ignore())
                .ForMember(d => d.Descricao, opt => opt.Ignore());

            CreateMap<MotoristaPutInput, Motorista>()
                .IncludeBase<MotoristaPostInput, Motorista>();

            CreateMap<PessoaMotoristaInput, Pessoa>()
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Cpf.SomenteDigitos()))
                .ForMember(dest => dest.NomeRazaoSocial, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.TipoPessoa, opt => opt.Ignore())
                .ForMember(dest => dest.InscricaoEstadual, opt => opt.Ignore())
                .ForMember(dest => dest.Papeis, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos ?? Enumerable.Empty<PessoaEnderecoInput>()))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos ?? Enumerable.Empty<PessoaContatoInput>()))
                .AfterMap((src, dest, ctx) =>
                {
                    // Garante coleções tipadas mesmo se o MapFrom de IEnumerable→ICollection falhar no runtime
                    dest.Enderecos = ctx.Mapper.Map<List<PessoaEndereco>>(src.Enderecos ?? Enumerable.Empty<PessoaEnderecoInput>());
                    dest.Contatos = ctx.Mapper.Map<List<PessoaContato>>(src.Contatos ?? Enumerable.Empty<PessoaContatoInput>());
                });

            CreateMap<PessoaEstacionamentoInput, Pessoa>()
               .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Cnpj.SomenteDigitos()))
               .ForMember(dest => dest.NomeRazaoSocial, opt => opt.MapFrom(src => src.NomeRazaoSocial))
               .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.NomeFantasia));

            CreateMap<Motorista, MotoristaOutput>()
                .ForMember(d => d.PessoaFisica, opt => opt.MapFrom(s => s.Pessoa));

            CreateMap<MotoristaSearchOutput, MotoristaOutput>();

            CreateMap<Pessoa, PessoaMotoristaInput>()
                .ForMember(d => d.Cpf, opt => opt.MapFrom(s => s.Documento.FormatarCpf()));

            CreateMap<PessoaEndereco, PessoaEnderecoInput>();

            CreateMap<PessoaContato, PessoaContatoInput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.PessoaId, opt => opt.MapFrom(src => src.PessoaId))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.FormatarCpf()))
                .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Principal, opt => opt.MapFrom(src => src.Principal))
                .ForMember(dest => dest.Observacao, opt => opt.MapFrom(src => src.Observacao));

            CreateMap<Pessoa, PessoaMotoristaOutput>()
              .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NomeRazaoSocial))
              .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Documento.FormatarCpf()))
              .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo))
              .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos ?? Enumerable.Empty<PessoaContato>()))
              .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos ?? Enumerable.Empty<PessoaEndereco>()))
              .AfterMap((src, dest, ctx) =>
              {
                  dest.Contatos = ctx.Mapper.Map<List<PessoaContatoInput>>(src.Contatos ?? Enumerable.Empty<PessoaContato>());
                  dest.Enderecos = ctx.Mapper.Map<List<PessoaEnderecoInput>>(src.Enderecos ?? Enumerable.Empty<PessoaEndereco>());
              });
        }
    }
}
