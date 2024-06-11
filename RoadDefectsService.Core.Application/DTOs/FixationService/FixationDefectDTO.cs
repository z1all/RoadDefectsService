namespace RoadDefectsService.Core.Application.DTOs.FixationService
{
    public class FixationDefectDTO
    {
        public required Guid Id { get; set; }
        public required DateTime RecordedDateTime { get; set; }
        public required string? ExactAddress { get; set; }
        public required double? CoordinatesX { get; set; }
        public required double? CoordinatesY { get; set; }
        public required double? DamagedCanvasSquareMeter { get; set; }
        public required DefectTypeDTO? DefectType { get; set; }
        public required List<PhotoInfoDTO> Photos { get; set; }
    }
}
