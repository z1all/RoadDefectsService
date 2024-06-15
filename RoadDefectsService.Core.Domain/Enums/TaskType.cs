using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskType
    {
        FixationDefectTask = 1,
        FixationWorkTask = 2,
    }
}
