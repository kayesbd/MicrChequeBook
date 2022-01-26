namespace KBZ.Utils.Models
{
    public class DayClosingOptionModel
    {
        public bool recurringBill { get; set; }
        public bool interBill { get; set; }
        public bool merchantSttelment { get; set; }
        public bool feeCollection { get; set; }
        public bool reversalProccessing { get; set; }
        public bool dataBackup { get; set; }
        public bool FtpReportForBank { get; set; }
        public bool issueResult { get; set; }
        public bool renueResult { get; set; }
        public bool moneyTransferResult { get; set; }
    }
}
