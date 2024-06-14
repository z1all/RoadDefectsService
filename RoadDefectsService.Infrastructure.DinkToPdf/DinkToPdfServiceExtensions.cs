using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.Extensions.DependencyInjection;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Infrastructure.DinkToPdf.Services;
using RoadDefectsService.Infrastructure.DinkToPdf.Helper;

namespace RoadDefectsService.Infrastructure.DinkToPdf
{
    public static class DinkToPdfServiceExtensions
    {
        public static IServiceCollection AddDinkToPdfServices(this IServiceCollection services)
        {
            services.AddScoped<IReportService, PdfReportService>();

            string path = "";
            if (AppHelper.IsDebugBuild())
            {
                path = "..\\RoadDefectsService.Infrastructure.DinkToPdf\\Libs\\libwkhtmltox.dll";
            }
            else
            {
                path = "Libs\\libwkhtmltox.dll";
            }

            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), path));

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            return services;
        }
    }
}
