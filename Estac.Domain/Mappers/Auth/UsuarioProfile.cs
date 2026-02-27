using AutoMapper;
using Estac.Domain.Auth;
using Estac.Domain.Input.Auth;
using Estac.Domain.Output.Auth;

namespace Estac.Domain.Mappers.Auth
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<ApplicationUser, LoginOutput>();

            CreateMap<ApplicationUser, RegisterInput>();
        }
    }
}