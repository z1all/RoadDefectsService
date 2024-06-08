using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskViewModeFilter
    {
        All = 0,
        OnlyAssigned = 1,
        OnlyNotAssigned = 2,
    }
}
