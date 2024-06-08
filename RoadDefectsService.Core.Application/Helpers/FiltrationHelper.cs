using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Application.Helpers
{
    public static class FiltrationHelper
    {
        public static Task<ExecutionResult<TPaged>> FilterAsync<TFilter, TEntity, TModel, TPaged>(
            TFilter filter, IFilterableRepository<TFilter, TEntity> repository,
            Func<List<TEntity>, List<TModel>> mapper
        )
            where TFilter : BaseFilterDTO
            where TEntity : IBaseEntity
            where TModel  : class
            where TPaged  : BasePagedDTO<TModel>, new()
        {
            return FilterAsync<TFilter, TEntity, TModel, TPaged>(filter, repository.CountByFilterAsync, repository.GetAllByFilterAsync, mapper);
        }

        public static async Task<ExecutionResult<TPaged>> FilterAsync<TFilter, TEntity, TModel, TPaged>(
            TFilter filter, 
            Func<TFilter, Task<int>> countByFilterAsync,
            Func<TFilter, Task<List<TEntity>>> getAllByFilterAsync,
            Func<List<TEntity>, List<TModel>> mapper
        )
            where TFilter : BaseFilterDTO
            where TEntity : IBaseEntity
            where TModel : class
            where TPaged : BasePagedDTO<TModel>, new()
        {
            if (filter.Page < 1)
            {
                return new(StatusCodeExecutionResult.BadRequest, keyError: "InvalidPageError", error: "Number of page can't be less than 1.");
            }

            int countEntities = await countByFilterAsync(filter);
            int countPage = countEntities == 0 ? 1 : (countEntities + filter.Size - 1) / filter.Size;
            if (filter.Page > countPage)
            {
                return new(StatusCodeExecutionResult.BadRequest, keyError: "InvalidPageError", error: $"Number of page can be from 1 to {countPage}.");
            }

            List<TEntity> entities = await getAllByFilterAsync(filter);
            return new TPaged()
            {
                Models = mapper(entities),
                Pagination = new()
                {
                    Count = countPage,
                    Current = filter.Page,
                    Size = filter.Size,
                },
            };
        }
    }
}
