using RoadDefectsService.Core.Application.Enums;
using RoadDefectsService.Core.Domain.Enums;
using System.Text.Json.Serialization;

namespace RoadDefectsService.Core.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoleEnum
    {
        Admin = 0,
        Operator = 1,
        RoadInspector = 2,
    }
}

public static class RoleEnumExtension
{
    public static RoleEnum ToRoleEnum(this string roleEnum)
    {
        return roleEnum switch
        {
            Role.Admin => RoleEnum.Admin,
            Role.Operator => RoleEnum.Operator,
            Role.RoadInspector => RoleEnum.RoadInspector,
            _ => throw new InvalidCastException()
        };
    }
}
