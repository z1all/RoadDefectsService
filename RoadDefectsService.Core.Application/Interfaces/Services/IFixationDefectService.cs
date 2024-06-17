using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IFixationDefectService
    {
        Task<ExecutionResult<FixationDefectDTO>> GetFixationDefectAsync(Guid fixationDefectId, Guid? userId);
        Task<ExecutionResult> DeleteFixationDefectAsync(Guid fixationDefectId, Guid? userId);
        Task<ExecutionResult<CreateFixationResponseDTO>> CreateFixationDefectAsync(CreateFixationDefectDTO createFixationDefect, Guid? userId);
        Task<ExecutionResult> ChangeFixationDefectAsync(EditFixationDefectDTO editFixationDefect, Guid fixationDefectId, Guid? userId);
        Task<ExecutionResult> ChangeMetaInfoFixationDefectAsync(EditMetaInfoFixationDefectDTO editMetaInfoFixationDefect, Guid fixationDefectId);
    }
}
