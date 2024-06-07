using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories.Base
{
    public interface IFilterableRepository<TFilter, TEntity> 
        where TFilter : BaseFilterDTO 
        where TEntity : IBaseEntity
    {
        Task<List<TEntity>> GetAllByFilterAsync(TFilter filter);
        Task<int> CountByFilterAsync(TFilter filter);
    }
}
