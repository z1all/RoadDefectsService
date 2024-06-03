using RoadDefectsService.Core.Application.DTOs.AuthService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ExecutionResult<TokenResponseDTO>> LoginAsync(LoginDTO login);
        Task<ExecutionResult> LogoutAsync(Guid accessTokenJTI);
        Task<ExecutionResult> CheckAuthenticationAsync(Guid accessTokenJTI);
    }
}
