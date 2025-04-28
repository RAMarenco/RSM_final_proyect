using System;
using System.Collections.Generic;

namespace NorthWindTraders.Infra.Persistence.Models;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
