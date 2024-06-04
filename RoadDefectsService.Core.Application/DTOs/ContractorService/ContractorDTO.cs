namespace RoadDefectsService.Core.Application.DTOs.ContractorService
{
    public class ContractorDTO
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string ContractorFullName { get; set; }
        public required string OrganizationName { get; set; }
    }
}
