using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class FixationWorkService : IFixationWorkService
    {
        private readonly IFixationWorkRepository _fixationWorkRepository;

        public FixationWorkService(IFixationWorkRepository fixationWorkRepository)
        {
            _fixationWorkRepository = fixationWorkRepository;
        }

        public Task<ExecutionResult<FixationWorkDTO>> GetFixationWorkAsync(Guid fixationWorkId, Guid? userId)
        {
            throw new NotImplementedException();
        }

        public Task<ExecutionResult> DeleteFixationWorkAsync(Guid fixationWorkId, Guid? userId)
        {
            throw new NotImplementedException();
        }

        public Task<ExecutionResult<CreateFixationResponseDTO>> CreateFixationWorkAsync(CreateFixationWorkDTO createFixationWork, Guid? userId)
        {
            throw new NotImplementedException();
        }

        public Task<ExecutionResult> ChangeFixationWorkAsync(EditFixationWorkDTO editFixationWork, Guid fixationWorkId, Guid? userId)
        {
            throw new NotImplementedException();
        }
    }
}
