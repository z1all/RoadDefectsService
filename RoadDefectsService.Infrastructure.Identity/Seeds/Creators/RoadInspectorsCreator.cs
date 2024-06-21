using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed;
using RoadDefectsService.Infrastructure.Identity.Mappers;
using RoadDefectsService.Infrastructure.Identity.Seeds.Creators.Base;
using RoadDefectsService.Infrastructure.Identity.Seeds.DTOs;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Creators
{
    public class RoadInspectorsCreator : DbModelCreator<CreateInspectorDTO, CreateInspectorsDTO>
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly IRoadInspectorRepository _roadInspectorRepository;

        public RoadInspectorsCreator(
            UserManager<CustomUser> userManager,
            IRoadInspectorRepository roadInspectorRepository,
            ILogger<DbModelCreator<CreateInspectorDTO, CreateInspectorsDTO>> logger, 
            IOptions<DbSeedOptions> options) 
            : base(logger, options)
        {
            _userManager = userManager;
            _roadInspectorRepository = roadInspectorRepository;
        }

        protected override bool CheckExistModel(CreateInspectorDTO model)
        {
            CustomUser? customUser = _userManager.FindByIdAsync(model.Id.ToString()).Result;
            return customUser is not null;
        }

        protected override void CreateModel(CreateInspectorDTO model)
        {
            CustomUser newUser = new()
            {
                Id = model.Id,
                HighestRole = Role.RoadInspector,
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email
            };

            IdentityResult creatingResult = _userManager.CreateAsync(newUser, model.Password).Result;
            if (!creatingResult.Succeeded) return;

            IdentityResult addingRoleResult = _userManager.AddToRolesAsync(newUser, [Role.RoadInspector]).Result;
            if (!addingRoleResult.Succeeded) return;

            RoadInspectorEntity roadInspector = new()
            {
                Id = newUser.Id,
                User = newUser,
            };
            _roadInspectorRepository.AddAsync(roadInspector).Wait();
        }

        protected override void UpdateModel(CreateInspectorDTO model)
        {
            CustomUser? customUser = _userManager.FindByIdAsync(model.Id.ToString()).Result;
            if (customUser is null) return;

            customUser.FullName = model.FullName;

            _userManager.UpdateAsync(customUser).Wait();
        }
    }
}
