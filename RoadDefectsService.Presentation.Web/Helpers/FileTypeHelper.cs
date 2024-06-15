using Microsoft.Extensions.Options;
using RoadDefectsService.Presentation.Web.Configurations.File;

namespace RoadDefectsService.Presentation.Web.Helpers
{
    /// <summary>
    /// Служит для работы с типами файлов
    /// </summary>
    public class FileTypeHelper
    {
        /// <summary>
        /// Словарь для сопоставления типа файла с типом контента для Http запроса
        /// </summary>
        public readonly Dictionary<string, string> _mappingToContentType;

        /// <summary>
        /// 
        /// </summary>
        public FileTypeHelper(IOptions<FileTypeOptions> options)
        {
            List<KeyValuePair<string, string>> mappings = options.Value.FileTypeToContentType.ToList();

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
        public bool ExistFileType(string fileType)
        {
            return _mappingToContentType.ContainsKey(fileType.ToLower());
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetExistFileTypesString()
        {
            List<string> keys = _mappingToContentType.Keys.ToList();
            return string.Join(", ", keys);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="contentType"></param>
        public bool TryMapToContentType(string fileType, out string? contentType)
        {
            return _mappingToContentType.TryGetValue(fileType.ToLower(), out contentType);
        }
    }
}
