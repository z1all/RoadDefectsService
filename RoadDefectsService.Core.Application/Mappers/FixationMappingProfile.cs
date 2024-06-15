using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.AssignmentService;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.DTOs.NotificationService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public class FixationMappingProfile : Profile
    {
        public FixationMappingProfile() 
        {
            CreateMap<FixationDefect, FixationDefectDTO>();
            CreateMap<FixationDefect, FixationDefectWithPhotoShortInfoDTO>();
            CreateMap<FixationDefect, FixationDefectShortInfoDTO>();

            CreateMap<FixationWork, FixationWorkDTO>();
        }
    }
}
