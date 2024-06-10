using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public static class PhotoMapper
    {
        public static PhotoInfoDTO ToPhotoInfoDTO(this Photo photo)
        {
            return new()
            {
                Id = photo.Id,
                Name = photo.Name,
                Type = photo.Type,
            };
        }

        public static List<PhotoInfoDTO> ToPhotoInfoDTOList(this IEnumerable<Photo> photos)
        {
            return photos.Select(ToPhotoInfoDTO).ToList();
        }
    }
}
