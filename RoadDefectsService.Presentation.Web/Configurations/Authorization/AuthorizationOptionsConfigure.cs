using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace RoadDefectsService.Presentation.Web.Configurations.Authorization
{
    public class AuthorizationOptionsConfigure : IConfigureNamedOptions<AuthorizationOptions>
    {
        public void Configure(string? name, AuthorizationOptions options)
        {
            Configure(options);
        }

        public void Configure(AuthorizationOptions options)
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        }
    }
}
