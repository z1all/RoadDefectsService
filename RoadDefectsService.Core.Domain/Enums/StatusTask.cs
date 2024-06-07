using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusTask
    {
        Created = 1,
        Processing = 2,
        Completed = 3,
    }
}
