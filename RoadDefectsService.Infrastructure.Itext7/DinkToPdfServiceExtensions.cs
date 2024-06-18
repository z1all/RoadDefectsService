using Microsoft.Extensions.DependencyInjection;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Infrastructure.Itext7.Services;

namespace RoadDefectsService.Infrastructure.Itext7
{
    public static class DinkToPdfServiceExtensions
    {
        public static IServiceCollection AddItext7Services(this IServiceCollection services)
        {
            services.AddScoped<IReportService, PdfReportService>();

            return services;
        }
    }
}
