using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed;
using RoadDefectsService.Infrastructure.Identity.Seeds.Creators.Base;
using RoadDefectsService.Infrastructure.Identity.Seeds.DTOs;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Creators
{
    public class FixationDefectCreator : DbModelCreator<CreateDefectDTO, CreateFixationDefectDTO>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IFixationDefectRepository _fixationDefectRepository;

        public FixationDefectCreator(
            ITaskRepository taskRepository,
            IFixationDefectRepository fixationDefectRepository,
            ILogger<DbModelCreator<CreateDefectDTO, CreateFixationDefectDTO>> logger,
            IOptions<DbSeedOptions> options) 
            : base(logger, options)
        {
            _taskRepository = taskRepository;
            _fixationDefectRepository = fixationDefectRepository;
        }

        protected override bool CheckExistModel(CreateDefectDTO model)
            => _fixationDefectRepository.AnyByIdAsync(model.Id).Result;

        protected override void CreateModel(CreateDefectDTO model)
        {
            TaskEntity? task = _taskRepository.GetByIdAsync(model.TaskId).Result;
            if (task is null) return;

            FixationDefectEntity fixationDefect = new()
            {
                Id = model.Id,
                RecordedDateTime = DateTime.UtcNow,
                CacheAddress = task.Address,
                DamagedCanvasSquareMeter = model.DamagedCanvasSquareMeter,
                DefectTypeId = model.DefectTypeId,
            };
            _fixationDefectRepository.AddAsync(fixationDefect).Wait();

            task.FixationDefect = fixationDefect;
            _taskRepository.UpdateAsync(task).Wait();
        }

        protected override void UpdateModel(CreateDefectDTO model)
        {
            FixationDefectEntity? fixationDefect = _fixationDefectRepository.GetByIdAsync(model.Id).Result;
            if (fixationDefect is null) return;

            fixationDefect.DefectTypeId = model.DefectTypeId;
            fixationDefect.DamagedCanvasSquareMeter = model.DamagedCanvasSquareMeter;

            _fixationDefectRepository.UpdateAsync(fixationDefect).Wait();
        }
    }
}
