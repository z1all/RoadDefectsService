using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class DefectTypeResponse : BaseWithBaseEntityRepository<DefectType, AppDbContext>, IDefectTypeResponse
    {
        public DefectTypeResponse(AppDbContext dbContext) : base(dbContext) { }
    }
}
