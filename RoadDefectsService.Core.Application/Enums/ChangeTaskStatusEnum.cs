using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ChangeTaskStatusEnum
    {
        StartTask = 0,
        CancelTask = 1,
        FinishTask = 2,
    }
}
