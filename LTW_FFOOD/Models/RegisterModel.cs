using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTW_FFOOD.Models
{
    public class RegisterModel
    {
        public string Ho { get; set; }
        public string TenDem { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}