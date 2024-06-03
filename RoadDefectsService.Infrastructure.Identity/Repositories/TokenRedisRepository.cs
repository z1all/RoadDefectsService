using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.Configurations.JwtToken;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using StackExchange.Redis;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class TokenRedisRepository : ITokenRepository
    {
        private readonly IDatabase _redis;
        private readonly TimeSpan expiration;

        public TokenRedisRepository(IConnectionMultiplexer connectionMultiplexer, IOptions<JwtOptions> jwtOptions)
        {
            _redis = connectionMultiplexer.GetDatabase();

            int timeLifeMinutes = jwtOptions.Value.AccessTokenTimeLifeMinutes;
            expiration = new TimeSpan(0, 0, timeLifeMinutes, 0);
        }

        public  Task<bool> RemoveTokensAsync(Guid accessTokenJTI)
        {
            return _redis.KeyDeleteAsync(accessTokenJTI.ToString());
        }

        public async Task<bool> SaveTokenAsync(Guid accessTokenJTI)
        {
            if (await TokensExistAsync(accessTokenJTI)) return false;

            bool result = await _redis.StringSetAsync(accessTokenJTI.ToString(), "", expiration);

            return result;
        }

        public Task<bool> TokensExistAsync(Guid accessTokenJTI)
        {
            return _redis.KeyExistsAsync(accessTokenJTI.ToString());
        }
    }
}
