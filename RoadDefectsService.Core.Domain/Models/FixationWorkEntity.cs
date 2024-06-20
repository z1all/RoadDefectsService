namespace RoadDefectsService.Core.Domain.Models
{
    public class FixationWorkEntity : FixationEntity
    {
        public required DateTime RecordedDateTime { get; set; }
        public bool? WorkDone { get; set; }

        public TaskFixationWorkEntity? TaskFixationWork { get; set; }
    }
}
