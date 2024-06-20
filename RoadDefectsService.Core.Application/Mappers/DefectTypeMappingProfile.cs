using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public class DefectTypeMappingProfile : Profile
    {
        public DefectTypeMappingProfile() 
        {
            CreateMap<DefectTypeEntity, DefectTypeDTO>();
        }
    }
}
