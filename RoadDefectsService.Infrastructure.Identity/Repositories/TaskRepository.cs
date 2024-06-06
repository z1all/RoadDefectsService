using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class TaskRepository : BaseWithBaseEntityRepository<TaskEntity, AppDbContext>, ITaskRepository
    {
        public TaskRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
