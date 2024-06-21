using Newtonsoft.Json;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Seeds.Models.Base;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Models
{
    public class CreateTasksFixationWorkDTO : BaseCreateDTO<TaskFixationWorkEntity>
    {
        [JsonProperty(PropertyName = "CreateTasksFixationWork")]
        public override required List<TaskFixationWorkEntity> Models { get; set; }
    }
}
