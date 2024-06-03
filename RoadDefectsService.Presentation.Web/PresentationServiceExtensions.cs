using Microsoft.AspNetCore.Authentication;
using RoadDefectsService.Core.Application.Configurations.JwtToken;
using RoadDefectsService.Presentation.Web.Configurations.Authorization;
using RoadDefectsService.Presentation.Web.Configurations.CORS;
using RoadDefectsService.Presentation.Web.Configurations.Other;
using RoadDefectsService.Presentation.Web.Configurations.Swagger;

namespace RoadDefectsService.Presentation.Web
{
    public static class PresentationServiceExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            // Configurations
            services.AddCorsConfigure();
            services.AddModalStateConfigure();
            services.AddSwaggerConfigure();
            services.AddJwtAuthentication();

            return services;
        }

        public static IServiceCollection AddCorsConfigure(this IServiceCollection services)
        {
            services.ConfigureOptions<ModalStateOptionsConfigure>();
            services.ConfigureOptions<CustomCorsOptionsConfigure>();

            return services;
        }

        public static IServiceCollection AddModalStateConfigure(this IServiceCollection services)
        {
            services.ConfigureOptions<CorsOptionsConfigure>();

            return services;
        }

        public static IServiceCollection AddSwaggerConfigure(this IServiceCollection services)
        {
            services.ConfigureOptions<SwaggerGenOptionsConfigure>();

            return services;
        }

        public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services)
        {
            services.ConfigureOptions<AuthenticationOptionsConfigure>();
            services.ConfigureOptions<AuthorizationOptionsConfigure>();
            services.ConfigureOptions<JwtBearerOptionsConfigure>();
            services.ConfigureOptions<JwtOptionsConfigure>();

            return services.AddAuthentication()
                    .AddJwtBearer();
        }
    }
}
