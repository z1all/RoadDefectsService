using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed;
using RoadDefectsService.Infrastructure.Identity.Seeds.Creators.Base;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Creators
{
    public class TaskFixationDefectCreator : DbModelCreator<TaskFixationDefectEntity, CreateTaskFixationDefectDTO>
    {
        private readonly ITaskFixationDefectRepository _taskFixationDefectRepository;

        public TaskFixationDefectCreator(
            ITaskFixationDefectRepository taskFixationDefectRepository,
            ILogger<DbModelCreator<TaskFixationDefectEntity, CreateTaskFixationDefectDTO>> logger, 
            IOptions<DbSeedOptions> options) 
            : base(logger, options)
        {
            _taskFixationDefectRepository = taskFixationDefectRepository;
        }

        protected override bool CheckExistModel(TaskFixationDefectEntity model)
            => _taskFixationDefectRepository.AnyByIdAsync(model.Id).Result;

        protected override void CreateModel(TaskFixationDefectEntity model)
            => _taskFixationDefectRepository.AddAsync(model).Wait();

        protected override void UpdateModel(TaskFixationDefectEntity model)
        {
            TaskFixationDefectEntity? task = _taskFixationDefectRepository.GetByIdAsync(model.Id).Result;
            if (task is null) return;

            task.RoadInspectorId = model.RoadInspectorId;
            task.CreatedDateTime = model.CreatedDateTime;
            task.TaskStatus = model.TaskStatus;
            task.Address = model.Address;
            task.CoordinateX = model.CoordinateX;
            task.CoordinateY = model.CoordinateY;
            task.Description = model.Description;
            task.IsTransfer = model.IsTransfer;

            _taskFixationDefectRepository.UpdateAsync(task).Wait();
        }
    }
}
