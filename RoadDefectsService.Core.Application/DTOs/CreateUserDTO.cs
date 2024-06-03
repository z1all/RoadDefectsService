using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        [MinLength(5)]
        public required string FullName { get; init; }
        [Required]
        [EmailAddress]
        public required string Email { get; init; }
        [Required]
        public required string Password { get; init; }
    }
}
