using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nam.Application.Users.Dto
{
    public class RQUserRegister
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public long RoleId { get; set; }

        public string SecCode { get; set; }
    }
}
