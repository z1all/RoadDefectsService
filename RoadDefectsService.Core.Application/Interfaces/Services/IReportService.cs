using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IReportService
    {
        ExecutionResult<ReportDTO> GenerateReport(GenerateWorkReportDTO generateWorkReport);
    }
}
