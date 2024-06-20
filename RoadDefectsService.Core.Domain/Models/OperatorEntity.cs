using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class OperatorEntity : BaseEntity
    {
        public required CustomUser User { get; set; }
    }
}
