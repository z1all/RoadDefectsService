using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Presentation.Web.Helpers;

namespace RoadDefectsService.Presentation.Web.Attributes
{
    public class CustomeAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.TryGetAccessTokenJTI(out Guid accessTokenJTI))
            {
                Forbid(context, "JWTToken", "Error: this token is no longer available!");
                return;
            }

            var _authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
            if (!(await _authService.CheckAuthenticationAsync(accessTokenJTI)).IsSuccess)
            {
                Forbid(context, "JWTToken", "Error: this token is no longer available!");
                return;
            }
        }

        private void Forbid(AuthorizationFilterContext context, string key, string message)
        {
            context.HttpContext.Response.Headers.TryAdd(key, message);
            context.Result = new ForbidResult();
        }
    }
}
