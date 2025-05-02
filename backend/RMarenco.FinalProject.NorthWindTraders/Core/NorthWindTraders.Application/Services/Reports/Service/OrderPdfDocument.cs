using NorthWindTraders.Application.DTOs.Order;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NorthWindTraders.Application.Reports.Service
{
    public class OrderPdfDocument(List<OrderWithDetailsDto> _orderWithDetails) : IDocument
    {
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        private readonly string _logoPath = "Images/logo.png";

        public void Compose(IDocumentContainer container)
        {
            decimal total = 0;

            byte[] logoBytes = File.ReadAllBytes(_logoPath);
            container.Page(page =>
            {
                page.Margin(30);
                // Add logo to the header
                page.Header().Row(row =>
                {
                    row.RelativeItem().AlignTop().AlignLeft().Width(100).Height(50).Image(logoBytes);
                    row.RelativeItem().AlignTop().AlignRight().Text("Order Report").FontSize(20).Bold();
                });
                page.Content().Column(col =>
                {
                    col.Spacing(16);

                    // Iterate over each order in the list
                    foreach (var orderWithDetails in _orderWithDetails)
                    {
                        col.Item().Row(row =>
                        {
                            row.Spacing(16);
                            row.RelativeItem().Border(1).BorderColor(Colors.Grey.Medium).Padding(10).Column(left =>
                            {
                                left.Item().Text(t =>
                                {
                                    t.Span("Order: ").SemiBold();
                                    t.Span(orderWithDetails.Order.OrderID.ToString());
                                });
                                left.Item().Text(t =>
                                {
                                    t.Span("Customer: ").SemiBold();
                                    t.Span(orderWithDetails.Order.Customer.CompanyName);
                                });
                                left.Item().Text(t =>
                                {
                                    t.Span("Employee: ").SemiBold();
                                    t.Span($"{orderWithDetails.Order.Employee.LastName} {orderWithDetails.Order.Employee.FirstName}");
                                });
                                left.Item().Text(t =>
                                {
                                    t.Span("Date: ").SemiBold();
                                    t.Span($"{orderWithDetails.Order.OrderDate:yyyy-MM-dd}");
                                });
                            });

                            row.RelativeItem().Border(1).BorderColor(Colors.Grey.Medium).Padding(10).Column(right =>
                            {
                                right.Item().Text(t =>
                                {
                                    t.Span("Shipper: ").SemiBold();
                                    t.Span(orderWithDetails.Order.Shipper.CompanyName);
                                });
                                right.Item().Text(t =>
                                {
                                    t.Span("Address: ").SemiBold();
                                    t.Span(orderWithDetails.Order.ShipAddress);
                                });
                                right.Item().Text(t =>
                                {
                                    t.Span("City: ").SemiBold();
                                    t.Span(orderWithDetails.Order.ShipCity);
                                });
                                right.Item().Text(t =>
                                {
                                    t.Span("Postal Code: ").SemiBold();
                                    t.Span(orderWithDetails.Order.ShipPostalCode);
                                });
                                right.Item().Text(t =>
                                {
                                    t.Span("Region: ").SemiBold();
                                    t.Span(orderWithDetails.Order.ShipRegion);
                                });
                                right.Item().Text(t =>
                                {
                                    t.Span("Ship Country: ").SemiBold();
                                    t.Span(orderWithDetails.Order.ShipCountry);
                                });
                            });
                        });

                        col.Item().LineHorizontal(1);
                        col.Item().Text("Items").FontSize(16).Bold();

                        decimal total = 0;
                        foreach (var item in orderWithDetails.OrderDetails)
                        {
                            var productTotal = item.Quantity * item.UnitPrice;
                            total += productTotal;
                            col.Item().Text($"- {item.ProductName} — {item.Quantity} x ${item.UnitPrice.ToString("F2")} = ${productTotal.ToString("F2")}");
                        }

                        col.Item().LineHorizontal(1);
                        col.Item().Text($"Total: ${total.ToString("F2")}").Bold();

                        if (orderWithDetails != _orderWithDetails.Last())
                            col.Item().PageBreak();
                    }
                });
            });
        }
    }
}
