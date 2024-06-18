using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusTask
    {
        Processing = 1,
        Created = 2,
        Completed = 3,
    }
}
