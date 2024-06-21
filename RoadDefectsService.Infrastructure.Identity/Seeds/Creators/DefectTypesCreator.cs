using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.CQRS.DefectType.Commands;
using RoadDefectsService.Core.Application.CQRS.DefectType.DTOs;
using RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed;
using RoadDefectsService.Infrastructure.Identity.Seeds.Creators.Base;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Creators
{
    public class DefectTypesCreator : DbModelCreator<CreateDefectTypeEntityDTO, CreateDefectTypesDTO>
    {
        private readonly IMediator _mediator;

        public DefectTypesCreator(
            IMediator mediator,
            ILogger<DbModelCreator<CreateDefectTypeEntityDTO, CreateDefectTypesDTO>> logger, IOptions<DbSeedOptions> options) 
            : base(logger, options)
        {
            _mediator = mediator;
        }

        protected override bool CheckExistModel(CreateDefectTypeEntityDTO model) => false;

        protected override void CreateModel(CreateDefectTypeEntityDTO model)
            => _mediator.Send(new CreateDefectTypeEntityCommand() { CreateDefectTypeEntity = model}).Wait();
    }
}
