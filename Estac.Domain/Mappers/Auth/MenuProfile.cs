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
               .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Nome))
               .ForMember(dest => dest.SubModules, opt => opt.MapFrom(src => src.SubMenus));

            CreateMap<MenuUpdateInput, Module>()
               .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Nome))
               .ForMember(dest => dest.SubModules, opt => opt.MapFrom(src => src.SubMenus));

            CreateMap<Module, MenuOutput>()
              .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
              .ForMember(dest => dest.SubMenus, opt => opt.MapFrom(src => src.SubModules));

            CreateMap<SubMenuCreateInput, SubModule>()
              .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Nome))
              .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions));

            CreateMap<SubModule, SubMenuOutput>()
             .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
             .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions));

            CreateMap<PermissionInput, Permission>()
             .ForMember(dest => dest.Acao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<Permission, PermissionOutput>()
             .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Acao));

        }
    }
}
