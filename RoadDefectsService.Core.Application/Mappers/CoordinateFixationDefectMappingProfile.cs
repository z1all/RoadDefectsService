using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Domain;

namespace RoadDefectsService.Core.Application.Mappers
{
    public class CoordinateFixationDefectMappingProfile : Profile
    {
        public CoordinateFixationDefectMappingProfile() 
        {
            CreateMap<CoordinateFixationDefect, CoordinateFixationDefectDTO>();
        }
    }
}
