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

            CreateMap<RegisterInput, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EstacionadoId, opt => opt.MapFrom(src => src.EstacionamentoId))
                .ForMember(dest => dest.Pessoa, opt => opt.Ignore())
                .ForMember(dest => dest.Estacionamento, opt => opt.Ignore())
                .ForMember(dest => dest.Transportadora, opt => opt.Ignore());

            CreateMap<ApplicationRole, PerfilCreateInput>()
               .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name));

            CreateMap<PerfilCreateInput, ApplicationRole>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome));

            CreateMap<PerfilUpdateInput, ApplicationRole>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome));

            CreateMap<ApplicationRole, PerfilOutput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}