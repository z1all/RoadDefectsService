using MediatR;
using RoadDefectsService.Core.Application.CQRS.DefectType.DTOs;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.DefectType.Commands
{
    public class CreateDefectTypeEntityCommand : IRequest<ExecutionResult>
    {
        public required CreateDefectTypeEntityDTO CreateDefectTypeEntity { get; set; }

        public class CreateDefectTypeEntityCommandHandler : IRequestHandler<CreateDefectTypeEntityCommand, ExecutionResult>
        {
            private readonly IDefectTypeRepository _defectTypeRepository;

            public CreateDefectTypeEntityCommandHandler(IDefectTypeRepository defectTypeRepository)
            {
                _defectTypeRepository = defectTypeRepository;
            }

            public async Task<ExecutionResult> Handle(CreateDefectTypeEntityCommand request, CancellationToken cancellationToken)
            {
                DefectTypeEntity? defectType = await _defectTypeRepository.GetByIdAsync(request.CreateDefectTypeEntity.Id);
                if (defectType is null)
                {
                    defectType = new()
                    {
                        Id = request.CreateDefectTypeEntity.Id,
                        Name = request.CreateDefectTypeEntity.Name,
                    };
                    
                    await _defectTypeRepository.AddAsync(defectType);
                }
                else
                {
                    defectType.Name = request.CreateDefectTypeEntity.Name;

                    await _defectTypeRepository.UpdateAsync(defectType);
                }

                return ExecutionResult.SucceededResult;
            }
        }
    }
}
