using iText.Html2pdf;
using iText.Kernel.Pdf;
using RoadDefectsService.Core.Application.DTOs.MetricsService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using iText.Kernel.Geom;

namespace RoadDefectsService.Infrastructure.DinkToPdf.Services
{
    public class PdfReportService : IReportService
    {

        public ExecutionResult<ReportDTO> GenerateReport(GenerateWorkReportDTO generateWorkReport)
        {
            string viewPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Views", "WorkReport", "WorkReport.html");

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
            foreach (var replacement in replacements)
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
            using (MemoryStream outputStream = new MemoryStream())
            {
                PdfWriter pdfWriter = new PdfWriter(outputStream);

                DocumentProperties documentProperties = new();

                PdfDocument pdf = new PdfDocument(pdfWriter);
                pdf.SetDefaultPageSize(new PageSize(700, 1100));

                ConverterProperties converterProperties = new ConverterProperties();

                HtmlConverter.ConvertToPdf(htmlContent, pdf, converterProperties);
                pdf.Close();

                return outputStream.ToArray();
            }
        }
    }
}
