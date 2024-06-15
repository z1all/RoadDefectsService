using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Presentation.Web.Attributes;

namespace RoadDefectsService.Presentation.Web.DTOs
{
    public class PhotoUpload
    {
        [AllowPhotoType]
        [PhotoMaxSize]
        public required IFormFile Photo { get; set; }
    }
}
