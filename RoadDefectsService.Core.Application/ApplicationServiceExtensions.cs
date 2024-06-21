using Microsoft.Extensions.DependencyInjection;
using RoadDefectsService.Core.Application.Configurations.FileStorage;
using RoadDefectsService.Core.Application.CQRS.Contractor.Utils;
using RoadDefectsService.Core.Application.CQRS.DefectType.Utils;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Mappers;
using RoadDefectsService.Core.Application.Services;

namespace RoadDefectsService.Core.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IAccessTokenService, JWTTokenService>();
            //services.AddScoped<IContractorService, ContractorService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskFixationDefectService, TaskFixationDefectService>();
            services.AddScoped<ITaskFixationWorkService, TaskFixationWorkService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFixationDefectService, FixationDefectService>();
            services.AddScoped<IFixationWorkService, FixationWorkService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IMetricsService, MetricsService>();

            // Configurations
            services.ConfigureOptions<FileStorageOptionsConfigure>();

            // AutoMapper
            services.AddAutoMapper();

            // MediatR
            services.AddMediator();

            return services;
        }

        private static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserMappingProfile), typeof(PhotoMappingProfile), typeof(FixationMappingProfile),
                typeof(ContractorMappingProfile), typeof(TaskMappingProfile), typeof(DefectTypeMappingProfile),
                typeof(AssignmentMappingProfile), typeof(CoordinateFixationDefectMappingProfile));

            return services;
        }

        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(conf => conf.RegisterServicesFromAssemblies(typeof(ApplicationServiceExtensions).Assembly));

            return services;
        }
    }
}
