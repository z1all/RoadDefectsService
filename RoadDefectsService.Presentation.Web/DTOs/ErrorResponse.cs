using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Presentation.Web.DTOs
{
    public class ErrorResponse
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public int Status { get; set; }
        [Required]
        public ImmutableDictionary<string, List<string>> Errors { get; set; } = null!;
    }
}
