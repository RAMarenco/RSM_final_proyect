using NorthWindTraders.Application.DTOs.Order;

namespace NorthWindTraders.Domain.Interfaces
{
    public interface IOrderReportService
    {
        Task<byte[]> GenerateOrderReportAsync(List<OrderWithDetailsDto> order);
    }
}
