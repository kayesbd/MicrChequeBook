using System.Web.Mvc;

namespace NusPay.Web.Areas.Transaction.Controllers
{
    public class TransactionController : Controller
    {    
        public ActionResult TransactionDetails()
        {
            return RedirectToAction("TransactionDetails", "Admin", new {area = "Administrator"});
        }        
	}
}