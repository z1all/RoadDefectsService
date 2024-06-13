using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IMetricsService
    {
        Task<ExecutionResult<List<CoordinateInfo>>> GetCoordinatesFixationsDefectsAsync(CoordinatesFilter filter);
    }
}
