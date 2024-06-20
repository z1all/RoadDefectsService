using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class PhotoRepository : BaseWithBaseEntityRepository<PhotoEntity, AppDbContext>, IPhotoRepository
    {
        public PhotoRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<PhotoEntity?> GetByIdAndFixationIdAsync(Guid id, Guid fixationId)
        {
            return _dbContext.Photos
                .FirstOrDefaultAsync(photo => photo.Id == id && (photo.FixationWorkId == fixationId || photo.FixationDefectId == fixationId));
        }
    }
}
