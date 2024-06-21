using AutoMapper;
using MediatR;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Task.Commands
{
    public class DeleteTaskCommand : IRequest<ExecutionResult>
    {
        public required Guid TaskId { get; set; }

        public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, ExecutionResult>
        {
            private readonly ITaskRepository _taskRepository;
            private readonly IMapper _mapper;

            public DeleteTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper)
            {
                _taskRepository = taskRepository;
                _mapper = mapper;
            }

            public async Task<ExecutionResult> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
            {
                TaskEntity? task = await _taskRepository.GetByIdAsync(request.TaskId);
                if (task is null)
                {
                    return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {request.TaskId} not found!");
                }

                if (!task.IsTransfer)
                {
                    ExecutionResult checkResult = CheckTaskHelper.CheckOnAllowedTaskStatus(task, AllowedTaskStatus.OnlyCreated);
                    if (checkResult.IsNotSuccess)
                    {
                        return checkResult;
                    }
                }

                await _taskRepository.DeleteAsync(task);

                return ExecutionResult.SucceededResult;
            }
        }
    }
}
