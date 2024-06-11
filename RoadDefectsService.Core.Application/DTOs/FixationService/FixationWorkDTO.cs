namespace RoadDefectsService.Core.Application.DTOs.FixationService
{
    public class FixationWorkDTO
    {
        public required Guid Id { get; set; }
        public required DateTime RecordedDateTime { get; set; }
        public required bool? WorkDone { get; set; }
        public required List<PhotoInfoDTO> Photos { get; set; }
    }
}
