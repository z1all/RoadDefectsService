using DinkToPdf;
using DinkToPdf.Contracts;
using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Infrastructure.DinkToPdf.Services
{
    public class ReportService : IReportService
    {
        private readonly IConverter _converter;

        public ReportService(IConverter converter)
        {
            _converter = converter;
        }

        public ExecutionResult<ReportDTO> GenerateReport(GenerateWorkReportDTO generateWorkReport)
        {
            string htmlContent = "<html><body><h1>Hello, World!</h1><p>This is a PDF document generated from HTML!</p></body></html>";

            var pdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = htmlContent
                    }
                }
            };

            byte[] pdf = _converter.Convert(pdfDocument);

            return new ReportDTO()
            {
                File = pdf,
                Name = "report",
                Type = ".pdf",
            };
        }
    }
}
