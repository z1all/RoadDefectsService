using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.CQRS.Task.DTOs
{
    public class AppointTaskDTO
    {
        [Required]
        public required Guid TaskId { get; set; }
    }
}
