using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<CustomUser, UserInfoDTO>()
                .ForMember(
                    userInfo => userInfo.HighestRole, 
                    options => options.MapFrom(customUser => customUser.HighestRole.ToRoleEnum())
                );
            CreateMap<CustomUser, RoadInspectorDTO>();
        }
    }
}
