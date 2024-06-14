using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Domain;

namespace RoadDefectsService.Core.Application.Mappers
{
    public static class CoordinateFixationDefectMapper
    {
        public static CoordinateFixationDefectDTO ToCoordinateFixationDefectDTO(this CoordinateFixationDefect coordinateFixationDefect)
        {
            return new()
            {
                FixationDefectId = coordinateFixationDefect.FixationDefectId,
                IsEliminated = coordinateFixationDefect.IsEliminated,
                X = coordinateFixationDefect.X,
                Y = coordinateFixationDefect.Y,
                FixationDateTime = coordinateFixationDefect.FixationDateTime,
            };
        }

        public static List<CoordinateFixationDefectDTO> ToCoordinateFixationDefectDTOList(this List<CoordinateFixationDefect> coordinateFixationDefects)
        {
            return coordinateFixationDefects.Select(ToCoordinateFixationDefectDTO).ToList();
        }
    }
}
