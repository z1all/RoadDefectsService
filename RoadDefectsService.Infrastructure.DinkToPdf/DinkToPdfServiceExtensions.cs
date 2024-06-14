using Microsoft.Extensions.DependencyInjection;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Infrastructure.DinkToPdf.Services;

namespace RoadDefectsService.Infrastructure.DinkToPdf
{
    public static class DinkToPdfServiceExtensions
    {
        public static IServiceCollection AddDinkToPdfServices(this IServiceCollection services)
        {
            services.AddScoped<IReportService, PdfReportService>();

            return services;
        }
    }
}
