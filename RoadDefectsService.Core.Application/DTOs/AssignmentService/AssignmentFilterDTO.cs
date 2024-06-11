using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.Enums;

namespace RoadDefectsService.Core.Application.DTOs.AssignmentService
{
    public class AssignmentFilterDTO : BaseFilterDTO
    {
        public Guid? ContractorId { get; set; }
        public string? FixationDefectAddress { get; set; }
        public AssignmentSortFilter AssignmentSort { get; set; } = AssignmentSortFilter.None;
    }
}
