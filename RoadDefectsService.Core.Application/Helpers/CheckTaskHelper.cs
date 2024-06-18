using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Helpers
{

    public class AllowedTaskStatus
    {
        public bool Completed { get; set; } = false;
        public bool Processing { get; set; } = false;
        public bool Created { get; set; } = false;

        public static AllowedTaskStatus OnlyCreated { get => new() { Created = true }; }
        public static AllowedTaskStatus OnlyProcessing { get => new() { Processing = true }; }
    }

    public static class CheckTaskHelper
    {
        public static ExecutionResult CheckOnTaskOwnerAndAllowedTaskStatusAndTaskTransfer(TaskEntity task, AllowedTaskStatus allowedTaskStatus, Guid? userId)
        {
            ExecutionResult checkTaskOwnerResult = CheckOnTaskOwner(task, userId);
            if (checkTaskOwnerResult.IsNotSuccess) return checkTaskOwnerResult;
            else if (task.IsTransfer) return ExecutionResult.SucceededResult;
            return CheckOnAllowedTaskStatus(task, allowedTaskStatus);
        }

        public static ExecutionResult CheckOnAllowedTaskStatus(TaskEntity task, AllowedTaskStatus allowedTaskStatus)
        {
            if (!allowedTaskStatus.Completed && task.TaskStatus == StatusTask.Completed)
            {
                return new(StatusCodeExecutionResult.Forbid, "TaskIsCompleted", "It is forbidden to perform actions on a task with the status 'Completed'!");
            }
            else if (!allowedTaskStatus.Created && task.TaskStatus == StatusTask.Created)
            {
                return new(StatusCodeExecutionResult.Forbid, "TaskIsCreated", "It is forbidden to perform actions on a task with the status 'Created'!");
            }
            else if (!allowedTaskStatus.Processing && task.TaskStatus == StatusTask.Processing)
            {
                return new(StatusCodeExecutionResult.Forbid, "TaskIsProcessing", "It is forbidden to perform actions on a task with the status 'Processing'!");
            }
            return ExecutionResult.SucceededResult;
        }

        public static ExecutionResult CheckOnTaskOwnerAndCompletedTaskStatus(TaskEntity task, Guid? userId)
        {
            ExecutionResult checkTaskOwnerResult = CheckOnTaskOwner(task, userId);
            if (checkTaskOwnerResult.IsNotSuccess) return checkTaskOwnerResult;

            if (task.TaskStatus != StatusTask.Completed)
            {
                return new(StatusCodeExecutionResult.Forbid, "TaskUncompleted", "You cannot modify a uncompleted task.");
            }
            return ExecutionResult.SucceededResult;
        }

        public static ExecutionResult CheckOnTaskOwner(TaskEntity task, Guid? userId)
        {
            if (userId is not null && task.RoadInspectorId != userId)
            {
                return new(StatusCodeExecutionResult.Forbid, "TaskAccessDenied", $"User with id {userId} does not have access to tasks with id {task.Id}");
            }
            return ExecutionResult.SucceededResult;
        }

        public static ExecutionResult CheckOnTaskOwnerAnsNextTaskOwner(TaskEntity task, Guid? userId)
        {
            if (userId is not null && (task.RoadInspectorId != userId && task.NextTask?.RoadInspectorId != userId))
            {
                return new(StatusCodeExecutionResult.Forbid, "TaskAccessDenied", $"User with id {userId} does not have access to tasks with id {task.Id}");
            }
            return ExecutionResult.SucceededResult;
        }
    }
}
