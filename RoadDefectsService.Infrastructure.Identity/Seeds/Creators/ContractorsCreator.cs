using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed;
using RoadDefectsService.Infrastructure.Identity.Seeds.Creators.Base;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Creators
{
    public class ContractorsCreator : DbModelCreator<CreateContractorDTO, CreateContractorsDTO>
    {
        private readonly IContractorService _contractorService;
        private readonly IContractorRepository _contractorRepository;

        public ContractorsCreator(
            IContractorService contractorService, IContractorRepository contractorRepository,
            ILogger<DbModelCreator<CreateContractorDTO, CreateContractorsDTO>> logger, IOptions<DbSeedOptions> options) 
            : base(logger, options)
        {
            _contractorService = contractorService;
            _contractorRepository = contractorRepository;
        }

        protected override bool CheckExistModel(CreateContractorDTO model)
            => _contractorRepository.AnyByEmailAsync(model.Email).Result;

        protected override void CreateModel(CreateContractorDTO model)
            => _contractorService.CreateContractorAsync(model).Wait();
    }
}
