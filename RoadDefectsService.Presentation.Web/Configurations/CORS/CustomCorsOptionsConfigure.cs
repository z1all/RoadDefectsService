using Microsoft.Extensions.Options;

namespace RoadDefectsService.Presentation.Web.Configurations.CORS
{
    public class CustomCorsOptionsConfigure(IConfiguration configuration) : IConfigureOptions<CustomCorsOptions>
    {
        private readonly string valueKey = "Cors";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(CustomCorsOptions options) => _configuration.GetSection(valueKey).Bind(options);
    }
}
