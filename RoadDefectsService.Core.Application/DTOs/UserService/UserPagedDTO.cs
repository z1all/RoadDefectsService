using RoadDefectsService.Core.Application.DTOs.ContractorService;

namespace RoadDefectsService.Core.Application.DTOs.UserService
{
    public class UserPagedDTO
    {
        public required List<UserInfoDTO> Users { get; set; }
        public required PageInfoDTO Pagination { get; set; }
    }
}
