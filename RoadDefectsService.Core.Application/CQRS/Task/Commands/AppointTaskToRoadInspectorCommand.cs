using AutoMapper;
using MediatR;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Task.Commands
{
    public class AppointTaskToRoadInspectorCommand : IRequest<ExecutionResult>
    {
        public required Guid TaskId { get; set; }
        public required Guid RoadInspectorId { get; set; }

        public class AppointTaskToRoadInspectorCommandHandler : IRequestHandler<AppointTaskToRoadInspectorCommand, ExecutionResult>
        {
            private readonly ITaskRepository _taskRepository;
            private readonly IRoadInspectorRepository _roadInspectorRepository;
            private readonly IMapper _mapper;

            public AppointTaskToRoadInspectorCommandHandler(
                ITaskRepository taskRepository, IRoadInspectorRepository roadInspectorRepository, 
                IMapper mapper)
            {
                _taskRepository = taskRepository;
                _roadInspectorRepository = roadInspectorRepository;
                _mapper = mapper;
            }

            public async Task<ExecutionResult> Handle(AppointTaskToRoadInspectorCommand request, CancellationToken cancellationToken)
            {
                TaskEntity? task = await _taskRepository.GetByIdAsync(request.TaskId);
                if (task is null)
                {
                    return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {request.TaskId} not found!");
                }

                bool existRoadInspector = await _roadInspectorRepository.AnyNotDeletedByIdAsync(request.RoadInspectorId);
                if (!existRoadInspector)
                {
                    return new(StatusCodeExecutionResult.NotFound, "RoadInspectorNotFound", $"Road inspector with id {request.RoadInspectorId} not found!");
                }

                if (!task.IsTransfer)
                {
                    if (task.TaskStatus == StatusTask.Completed)
                    {
                        return new(StatusCodeExecutionResult.Forbid, "TaskCompleted", $"Assigning an inspector to a completed task!");
                    }
                    else if (task.TaskStatus == StatusTask.Processing)
                    {
                        return new(StatusCodeExecutionResult.Forbid, "TaskProcessing", $"Assigning an inspector to a processing task!");
                    }
                }

                task.RoadInspectorId = request.RoadInspectorId;

                await _taskRepository.UpdateAsync(task);

                return ExecutionResult.SucceededResult;
            }
        }
    }
}
