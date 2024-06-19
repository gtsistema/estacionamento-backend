using AutoMapper;
using Gp.Domain.Auth;
using Gp.Domain.Output.Auth;

namespace Gp.Domain.Mappers.Auth
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<ApplicationUser, LoginOutput>();
        }
    }
}
