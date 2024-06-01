namespace RoadDefectsService.Core.Application.DTOs
{
    public class DefectFixationShortInfoDTO
    {
        public required Guid Id { get; set; }
        public required DateTime RecordedDateTime { get; set; }
        public required string ExactAddress { get; set; }
        public required string Coordinates { get; set; }
        public required int DamagedCanvasSquareMeter { get; set; }
        public required DefectType DefectType { get; set; }
    }
}
