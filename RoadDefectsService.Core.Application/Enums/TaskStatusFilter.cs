using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskStatusFilter
    {
        None = 0,
        Processing = 1,
        Created = 2,
        Completed = 3,
    }
}
