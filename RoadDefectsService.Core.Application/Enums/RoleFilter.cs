using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoleFilter
    {
        None = 0,
        Operator = 1,
        RoadInspector = 2,
    }
}
