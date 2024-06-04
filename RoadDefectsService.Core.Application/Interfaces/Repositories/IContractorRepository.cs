using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IContractorRepository : IBaseWithBaseEntityRepository<Contractor>
    {
        Task<List<Contractor>> GetContractorsByFilterAsync(ContractorFilterDTO contractorFilter);
        Task<int> CountContractorsByFilterAsync(ContractorFilterDTO contractorFilter);
        Task<bool> AnyByEmailAsync(string email);
    }
}
