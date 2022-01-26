using System;

namespace KBZ.Utils.Models
{
    public class MerchantOrderInfo
    {
        public string OrderId { get; set; }
        public DateTime? OrderRequestedDate { get; set; }
        public decimal? OrderAmount { get; set; }
    }
}
