using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nam.Core.Entities
{
    public class Product : FullAuditedEntity<long>
    {
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string ImageUrl { get; set; }

        public decimal? Price { get; set; }

        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<BillDetail> BillDetails { get; set; }
        public virtual ICollection<Storage> Storages { get; set; }
    }
}
