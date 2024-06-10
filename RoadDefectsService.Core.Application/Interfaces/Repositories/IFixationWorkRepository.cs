using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IFixationWorkRepository : IBaseWithBaseEntityRepository<FixationWork>
    {
        Task<FixationWork?> GetByIdWithTaskAsync(Guid id);
    }
}
