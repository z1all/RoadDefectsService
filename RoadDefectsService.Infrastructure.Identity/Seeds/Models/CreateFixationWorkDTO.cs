using Newtonsoft.Json;
using RoadDefectsService.Infrastructure.Identity.Seeds.DTOs;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models.Base;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Models
{
    public class CreateFixationWorkDTO : BaseCreateDTO<CreateWorkDTO>
    {
        [JsonProperty(PropertyName = "CreateFixationWork")]
        public override required List<CreateWorkDTO> Models { get; set; }
    }
}
