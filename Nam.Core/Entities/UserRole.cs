using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Core.Entities
{
    public class UserRole : Entity<long>
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }

        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
