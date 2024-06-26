﻿using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class RoadInspectorRepository : BaseWithBaseEntityRepository<RoadInspector, AppDbContext>, IRoadInspectorRepository
    {
        public RoadInspectorRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<bool> AnyNotDeletedByIdAsync(Guid id)
        {
            return _dbContext.RoadInspectors
                .AnyAsync(roadInspector => roadInspector.Id == id && !roadInspector.User.IsDeleted);
        }
    }
}
