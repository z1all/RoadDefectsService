using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs
{
    public class AppointTaskDTO
    {
        [Required]
        public required Guid taskId { get; set; }
    }
}
