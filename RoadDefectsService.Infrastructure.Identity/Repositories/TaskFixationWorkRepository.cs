using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class TaskFixationWorkRepository : BaseWithBaseEntityRepository<TaskFixationWorkEntity, AppDbContext>, ITaskFixationWorkRepository
    {
        public TaskFixationWorkRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<TaskFixationWorkEntity?> GetByIdWithInspectorAndPrevTaskAndNextTaskAndFixationsWithPhotosAndDefectTypeAsync(Guid id)
        {
            return _dbContext.FixationWorkTasks
                .Include(task => task.RoadInspector)
                    .ThenInclude(inspector => inspector!.User)
                .Include(task => task.PrevTask)
                .Include(task => task.NextTask)
                .Include(task => task.FixationDefect)
                    .ThenInclude(fixationDefect => fixationDefect!.Photos)
                .Include(task => task.FixationDefect)
                    .ThenInclude(fixationDefect => fixationDefect!.DefectType)
                .Include(task => task.FixationWork)
                    .ThenInclude(fixationWork => fixationWork!.Photos)
                .FirstOrDefaultAsync(task => task.Id == id);
        }

        public Task<bool> AnyWithPrevTaskId(Guid prevTaskId)
        {
            return _dbContext.FixationWorkTasks
                .AnyAsync(task => task.PrevTaskId == prevTaskId);
        }

        public Task<TaskFixationWorkEntity?> GetByIdWithPrevTaskWithFixationDefectAsync(Guid id)
        {
            return _dbContext.FixationWorkTasks
                .Include(task => task.PrevTask)
                    .ThenInclude(prevTask => prevTask!.FixationDefect)              
                .FirstOrDefaultAsync(task => task.Id == id);
        }
    }
}
