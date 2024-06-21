using AutoMapper;
using MediatR;
using RoadDefectsService.Core.Application.CQRS.Task.DTOs;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Task.Queries
{
    public class GetTasksByFiltersQuery : IRequest<ExecutionResult<TaskPagedDTO>>
    {
        public required CommonTaskFilterDTO TaskFilter { get; set; }

        public class GetTasksByFiltersQueryHandler : IRequestHandler<GetTasksByFiltersQuery, ExecutionResult<TaskPagedDTO>>
        {
            private readonly ITaskRepository _taskRepository;
            private readonly IMapper _mapper;

            public GetTasksByFiltersQueryHandler(ITaskRepository taskRepository, IMapper mapper)
            {
                _taskRepository = taskRepository;
                _mapper = mapper;
            }

            public Task<ExecutionResult<TaskPagedDTO>> Handle(GetTasksByFiltersQuery request, CancellationToken cancellationToken)
            {
                return FiltrationHelper
                    .FilterAsync<CommonTaskFilterDTO, TaskEntity, TaskDTO, TaskPagedDTO>(
                    request.TaskFilter, _taskRepository, (tasks) => _mapper.Map<List<TaskDTO>>(tasks));
            }
        }
    }
}
