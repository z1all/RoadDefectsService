using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.CQRS.Contractor.Commands;
using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed;
using RoadDefectsService.Infrastructure.Identity.Seeds.Creators.Base;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Creators
{
    public class ContractorsCreator : DbModelCreator<CreateContractorDTO, CreateContractorsDTO>
    {
        private readonly IMediator _mediator;
        private readonly IContractorRepository _contractorRepository;

        public ContractorsCreator(
            IMediator mediator, IContractorRepository contractorRepository,
            ILogger<DbModelCreator<CreateContractorDTO, CreateContractorsDTO>> logger, IOptions<DbSeedOptions> options)
            : base(logger, options)
        {
            _mediator = mediator;
            _contractorRepository = contractorRepository;
        }

        protected override bool CheckExistModel(CreateContractorDTO model)
            => _contractorRepository.AnyByEmailAsync(model.Email).Result;

        protected override void CreateModel(CreateContractorDTO model)
            => _mediator.Send(new CreateContractorCommand() { CreateContractor = model });
    }
}
