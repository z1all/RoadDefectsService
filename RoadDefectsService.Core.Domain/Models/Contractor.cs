using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class Contractor : SoftDeleteBaseEntity
    {
        public required string Email { get; set; }
        public required string ContractorFullName { get; set; }
        public required string OrganizationName { get; set; }
    }
}
