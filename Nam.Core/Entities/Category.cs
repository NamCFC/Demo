using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nam.Core.Entities
{
    public class Category : FullAuditedEntity<long>
    {
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
