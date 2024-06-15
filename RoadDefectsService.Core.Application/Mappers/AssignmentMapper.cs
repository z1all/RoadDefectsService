using RoadDefectsService.Core.Application.DTOs.AssignmentService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public static class AssignmentMapper
    {
        public static AssignmentDTO ToAssignmentDTO(this Assignment assignment)
        {
            return new()
            {
                Id = assignment.Id,
                Contractor = assignment.Contractor!.ToContractorDTO(),
                FixationDefect = assignment.FixationDefect!.ToFixationDefectDTO(),
            };
        }

        public static AssignmentShortInfoDTO ToAssignmentShortInfoDTO(this Assignment assignment)
        {
            return new()
            {
                Id = assignment.Id,
                Contractor = assignment.Contractor!.ToContractorDTO(),
                FixationDefect = assignment.FixationDefect!.ToFixationDefectShortInfoDTO(),
            };
        }

        public static List<AssignmentShortInfoDTO> ToAssignmentShortInfoDTOList(this List<Assignment> assignments)
        {
            return assignments.Select(ToAssignmentShortInfoDTO).ToList();
        }
    }
}
