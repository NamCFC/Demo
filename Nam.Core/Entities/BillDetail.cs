using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Core.Entities
{
    public class BillDetail : Entity<long>
    {
        public long BillId { get; set; }
        public virtual Bill Bill { get; set; }

        public long ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
        
        public decimal Amount { get; set; }
    }
}
