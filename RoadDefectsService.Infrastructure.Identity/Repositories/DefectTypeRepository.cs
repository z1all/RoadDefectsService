﻿using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.DTOs.DefectTypeService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class DefectTypeRepository : BaseWithBaseEntityRepository<DefectType, AppDbContext>, IDefectTypeRepository
    {
        public DefectTypeRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<List<DefectType>> GetAllByFilterAsync(DefectTypeFilterDTO defectTypeFilter)
        {
            var defectTypes = _dbContext.DefectTypes.AsQueryable();

            if (defectTypeFilter.DefectTypeName is not null)
            {
                defectTypes = defectTypes.Where(defectType => defectType.Name.ToLower().Contains(defectTypeFilter.DefectTypeName.ToLower()));
            }

            return defectTypes
                .Take(20)
                .ToListAsync();
        }
    }
}
