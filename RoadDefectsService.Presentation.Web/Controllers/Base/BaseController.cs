using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.DTOs;
using RoadDefectsService.Presentation.Web.Helpers;

namespace RoadDefectsService.Presentation.Web.Controllers.Base
{
    [ApiController]
    [ValidateModelState]
    public abstract class BaseController : ControllerBase
    {
        protected ObjectResult ExecutionResultHandlerAsync(ExecutionResult executionResult, string? otherMassage = null)
        {
            return StatusCode((int)executionResult.StatusCode, new ErrorResponse()
            {
                Title = otherMassage ?? "One or more errors occurred.",
                Status = (int)executionResult.StatusCode,
                Errors = executionResult.Errors,
            });
        }

        protected async Task<ActionResult<TResult>> ExecutionResultHandlerAsync<TResult>(Func<Guid, Task<ExecutionResult<TResult>>> operation)
        {
            if (!HttpContext.TryGetUserId(out Guid userId))
            {
                return ExecutionResultHandlerAsync(new ExecutionResult(StatusCodeExecutionResult.InternalServer, "UnknowError", "Unknow error"));
            }

            ExecutionResult<TResult> response = await operation(userId);

            if (!response.IsSuccess) return ExecutionResultHandlerAsync(response);
            return Ok(response.Result!);
        }

        protected async Task<ActionResult> ExecutionResultHandlerAsync(Func<Guid, Task<ExecutionResult>> operation)
        {
            if (!HttpContext.TryGetUserId(out Guid userId))
            {
                return ExecutionResultHandlerAsync(new ExecutionResult(StatusCodeExecutionResult.InternalServer, "UnknowError", "Unknow error"));
            }

            ExecutionResult response = await operation(userId);

            if (!response.IsSuccess) return ExecutionResultHandlerAsync(response);
            return NoContent();
        }

        protected async Task<ActionResult<TResult>> ExecutionResultHandlerAsync<TResult>(Func<Task<ExecutionResult<TResult>>> operation)
        {
            ExecutionResult<TResult> response = await operation();

            if (!response.IsSuccess) return ExecutionResultHandlerAsync(response);
            return Ok(response.Result!);
        }

        protected async Task<ActionResult> ExecutionResultHandlerAsync(Func<Task<ExecutionResult>> operation)
        {
            ExecutionResult response = await operation();

            if (!response.IsSuccess) return ExecutionResultHandlerAsync(response);
            return NoContent();
        }
    }
}
