using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.DTOs.AssignmentService;
using RoadDefectsService.Core.Application.Enums;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class AssignmentRepository : BaseWithBaseEntityRepository<Assignment, AppDbContext>, IAssignmentRepository
    {
        public AssignmentRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<Assignment?> GetByIdWithContractorAndFixationDefectWithDefectTypeAndPhotosAsync(Guid id)
        {
            return _dbContext.Assignments
                .Include(assignment => assignment.Contractor)
                .Include(assignment => assignment.FixationDefect)
                    .ThenInclude(fixationDefect => fixationDefect!.DefectType)
                .Include(assignment => assignment.FixationDefect)
                    .ThenInclude(fixationDefect => fixationDefect!.Photos)
                .FirstOrDefaultAsync(assignment => assignment.Id == id);
        }

        public Task<List<Assignment>> GetAllByFilterAsync(AssignmentFilterDTO filter)
        {
            return ApplyFilter(filter)
                .Include(assignment => assignment.Contractor)
                .Include(assignment => assignment.FixationDefect)
                    .ThenInclude(fixationDefect => fixationDefect!.DefectType)
                .Skip((filter.Page - 1) * filter.Size)
                .Take(filter.Size)
                .ToListAsync();
        }

        public Task<int> CountByFilterAsync(AssignmentFilterDTO filter)
        {
            return ApplyFilter(filter)
              .CountAsync();
        }

        private IQueryable<Assignment> ApplyFilter(AssignmentFilterDTO filter)
        {
            var assignments = _dbContext.Assignments.AsQueryable();

            if (filter.FixationDefectAddress is not null)
            {
                assignments = assignments.Where(assignment => assignment.FixationDefect!.ExactAddress!.ToLower().Contains(filter.FixationDefectAddress));
            }

            if (filter.ContractorId.HasValue)
            {
                assignments = assignments.Where(assignment => assignment.ContractorId == filter.ContractorId);
            }

            assignments = filter.AssignmentSort switch
            {
                AssignmentSortFilter.CreatedDateTimeAsc => assignments.OrderBy(assignment => assignment.CreatedDateTime),
                AssignmentSortFilter.CreatedDateTimeDesc => assignments.OrderByDescending(assignment => assignment.CreatedDateTime),
                AssignmentSortFilter.DeadlineDateOnlyAsc => assignments.OrderBy(assignment => assignment.DeadlineDateOnly),
                AssignmentSortFilter.DeadlineDateOnlyDesc => assignments.OrderByDescending(assignment => assignment.DeadlineDateOnly),
                _ => assignments
            };

            return assignments;
        }
    }
}
