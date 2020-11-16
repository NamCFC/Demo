using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Nam.Core.Enum.StatusEnum;

namespace Nam.Core.Entities
{
    public class Customer : FullAuditedEntity<long>
    {
        [StringLength(30)]
        public string FullName { get; set; }

        [Column(TypeName = "tinyint")]
        public Gender Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birth { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
