using Microsoft.Extensions.DependencyInjection;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Infrastructure.Identity.Services;

namespace RoadDefectsService.Infrastructure.Identity
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
