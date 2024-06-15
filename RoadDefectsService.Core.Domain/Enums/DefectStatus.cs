using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DefectStatus
    {
        NotVerified = 1,
        ThereIsDefect = 2,
        ThereIsNotDefect = 3,
    }
}
