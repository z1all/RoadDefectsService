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
        /// Словарь для сопоставления типа контента для Http запроса с типом фотографии 
        /// </summary>
        public readonly Dictionary<string, string> _mappingToPhotoType;

        /// <summary>
        /// 
        /// </summary>
        public PhotoTypeHelper(IOptions<PhotoTypeOptions> options)
        {
            List<KeyValuePair<string, string>> mappings = options.Value.PhotoTypeToContentType.ToList();

            Dictionary<string, string> mappingToContentType = new();
            Dictionary<string, string> mappingToPhotoType = new();
            foreach (var mapping in mappings)
            {
                mappingToContentType.Add(mapping.Key, mapping.Value);
                mappingToPhotoType.TryAdd(mapping.Value, mapping.Key);
            }
            _mappingToContentType = mappingToContentType;
            _mappingToPhotoType = mappingToPhotoType;
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

        /// <summary>
        /// 
        /// </summary>
        public bool TryMapToPhotoType(string contentType, out string? photoType)
        {
            return _mappingToPhotoType.TryGetValue(contentType.ToLower(), out photoType);
        }
    }
}
