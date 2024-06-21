using AutoMapper;
using MediatR;
using RoadDefectsService.Core.Application.Enums;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Task.Commands
{
    public class ChangeTaskStatusCommand : IRequest<ExecutionResult>
    {
        public required Guid TaskId { get; set; }
        public required ChangeTaskStatusEnum ChangeTaskStatus { get; set; }

        public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, ExecutionResult>
        {
            private readonly ITaskRepository _taskRepository;
            private readonly IMapper _mapper;

            public ChangeTaskStatusCommandHandler(ITaskRepository taskRepository, IMapper mapper)
            {
                _taskRepository = taskRepository;
                _mapper = mapper;
            }

            public async Task<ExecutionResult> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
            {
                TaskEntity? task = await _taskRepository.GetByIdWithFixationDefectAsync(request.TaskId);
                if (task is null)
                {
                    return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {request.TaskId} not found!");
                }

                if (request.ChangeTaskStatus == ChangeTaskStatusEnum.FinishTask)
                {
                    ExecutionResult checkResult = CheckOnNullProperties(task.FixationDefect);
                    if (checkResult.IsNotSuccess) return checkResult;
                }

                if (task.TaskStatus == StatusTask.Completed)
                {
                    return new(StatusCodeExecutionResult.Forbid, "TaskCompleted", $"Changing the status of a completed task!");
                }

                if (task.TaskStatus != StatusTask.Processing && request.ChangeTaskStatus == ChangeTaskStatusEnum.FinishTask)
                {
                    return new(StatusCodeExecutionResult.Forbid, "TaskNotProcessing", $"Completing a task that has not been started!");
                }

                task.TaskStatus = request.ChangeTaskStatus switch
                {
                    ChangeTaskStatusEnum.StartTask => StatusTask.Processing,
                    ChangeTaskStatusEnum.CancelTask => StatusTask.Created,
                    ChangeTaskStatusEnum.FinishTask => StatusTask.Completed,
                    _ => task.TaskStatus
                };

                await _taskRepository.UpdateAsync(task);

                return ExecutionResult.SucceededResult;
            }

            private ExecutionResult CheckOnNullProperties(FixationDefectEntity? fixationDefect)
            {
                if (fixationDefect is not null &&
                   (fixationDefect.DamagedCanvasSquareMeter is null || fixationDefect.DefectTypeId is null))
                {
                    return new(StatusCodeExecutionResult.BadRequest, "FixationDefectNullProperties", "You cannot complete the task because one or more properties of the fixation defect have a null value!");
                }

                return ExecutionResult.SucceededResult;
            }
        }
    }
}
