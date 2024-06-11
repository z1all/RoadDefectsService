using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace RoadDefectsService.Infrastructure.SMTP.Configurations
{
    internal class GmailSmtpOptionsConfigure(IConfiguration configuration) : IConfigureOptions<GmailSmtpOptions>
    {
        private readonly string valueKey = "GmailSmtp";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(GmailSmtpOptions options) => _configuration.GetSection(valueKey).Bind(options);
    }
}
