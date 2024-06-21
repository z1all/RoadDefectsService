using Newtonsoft.Json;
using RoadDefectsService.Infrastructure.Identity.Seeds.DTOs;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models.Base;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Models
{
    public class CreateFixationDefectDTO : BaseCreateDTO<CreateDefectDTO>
    {
        [JsonProperty(PropertyName = "CreateFixationDefect")]
        public override required List<CreateDefectDTO> Models { get; set; }
    }
}
