using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.ProfileService
{
    public class EditProfileDTO
    {
        [Required]
        public required string FullName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
