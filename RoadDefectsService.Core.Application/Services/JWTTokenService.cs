using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoadDefectsService.Core.Application.Configurations.JwtToken;
using RoadDefectsService.Core.Application.DTOs.AccessTokenService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RoadDefectsService.Core.Application.Services
{
    public class JWTTokenService(IOptions<JwtOptions> options) : IAccessTokenService
    {
        private readonly JwtOptions jwtOptions = options.Value;
        private readonly JwtSecurityTokenHandler _tokenHandler = new();

        public ExecutionResult<AccessTokenDTO> GenerateToken(TokenUserInfoDTO user, List<Claim> additionClaims)
        {
            byte[] key = Encoding.ASCII.GetBytes(jwtOptions.SecretKey);
            (List<Claim> claims, Guid JTI) = GetClaims(user, additionClaims);

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(jwtOptions.AccessTokenTimeLifeMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
            };

            var token = _tokenHandler.CreateToken(tokenDescription);

            return new()
            {
                Result = new()
                {
                    Access = _tokenHandler.WriteToken(token),
                    JTI = JTI
                }
            };
        }

        private(List<Claim> claims, Guid JTI) GetClaims(TokenUserInfoDTO user, List<Claim> additionClaims)
        {
            Guid JTI = Guid.NewGuid();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, JTI.ToString()),
            };

            claims.AddRange(additionClaims);

            return (claims, JTI);
        }
    }
}
