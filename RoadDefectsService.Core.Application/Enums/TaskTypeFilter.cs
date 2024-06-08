using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskTypeFilter
    {
        None = 0,
        FixationDefectTask = 1,
        FixationWorksTask = 2,
    }
}
