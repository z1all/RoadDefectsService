using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FixationType
    {
        FixationDefect = 1,
        FixationWork = 2,
    }
}
