namespace RoadDefectsService.Core.Application.DTOs.FixationService
{
    public class EditFixationDefectDTO
    {
        public required string ExactAddress { get; set; }
        public required double CoordinatesX { get; set; }
        public required double CoordinatesY { get; set; }
        public required double DamagedCanvasSquareMeter { get; set; }
        public required Guid DefectTypeId { get; set; }
    }
}
