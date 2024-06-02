namespace RoadDefectsService.Core.Application.DTOs
{
    public class ContractorDTO
    {
        public required Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
    }
}
