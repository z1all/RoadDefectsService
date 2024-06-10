using Microsoft.Extensions.Options;

namespace RoadDefectsService.Presentation.Web.Configurations.Photo
{
    public class PhotoUploadOptionsConfigure(IConfiguration configuration) : IConfigureOptions<PhotoUploadOptions>
    {
        private readonly string valueKey = "PhotoUpload";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(PhotoUploadOptions options) => _configuration.GetSection(valueKey).Bind(options);
    }
}
