using System.Globalization;
using System.Threading;

namespace KBZ.Web
{
    public class CurrentCultureConfig
    {
        public static void SetCurrentCulture()
        {
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "yyyy-MMM-dd";
            newCulture.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = newCulture;
           
        }
    }
}