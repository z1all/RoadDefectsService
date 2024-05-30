using System.Security.Claims;

namespace RoadDefectsService.Presentation.Web.Helpers
{
    public static class JwtTokenHelper
    {
        public static bool TryGetUserId(this HttpContext httpContext, out Guid userId)
        {
            Claim? userIdClaim = httpContext.User.Claims.FirstOrDefault(clam => clam.Type == ClaimTypes.NameIdentifier);

            return Guid.TryParse(userIdClaim?.Value, out userId);
        }
    }
}
