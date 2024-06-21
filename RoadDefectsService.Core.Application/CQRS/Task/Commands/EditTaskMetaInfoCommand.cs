using AutoMapper;
using MediatR;
using RoadDefectsService.Core.Application.CQRS.Task.DTOs;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.CQRS.Task.Commands
{
    public class EditTaskMetaInfoCommand : IRequest<ExecutionResult>
    {
        public required Guid TaskId { get; set; }
        public required EditTaskMetaInfoDTO EditTaskMetaInfo { get; set; }

        public class EditTaskMetaInfoCommandHandler : IRequestHandler<EditTaskMetaInfoCommand, ExecutionResult>
        {
            private readonly ITaskRepository _taskRepository;
            private readonly IMapper _mapper;

            public EditTaskMetaInfoCommandHandler(ITaskRepository taskRepository, IMapper mapper)
            {
                _taskRepository = taskRepository;
                _mapper = mapper;
            }

            public async Task<ExecutionResult> Handle(EditTaskMetaInfoCommand request, CancellationToken cancellationToken)
            {
                TaskEntity? task = await _taskRepository.GetByIdAsync(request.TaskId);
                if (task is null)
                {
                    return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {request.TaskId} not found!");
                }

                if (!task.IsTransfer)
                {
                    return new(StatusCodeExecutionResult.BadRequest, "TaskIsNotTransfer", $"The task should be to transfer data from paper to electronic form!");
                }

                task.CreatedDateTime = request.EditTaskMetaInfo.CreatedDateTime;
                await _taskRepository.UpdateAsync(task);

                return ExecutionResult.SucceededResult;
            }
        }
    }
}
