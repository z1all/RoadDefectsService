using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.AssignmentService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public class AssignmentMappingProfile : Profile
    {
        public AssignmentMappingProfile() 
        {
            CreateMap<AssignmentEntity, AssignmentDTO>();
            CreateMap<AssignmentEntity, AssignmentShortInfoDTO>();
        }
    }
}
