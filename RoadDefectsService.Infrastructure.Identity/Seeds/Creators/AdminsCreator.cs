using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed;
using RoadDefectsService.Infrastructure.Identity.Seeds.Creators.Base;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Creators
{
    public class AdminsCreator : DbModelCreator<CreateUserDTO, CreateAdminsDTO>
    {
        private readonly IUserService _profileService;
        private readonly UserManager<CustomUser> _userManager;

        public AdminsCreator(
            IUserService profileService, UserManager<CustomUser> userManager,
            ILogger<DbModelCreator<CreateUserDTO, CreateAdminsDTO>> logger, IOptions<DbSeedOptions> options) 
            : base(logger, options)
        {
            _profileService = profileService;
            _userManager = userManager;
        }

        protected override bool CheckExistModel(CreateUserDTO model)
        {
            CustomUser? customUser = _userManager.FindByEmailAsync(model.Email).Result;
            return customUser is not null;
        }

        protected override void CreateModel(CreateUserDTO model)
        {
            _profileService.CreateAdminAsync(model).Wait();
        }
    }
}
