using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed
{
    public class AdminsOptionsConfigure(IConfiguration configuration) : IConfigureOptions<AdminsOptions>
    {
        private readonly string valueKey = "DbSeed";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(AdminsOptions options) => _configuration.GetSection(valueKey).Bind(options);
    }
}
