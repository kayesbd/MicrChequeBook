using System.Collections.Generic;

namespace KBZ.Utils.Infrastructure
{
    public class UserStatus
    {
        public static bool IsCurrentStatusValid(string previousStatus, string currentStatus)
        {
            bool result = false;
            Dictionary<string, List<string>> customerStatus = new Dictionary<string, List<string>>();

            List<string> New = new List<string>();

            New.Add("New");
            New.Add("UnderReview");
            New.Add("Canceled");
            New.Add("Decliend");
            string status = "New";

            customerStatus.Add(status, New);


            List<string> Verified = new List<string>();

            Verified.Add("Verified");
            Verified.Add("UnderReview");
            Verified.Add("Canceled");
            Verified.Add("Decliend");
            status = "Verified";

            customerStatus.Add(status, Verified);

            List<string> UnderReview = new List<string>();

            UnderReview.Add("UnderReview");
            UnderReview.Add("BankVerified");
            UnderReview.Add("Canceled");
            UnderReview.Add("Decliend");
            status = "UnderReview";

            customerStatus.Add(status, UnderReview);

            List<string> BankVerified = new List<string>();

            BankVerified.Add("BankVerified");
            BankVerified.Add("Approved");
            BankVerified.Add("Canceled");
            BankVerified.Add("Decliend");
            status = "BankVerified";

            customerStatus.Add(status, BankVerified);

            List<string> Approved = new List<string>();

            Approved.Add("Approved");
            Approved.Add("Canceled");
            Approved.Add("Decliend");
            status = "Approved";

            customerStatus.Add(status, Approved);

            List<string> Canceled = new List<string>();

            Canceled.Add("UnderReview");
            Canceled.Add("Canceled");
            Canceled.Add("Decliend");
            status = "Canceled";


            customerStatus.Add(status, Canceled);
            // check to make sure the key exists, otherwise you'll get an exception.

            if (customerStatus.ContainsKey(previousStatus))
            {
                result = customerStatus[previousStatus].Contains(currentStatus);
            }
            return result;

        }

        public static bool IsCurrentStatusValidForAnonymous(string previousStatus, string currentStatus)
        {
            bool result = false;
            Dictionary<string, List<string>> customerStatus = new Dictionary<string, List<string>>();

            List<string> New = new List<string>();

            New.Add("New");
            New.Add("UnderReview");
            New.Add("Canceled");
            New.Add("Decliend");
            string status = "New";

            customerStatus.Add(status, New);


            List<string> Verified = new List<string>();

            Verified.Add("Verified");
            Verified.Add("UnderReview");
            Verified.Add("Canceled");
            Verified.Add("Decliend");
            status = "Verified";

            customerStatus.Add(status, Verified);

            List<string> UnderReview = new List<string>();

            UnderReview.Add("UnderReview");
            UnderReview.Add("Approved");
            UnderReview.Add("Canceled");
            UnderReview.Add("Decliend");
            status = "UnderReview";

            customerStatus.Add(status, UnderReview);

            List<string> Approved = new List<string>();

            Approved.Add("Approved");
            Approved.Add("Canceled");
            Approved.Add("Decliend");
            status = "Approved";

            customerStatus.Add(status, Approved);

            List<string> Canceled = new List<string>();

            Canceled.Add("UnderReview");
            Canceled.Add("Canceled");
            Canceled.Add("Decliend");
            status = "Canceled";


            customerStatus.Add(status, Canceled);
            // check to make sure the key exists, otherwise you'll get an exception.

            if (customerStatus.ContainsKey(previousStatus))
            {
                result = customerStatus[previousStatus].Contains(currentStatus);
            }
            return result;

        }
    }
}
