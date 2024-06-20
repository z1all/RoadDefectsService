using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class FixationWorkRepository : BaseWithBaseEntityRepository<FixationWorkEntity, AppDbContext>, IFixationWorkRepository
    {
        public FixationWorkRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<FixationWorkEntity?> GetByIdWithPhotosAndTaskWithPrevTaskAsync(Guid id)
        {
            return _dbContext.FixationWorks
                .Include(fixation => fixation.Photos)
                .Include(fixation => fixation.TaskFixationWork)
                    .ThenInclude(task => task!.PrevTask)
                .FirstOrDefaultAsync(fixation => fixation.Id == id);
        }

        public Task<FixationWorkEntity?> GetByIdWithTaskAndPhotosAsync(Guid id)
        {
            return _dbContext.FixationWorks
                .Include(fixation => fixation.TaskFixationWork)
                .Include(fixation => fixation.Photos)
                .FirstOrDefaultAsync(fixation => fixation.Id == id);
        }

        public Task<FixationWorkEntity?> GetByIdWithTaskAsync(Guid id)
        {
            return _dbContext.FixationWorks
                .Include(fixation => fixation.TaskFixationWork)
                .FirstOrDefaultAsync(fixation => fixation.Id == id);
        }

        public Task<FixationWorkEntity?> GetByIdWithTaskWithPrevTaskWithFixationDefectAsync(Guid id)
        {
            return _dbContext.FixationWorks
                .Include(fixation => fixation.TaskFixationWork)
                    .ThenInclude(task => task!.PrevTask)
                        .ThenInclude(prevTask => prevTask!.FixationDefect)
                .FirstOrDefaultAsync(fixation => fixation.Id == id);
        }
    }
}
