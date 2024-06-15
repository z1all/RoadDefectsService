namespace RoadDefectsService.Core.Application.Configurations.JwtToken
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = null!;
        public int AccessTokenTimeLifeMinutes { get; set; }
        // public int RefreshTokenTimeLifeDays { get; set; }
    }
}
