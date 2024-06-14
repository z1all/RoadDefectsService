using DinkToPdf.Contracts;
using DinkToPdf;
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

            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "..\\RoadDefectsService.Infrastructure.DinkToPdf\\Libs\\libwkhtmltox.dll"));

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            return services;
        }
    }
}
