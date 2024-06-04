using System.Security.Claims;
using RoadDefectsService.Core.Application.DTOs.AccessTokenService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IAccessTokenService
    {
        ExecutionResult<AccessTokenDTO> GenerateToken(TokenUserInfoDTO user, List<Claim> additionClaims);
    }
}
