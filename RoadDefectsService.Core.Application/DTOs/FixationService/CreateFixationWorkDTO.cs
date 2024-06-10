namespace RoadDefectsService.Core.Application.DTOs.FixationService
{
    public class CreateFixationWorkDTO
    {
        public required bool WorkDone { get; set; }
        public required List<Guid> PhotosIds { get; set; }
        public required Guid TaskId { get; set; }
    }
}
