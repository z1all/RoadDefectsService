using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Services;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Configurations;
using RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories;
using RoadDefectsService.Infrastructure.Identity.Seeds.Creators;
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
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskFixationDefectRepository, TaskFixationDefectRepository>();
            services.AddScoped<ITaskFixationWorkRepository, TaskFixationWorkRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IFixationDefectRepository, FixationDefectRepository>();
            services.AddScoped<IFixationWorkRepository, FixationWorkRepository>();
            services.AddScoped<IDefectTypeRepository, DefectTypeRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<ICoordinateFixationDefectRepository, CoordinateFixationDefectRepository>();

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
            services.ConfigureOptions<DbSeedOptionsConfigure>();

            return services;
        }

        public static void AddDatabaseSeed(this IServiceCollection services)
        {
            services.AddScoped<AdminsCreator>();
            services.AddScoped<ContractorsCreator>();
            services.AddScoped<DefectTypesCreator>();
            services.AddScoped<RolesCreator>();
        }

        public static void AddDatabaseSeed(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<AdminsCreator>().AddModels();
                scope.ServiceProvider.GetRequiredService<ContractorsCreator>().AddModels();
                scope.ServiceProvider.GetRequiredService<DefectTypesCreator>().AddModels();
                scope.ServiceProvider.GetRequiredService<RolesCreator>().AddModels();
            }
        }
    }
}
