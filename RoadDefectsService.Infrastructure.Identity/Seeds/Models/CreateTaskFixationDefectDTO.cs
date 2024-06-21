using Newtonsoft.Json;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models.Base;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Models
{
    public class CreateTaskFixationDefectDTO : BaseCreateDTO<TaskFixationDefectEntity>
    {
        [JsonProperty(PropertyName = "CreateTasksFixationDefect")]
        public override required List<TaskFixationDefectEntity> Models { get; set; }
    }
}
