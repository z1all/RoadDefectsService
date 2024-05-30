namespace RoadDefectsService.Presentation.Web.Middlewares.Extensions
{
    public static class CommonMiddlewareExtensions
    {
        public static void UseExceptionsHandler(this WebApplication webApplication)
        {
            webApplication.UseMiddleware<ExceptionsHandlerMiddleware>();
        }
    }
}
