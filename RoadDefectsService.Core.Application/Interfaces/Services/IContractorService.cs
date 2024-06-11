using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IContractorService
    {
        Task<ExecutionResult<ContractorPagedDTO>> GetContractorsAsync(ContractorFilterDTO contractorFilter);
        Task<ExecutionResult<ContractorDTO>> GetContractorAsync(Guid contractorId);
        Task<ExecutionResult> CreateContractorAsync(CreateContractorDTO createContractor);
        Task<ExecutionResult> EditContractorAsync(EditContractorDTO editContractor, Guid contractorId);
        Task<ExecutionResult> DeleteContractorAsync(Guid contractorId);
    }
}
