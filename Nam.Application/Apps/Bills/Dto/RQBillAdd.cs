using System;
using System.Collections.Generic;
using System.Text;
using static Nam.Core.Enum.StatusEnum;

namespace Nam.Application.Apps.Bills.Dto
{
    public class RQBillAdd
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }

        public long EmployeeId { get; set; }

        public string AddressDelivery { get; set; }

        public bool Status { get; set; }

        public PayType PayType { get; set; }

        public long ShopId { get; set; }

        public List<RQBillDetail> RQBillDetails { get; set; }

        public string SecCode { get; set; }
    }

    public class RQBillDetail
    {
        public long BillId { get; set; }

        public long ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }
    }
}
