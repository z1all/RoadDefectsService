using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Domain.Models.Base
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }

    public class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
