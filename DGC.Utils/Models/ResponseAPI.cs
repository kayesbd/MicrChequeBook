using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZ.Utils.Models
{
    public class ResponseAPI
    {

        public bool IsResponseSuccess { get; set; }
        public string AccessToken { get; set; }
        public string ResponseMessage { get; set; }

    }
}
