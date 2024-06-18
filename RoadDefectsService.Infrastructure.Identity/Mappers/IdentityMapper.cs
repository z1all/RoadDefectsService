using Microsoft.AspNetCore.Identity;
using System.Collections.Immutable;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Infrastructure.Identity.Mappers
{
    public static class IdentityMapper
    {
        public static ImmutableDictionary<string, List<string>> ToErrorDictionary(this IEnumerable<IdentityError> identityErrors)
        {
            ImmutableDictionary<string, List<string>> errors = ImmutableDictionary<string, List<string>>.Empty;

            foreach (var error in identityErrors)
            {
                errors = errors.Add(error.Code, [error.Description]);
            }

            return errors;
        }

        public static ExecutionResult ToExecutionResultError(this IdentityResult identityResult)
        {
            return new(StatusCodeExecutionResult.BadRequest, errors: identityResult.Errors.ToErrorDictionary());
        }

        public static ExecutionResult<T> ToExecutionResultError<T>(this IdentityResult identityResult)
        {
            return new(StatusCodeExecutionResult.BadRequest, errors: identityResult.Errors.ToErrorDictionary());
        }
    }
}
