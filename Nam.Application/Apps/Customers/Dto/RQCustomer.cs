using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Nam.Core.Enum.StatusEnum;

namespace Nam.Application.Apps.Customers.Dto
{
    public class RQCustomer
    {
        public long Id { get; set; }
        [Required]
        public string FullName { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birth { get; set; }

        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string SecCode { get; set; }
    }
}
