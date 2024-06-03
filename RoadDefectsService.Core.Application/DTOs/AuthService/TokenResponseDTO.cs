
using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.AuthService
{
    public class TokenResponseDTO
    {
        [Required]
        public required string Access { get; set; }
    }
}
