using RoadDefectsService.Core.Application.DTOs.AssignmentService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IAssignmentService
    {
        Task<ExecutionResult<AssignmentPagedDTO>> GetAssignmentsAsync(AssignmentFilterDTO assignmentFilterDTO);
        Task<ExecutionResult<AssignmentDTO>> GetAssignmentAsync(Guid assignmentId);
        Task<ExecutionResult> CreateAssignmentAsync(CreateAssignmentDTO createAssignment, Guid? userId);
    }
}
