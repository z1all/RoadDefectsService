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

        public Task<TaskFixationDefect?> GetByIdWithInspectorAndDefectWithPhotosAndDefectTypeAsync(Guid id)
        {
            return _dbContext.FixationDefectTasks
                .Include(task => task.RoadInspector)
                    .ThenInclude(inspector => inspector!.User)
                .Include(task => task.FixationDefect)
                    .ThenInclude(fixationDefect => fixationDefect!.Photos)
                .Include(task => task.FixationDefect)
                    .ThenInclude(fixationDefect => fixationDefect!.DefectType)
                .FirstOrDefaultAsync(task => task.Id == id);
        }

        public Task<TaskFixationDefect?> GetByIdWithInspectorAndNextTaskAndDefectWithPhotosAndDefectTypeAsync(Guid id)
        {
            return _dbContext.FixationDefectTasks
                .Include(task => task.RoadInspector)
                    .ThenInclude(inspector => inspector!.User)
                .Include(task => task.NextTask)
                .Include(task => task.FixationDefect)
                    .ThenInclude(fixationDefect => fixationDefect!.Photos)
                .Include(task => task.FixationDefect)
                    .ThenInclude(fixationDefect => fixationDefect!.DefectType)
                .FirstOrDefaultAsync(task => task.Id == id);
        }
    }
}
