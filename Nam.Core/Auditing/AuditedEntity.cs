using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nam.Core.Auditing
{
    public abstract class AuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ISoftDelete
    {
        protected AuditedEntity()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? LastModifiedDate { get; set; }
        public virtual long? LastModifiedBy { get; set; }
        public virtual DateTime? DeletedDate { get; set; }
        public virtual long? DeletedBy { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
