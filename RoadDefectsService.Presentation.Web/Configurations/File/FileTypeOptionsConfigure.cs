using Microsoft.Extensions.Options;

namespace RoadDefectsService.Presentation.Web.Configurations.File
{
    public class FileTypeOptionsConfigure(IConfiguration configuration) : IConfigureOptions<FileTypeOptions>
    {
        private readonly string valueKey = "MimeType";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(FileTypeOptions options) => _configuration.GetSection(valueKey).Bind(options);
    }
}
