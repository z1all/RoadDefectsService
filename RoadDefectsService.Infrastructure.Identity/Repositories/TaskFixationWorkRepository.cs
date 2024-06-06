using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class TaskFixationWorkRepository : BaseWithBaseEntityRepository<TaskFixationWork, AppDbContext>, ITaskFixationWorkRepository
    {
        public TaskFixationWorkRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
