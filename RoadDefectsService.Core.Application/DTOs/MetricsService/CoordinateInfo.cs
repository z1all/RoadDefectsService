namespace RoadDefectsService.Core.Application.DTOs.MetricsService
{
    public class CoordinateInfo
    {
        public required Guid FixationDefectId { get; set; }
        public required bool IsEliminated { get; set; }
        public required long X { get; set; }
        public required long Y { get; set; }
    }
}
