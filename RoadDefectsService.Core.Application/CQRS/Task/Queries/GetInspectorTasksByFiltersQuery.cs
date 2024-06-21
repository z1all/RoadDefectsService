using AutoMapper;
using MediatR;
using RoadDefectsService.Core.Application.CQRS.Task.DTOs;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Task.Queries
{
    public class GetInspectorTasksByFiltersQuery : IRequest<ExecutionResult<TaskPagedDTO>>
    {
        public required Guid RoadInspectorId { get; set; }
        public required TaskFilterDTO TaskFilter { get; set; }

        public class GetInspectorTasksByFiltersQueryHandler : IRequestHandler<GetInspectorTasksByFiltersQuery, ExecutionResult<TaskPagedDTO>>
        {
            private readonly ITaskRepository _taskRepository;
            private readonly IMapper _mapper;

            public GetInspectorTasksByFiltersQueryHandler(ITaskRepository taskRepository, IMapper mapper)
            {
                _taskRepository = taskRepository;
                _mapper = mapper;
            }

            public Task<ExecutionResult<TaskPagedDTO>> Handle(GetInspectorTasksByFiltersQuery request, CancellationToken cancellationToken)
            {
                return FiltrationHelper
                   .FilterAsync<TaskFilterDTO, TaskEntity, TaskDTO, TaskPagedDTO>(
                       request.TaskFilter,
                       (filter) => _taskRepository.CountByFilterAsync(filter, request.RoadInspectorId),
                       (filter) => _taskRepository.GetAllByFilterAsync(filter, request.RoadInspectorId),
                       (tasks) => _mapper.Map<List<TaskDTO>>(tasks)
                   );
            }
        }
    }
}
