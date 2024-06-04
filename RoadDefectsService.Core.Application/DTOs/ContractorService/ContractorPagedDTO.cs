namespace RoadDefectsService.Core.Application.DTOs.ContractorService
{
    public class ContractorPagedDTO
    {
        public required List<ContractorDTO> Contractors { get; set; }
        public required PageInfoDTO Pagination { get; set; }
    }
}
