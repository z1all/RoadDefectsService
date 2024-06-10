namespace RoadDefectsService.Core.Application.DTOs.FixationService
{
    public class CreateFixationDefectDTO
    {
        public required string ExactAddress { get; set; }
        public required double CoordinatesX { get; set; }
        public required double CoordinatesY { get; set; }
        public required double DamagedCanvasSquareMeter { get; set; }
        public required Guid DefectTypeId { get; set; }
        public required List<Guid> PhotosIds { get; set; }

        public required Guid TaskId { get; set; }
    }
}
