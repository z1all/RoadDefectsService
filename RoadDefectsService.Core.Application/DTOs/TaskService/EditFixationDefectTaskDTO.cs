using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class EditFixationDefectTaskDTO
    {
        [Required]
        public required string Address { get; set; }
        [Required]
        public required double CoordinateX { get; set; }
        [Required]
        public required double CoordinateY { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}
