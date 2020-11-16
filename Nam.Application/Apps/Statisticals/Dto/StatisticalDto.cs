using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Application.Apps.Statisticals.Dto
{
    public class StatisticalDto
    {
        public decimal TotalRevenue { get; set; }

        public List<StatisticalShopDto> StatisticalShops { get; set; }

        public List<StatisticalProductDto> StatisticalProducts { get; set; }
    }

    public class StatisticalShopDto
    {
        public int QuantityProductsSold { get; set; }

        public decimal TotalRevenue { get; set; }

        public string ShopAddress { get; set; }
    }

    public class StatisticalProductDto
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}
