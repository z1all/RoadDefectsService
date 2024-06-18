using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.Exceptions;
using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories.Base
{
    public class SoftDeleteBaseRepository<TEntity, TDbContext> :
        ISoftDeleteBaseRepository<TEntity>
        where TEntity : SoftDeleteBaseEntity
        where TDbContext : DbContext
    {
        protected readonly TDbContext _dbContext;

        public SoftDeleteBaseRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual Task<bool> AnyByIdAsync(Guid id, bool showDeleted = false)
        {
            return _dbContext.Set<TEntity>()
              .AnyAsync(entity => entity.Id == id && (showDeleted ? true : !entity.IsDeleted));
        }

        public virtual Task<TEntity?> GetByIdAsync(Guid id, bool showDeleted = false)
        {
            return _dbContext.Set<TEntity>()
              .FirstOrDefaultAsync(entity => entity.Id == id && (showDeleted ? true : !entity.IsDeleted));
        }

        public virtual async Task AddAsync(TEntity entity, bool recoverDeletedRecords = true)
        {
            TEntity? existEntity = await FindEntityBy(entity);
            if (existEntity is not null)
            {
                if (!existEntity.IsDeleted) throw new EntityAlreadyExistException($"Entity '{typeof(TEntity).Name}' with id {existEntity.Id} already exist");

                existEntity.IsDeleted = false;
                _dbContext.Entry(existEntity).State = EntityState.Modified;
            }
            else
            {
                await _dbContext.Set<TEntity>().AddAsync(entity);
            }
            await _dbContext.SaveChangesAsync();
        }

        public virtual Task<TEntity?> FindEntityBy(TEntity entity)
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == entity.Id);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            bool existEntity = await AnyByIdAsync(entity.Id, false);
            if (!existEntity)
            {
                throw new EntityNotFoundException($"Entity '{typeof(TEntity).Name}' with id {entity.Id} not found!");
            }

            entity.IsDeleted = true;
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity, bool updateDeletedRecords = true)
        {
            bool existEntity = await AnyByIdAsync(entity.Id, false);
            if (!existEntity)
            {
                throw new EntityNotFoundException($"Entity '{typeof(TEntity).Name}' with id {entity.Id} not found!");
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
