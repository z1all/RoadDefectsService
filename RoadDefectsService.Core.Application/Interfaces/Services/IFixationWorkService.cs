using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IFixationWorkService
    {
        Task<ExecutionResult<FixationWorkDTO>> GetFixationWorkAsync(Guid fixationWorkId, Guid? userId);
        Task<ExecutionResult> DeleteFixationWorkAsync(Guid fixationWorkId, Guid? userId);
        Task<ExecutionResult<CreateFixationResponseDTO>> CreateFixationWorkAsync(CreateFixationWorkDTO createFixationWork, Guid? userId);
        Task<ExecutionResult> ChangeFixationWorkAsync(EditFixationWorkDTO editFixationWork, Guid fixationWorkId, Guid? userId);
    }
}
