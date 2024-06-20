using MediatR;
using RoadDefectsService.Core.Application.DTOs.ContractorService;

namespace RoadDefectsService.Core.Application.CQRS.Contractor.Commands
{
    public record class GetAllContractorsByFiltersCommand(ContractorFilterDTO ContractorFilter) 
        : IRequest<ContractorPagedDTO>;
}
