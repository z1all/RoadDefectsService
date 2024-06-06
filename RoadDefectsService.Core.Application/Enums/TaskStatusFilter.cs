using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskStatusFilter
    {
        None = 0,
        Created = 1,
        Appointed = 2,
        Completed = 3,
    }
}
