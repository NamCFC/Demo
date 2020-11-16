using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Application.Apps.Bills.Dto
{
    public class BillDetailDto
    {

        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }
    }
}
