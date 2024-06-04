using Microsoft.AspNetCore.Identity;
using RoadDefectsService.Core.Application.DTOs.AccessTokenService;
using RoadDefectsService.Core.Application.DTOs.AuthService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Mappers;

namespace RoadDefectsService.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly IAccessTokenService _tokenService;
        private readonly ITokenRepository _tokenRepository;

        public AuthService(
            UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager,
            IAccessTokenService tokenService, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
        }

        public async Task<ExecutionResult<TokenResponseDTO>> LoginAsync(LoginDTO login)
        {
            CustomUser? user = await _userManager.FindByEmailAsync(login.Email);
            if (user is null)
            {
                return new(StatusCodeExecutionResult.BadRequest, keyError: "LoginFail", error: "Invalid email or password");
            }

            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!signInResult.Succeeded)
            {
                return new(StatusCodeExecutionResult.BadRequest, keyError: "LoginFail", error: "Invalid email or password");
            }

            return await GenerateTokenAsync(user);
        }

        public async Task<ExecutionResult> LogoutAsync(Guid accessTokenJTI)
        {
            bool result = await _tokenRepository.RemoveTokensAsync(accessTokenJTI);
            if (!result)
            {
                return new(StatusCodeExecutionResult.BadRequest, keyError: "LogoutFail", error: "The tokens have already been deleted.");
            }
            return new(true);
        }

        public async Task<ExecutionResult> CheckAuthenticationAsync(Guid accessTokenJTI)
        {
            bool result = await _tokenRepository.TokensExistAsync(accessTokenJTI);
            return new(result);
        }

        private async Task<ExecutionResult<TokenResponseDTO>> GenerateTokenAsync(CustomUser user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);

            ExecutionResult<AccessTokenDTO> generateResult = _tokenService.GenerateToken(user.ToUserDTO(), roles.ToRoleClaims());
            if (!generateResult.IsSuccess)
            {
                return new() { Errors = generateResult.Errors };
            }
            AccessTokenDTO accessToken = generateResult.Result!;

            bool saveTokenResult = await _tokenRepository.SaveTokenAsync(accessToken.JTI);
            if (!saveTokenResult)
            {
                //_logger.LogError(new Exception(), $"The tokens could not be saved for unknown reasons. User id: {user.Id}");
                return new(StatusCodeExecutionResult.InternalServer, keyError: "UnknowError", error: "Unknown error");
            }

            //_logger.LogInformation($"Created new tokens for user with id {user.Id}");

            return new()
            {
                Result = new()
                {
                    Access = accessToken.Access
                }
            };
        }
    }
}