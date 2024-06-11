using Microsoft.Extensions.Options;

namespace RoadDefectsService.Presentation.Web.Configurations.Photo
{
    public class PhotoTypeOptionsConfigure(IConfiguration configuration) : IConfigureOptions<PhotoTypeOptions>
    {
        private readonly string valueKey = "MimeType";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(PhotoTypeOptions options) => _configuration.GetSection(valueKey).Bind(options);
    }
}
