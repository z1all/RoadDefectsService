namespace RoadDefectsService.Core.Application.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity>
         where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task SaveChangesAsync();
    }
}
