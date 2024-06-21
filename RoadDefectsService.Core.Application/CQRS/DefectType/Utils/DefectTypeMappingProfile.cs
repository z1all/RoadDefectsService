using AutoMapper;
using RoadDefectsService.Core.Application.CQRS.DefectType.DTOs;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.DefectType.Utils
{
    public class DefectTypeMappingProfile : Profile
    {
        public DefectTypeMappingProfile()
        {
            CreateMap<DefectTypeEntity, DefectTypeDTO>();
        }
    }
}
