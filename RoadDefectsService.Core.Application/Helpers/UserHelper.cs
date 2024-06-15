using Microsoft.AspNetCore.Http;
using RoadDefectsService.Core.Domain.Enums;

namespace RoadDefectsService.Core.Application.Helpers
{
    public static class UserHelper
    {
        public static Guid? GetUserIdIfRoadInspectorOrNull(this Guid userId, HttpContext httpContext)
        {
            if (httpContext.User.IsInRole(Role.RoadInspector))
            {
                return userId;
            }
            return null;
        }
    }
}
