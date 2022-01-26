using KBZ.Utils.Infrastructure;
using KBZ.Utils.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZ.DAL.BankAdmin.Repository
{
    public interface IPrinterInformationRepository
    {
        List<PrinterInformation> GetAll();
        PrinterInformation GetPrinterInformationById(long? Id);
       
    }

    public class PrinterInformationRepository : IPrinterInformationRepository
    {
        DCBankAdminContext context = new DCBankAdminContext();
        AutoMapper.Mapper map;
        public PrinterInformationRepository()
        { 
        }

        public List<PrinterInformation> GetAll()
        {
            return context.PrinterInformations.ToList();
        }

        public PrinterInformation GetPrinterInformationById(long? branchId)
        {
            return context.PrinterInformations.Where(x=>x.Id==branchId.Value).FirstOrDefault();
        }
        public PrinterInformation SavePrinter(PrinterInformation model)
        {
            context.PrinterInformations.Add(model);
            context.SaveChanges();
            return model;
        }
        public Requisition SaveRequisition(Requisition model)
        {
            var printerInfo = this.GetPrinterInformationById(model.PickupBranchId);
            model.PrinterId = printerInfo.Id;
            model.OrderDateTime = DateTime.Now;
            model.CreatedDate = DateTime.Now;
            context.Requisitions.Add(model);
            
            context.SaveChanges();
            return model;
        }
        public PrinterInformation GetPrinterInformationByBranch(string RequisitionBranch)
        {
            PrinterInformation printer = null;
            var printerId = context.BranchOfBanks.Where(x => x.BranchCode == RequisitionBranch).FirstOrDefault().PrinterId;
            if (printerId != null)
            {
                printer= context.PrinterInformations.Where(x => x.Id == printerId).FirstOrDefault();
            }
            return printer;
        }
      
    }
}
