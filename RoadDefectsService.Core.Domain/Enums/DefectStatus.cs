using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DefectStatus
    {
        NotVerified = 0,
        ThereIsDefect = 1,
        ThereIsNotDefect = 2,
    }
}
