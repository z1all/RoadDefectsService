using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public static class FixationMapper
    {
        public static FixationDefectDTO ToFixationDefectDTO(this FixationDefect fixationDefect)
        {
            return new()
            {
                Id = fixationDefect.Id,
                RecordedDateTime = fixationDefect.RecordedDateTime,
                ExactAddress = fixationDefect.ExactAddress,
                CoordinatesX = fixationDefect.CoordinatesX,
                CoordinatesY = fixationDefect.CoordinatesY,
                DamagedCanvasSquareMeter = fixationDefect.DamagedCanvasSquareMeter,
                DefectType = fixationDefect.DefectType?.ToDefectTypeDTO(),
                Photos = fixationDefect.Photos.ToPhotoInfoDTOList(),
            };
        }

        public static FixationWorkDTO ToFixationWorkDTO(this FixationWork fixationWork)
        {
            return new()
            {
                Id = fixationWork.Id,
                RecordedDateTime = fixationWork.RecordedDateTime,
                WorkDone = fixationWork.WorkDone,
                Photos = fixationWork.Photos.ToPhotoInfoDTOList(),
            };
        }
    }
}
