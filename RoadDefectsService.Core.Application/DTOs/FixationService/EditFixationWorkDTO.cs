namespace RoadDefectsService.Core.Application.DTOs.FixationService
{
    public class EditFixationWorkDTO
    {
        public required bool WorkDone { get; set; }
        public required List<Guid> PhotosIds { get; set; }
    }
}
