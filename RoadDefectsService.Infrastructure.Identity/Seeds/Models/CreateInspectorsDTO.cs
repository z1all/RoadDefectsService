using Newtonsoft.Json;
using RoadDefectsService.Infrastructure.Identity.Seeds.DTOs;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models.Base;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Models
{
    public class CreateInspectorsDTO : BaseCreateDTO<CreateInspectorDTO>
    {
        [JsonProperty(PropertyName = "CreateInspectors")]
        public override required List<CreateInspectorDTO> Models { get; set; }
    }
}
