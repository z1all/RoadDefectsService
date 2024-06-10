namespace RoadDefectsService.Core.Domain.Models
{
    public class FixationWork : Fixation
    {
        //public required Guid TaskFixationWorkId { get; set; }
        public TaskFixationWork? TaskFixationWork { get; set; }
    }
}
