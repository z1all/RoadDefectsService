using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Services;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Configurations;
using RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories;
using RoadDefectsService.Infrastructure.Identity.Services;
using StackExchange.Redis;

namespace RoadDefectsService.Infrastructure.Identity
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositories
            services.AddScoped<ITokenRepository, TokenRedisRepository>();
            services.AddScoped<IContractorRepository, ContractorRepository>();
            services.AddScoped<IRoadInspectorRepository, RoadInspectorRepository>();
            services.AddScoped<IOperatorRepository, OperatorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAccessTokenService, JWTTokenService>();

            // Configuration
            services.AddIdentityConfigurations();
            services.AddDatabaseSeedConfigurations();

            // DataBase
            services.AddEntityFrameworkDbContext(configuration);
            services.AddRedisDbAddRedisDb(configuration);

            return services;
        }

        private static void AddRedisDbAddRedisDb(this IServiceCollection services, IConfiguration configuration)
        {
            string? redisConnectionString = configuration.GetConnectionString("RedisConnection");
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString!));
        }

        private static IServiceCollection AddIdentityConfigurations(this IServiceCollection services)
        {
            services.ConfigureOptions<IdentityOptionsConfigure>();

            return services;
        }

        private static void AddEntityFrameworkDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string? postgreConnectionString = configuration.GetConnectionString("PostgreConnection");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(postgreConnectionString!));

            services.AddIdentity<CustomUser, CustomRole>()
                    .AddEntityFrameworkStores<AppDbContext>();
        }

        public static void AddAutoMigration(this IServiceProvider services)
        {
            using var dbContext = services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }

        private static IServiceCollection AddDatabaseSeedConfigurations(this IServiceCollection services)
        {
            services.ConfigureOptions<AdminsOptionsConfigure>();

            return services;
        }

        public static void AddDatabaseSeed(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                // Roles
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<CustomRole>>();
                AppDbSeed.AddRoles(roleManager);

                // Admins
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();
                var _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                var _adminsOptions = scope.ServiceProvider.GetRequiredService<IOptions<AdminsOptions>>();
                AppDbSeed.AddAdmins(_userService, _userManager, _adminsOptions.Value.CreateAdmins);

                // Contractors
                var _contractorService = scope.ServiceProvider.GetRequiredService<IContractorService>();
                var _contractorRepository = scope.ServiceProvider.GetRequiredService<IContractorRepository>();
                AppDbSeed.AddContractors(_contractorService, _contractorRepository);
            }
        }

    }
}
