﻿using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class ContractorService : IContractorService
    {
        private readonly IContractorRepository _contractorRepository;
        private readonly IMapper _mapper;

        public ContractorService(IContractorRepository contractorRepository, IMapper mapper)
        {
            _contractorRepository = contractorRepository;
            _mapper = mapper;
        }

        public async Task<ExecutionResult<ContractorPagedDTO>> GetContractorsAsync(ContractorFilterDTO contractorFilter)
        {
            return await FiltrationHelper
                .FilterAsync<ContractorFilterDTO, Contractor, ContractorDTO, ContractorPagedDTO>(
                contractorFilter, _contractorRepository, (contractors) => _mapper.Map<List<ContractorDTO>>(contractors));
        }

        public async Task<ExecutionResult<ContractorDTO>> GetContractorAsync(Guid contractorId)
        {
            Contractor? contractor = await _contractorRepository.GetByIdAsync(contractorId);
            if (contractor is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "ContractorNotFound", $"Contractor with id {contractorId} not found!");
            }

            return _mapper.Map<ContractorDTO>(contractor);
        }

        public async Task<ExecutionResult> CreateContractorAsync(CreateContractorDTO createContractor)
        {
            bool emailAlreadyUse = await _contractorRepository.AnyByEmailAsync(createContractor.Email);
            if (emailAlreadyUse)
            {
                return new(StatusCodeExecutionResult.BadRequest, "EmailAlreadyUse", $"Email {createContractor.Email} is already in use!");
            }

            Contractor newContractor = _mapper.Map<Contractor>(createContractor);

            await _contractorRepository.AddAsync(newContractor);

            return ExecutionResult.SucceededResult;
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

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult> DeleteContractorAsync(Guid contractorId)
        {
            Contractor? contractor = await _contractorRepository.GetByIdAsync(contractorId);
            if (contractor is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "ContractorNotFound", $"Contractor with id {contractorId} not found!");
            }

            await _contractorRepository.DeleteAsync(contractor);

            return ExecutionResult.SucceededResult;
        }
    }
}
