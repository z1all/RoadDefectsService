using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface ITaskRepository : 
        IBaseWithBaseEntityRepository<TaskEntity>,
        IFilterableRepository<CommonTaskFilterDTO, TaskEntity>
        //IFilterableRepository<TaskFilterDTO, TaskEntity>
    {
        Task<List<TaskEntity>> GetAllByFilterAsync(TaskFilterDTO taskFilter, Guid inspectorId);
        Task<int> CountByFilterAsync(TaskFilterDTO taskFilter, Guid inspectorId);
    }
}
