using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories.Base
{
    public interface IBaseWithBaseEntityRepository<TEntity> :
        IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<bool> AnyByIdAsync(Guid id);
    }
}
