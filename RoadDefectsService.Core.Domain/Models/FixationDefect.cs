namespace RoadDefectsService.Core.Domain.Models
{
    public class FixationDefect : Fixation
    {
        public TaskEntity? Task { get; set; }
    }
}
