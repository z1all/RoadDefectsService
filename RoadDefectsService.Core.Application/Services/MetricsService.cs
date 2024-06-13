using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Mappers;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain;

namespace RoadDefectsService.Core.Application.Services
{
    public class MetricsService : IMetricsService
    {
        private readonly ICoordinateFixationDefectRepository _coordinateFixationDefectRepository;

        public MetricsService(ICoordinateFixationDefectRepository coordinateFixationDefectRepository)
        {
            _coordinateFixationDefectRepository = coordinateFixationDefectRepository;
        }

        public async Task<ExecutionResult<List<CoordinateFixationDefectDTO>>> GetCoordinatesFixationsDefectsAsync(CoordinatesFilter filter)
        {
            List<CoordinateFixationDefect> coordinates = await _coordinateFixationDefectRepository.GetAllByFilterAsync(filter);

            return coordinates.ToCoordinateFixationDefectDTOList();
        }
    }
}
