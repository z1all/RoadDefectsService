using RoadDefectsService.Core.Application.DTOs.Common;

namespace RoadDefectsService.Core.Application.CQRS.Contractor.DTOs
{
    public class ContractorFilterDTO : BaseFilterDTO
    {
        public string? ContractorFullName { get; set; }
        public string? OrganizationName { get; set; }
    }
}
