using AutoMapper;
using MediatR;
using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Contractor.Commands
{
    public class CreateContractorCommand : IRequest<ExecutionResult>
    {
        public required CreateContractorDTO CreateContractor { get; set; }

        public class CreateContractorCommandHandler : IRequestHandler<CreateContractorCommand, ExecutionResult>
        {
            private readonly IContractorRepository _contractorRepository;
            private readonly IMapper _mapper;

            public CreateContractorCommandHandler(
                IContractorRepository contractorRepository, IMapper mapper)
            {
                _contractorRepository = contractorRepository;
                _mapper = mapper;
            }

            public async Task<ExecutionResult> Handle(CreateContractorCommand request, CancellationToken cancellationToken)
            {
                bool emailAlreadyUse = await _contractorRepository.AnyByEmailAsync(request.CreateContractor.Email);
                if (emailAlreadyUse)
                {
                    return new(StatusCodeExecutionResult.BadRequest, "EmailAlreadyUse", $"Email {request.CreateContractor.Email} is already in use!");
                }

                ContractorEntity newContractor = _mapper.Map<ContractorEntity>(request.CreateContractor);

                await _contractorRepository.AddAsync(newContractor);

                return ExecutionResult.SucceededResult;
            }
        }
    }
}
