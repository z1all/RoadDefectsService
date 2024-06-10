using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class FixationDefectRepository : BaseWithBaseEntityRepository<FixationDefect, AppDbContext>, IFixationDefectRepository
    {
        public FixationDefectRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<FixationDefect?> GetByIdWithTaskAsync(Guid id)
        {
            return _dbContext.FixationDefects
                .Include(fixation => fixation.Task)
                .FirstOrDefaultAsync(fixation => fixation.Id == id);
        }
    }
}
