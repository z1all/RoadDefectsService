using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class CreateFixationDefectTaskDTO
    {
        [Required]
        public required string Address { get; set; }
        [Required]
        public required double CoordinateX { get; set; }
        [Required]
        public required double CoordinateY { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required bool IsTransfer { get; set; }
    }
}
