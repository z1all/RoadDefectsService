using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.CQRS.Task.DTOs
{
    public class EditTaskMetaInfoDTO
    {
        [Required]
        public required DateTime CreatedDateTime { get; set; }
    }
}
