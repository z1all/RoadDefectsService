namespace RoadDefectsService.Core.Domain.Models
{
    public class FixationWork : Fixation
    {
        public required DateTime RecordedDateTime { get; set; }
        public bool? WorkDone { get; set; }

        public TaskFixationWork? TaskFixationWork { get; set; }
    }
}
