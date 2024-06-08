namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class RoadInspectorDTO
    {
        public required Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
    }
}
