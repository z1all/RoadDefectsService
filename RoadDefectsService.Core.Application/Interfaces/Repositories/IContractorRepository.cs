using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IContractorRepository : IBaseWithBaseEntityRepository<Contractor>
    {
        Task<List<Contractor>> GetAllByFilterAsync(ContractorFilterDTO contractorFilter);
        Task<int> CountByFilterAsync(ContractorFilterDTO contractorFilter);
        Task<bool> AnyByEmailAsync(string email);
    }
}
