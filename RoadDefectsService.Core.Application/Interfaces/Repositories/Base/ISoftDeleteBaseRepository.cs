using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories.Base
{
    public interface ISoftDeleteBaseRepository<TEntity>
         where TEntity : ISoftDeleteBaseEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id, bool showDeleted = false);
        Task<bool> AnyByIdAsync(Guid id, bool showDeleted = false);
        Task AddAsync(TEntity entity, bool recoverDeletedRecords = true);
        Task UpdateAsync(TEntity entity, bool updateDeletedRecords = true);
        Task DeleteAsync(TEntity entity);
        Task SaveChangesAsync();
    }
}
