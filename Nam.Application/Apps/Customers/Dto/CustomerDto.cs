using System;
using System.Collections.Generic;
using System.Text;
using static Nam.Core.Enum.StatusEnum;

namespace Nam.Application.Apps.Customers.Dto
{
    public class CustomerDto
    {
        public string FullName { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birth { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
