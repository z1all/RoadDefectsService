using Microsoft.Extensions.Options;
using RoadDefectsService.Presentation.Web.Configurations.Photo;

namespace RoadDefectsService.Presentation.Web.Helpers
{
    /// <summary>
    /// Служит для работы с типами фотографий
    /// </summary>
    public class PhotoTypeHelper
    {
        private readonly HashSet<string> _photoTypes;
        private readonly FileTypeHelper _fileTypeHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public PhotoTypeHelper(IOptions<PhotoTypeOptions> options, FileTypeHelper fileTypeHelper)
        {
            _photoTypes = new(options.Value.AllowPhotoTypes);
            _fileTypeHelper = fileTypeHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ExistPhotoType(string photoType)
        {
            return _photoTypes.Contains(photoType) && _fileTypeHelper.ExistFileType(photoType);
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetExistPhotoTypeString()
        {
            List<string> keys = _photoTypes.Where(_fileTypeHelper.ExistFileType).ToList();
            return string.Join(", ", keys);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool TryMapToContentType(string photoType, out string? contentType)
        {
            if (_photoTypes.Contains(photoType))
            {
                return _fileTypeHelper.TryMapToContentType(photoType, out contentType);
            }

            contentType = null;
            return false;
        }
    }
}
