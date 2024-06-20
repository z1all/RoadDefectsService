using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class DefectTypeEntity : BaseEntity
    {
        public required string Name { get; set; }
    }
}
