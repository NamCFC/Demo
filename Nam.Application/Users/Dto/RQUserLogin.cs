using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Application.Users.Dto
{
    public class RQUserLogin
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }

        public string SecCode { get; set; }
    }
}
