using DinkToPdf;
using DinkToPdf.Contracts;
using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Infrastructure.DinkToPdf.Helper;

namespace RoadDefectsService.Infrastructure.DinkToPdf.Services
{
    public class PdfReportService : IReportService
    {
        private readonly IConverter _converter;

        public PdfReportService(IConverter converter)
        {
            _converter = converter;
        }

        public ExecutionResult<ReportDTO> GenerateReport(GenerateWorkReportDTO generateWorkReport)
        {
            string viewPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "RoadDefectsService.Infrastructure.DinkToPdf", "Views", "WorkReport", "WorkReport.html");

            string htmlContent = File.ReadAllText(viewPath);

            (string workVisualAssessment, string conclusion) = GetConclusion(generateWorkReport);
            Dictionary<string, string> replacements = new()
            {
                { "{createdDateOnly}", DateTime.Now.ToString("dd.MM.yyyy") },
                { "{workPlace}",  generateWorkReport.FixationDefect.ExactAddress! },
                { "{contractorOrganizationName}",  generateWorkReport.Contractor.OrganizationName },
                { "{creator}",  generateWorkReport.Creator.FullName },

                { "{assignmentId}",  generateWorkReport.AssignmentId.ToString() },
                { "{assignmentCreatedDateOnly}", generateWorkReport.CreatedAssignmentDateTime.ToLocalTime().ToString("dd.MM.yyyy") },
                { "{fixationDefectDateOnly}", generateWorkReport.FixationDefect.RecordedDateTime.ToLocalTime().ToString("dd.MM.yyyy") },
                { "{damagedCanvasSquareMeter}", generateWorkReport.FixationDefect.DamagedCanvasSquareMeter.ToString() ?? "ERROR" },
                { "{defectTypeName}", generateWorkReport.FixationDefect.DefectType!.Name },

                { "{fixationWorkDateOnly}", generateWorkReport.FixationWork.RecordedDateTime.ToLocalTime().ToString("dd.MM.yyyy") },
                { "{workVisualAssessment}",  workVisualAssessment },
                { "{conclusion}", conclusion },
            };

            htmlContent = ReplaceKeys(htmlContent, replacements);

            return new ReportDTO()
            {
                File = GeneratePdfFromHtml(htmlContent),
                Name = $"work_{generateWorkReport.FixationWork.Id}_report_dated_{DateTime.Now.ToString("dd_MM_yyyy")}",
                Type = ".pdf",
            };
        }

        private string ReplaceKeys(string htmlContent, Dictionary<string, string> replacements)
        {
            foreach(var replacement in replacements)
            {
                htmlContent = htmlContent.Replace(replacement.Key, replacement.Value);
            }

            return htmlContent;
        }

        private (string workVisualAssessment, string conclusion) GetConclusion(GenerateWorkReportDTO generateWorkReport)
        {
            string workVisualAssessment = "";
            string conclusion = "";

            if (generateWorkReport.FixationWork.WorkDone.HasValue)
            {
                if (generateWorkReport.FixationWork.WorkDoneWithDefect.HasValue && generateWorkReport.FixationWork.WorkDoneWithDefect.Value)
                {
                    workVisualAssessment = "Работы выполнены с дефектом";
                    conclusion =
                        $"Работы по устранению дефектов на {generateWorkReport.FixationDefect.ExactAddress!} выполнены. " +
                        "Однако в процессе технического осмотра были выявлены дефекты."; 
                }
                else
                {
                    workVisualAssessment = "Работы выполнены в полном объеме";
                    conclusion = 
                        $"Все работы по устранению дефектов на {generateWorkReport.FixationDefect.ExactAddress!} выполнены в полном объеме и в установленные сроки. " +
                        "Качество выполненных работ соответствует нормативным требованиям и договорным обязательствам. " +
                        "Рекомендовано принять выполненные работы и произвести оплату в соответствии с условиями договора.";
                }
            }
            else
            {
                workVisualAssessment = "Работы не выполнены";
                conclusion = 
                    $"В процессе технического осмотра было выявлено, что работы по устранению дефектов на {generateWorkReport.FixationDefect.ExactAddress!} не выполнен.";
            }

            return (workVisualAssessment, conclusion);
        }

        private byte[] GeneratePdfFromHtml(string htmlContent)
        {
            HtmlToPdfDocument pdfDocument = new()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 0, Bottom = 0, Left = 0, Right = 0 }
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = htmlContent
                    }
                }
            };

            return _converter.Convert(pdfDocument);
        }
    }
}
