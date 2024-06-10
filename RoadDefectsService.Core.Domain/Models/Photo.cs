using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class Photo : BaseEntity
    {
        public string PathName { get => $"{Id}_{Name}{Type}"; }
        public required string Name { get; set; }
        public required string Type { get; set; }

        public required Guid OwnerId { get; set; }
        public CustomUser? Owner { get; set; }

        public Guid? FixationWorkId { get; set; }
        public Guid? FixationDefectId { get; set; }
    }
}
