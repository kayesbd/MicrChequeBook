using System;
using System.Linq;
using System.Collections.Generic;
using KBZ.DAL.BankAdmin;
using KBZ.Utils.Infrastructure.Contract;
using KBZ.Administration.Domain.Model;

namespace DGC.BLL
{
    public partial interface IBranchOfBankService 
    {
        List<BranchOfBankModel> GetBranchList();
        BranchOfBankModel GetBranchValue(long id);
        BranchOfBankModel InsertBranchValue(BranchOfBankModel entity);
        bool UpdateBranch(BranchOfBankModel entity);
        List<PrinterInformationModel> GetPrinterLocation();
    }



    public class BranchOfBankService : IBranchOfBankService
    {
        private readonly KBZ.DAL.BankAdmin.Repository.BranchRepository _branchRepository;
        public AutoMapper.Mapper map;
        public BranchOfBankService(KBZ.DAL.BankAdmin.Repository.BranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }


        public List<BranchOfBankModel> GetBranchList()
        {
            var branchlist = _branchRepository.GetBranchList();
            return map.Map<List<BranchOfBank>, List<BranchOfBankModel>>(branchlist);
        }

        public BranchOfBankModel GetBranchValue(long id)
        {
            var branchData = _branchRepository.GetBranchValue(id);
            return map.Map<BranchOfBank,BranchOfBankModel>(branchData);
        }



        public BranchOfBankModel InsertBranchValue(BranchOfBankModel entity)
        {
            BranchOfBank br = new BranchOfBank();
            br = map.Map<BranchOfBankModel, BranchOfBank>(entity);
            return map.Map < BranchOfBank, BranchOfBankModel > (_branchRepository.InsertBranchValue(br));
            
        }



        public List<PrinterInformationModel> GetPrinterLocation()
        {
            return map.Map<List<PrinterInformation>, List<PrinterInformationModel>>(_branchRepository.GetPrinterLocation());
        }

        public bool UpdateBranch(BranchOfBankModel entity)
        {
            var obj = map.Map<BranchOfBankModel, BranchOfBank>(entity);
            _branchRepository.UpdateBranch(obj);
            return true;
        }
    }

}

