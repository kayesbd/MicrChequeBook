using AutoMapper;
using KBZ.DAL.Security;
using KBZ.Administration.Domain.Model;
using KBZ.DAL.BankAdmin;

namespace KBZ.Administration.Mapping.Core
{
    public partial class AdminDomainToDatabaseProfile : Profile
    {
        protected override void Configure()
        {
        
            CreateMap<PrinterInformationModel, PrinterInformation>();
       
            CreateMap<BranchOfBankModel, BranchOfBank>();
         
        }
    }
}

