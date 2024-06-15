using RoadDefectsService.Core.Application.DTOs.Common;

namespace RoadDefectsService.Core.Application.DTOs.AssignmentService
{
    public class AssignmentPagedDTO : BasePagedDTO<AssignmentShortInfoDTO>
    {
        public List<AssignmentShortInfoDTO> Assignments { get => Models; set => Models = value; }
    }
}
