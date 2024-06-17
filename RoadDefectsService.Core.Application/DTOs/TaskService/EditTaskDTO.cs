using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class EditTaskDTO
    {
        [Required]
        public required DateTime CreatedDateTime { get; set; }
    }
}
