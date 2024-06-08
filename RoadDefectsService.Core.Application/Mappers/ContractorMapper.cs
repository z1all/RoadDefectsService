using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public static class ContractorMapper
    {
        public static ContractorDTO ToContractorDTO(this Contractor contractor)
        {
            return new()
            {
                Id = contractor.Id,
                Email = contractor.Email,
                ContractorFullName = contractor.ContractorFullName,
                OrganizationName = contractor.OrganizationName
            };
        }

        public static List<ContractorDTO> ToContractorDTOList(this List<Contractor> contractors)
        {
            return contractors.Select(ToContractorDTO).ToList();
        }

        public static Contractor ToContractor(this CreateContractorDTO contractor)
        {
            return new()
            {
                Email = contractor.Email,
                ContractorFullName = contractor.ContractorFullName,
                OrganizationName = contractor.OrganizationName
            };
        }
    }
}
