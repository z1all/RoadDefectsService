using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.UserService
{
    public class EditUserDTO
    {
        [Required]
        [MinLength(5)]
        public required string FullName { get; init; }
    }
}
