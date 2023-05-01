using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Models.Account
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
