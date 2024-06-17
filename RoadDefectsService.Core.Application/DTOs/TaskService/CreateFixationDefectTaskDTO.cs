using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class CreateFixationDefectTaskDTO
    {
        [Required]
        public required string ApproximateAddress { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required bool IsTransfer { get; set; }
    }
}
