using Microsoft.AspNetCore.Authentication;
using RoadDefectsService.Core.Application.Configurations.JwtToken;
using RoadDefectsService.Presentation.Web.Configurations.Authorization;
using RoadDefectsService.Presentation.Web.Configurations.CORS;
using RoadDefectsService.Presentation.Web.Configurations.Other;
using RoadDefectsService.Presentation.Web.Configurations.Photo;
using RoadDefectsService.Presentation.Web.Configurations.Swagger;
using RoadDefectsService.Presentation.Web.Helpers;

namespace RoadDefectsService.Presentation.Web
{
    public static class PresentationServiceExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            // Configurations
            services.AddFormFileConfigure();
            services.AddPhotoTypeOptionsConfigure();
            services.AddCorsConfigure();
            services.AddModalStateConfigure();
            services.AddSwaggerConfigure();
            services.AddJwtAuthentication();

            // Helpers
            services.AddSingleton<PhotoTypeHelper>();

            return services;
        }

        public static IServiceCollection AddFormFileConfigure(this IServiceCollection services)
        {
            services.ConfigureOptions<PhotoUploadOptionsConfigure>();

            return services;
        }

        public static IServiceCollection AddPhotoTypeOptionsConfigure(this IServiceCollection services)
        {
            services.ConfigureOptions<PhotoTypeOptionsConfigure>();

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
