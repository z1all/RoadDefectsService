namespace RoadDefectsService.Core.Application.DTOs.MetricsService
{
    public class ReportDTO
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required byte[] File { get; set; }
    }
}
