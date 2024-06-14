using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.MetricsService
{
    public class CoordinatesFilter : IValidatableObject
    {
        public required bool ShowNotEliminated { get; set; }
        public required DateOnly BeginDateOnly { get; set; }
        public required DateOnly EndDateOnly { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BeginDateOnly > EndDateOnly)
            {
                yield return new ValidationResult(
                    "BeginDateOnly should not be later than EndDateOnly",
                    [nameof(BeginDateOnly), nameof(EndDateOnly)]);
            }
        }
    }
}
