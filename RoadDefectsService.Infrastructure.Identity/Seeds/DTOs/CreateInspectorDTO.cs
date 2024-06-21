namespace RoadDefectsService.Infrastructure.Identity.Seeds.DTOs
{
    public class CreateInspectorDTO
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public required string Password { get; set; }
    }
}
