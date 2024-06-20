using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;
using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IContractorRepository : 
        ISoftDeleteBaseRepository<ContractorEntity>, 
        IFilterableRepository<ContractorFilterDTO, ContractorEntity>
    {
        Task<bool> AnyByEmailAsync(string email);
    }
}
