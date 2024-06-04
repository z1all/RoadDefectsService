using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Domain.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
