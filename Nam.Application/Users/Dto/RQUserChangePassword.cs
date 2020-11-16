using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Application.Users.Dto
{
    public class RQUserChangePassword
    {
        public long UserId { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }

        public string SecCode { get; set; }
    }
}
