using AutoMapper;
using MediatR;
using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Contractor.Queries
{
    public class GetAllContractorsByFiltersQuery
        : IRequest<ExecutionResult<ContractorPagedDTO>>
    {
        public required ContractorFilterDTO ContractorFilter { get; set; }

        public class GetAllContractorsByFiltersQueryHandler
            : IRequestHandler<GetAllContractorsByFiltersQuery, ExecutionResult<ContractorPagedDTO>>
        {
            private readonly IContractorRepository _contractorRepository;
            private readonly IMapper _mapper;

            public GetAllContractorsByFiltersQueryHandler(
                IContractorRepository contractorRepository, IMapper mapper)
            {
                _contractorRepository = contractorRepository;
                _mapper = mapper;
            }

            public Task<ExecutionResult<ContractorPagedDTO>> Handle(GetAllContractorsByFiltersQuery request, CancellationToken cancellationToken)
            {
                return FiltrationHelper
                    .FilterAsync<ContractorFilterDTO, ContractorEntity, ContractorDTO, ContractorPagedDTO>(
                    request.ContractorFilter, _contractorRepository, (contractors) => _mapper.Map<List<ContractorDTO>>(contractors));
            }
        }
    }
}
