using System;

namespace KBZ.Utils.Models
{
    public class OnlineTransactionResponseModel
    {
        public Guid? InitialRef { get; set; }
        public string VirtualAccountNumber { get; set; }
        public string RegisteredMobileNumber { get; set; }
    }
}
