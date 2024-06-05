using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs
{
    public class AppointTaskDTO
    {
        [Required]
        public required Guid TaskId { get; set; }
    }
}
