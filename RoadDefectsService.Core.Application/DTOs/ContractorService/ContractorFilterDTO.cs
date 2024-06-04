namespace RoadDefectsService.Core.Application.DTOs.ContractorService
{
    public class ContractorFilterDTO
    {
        public string? ContractorFullName { get; set; }
        public string? OrganizationName { get; set; }

        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
