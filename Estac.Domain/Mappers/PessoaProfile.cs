using AutoMapper;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Models;
using Estac.Domain.Output.Pessoa;

namespace Estac.Domain.Mappers.Auth
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<PessoaInput, Pessoa>();
            CreateMap<Pessoa, PessoaOutput>();

        }
    }
}
