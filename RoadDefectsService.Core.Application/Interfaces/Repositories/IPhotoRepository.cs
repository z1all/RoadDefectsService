using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IPhotoRepository : IBaseWithBaseEntityRepository<Photo>
    {
        Task<Photo?> GetByIdAndFixationIdAsync(Guid id, Guid fixationId);
    }
}
