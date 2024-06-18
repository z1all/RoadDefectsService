namespace RoadDefectsService.Core.Domain.Models.Base
{

    public interface ISoftDeleteBaseEntity : IBaseEntity
    {
        bool IsDeleted { get; set; }
    }

    public abstract class SoftDeleteBaseEntity : BaseEntity, ISoftDeleteBaseEntity
    {
        public bool IsDeleted { get; set; } = false;
    }
}
