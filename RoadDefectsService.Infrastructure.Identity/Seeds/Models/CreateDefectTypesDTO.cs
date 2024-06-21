using Newtonsoft.Json;
using RoadDefectsService.Core.Application.CQRS.DefectType.DTOs;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models.Base;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Models
{
    public class CreateDefectTypesDTO : BaseCreateDTO<CreateDefectTypeEntityDTO>
    {
        [JsonProperty(PropertyName = "CreateDefectTypes")]
        public override required List<CreateDefectTypeEntityDTO> Models { get; set; }
    }
}
