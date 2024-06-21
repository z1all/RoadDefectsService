using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed;
using RoadDefectsService.Infrastructure.Identity.Seeds.Creators.Base;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Creators
{
    public class TaskFixationWorkCreator : DbModelCreator<TaskFixationWorkEntity, CreateTasksFixationWorkDTO>
    {
        private readonly ITaskFixationWorkRepository _taskFixationWorkRepository;

        public TaskFixationWorkCreator(
            ITaskFixationWorkRepository taskFixationWorkRepository,
            ILogger<DbModelCreator<TaskFixationWorkEntity, CreateTasksFixationWorkDTO>> logger,
            IOptions<DbSeedOptions> options)
            : base(logger, options)
        {
            _taskFixationWorkRepository = taskFixationWorkRepository;
        }

        protected override bool CheckExistModel(TaskFixationWorkEntity model)
            => _taskFixationWorkRepository.AnyByIdAsync(model.Id).Result;

        protected override void CreateModel(TaskFixationWorkEntity model)
            => _taskFixationWorkRepository.AddAsync(model).Wait();

        protected override void UpdateModel(TaskFixationWorkEntity model)
        {
            TaskFixationWorkEntity? task = _taskFixationWorkRepository.GetByIdAsync(model.Id).Result;
            if (task is null) return;

            task.RoadInspectorId = model.RoadInspectorId;
            task.Address = model.Address;
            task.CoordinateX = model.CoordinateX;
            task.CoordinateY = model.CoordinateY;
            task.CreatedDateTime = model.CreatedDateTime;
            task.TaskStatus = model.TaskStatus;
            task.PrevTaskId = model.PrevTaskId;
            task.IsTransfer = model.IsTransfer;

            _taskFixationWorkRepository.UpdateAsync(task).Wait();
        }
    }
}
