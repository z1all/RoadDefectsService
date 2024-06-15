using Newtonsoft.Json;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models.Base;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Models
{
    public class CreateDefectTypesDTO : BaseCreateDTO<DefectType>
    {
        [JsonProperty(PropertyName = "CreateDefectTypes")]
        public override required List<DefectType> Models { get; set; }
    }
}
