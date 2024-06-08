using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class FixationWork : BaseEntity
    {
        //public required Guid TaskFixationWorkId { get; set; }
        public TaskFixationWork? TaskFixationWork { get; set; }
    }
}
