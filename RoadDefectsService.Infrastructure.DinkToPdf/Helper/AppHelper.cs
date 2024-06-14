namespace RoadDefectsService.Infrastructure.DinkToPdf.Helper
{
    public static class AppHelper
    {
        public static bool IsDebugBuild()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}
