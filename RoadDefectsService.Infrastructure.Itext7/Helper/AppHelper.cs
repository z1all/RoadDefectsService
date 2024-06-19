using System.Runtime.InteropServices;

namespace RoadDefectsService.Infrastructure.Itext7.Helper
{
    public static class AppHelper
    {
        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
    }
}
