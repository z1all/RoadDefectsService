using System.Security.Claims;
using RoadDefectsService.Core.Application.DTOs;
using RoadDefectsService.Core.Application.DTOs.AccessTokenService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IAccessTokenService
    {
        ExecutionResult<AccessTokenDTO> GenerateToken(UserDTO user, List<Claim> additionClaims);
    }
}
