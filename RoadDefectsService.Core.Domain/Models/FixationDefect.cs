namespace RoadDefectsService.Core.Domain.Models
{
    public class FixationDefect : Fixation
    {
        //public required Guid TaskId { get; set; }
        public TaskEntity? Task { get; set; }
    }
}
