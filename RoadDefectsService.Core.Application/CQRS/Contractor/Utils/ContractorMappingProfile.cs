using AutoMapper;
using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Contractor.Utils
{
    public class ContractorMappingProfile : Profile
    {
        public ContractorMappingProfile()
        {
            CreateMap<ContractorEntity, ContractorDTO>();
            CreateMap<CreateContractorDTO, ContractorEntity>();
        }
    }
}
