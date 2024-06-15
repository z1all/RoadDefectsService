using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories.Base
{
    public class BaseWithBaseEntityRepository<TEntity, TDbContext> :
        BaseRepository<TEntity, TDbContext>, IBaseWithBaseEntityRepository<TEntity>
        where TEntity : BaseEntity
        where TDbContext : DbContext
    {
        public BaseWithBaseEntityRepository(TDbContext dbContext) : base(dbContext) { }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<TEntity>()
               .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual async Task<bool> AnyByIdAsync(Guid id)
        {
            return await _dbContext.Set<TEntity>()
               .AnyAsync(entity => entity.Id == id);
        }
    }
}
