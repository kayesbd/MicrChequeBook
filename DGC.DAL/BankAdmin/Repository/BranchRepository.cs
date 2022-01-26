
using KBZ.Utils.Infrastructure;
using KBZ.Utils.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZ.DAL.BankAdmin.Repository
{
    public interface IBranchRepository 
    {
        List<BranchOfBank> GetBranchList();
        BranchOfBank GetBranchValue(long id);
        BranchOfBank InsertBranchValue(BranchOfBank entity);
        void UpdateBranch(BranchOfBank entity);
        List<PrinterInformation> GetPrinterLocation();

    }

    public class BranchRepository :  IBranchRepository
    {
        private readonly DCBankAdminContext _dbContextBankAdmin = new DCBankAdminContext();

        public BranchRepository() 
        {
        }
        public List<BranchOfBank> GetBranchList()
        {
            return _dbContextBankAdmin.BranchOfBanks.ToList();
        }

        public BranchOfBank GetBranchValue(long id)
        {
            return _dbContextBankAdmin.BranchOfBanks.Where(x=>x.Id==id).FirstOrDefault();
        }

        public BranchOfBank InsertBranchValue(BranchOfBank entity)
        {
           var br= _dbContextBankAdmin.BranchOfBanks.Add(entity);
            _dbContextBankAdmin.SaveChanges();
            return br;
        }

        public void UpdateBranch(BranchOfBank entity)
        {
            _dbContextBankAdmin.Entry(entity).State = EntityState.Modified;
            _dbContextBankAdmin.SaveChanges();
        }
        public List<PrinterInformation> GetPrinterLocation()
        {
            return _dbContextBankAdmin.PrinterInformations.ToList();
        }
    }
}
