using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class MetricsService : IMetricsService
    {
        public Task<ExecutionResult<List<CoordinateInfo>>> GetCoordinatesFixationsDefectsAsync(CoordinatesFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
