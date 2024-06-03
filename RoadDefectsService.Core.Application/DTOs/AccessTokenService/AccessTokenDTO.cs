namespace RoadDefectsService.Core.Application.DTOs.AccessTokenService
{
    public class AccessTokenDTO
    {
        public required string Access { get; set; }
        public required Guid JTI { get; set; }
    }
}
