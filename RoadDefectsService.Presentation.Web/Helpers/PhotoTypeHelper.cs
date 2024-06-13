using Microsoft.Extensions.Options;
using RoadDefectsService.Presentation.Web.Configurations.Photo;

namespace RoadDefectsService.Presentation.Web.Helpers
{
    /// <summary>
    /// Служит для работы с типами фотографий
    /// </summary>
    public class PhotoTypeHelper
    {
        /// <summary>
        /// Словарь для сопоставления типа фотографии с типом контента для Http запроса
        /// </summary>
        public readonly Dictionary<string, string> _mappingToContentType;

        /// <summary>
        /// 
        /// </summary>
        public PhotoTypeHelper(IOptions<PhotoTypeOptions> options)
        {
            List<KeyValuePair<string, string>> mappings = options.Value.PhotoTypeToContentType.ToList();

            Dictionary<string, string> mappingToContentType = new();
            foreach (var mapping in mappings)
            {
                mappingToContentType.Add(mapping.Key, mapping.Value);
            }
            _mappingToContentType = mappingToContentType;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ExistPhotoType(string photoType)
        {
            return _mappingToContentType.ContainsKey(photoType.ToLower());
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetExistPhotoTypeString()
        {
            List<string> keys = _mappingToContentType.Keys.ToList();
            return string.Join(", ", keys);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool TryMapToContentType(string photoType, out string? contentType)
        {
            return _mappingToContentType.TryGetValue(photoType.ToLower(), out contentType);
        }
    }
}
