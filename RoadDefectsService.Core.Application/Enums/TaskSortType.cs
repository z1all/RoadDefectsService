using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskSortType
    {
        None = 0,
        CreatedDateTimeAsc = 1,
        CreatedDateTimeDesc = 2,
    }
}
