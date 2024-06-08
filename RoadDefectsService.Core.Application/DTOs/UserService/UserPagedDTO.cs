using RoadDefectsService.Core.Application.DTOs.Common;

namespace RoadDefectsService.Core.Application.DTOs.UserService
{
    public class UserPagedDTO : BasePagedDTO<UserInfoDTO>
    {
        public List<UserInfoDTO> Users { get => Models; set => Models = value; }
    }
}
