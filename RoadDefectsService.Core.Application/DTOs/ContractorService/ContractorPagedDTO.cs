using RoadDefectsService.Core.Application.DTOs.Common;

namespace RoadDefectsService.Core.Application.DTOs.ContractorService
{
    public class ContractorPagedDTO : BasePagedDTO<ContractorDTO>
    {
        public List<ContractorDTO> Contractors { get => Models; set => Models = value; }
    }
}
