using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class AppointTaskDTO
    {
        [Required]
        public required Guid TaskId { get; set; }
    }
}
