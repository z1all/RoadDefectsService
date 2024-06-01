using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs
{
    public class CreateFixationDefectTaskDTO
    {
        [Required]
        public required string Address { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}
