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
    public class FixationWorkCreator : DbModelCreator<CreateWorkDTO, CreateFixationWorkDTO>
    {
        private readonly ITaskFixationWorkRepository _taskFixationWorkRepository;
        private readonly IFixationWorkRepository _fixationWorkRepository;

        public FixationWorkCreator(
            ITaskFixationWorkRepository taskFixationWorkRepository,
            IFixationWorkRepository fixationWorkRepository,
            ILogger<DbModelCreator<CreateWorkDTO, CreateFixationWorkDTO>> logger, 
            IOptions<DbSeedOptions> options) : base(logger, options)
        {
            _taskFixationWorkRepository = taskFixationWorkRepository;
            _fixationWorkRepository = fixationWorkRepository;
        }

        protected override bool CheckExistModel(CreateWorkDTO model)
            => _fixationWorkRepository.AnyByIdAsync(model.Id).Result;

        protected override void CreateModel(CreateWorkDTO model)
        {
            TaskFixationWorkEntity? task = _taskFixationWorkRepository.GetByIdAsync(model.TaskFixationWorkId).Result;
            if (task is null) return;

            FixationWorkEntity fixationWork = new()
            {
                Id = model.Id,
                WorkDone = model.WorkDone,
                RecordedDateTime = DateTime.UtcNow,
            };
            _fixationWorkRepository.AddAsync(fixationWork).Wait();

            task.FixationWork = fixationWork;
            _taskFixationWorkRepository.UpdateAsync(task).Wait();
        }

        protected override void UpdateModel(CreateWorkDTO model)
        {
            FixationWorkEntity? fixationWork = _fixationWorkRepository.GetByIdAsync(model.Id).Result;
            if (fixationWork is null) return;

            fixationWork.WorkDone = model.WorkDone;

            _fixationWorkRepository.UpdateAsync(fixationWork);
        }
    }
}
