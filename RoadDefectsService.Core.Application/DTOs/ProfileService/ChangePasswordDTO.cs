using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.ProfileService
{
    public class ChangePasswordDTO
    {
        [Required]
        public required string CurrentPassword { get; set; }
        [Required]
        public required string NewPassword { get; set; }
    }
}
