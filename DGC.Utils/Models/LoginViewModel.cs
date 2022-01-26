using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBZ.Utils.Models
{
    public class LoginViewModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenValidityPeriod { get; set; }
        public string FullName { get; set; }
    }
}