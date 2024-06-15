namespace RoadDefectsService.Core.Application.DTOs.AccessTokenService
{
    public class TokenUserInfoDTO
    {
        public required Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
    }
}
