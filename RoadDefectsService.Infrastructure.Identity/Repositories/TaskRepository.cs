using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.CQRS.Task.DTOs;
using RoadDefectsService.Core.Application.Enums;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class TaskRepository : BaseWithBaseEntityRepository<TaskEntity, AppDbContext>, ITaskRepository
    {
        public TaskRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<TaskEntity?> GetByIdWithFixationDefectAsync(Guid id)
        {
            return _dbContext.Tasks
                .Include(task => task.FixationDefect)
                .FirstOrDefaultAsync(task => task.Id == id);
        }

        public async Task<int> CountByFilterAsync(TaskFilterDTO taskFilter, Guid inspectorId)
        {
            return await ApplyFilter(taskFilter, inspectorId)
                .CountAsync();
        }

        public async Task<List<TaskEntity>> GetAllByFilterAsync(TaskFilterDTO taskFilter, Guid inspectorId)
        {
            return await ApplyFilter(taskFilter, inspectorId)
                .Skip((taskFilter.Page - 1) * taskFilter.Size)
                .Take(taskFilter.Size)
                .ToListAsync();
        }

        public async Task<int> CountByFilterAsync(CommonTaskFilterDTO taskFilter)
        {
            return await ApplyFilter(taskFilter)
                .CountAsync();
        }

        public async Task<List<TaskEntity>> GetAllByFilterAsync(CommonTaskFilterDTO taskFilter)
        {
            return await ApplyFilter(taskFilter)
                .Skip((taskFilter.Page - 1) * taskFilter.Size)
                .Take(taskFilter.Size)
                .ToListAsync();
        }

        private IQueryable<TaskEntity> ApplyFilter(CommonTaskFilterDTO filter)
        {
            return ApplyFilter(filter, (tasks) => filter.TaskViewMode switch
            {
                TaskViewModeFilter.OnlyAssigned => tasks.Where(task => task.RoadInspectorId != null),
                TaskViewModeFilter.OnlyNotAssigned => tasks.Where(task => task.RoadInspectorId == null),
                _ => tasks
            });
        }

        private IQueryable<TaskEntity> ApplyFilter(TaskFilterDTO filter, Guid inspectorId)
        {
            return ApplyFilter(filter, (tasks) => tasks.Where(task => task.RoadInspectorId == inspectorId));
        }

        private IQueryable<TaskEntity> ApplyFilter(TaskFilterDTO filter, Func<IQueryable<TaskEntity>, IQueryable<TaskEntity>> additionFilter)
        {
            var tasks = _dbContext.Tasks
                .AsQueryable();

            if (filter.TaskType is not null && filter.TaskType != TaskTypeFilter.None)
            {
                tasks = tasks.Where(task => task.TaskType == (TaskType)filter.TaskType);
            }

            if (filter.TaskStatus is not null && filter.TaskStatus != TaskStatusFilter.None)
            {
                tasks = tasks.Where(task => task.TaskStatus == (StatusTask)filter.TaskStatus);
            }

            if (filter.Address is not null && filter.Address is not null)
            {
                tasks = tasks.Where(task => task.Address.ToLower().Contains(filter.Address.ToLower()));
            }

            tasks = filter.DefectStatus switch
            {
                DefectStatusFilter.NotVerified => tasks.Where(task => task.TaskStatus != StatusTask.Completed),
                DefectStatusFilter.ThereIsDefect => tasks.Where(task => task.TaskStatus == StatusTask.Completed && task.FixationDefectId != null),
                DefectStatusFilter.ThereIsNotDefect => tasks.Where(task => task.TaskStatus == StatusTask.Completed && task.FixationDefectId == null),
                _ => tasks
            };

            tasks = additionFilter(tasks);

            tasks = filter.TaskSort switch
            {
                TaskSortType.CreatedDateTimeAsc => tasks.OrderBy(task => task.CreatedDateTime),
                TaskSortType.CreatedDateTimeDesc => tasks.OrderByDescending(task => task.CreatedDateTime),
                _ => tasks.OrderBy(task => task.TaskStatus).ThenBy(task => task.CreatedDateTime)
            };

            return tasks;
        }
    }
}
