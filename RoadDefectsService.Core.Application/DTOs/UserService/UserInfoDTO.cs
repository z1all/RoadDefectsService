namespace RoadDefectsService.Core.Application.DTOs.UserService
{
    public class UserInfoDTO
    {
        public required Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string HighestRole { get; set; }
    }
}
