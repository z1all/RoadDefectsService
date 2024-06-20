using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.CQRS.Contractor.DTOs
{
    public class EditContractorDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string ContractorFullName { get; set; }
        [Required]
        public required string OrganizationName { get; set; }
    }
}
