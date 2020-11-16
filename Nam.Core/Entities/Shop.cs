using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nam.Core.Entities
{
    public class Shop : FullAuditedEntity<long>
    {
        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Storage> Storages { get; set; }
    }
}
