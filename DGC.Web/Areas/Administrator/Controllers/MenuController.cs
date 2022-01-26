using System.Web.Mvc;

namespace NusPay.Web.Areas.Administrator.Controllers
{

    public class MenuController : Controller
    {
        //
        // GET: /Administrator/SecMenu/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Administrator/SecMenu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Administrator/SecMenu/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Administrator/SecMenu/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Administrator/SecMenu/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Administrator/SecMenu/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Administrator/SecMenu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Administrator/SecMenu/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
