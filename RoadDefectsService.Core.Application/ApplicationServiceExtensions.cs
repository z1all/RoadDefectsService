using Microsoft.Extensions.DependencyInjection;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Services;

namespace RoadDefectsService.Core.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IAccessTokenService, JWTTokenService>();
            services.AddScoped<IContractorService, ContractorService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskFixationDefectService, TaskFixationDefectService>();
            services.AddScoped<ITaskFixationWorkService, TaskFixationWorkService>();

            return services;
        }
    }
}
