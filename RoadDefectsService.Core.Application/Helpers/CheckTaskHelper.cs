using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Helpers
{
    public static class CheckTaskHelper
    {
        public static ExecutionResult CheckOnTaskOwnerAndTaskStatus(TaskEntity task, Guid? userId)
        {
            ExecutionResult checkTaskOwnerResult = CheckOnTaskOwner(task, userId);
            if (checkTaskOwnerResult.IsNotSuccess) return checkTaskOwnerResult;
            return CheckOnTaskStatus(task);
        }

        public static ExecutionResult CheckOnTaskOwner(TaskEntity task, Guid? userId)
        {
            if (userId is not null && task!.RoadInspectorId != userId)
            {
                return new(StatusCodeExecutionResult.Forbid, "DeleteFixationDefectFail", $"You cannot delete a fixation defect, because the task is assigned to another inspector.");
            }
            return ExecutionResult.SucceededResult;
        }

        public static ExecutionResult CheckOnTaskStatus(TaskEntity task)
        {
            if (task!.TaskStatus == StatusTask.Completed)
            {
                return new(StatusCodeExecutionResult.BadRequest, "TaskCompleted", "You cannot modify a completed task.");
            }
            else if (task!.TaskStatus == StatusTask.Created)
            {
                return new(StatusCodeExecutionResult.BadRequest, "TaskNotProcessing", "You cannot change a task that has not been started.");
            }
            return ExecutionResult.SucceededResult;
        }
    }
}
