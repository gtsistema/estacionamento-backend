using AutoMapper;
using Estac.Domain.Auth;
using Estac.Domain.Input.Auth;
using Estac.Domain.Output.Auth;
using Microsoft.AspNetCore.Identity;

namespace Estac.Domain.Mappers.Auth
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<ApplicationUser, LoginOutput>();

            CreateMap<ApplicationUser, RegisterInput>()
               .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa));

            CreateMap<ApplicationRole, PerfilCreateInput>()
               .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name));

            CreateMap<ApplicationRole, PerfilOutput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}