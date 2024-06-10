using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class DefectType : BaseEntity
    {
        public required string Name { get; set; }
    }
}
