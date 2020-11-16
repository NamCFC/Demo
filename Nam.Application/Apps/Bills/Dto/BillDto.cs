using System;
using System.Collections.Generic;
using System.Text;
using static Nam.Core.Enum.StatusEnum;

namespace Nam.Application.Apps.Bills.Dto
{
    public class BillDto
    {
        public long CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhoneNumber { get; set; }

        public long EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string AddressDelivery { get; set; }

        public bool Status { get; set; }

        public PayType PayType { get; set; }

        public decimal TotalMoney { get; set; }

        public DateTime CreatedDate { get; set; }

        public long ShopId { get; set; }

        public List<BillDetailDto> BillDetails { get; set; }
    }
}
