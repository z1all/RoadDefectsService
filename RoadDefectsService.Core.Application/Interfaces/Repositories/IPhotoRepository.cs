using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IPhotoRepository : IBaseWithBaseEntityRepository<PhotoEntity>
    {
        Task<PhotoEntity?> GetByIdAndFixationIdAsync(Guid id, Guid fixationId);
    }
}
