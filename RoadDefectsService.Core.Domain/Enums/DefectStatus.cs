using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DefectStatus
    {
        ThereIsDefect = 0,
        ThereIsNotDefect = 1,
        NotVerified = 2,
    }
}
