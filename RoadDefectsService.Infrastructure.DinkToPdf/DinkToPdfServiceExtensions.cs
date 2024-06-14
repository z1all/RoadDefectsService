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

            //string lib = "";
            //if (AppHelper.IsLinux())
            //{
            //    lib = "libwkhtmltox.so";
            //}
            //else if (AppHelper.IsWindows())
            //{
            //    lib = "libwkhtmltox.dll";
            //}
            ////Console.WriteLine(Path.Combine(Directory.GetCurrentDirectory(), "Libs", lib));
            ////Console.WriteLine(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Libs", lib)));
            //var context = new CustomAssemblyLoadContext();
            //context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "Libs", lib));

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            return services;
        }
    }
}
