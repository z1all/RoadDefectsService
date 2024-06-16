using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Helpers
{
    public static class CheckTaskHelper
    {
        public static ExecutionResult CheckOnTaskOwnerAndProcessingTaskStatus(TaskEntity task, Guid? userId)
        {
            ExecutionResult checkTaskOwnerResult = CheckOnTaskOwner(task, userId);
            if (checkTaskOwnerResult.IsNotSuccess) return checkTaskOwnerResult;
            return CheckOnProcessingTaskStatus(task);
        }

        public static ExecutionResult CheckOnTaskOwnerAndCompletedTaskStatus(TaskEntity task, Guid? userId)
        {
            ExecutionResult checkTaskOwnerResult = CheckOnTaskOwner(task, userId);
            if (checkTaskOwnerResult.IsNotSuccess) return checkTaskOwnerResult;
            return CheckOnCompletedTaskStatus(task);
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

        public static ExecutionResult CheckOnProcessingTaskStatus(TaskEntity task)
        {
            if (task.TaskStatus == StatusTask.Completed)
            {
                return new(StatusCodeExecutionResult.BadRequest, "TaskCompleted", "You cannot modify a completed task.");
            }
            else if (task.TaskStatus == StatusTask.Created)
            {
                return new(StatusCodeExecutionResult.BadRequest, "TaskNotProcessing", "You cannot change a task that has not been started.");
            }
            return ExecutionResult.SucceededResult;
        }

        public static ExecutionResult CheckOnCompletedTaskStatus(TaskEntity task)
        {
            if (task.TaskStatus != StatusTask.Completed)
            {
                return new(StatusCodeExecutionResult.BadRequest, "TaskUncompleted", "You cannot modify a uncompleted task.");
            }

            return ExecutionResult.SucceededResult;
        }
    }
}
