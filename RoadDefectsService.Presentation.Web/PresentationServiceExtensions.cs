using Microsoft.AspNetCore.Authentication;
using RoadDefectsService.Presentation.Web.Configurations.Authorization;
using RoadDefectsService.Presentation.Web.Configurations.Other;
using RoadDefectsService.Presentation.Web.Configurations.Swagger;

namespace RoadDefectsService.Presentation.Web
{
    public static class PresentationServiceExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            // Configurations
            services.AddModalStateConfigure();
            services.AddSwaggerConfigure();
            services.AddJwtAuthentication();

            return services;
        }

        public static IServiceProvider UsePresentationServices(this IServiceProvider services)
        {
            return services;
        }

        public static IServiceCollection AddModalStateConfigure(this IServiceCollection services)
        {
            services.ConfigureOptions<ModalStateOptionsConfigure>();

            return services;
        }

        public static IServiceCollection AddSwaggerConfigure(this IServiceCollection services)
        {
            services.ConfigureOptions<SwaggerGenOptionsConfigure>();

            return services;
        }

        public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services)
        {
            services.ConfigureOptions<AuthorizationOptionsConfigure>();
            services.ConfigureOptions<JwtBearerOptionsConfigure>();
            services.ConfigureOptions<JwtOptionsConfigure>();

            return services.AddAuthentication()
                    .AddJwtBearer();
        }
    }
}
