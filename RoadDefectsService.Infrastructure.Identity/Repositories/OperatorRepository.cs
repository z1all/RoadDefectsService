using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class OperatorRepository : BaseWithBaseEntityRepository<OperatorEntity, AppDbContext>, IOperatorRepository
    {
        public OperatorRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
