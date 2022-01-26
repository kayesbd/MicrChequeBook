using DGC.BLL;
using KBZ.Administration.Domain.Model;
using KBZ.DAL.BankAdmin;

using KBZ.BLL.Security;
using KBZ.Utils.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KBZ.Web.Areas.Administrator.Controllers
{
    public class BranchController : Controller
    {
        // GET: Branch
        public BranchOfBankService branchService;
        //public BranchController(BranchOfBankService _branchService)
        //{
        //    branchService = _branchService;
        //}
        public BranchController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            branchService = kernel.GetService(typeof(IBranchOfBankService)) as BranchOfBankService;
        }
        public ActionResult BranchList()
        {
            return PartialView();
        }

        public JsonResult BranchListForTable()
        {
          

            var branchList = branchService.GetBranchList();
            var json= Json(branchList,JsonRequestBehavior.AllowGet);
            return json;
        }

        // GET: Branch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Branch/Create
        public ActionResult BranchCreate()
        {
            KBZ.Administration.Domain.Model.BranchOfBankModel br = new BranchOfBankModel();
            return PartialView(br);
        }
        public JsonResult GetPrinterLocation()
        {
            try
            {
         
                var data = branchService.GetPrinterLocation();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                FileLogger.Info(ex.Message);
            }
            return null;
        }


        public ActionResult CreateBranch(BranchOfBankModel brModel)
        {
            try
            {
              
                branchService.InsertBranchValue(brModel);
                return RedirectToAction("BranchList");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // GET: Branch/Edit/5
        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpGet]
        public ActionResult UpdateBranch(int id)
        {
           
            return PartialView("BranchCreate");
        }
        public ActionResult InsertUpdatedBranchData(BranchOfBankModel branchData)
        {
            var res= branchService.UpdateBranch(branchData);
            if(res==true)
            {
                return RedirectToAction("BranchList");
            }else
            { return null; }
             
        }
        public JsonResult GetUpdateData(int id)
        {
            KBZ.Administration.Domain.Model.BranchOfBankModel br = new BranchOfBankModel();
            br = branchService.GetBranchValue(id);
            return Json(br, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult ToggleStatus(string  id)
        {
            return View();
        }
        
        // POST: Branch/Edit/5

        public ActionResult BranchUpdateByChangedData(int id, BranchOfBankModel model)
        {
            try
            {
                // TODO: Add update logic here
                branchService.UpdateBranch(model);

                return RedirectToAction("BranchList");
            }
            catch
            {
                return View();
            }
        }

        // GET: Branch/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Branch/Delete/5
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
