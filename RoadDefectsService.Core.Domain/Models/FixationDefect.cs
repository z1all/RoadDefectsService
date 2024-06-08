using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class FixationDefect : BaseEntity
    {
        //public required Guid TaskId { get; set; }
        public TaskEntity? Task { get; set; }
    }
}
