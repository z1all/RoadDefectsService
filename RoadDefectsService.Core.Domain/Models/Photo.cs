using RoadDefectsService.Core.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Domain.Models
{
    public class Photo : BaseEntity
    {
        public string PathName { get => $"{Id}_{Name}{Type}"; }
        public required string Name { get; set; }
        public required string Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Guid? FixationWorkId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public Guid? FixationDefectId { get; set; }
    }
}
