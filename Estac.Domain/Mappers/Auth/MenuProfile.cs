using AutoMapper;
using Estac.Domain.Input.Auth;
using Estac.Domain.Input.ContaBancaria;
using Estac.Domain.Models;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output.Auth;

namespace Estac.Domain.Mappers
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            // Map POST input to domain model
            CreateMap<MenuCreateInput, Module>()
               .ForMember(dest => dest.SubModules, opt => opt.MapFrom(src => src.SubMenus));

            CreateMap<MenuUpdateInput, Module>()
               .ForMember(dest => dest.SubModules, opt => opt.MapFrom(src => src.SubMenus));

            CreateMap<Module, MenuOutput>()
              .ForMember(dest => dest.SubMenus, opt => opt.MapFrom(src => src.SubModules));

            CreateMap<SubModule, SubMenuOutput>()
             .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions));

            CreateMap<Permission, PermissionOutput>();
        }
    }
}
