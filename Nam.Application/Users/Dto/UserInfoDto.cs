using System;
using System.Collections.Generic;
using System.Text;
using static Nam.Core.Enum.StatusEnum;

namespace Nam.Application.Users.Dto
{
    public class UserInfoDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }

        public string JwtToken { get; set; }

        public string Role { get; set; }

    }
}
