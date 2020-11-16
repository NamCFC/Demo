using Nam.Core.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Nam.Core.Enum.StatusEnum;

namespace Nam.Core.Entities
{
    public class Employee : AuditedEntity<long>
    {
        [StringLength(30)]
        public string FullName { get; set; }

        [StringLength(15)]
        public string IdentityCardNumber { get; set; }

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

        public long UserId { get; set; }
        public virtual User User { get; set; }

        public long ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
