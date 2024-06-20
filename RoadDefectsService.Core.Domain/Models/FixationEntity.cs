using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class FixationEntity : BaseEntity
    {
        public IEnumerable<PhotoEntity> Photos { get; set; } = null!;
    }
}
