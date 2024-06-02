using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs
{
    public class ProfileDTO
    {
        [Required]
        public required string FullName { get; set; }
        [Required]
        public required string Email { get; set; }
    }
}
