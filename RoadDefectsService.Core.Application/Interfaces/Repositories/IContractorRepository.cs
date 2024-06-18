using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IContractorRepository : 
        ISoftDeleteBaseRepository<Contractor>, 
        IFilterableRepository<ContractorFilterDTO, Contractor>
    {
        Task<bool> AnyByEmailAsync(string email);
    }
}
