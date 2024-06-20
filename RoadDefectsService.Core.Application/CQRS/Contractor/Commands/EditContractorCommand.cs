using AutoMapper;
using MediatR;
using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Contractor.Commands
{
    public class EditContractorCommand : IRequest<ExecutionResult>
    {
        public required Guid ContractorId { get; set; }
        public required EditContractorDTO EditContractor { get; set; }

        public class EditContractorCommandHandler
            : IRequestHandler<EditContractorCommand, ExecutionResult>
        {
            private readonly IContractorRepository _contractorRepository;
            private readonly IMapper _mapper;

            public EditContractorCommandHandler(
                IContractorRepository contractorRepository, IMapper mapper)
            {
                _contractorRepository = contractorRepository;
                _mapper = mapper;
            }


            public async Task<ExecutionResult> Handle(EditContractorCommand request, CancellationToken cancellationToken)
            {
                ContractorEntity? contractor = await _contractorRepository.GetByIdAsync(request.ContractorId);
                if (contractor is null)
                {
                    return new(StatusCodeExecutionResult.NotFound, "ContractorNotFound", $"Contractor with id {request.ContractorId} not found!");
                }

                if (contractor.Email != request.EditContractor.Email)
                {
                    bool emailAlreadyUse = await _contractorRepository.AnyByEmailAsync(request.EditContractor.Email);
                    if (emailAlreadyUse)
                    {
                        return new(StatusCodeExecutionResult.BadRequest, "EmailAlreadyUse", $"Email {request.EditContractor.Email} is already in use!");
                    }
                }

                contractor.Email = request.EditContractor.Email;
                contractor.OrganizationName = request.EditContractor.OrganizationName;
                contractor.ContractorFullName = request.EditContractor.ContractorFullName;

                await _contractorRepository.UpdateAsync(contractor);

                return ExecutionResult.SucceededResult;
            }
        }
    }
}
