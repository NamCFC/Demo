using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Core.Entities
{
    public class Role : FullAuditedEntity<long>
    {
        public string DisplayName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
