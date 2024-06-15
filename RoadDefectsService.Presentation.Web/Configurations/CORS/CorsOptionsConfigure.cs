using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace RoadDefectsService.Presentation.Web.Configurations.CORS
{
    public class CorsOptionsConfigure(IOptions<CustomCorsOptions> options) : IConfigureNamedOptions<CorsOptions>
    {
        private readonly CustomCorsOptions _options = options.Value;

        public void Configure(string? name, CorsOptions options)
        {
            Configure(options);
        }

        public void Configure(CorsOptions options)
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .WithOrigins(_options.AllowedOrigins.Split())
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        }
    }
}
