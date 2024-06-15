using Newtonsoft.Json;
using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models.Base;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Models
{
    public class CreateAdminsDTO : BaseCreateDTO<CreateUserDTO>
    {
        [JsonProperty(PropertyName = "CreateAdmins")]
        public override required List<CreateUserDTO> Models { get; set; }
    }
}
