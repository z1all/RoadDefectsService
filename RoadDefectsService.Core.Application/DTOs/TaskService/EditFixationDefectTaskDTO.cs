using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class EditFixationDefectTaskDTO
    {
        [Required]
        public required string ApproximateAddress { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}
