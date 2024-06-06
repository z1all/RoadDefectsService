using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Mappers;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class ContractorService : IContractorService
    {
        private readonly IContractorRepository _contractorRepository;

        public ContractorService(IContractorRepository contractorRepository)
        {
            _contractorRepository = contractorRepository;
        }

        public async Task<ExecutionResult<ContractorPagedDTO>> GetContractorsAsync(ContractorFilterDTO contractorFilter)
        {
            return await FiltrationHelper
                .FilterAsync<ContractorFilterDTO, Contractor, ContractorDTO, ContractorPagedDTO>(
                contractorFilter, _contractorRepository, (contractors) => contractors.ToContractorDTOList());
        }

        public async Task<ExecutionResult<ContractorDTO>> GetContractorAsync(Guid contractorId)
        {
            Contractor? contractor = await _contractorRepository.GetByIdAsync(contractorId);
            if (contractor is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "ContractorNotFound", $"Contractor with id {contractorId} not found!");
            }

            return contractor.ToContractorDTO();
        }

        public async Task<ExecutionResult> CreateContractorAsync(CreateContractorDTO createContractor)
        {
            bool emailAlreadyUse = await _contractorRepository.AnyByEmailAsync(createContractor.Email);
            if (emailAlreadyUse)
            {
                return new(StatusCodeExecutionResult.BadRequest, "EmailAlreadyUse", $"Email {createContractor.Email} is already in use!");
            }

            Contractor newContractor = createContractor.ToContractor();

            await _contractorRepository.AddAsync(newContractor);

            return new(isSuccess: true);
        }

        public async Task<ExecutionResult> EditContractorAsync(EditContractorDTO editContractor, Guid contractorId)
        {
            Contractor? contractor = await _contractorRepository.GetByIdAsync(contractorId);
            if (contractor is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "ContractorNotFound", $"Contractor with id {contractorId} not found!");
            }

            if (contractor.Email != editContractor.Email)
            {
                bool emailAlreadyUse = await _contractorRepository.AnyByEmailAsync(editContractor.Email);
                if (emailAlreadyUse)
                {
                    return new(StatusCodeExecutionResult.BadRequest, "EmailAlreadyUse", $"Email {editContractor.Email} is already in use!");
                }
            }

            contractor.Email = editContractor.Email;
            contractor.OrganizationName = editContractor.OrganizationName;
            contractor.ContractorFullName = editContractor.ContractorFullName;

            await _contractorRepository.UpdateAsync(contractor);

            return new(isSuccess: true);
        }

        public async Task<ExecutionResult> DeleteContractorAsync(Guid contractorId)
        {
            Contractor? contractor = await _contractorRepository.GetByIdAsync(contractorId);
            if (contractor is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "ContractorNotFound", $"Contractor with id {contractorId} not found!");
            }

            await _contractorRepository.DeleteAsync(contractor);

            return new(isSuccess: true);
        }
    }
}
