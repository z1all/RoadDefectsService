using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IRoadInspectorRepository : IBaseWithBaseEntityRepository<RoadInspector>
    {
        Task<bool> AnyNotDeletedByIdAsync(Guid id);
    }
}
