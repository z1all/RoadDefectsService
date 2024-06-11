using RoadDefectsService.Core.Application.DTOs.AssignmentService;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.DTOs.NotificationService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public static class FixationMapper
    {
        public static FixationDefectWithPhotoShortInfoDTO ToFixationDefectWithPhotoShortInfoDTO(this FixationDefect fixationDefect)
        {
            return new()
            {
                Id = fixationDefect.Id,
                RecordedDateTime = fixationDefect.RecordedDateTime,
                ExactAddress = fixationDefect.ExactAddress,
                DamagedCanvasSquareMeter = fixationDefect.DamagedCanvasSquareMeter,
                DefectTypeName = fixationDefect.DefectType?.Name,
                Photos = fixationDefect.Photos.ToPhotoShortInfoDTOList(),
            };
        }

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

        public static FixationDefectShortInfoDTO ToFixationDefectShortInfoDTO(this FixationDefect fixationDefect)
        {
            return new()
            {
                Id = fixationDefect.Id,
                ExactAddress = fixationDefect.ExactAddress,
                DamagedCanvasSquareMeter = fixationDefect.DamagedCanvasSquareMeter,
                DefectTypeName = fixationDefect.DefectType?.Name,
            };
        }
    }
}
