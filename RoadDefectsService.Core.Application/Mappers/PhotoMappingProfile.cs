using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.DTOs.NotificationService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public class PhotoMappingProfile : Profile
    {
        public PhotoMappingProfile() 
        {
            CreateMap<PhotoEntity, PhotoInfoDTO>();
            CreateMap<PhotoEntity, PhotoShortInfoDTO>();
        }
    }
}
