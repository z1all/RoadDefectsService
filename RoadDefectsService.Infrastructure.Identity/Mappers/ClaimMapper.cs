using System.Security.Claims;

namespace RoadDefectsService.Infrastructure.Identity.Mappers
{
    public static class ClaimMapper
    {
        public static List<Claim> ToRoleClaims(this IList<string> roles)
        {
            List<Claim> claims = new();

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
