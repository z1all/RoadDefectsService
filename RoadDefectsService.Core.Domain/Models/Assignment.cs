using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class Assignment : BaseEntity
    {
        public required DateTime CreatedDateTime { get; set; }
        public required DateOnly DeadlineDateOnly { get; set; }

        public required Guid ContractorId { get; set; }
        public Contractor? Contractor { get; set; }

        public required Guid FixationDefectId { get; set; }
        public FixationDefect? FixationDefect { get; set; }
    }
}
