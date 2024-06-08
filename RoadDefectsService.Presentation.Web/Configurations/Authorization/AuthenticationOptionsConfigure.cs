using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace RoadDefectsService.Presentation.Web.Configurations.Authorization
{
    public class AuthenticationOptionsConfigure : IConfigureNamedOptions<AuthenticationOptions>
    {
        public void Configure(string? name, AuthenticationOptions options)
        {
            Configure(options);
        }

        public void Configure(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
