using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class RoadInspector : BaseEntity
    {
        public required CustomUser User { get; set; }

        public IEnumerable<TaskEntity> AppointedTasks { get; set; } = null!;
    }
}
