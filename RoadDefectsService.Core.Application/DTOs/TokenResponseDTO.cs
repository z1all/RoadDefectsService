
using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs
{
    public class TokenResponseDTO
    {
        [Required]
        public required string Access { get; set; }
    }
}
