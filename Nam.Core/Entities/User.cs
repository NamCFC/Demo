using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Nam.Core.Enum.StatusEnum;

namespace Nam.Core.Entities
{
    public class User : AuditedEntity<long>
    {
        [StringLength(20)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
