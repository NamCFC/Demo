using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Core.Auditing
{
    public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, ISoftDelete
    {
        protected FullAuditedEntity()
        {

        }

        public virtual long? CreatedBy { get; set; }
    }
}
