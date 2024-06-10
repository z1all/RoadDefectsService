using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IFixationDefectRepository : IBaseWithBaseEntityRepository<FixationDefect>
    {
        Task<FixationDefect?> GetByIdWithTaskAsync(Guid id);
    }
}
