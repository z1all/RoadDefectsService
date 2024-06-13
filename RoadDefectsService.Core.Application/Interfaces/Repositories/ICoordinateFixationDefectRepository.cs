using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Domain;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface ICoordinateFixationDefectRepository
    {
        Task<List<CoordinateFixationDefect>> GetAllByFilterAsync(CoordinatesFilter coordinatesFilter);
    }
}
