using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DefectStatusFilter
    {
        None = 0,
        NotVerified = 1,
        ThereIsDefect = 2,
        ThereIsNotDefect = 3,
    }
}
