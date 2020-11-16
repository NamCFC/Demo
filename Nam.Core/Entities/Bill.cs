using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.Text;
using static Nam.Core.Enum.StatusEnum;

namespace Nam.Core.Entities
{
    public class Bill : AuditedEntity<long>
    {
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public long EmployeeId { get; set; }

        public string AddressDelivery { get; set; }

        public bool Status { get; set; }

        public PayType PayType { get; set; }

        public long ShopId { get; set; }

        public virtual Shop Shop { get; set; }

        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
