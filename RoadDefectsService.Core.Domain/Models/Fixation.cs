using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class Fixation : BaseEntity
    {
        public IEnumerable<Photo> Photos { get; set; } = null!;
    }
}
