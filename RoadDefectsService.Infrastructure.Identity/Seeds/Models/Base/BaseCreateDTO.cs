namespace RoadDefectsService.Infrastructure.Identity.Seeds.Models.Base
{
    public abstract class BaseCreateDTO<TModel>
    {
        public abstract List<TModel> Models { get; set; }
    }
}
