using Microsoft.Extensions.Options;
using RoadDefectsService.Presentation.Web.Configurations.Photo;
using System.ComponentModel.DataAnnotations;

namespace RoadDefectsService.Presentation.Web.Attributes
{
    public class PhotoMaxSizeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile formFile)
            {
                PhotoUploadOptions options = validationContext.GetRequiredService<IOptions<PhotoUploadOptions>>().Value;

                if (formFile.Length > options.MaxSize)
                {
                    return new ValidationResult($"Maximum allowed file size is {options.MaxSize / (1024.0 * 1024.0)} megabytes.");
                }
            }

            return ValidationResult.Success;        
        }
    }
}
