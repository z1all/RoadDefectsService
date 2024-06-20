using Newtonsoft.Json;
using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models.Base;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Models
{
    public class CreateContractorsDTO : BaseCreateDTO<CreateContractorDTO>
    {
        [JsonProperty(PropertyName = "CreateContractors")]
        public override required List<CreateContractorDTO> Models { get; set; }
    }
}
