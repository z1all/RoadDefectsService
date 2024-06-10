using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Core.Application.DTOs.FixationService
{
    public class CreateFixationDefectDTO
    {
        public required Guid TaskId { get; set; }
    }
}
