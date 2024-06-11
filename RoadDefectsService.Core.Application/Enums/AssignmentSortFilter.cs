using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AssignmentSortFilter
    {
        None = 0,
        CreatedDateTimeAsc = 1,
        CreatedDateTimeDesc = 2,
        DeadlineDateOnlyAsc = 3,
        DeadlineDateOnlyDesc = 4,
    }
}
