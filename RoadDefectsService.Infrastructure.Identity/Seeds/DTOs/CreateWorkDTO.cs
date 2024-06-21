namespace RoadDefectsService.Infrastructure.Identity.Seeds.DTOs
{
    public class CreateWorkDTO
    {
        public required Guid Id { get; set; }
        public required Guid TaskFixationWorkId { get; set; }
        public required bool WorkDone { get; set; }
    }
}
