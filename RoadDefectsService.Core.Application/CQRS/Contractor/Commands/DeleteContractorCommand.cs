using AutoMapper;
using MediatR;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Contractor.Commands
{
    public class DeleteContractorCommand : IRequest<ExecutionResult>
    {
        public required Guid ContractorId { get; set; }

        public class DeleteContractorCommandHandler : IRequestHandler<DeleteContractorCommand, ExecutionResult>
        {
            private readonly IContractorRepository _contractorRepository;
            private readonly IMapper _mapper;

            public DeleteContractorCommandHandler(
                IContractorRepository contractorRepository, IMapper mapper)
            {
                _contractorRepository = contractorRepository;
                _mapper = mapper;
            }

            public async Task<ExecutionResult> Handle(DeleteContractorCommand request, CancellationToken cancellationToken)
            {
                ContractorEntity? contractor = await _contractorRepository.GetByIdAsync(request.ContractorId);
                if (contractor is null)
                {
                    return new(StatusCodeExecutionResult.NotFound, "ContractorNotFound", $"Contractor with id {request.ContractorId} not found!");
                }

                await _contractorRepository.DeleteAsync(contractor);

                return ExecutionResult.SucceededResult;
            }
        }
    }
}
