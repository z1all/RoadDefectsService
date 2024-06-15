using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.AuthService
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; init; }
        [Required]
        public required string Password { get; init; }
    }
}
