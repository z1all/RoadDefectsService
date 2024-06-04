using RoadDefectsService.Core.Application.DTOs.ContractorService;
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
            if (contractorFilter.Page < 1)
            {
                return new(StatusCodeExecutionResult.BadRequest, keyError: "InvalidPageError", error: "Number of page can't be less than 1.");
            }

            int countContractors = await _contractorRepository.CountContractorsByFilterAsync(contractorFilter);
            int countPage = countContractors == 0 ? 1 : (countContractors + contractorFilter.Size - 1) / contractorFilter.Size;
            if (contractorFilter.Page > countPage)
            {
                return new(StatusCodeExecutionResult.BadRequest, keyError: "InvalidPageError", error: $"Number of page can be from 1 to {countPage}.");
            }

            List<Contractor> contractors = await _contractorRepository.GetContractorsByFilterAsync(contractorFilter);
            return new ContractorPagedDTO()
            {
                Contractors = contractors.ToContractorDTOList(),
                Pagination = new()
                {
                    Count = countPage,
                    Current = contractorFilter.Page,
                    Size = contractorFilter.Size,
                },
            };
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
    }
}
