using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class Photo : BaseEntity
    {
        public string PathName { get => $"{Id}_{Name}{Type}"; }
        public required string Name { get; set; }
        public required string Type { get; set; }

        public Guid? FixationWorkId { get; set; }
        public FixationWork? FixationWork { get; set; }

        public Guid? FixationDefectId { get; set; }
        public FixationDefect? FixationDefect { get; set; }
    }
}
