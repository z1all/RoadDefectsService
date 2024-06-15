using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public class ContractorMappingProfile : Profile
    {
        public ContractorMappingProfile() 
        {
            CreateMap<Contractor, ContractorDTO>();
            CreateMap<CreateContractorDTO, Contractor>();
        }
    }
}
