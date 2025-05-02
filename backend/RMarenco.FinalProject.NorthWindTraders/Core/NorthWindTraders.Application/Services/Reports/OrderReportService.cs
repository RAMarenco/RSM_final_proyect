using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Application.Reports.Service;
using NorthWindTraders.Domain.Interfaces;
using QuestPDF.Fluent;

namespace NorthWindTraders.Infra.Reports
{
    public class OrderReportService : IOrderReportService
    {
        public Task<byte[]> GenerateOrderReportAsync(List<OrderWithDetailsDto> orderWithDetails)
        {
            var document = new OrderPdfDocument(orderWithDetails);
            var stream = document.GeneratePdf();
            return Task.FromResult(stream);
        }
    }
}
