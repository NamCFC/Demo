using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Core.Entities
{
    public class Storage : FullAuditedEntity<long>
    {
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }

        public long ShopId { get; set; }
        public virtual Shop Shop { get; set; }

        public int Quantity { get; set; }
    }
}
