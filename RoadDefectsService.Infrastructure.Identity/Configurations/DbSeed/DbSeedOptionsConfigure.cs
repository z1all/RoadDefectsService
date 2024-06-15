using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed
{
    public class DbSeedOptionsConfigure(IConfiguration configuration) : IConfigureOptions<DbSeedOptions>
    {
        private readonly string valueKey = "DbSeed";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(DbSeedOptions options) => _configuration.GetSection(valueKey).Bind(options);
    }
}
