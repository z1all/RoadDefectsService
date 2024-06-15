namespace RoadDefectsService.Core.Application.DTOs.NotificationService
{
    public class FixationDefectWithPhotoShortInfoDTO
    {
        public required Guid Id { get; set; }
        public required DateTime RecordedDateTime { get; set; }
        public required string? ExactAddress { get; set; }
        public required double? DamagedCanvasSquareMeter { get; set; }
        public required string? DefectTypeName { get; set; }
        public required List<PhotoShortInfoDTO> Photos { get; set; }
    }
}
