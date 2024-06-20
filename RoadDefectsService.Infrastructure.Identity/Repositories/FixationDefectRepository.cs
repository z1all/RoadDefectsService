using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class FixationDefectRepository : BaseWithBaseEntityRepository<FixationDefectEntity, AppDbContext>, IFixationDefectRepository
    {
        public FixationDefectRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<FixationDefectEntity?> GetByIdWithTaskAsync(Guid id)
        {
            return _dbContext.FixationDefects
                .Include(fixation => fixation.Task)
                .FirstOrDefaultAsync(fixation => fixation.Id == id);
        }

        public Task<FixationDefectEntity?> GetByIdWithTaskAndPhotosAndDefectTypeAsync(Guid id)
        {
            return _dbContext.FixationDefects
                .Include(fixation => fixation.Task)
                .Include(fixation => fixation.Photos)
                .Include(fixation => fixation.DefectType)
                .FirstOrDefaultAsync(fixation => fixation.Id == id);
        }
    }
}
