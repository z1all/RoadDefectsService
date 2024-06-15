using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed;
using RoadDefectsService.Infrastructure.Identity.Seeds.Creators.Base;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Creators
{
    public class DefectTypesCreator : DbModelCreator<DefectType, CreateDefectTypesDTO>
    {
        private readonly IDefectTypeRepository _defectTypeResponse;

        public DefectTypesCreator(
            IDefectTypeRepository defectTypeResponse,
            ILogger<DbModelCreator<DefectType, CreateDefectTypesDTO>> logger, IOptions<DbSeedOptions> options) 
            : base(logger, options)
        {
            _defectTypeResponse = defectTypeResponse;
        }

        protected override bool CheckExistModel(DefectType model)
            => _defectTypeResponse.AnyByIdAsync(model.Id).Result;

        protected override void CreateModel(DefectType model)
            => _defectTypeResponse.AddAsync(model).Wait();
    }
}
