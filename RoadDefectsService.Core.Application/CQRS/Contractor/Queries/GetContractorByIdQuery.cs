using AutoMapper;
using MediatR;
using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Contractor.Queries
{
    public class GetContractorByIdQuery : IRequest<ExecutionResult<ContractorDTO>>
    {
        public required Guid ContractorId { get; set; }

        public class GetContractorByIdQueryHandler : IRequestHandler<GetContractorByIdQuery, ExecutionResult<ContractorDTO>>
        {
            private readonly IContractorRepository _contractorRepository;
            private readonly IMapper _mapper;

            public GetContractorByIdQueryHandler(
                IContractorRepository contractorRepository, IMapper mapper)
            {
                _contractorRepository = contractorRepository;
                _mapper = mapper;
            }

            public async Task<ExecutionResult<ContractorDTO>> Handle(GetContractorByIdQuery request, CancellationToken cancellationToken)
            {
                ContractorEntity? contractor = await _contractorRepository.GetByIdAsync(request.ContractorId);
                if (contractor is null)
                {
                    return new(StatusCodeExecutionResult.NotFound, "ContractorNotFound", $"Contractor with id {request.ContractorId} not found!");
                }

                return _mapper.Map<ContractorDTO>(contractor);
            }
        }
    }
}
