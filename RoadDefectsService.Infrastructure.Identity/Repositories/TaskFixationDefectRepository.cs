using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class TaskFixationDefectRepository : BaseWithBaseEntityRepository<TaskFixationDefect, AppDbContext>, ITaskFixationDefectRepository
    {
        public TaskFixationDefectRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<TaskFixationDefect?> GetByIdWithInspectorAndDefectAsync(Guid id)
        {
            return await _dbContext.FixationDefectTasks
                .Include(task => task.RoadInspector)
                    .ThenInclude(inspector => inspector!.User)
                //.Include(task => task.Defect)
                .FirstOrDefaultAsync(task => task.Id == id);
        }
    }
}
