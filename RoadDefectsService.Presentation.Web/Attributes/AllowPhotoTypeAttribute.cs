using System.ComponentModel.DataAnnotations;
using RoadDefectsService.Presentation.Web.Helpers;

namespace RoadDefectsService.Presentation.Web.Attributes
{
    public class AllowPhotoTypeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile formFile)
            {
                PhotoTypeHelper photoTypeHelper = validationContext.GetRequiredService<PhotoTypeHelper>();

                string fileType = Path.GetExtension(formFile.FileName);
                if (!photoTypeHelper.ExistPhotoType(fileType))
                {
                    return new ValidationResult($"File type is not allowed. These extensions are allowed only: {photoTypeHelper.GetExistPhotoTypeString()}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
