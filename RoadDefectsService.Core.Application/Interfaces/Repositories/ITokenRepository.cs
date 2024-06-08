namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface ITokenRepository
    {
        Task<bool> RemoveTokensAsync(Guid accessTokenJTI);
        Task<bool> SaveTokenAsync(Guid accessTokenJTI);
        Task<bool> TokensExistAsync(Guid accessTokenJTI);
    }
}