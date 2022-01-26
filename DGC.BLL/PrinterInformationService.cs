using DGC.BLL.Admin.Model;
using KBZ.Administration.Domain.Model;
using KBZ.DAL.BankAdmin;
using KBZ.DAL.BankAdmin.Repository;
using KBZ.Utils.Caching;
using KBZ.Utils.Infrastructure;
using KBZ.Utils.Infrastructure.Contract;
using System.Collections.Generic;

namespace DGC.BLL
{


   
    public class PrinterInformationService 
    {
        private PrinterInformationRepository _printerInformationRepository;
        private AutoMapper.Mapper map;
        public PrinterInformationService()
            
        {
            this._printerInformationRepository = new PrinterInformationRepository();
        }

        public PrintRequesition SaveRequistion(PrintRequesition printerInfoModel)
        {
            var mappingObject = map.Map<PrintRequesition, Requisition>(printerInfoModel);
            var savedObject = _printerInformationRepository.SaveRequisition(mappingObject);
            return map.Map<Requisition, PrintRequesition>(savedObject);
            
        }
    }
}
