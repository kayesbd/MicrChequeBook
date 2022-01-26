using System;

namespace KBZ.Utils.Models
{
    public class TransactionUtility
    {
        public static string GetReferalId()
        {
            //var sbTransactionId = new StringBuilder();
            //string strRando = (new Random().Next(1, 9)).ToString();
            //DateTime utc = DateTime.UtcNow;
            //string nextNumber = "";
            //var refferalliList = MessageTemplateBLL.GlobalTransactionSettings["REFERRALNUMBERLIST"] as List<int>;
            //if (refferalliList != null)
            //{
            //    if (refferalliList.Count == 0)
            //    {
            //        refferalliList.Add(0);
            //    }
            //    int newReferalNumber = refferalliList.Max() + 1;
            //    if (newReferalNumber > 9999)
            //    {
            //        newReferalNumber = 1;
            //        refferalliList = new List<int> { 9999, 1 };
            //    }
            //    refferalliList.Add(newReferalNumber);
            //    sbTransactionId.AppendFormat("{0}{1}{2}{3}", utc.ToString("yy").Last().ToString(), utc.DayOfYear.ToString(),
            //    utc.ToString("HHmm").PadLeft(4, '0'), newReferalNumber.ToString().PadLeft(4, '0'));
            //    MessageTemplateBLL.GlobalTransactionSettings["REFERRALNUMBERLIST"] = refferalliList;
            //    return sbTransactionId.ToString();
            //}
            return "";
        }

        public static string GetStan()
        {
            //    var stanlist = MessageTemplateBLL.GlobalTransactionSettings["STANLIST"] as List<int>;
            //    int newStan;
            //    if (stanlist != null)
            //    {
            //        if (stanlist.Count == 0)
            //        {
            //            stanlist.Add(100000); // Stan will be started from 1000000
            //        }

            //        newStan = stanlist.Max() + 1;
            //        if (newStan > 999999)
            //        {
            //            newStan = 1;
            //            stanlist = new List<int> { 999999, 1 };
            //            stanlist.Add(newStan);
            //            return newStan.ToString(CultureInfo.InvariantCulture).PadLeft(6, '0');
            //        }
            //        stanlist.Add(newStan);
            //        MessageTemplateBLL.GlobalTransactionSettings["STANLIST"] = stanlist;
            //        return newStan.ToString(CultureInfo.InvariantCulture);
            //    }
            return "";
        }

        public static string GetTransactionIn()
        {
            string trnsId = DateTime.UtcNow.ToLongTimeString();
            return trnsId;
        }
    }
}
