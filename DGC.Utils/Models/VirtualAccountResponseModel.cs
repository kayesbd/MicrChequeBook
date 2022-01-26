using System;

namespace KBZ.Utils.Models
{
    public class VirtualAccountResponseModel
    {
        public string TransactionId { get; set; }
        public Nullable<long> TransactionTypeId { get; set; }
        public Nullable<long> AccountTypeId { get; set; }
        public string OrderId { get; set; }
        public string VirtualAccountNumber { get; set; }
        public Nullable<decimal> OrderAmount { get; set; }
        public Nullable<int> TransactionStatus { get; set; }
        public Nullable<long> MerchantId { get; set; }
        public Nullable<long> CustomerId { get; set; }
        public Nullable<bool> IsRequestedToVA { get; set; }
        public Nullable<System.DateTime> OrderRequestedDate { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public Nullable<long> FromBankId { get; set; }
        public Nullable<long> ToBankId { get; set; }
        public string MobileNumber { get; set; }
    }
}
