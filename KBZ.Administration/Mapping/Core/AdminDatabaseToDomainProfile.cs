using AutoMapper;

using KBZ.DAL.Security;
using KBZ.Administration.Domain.Model;
using NusPay.Security.Domain.Model;
using System.Collections.Generic;
using KBZ.DAL.BankAdmin;

namespace KBZ.Administration.Mapping.Core
{
    public partial class AdminDatabaseToDomainProfile : Profile
    {
        protected override void Configure()
        {
            { }
            CreateMap<Role, RoleModel>();
            CreateMap<UserInformation, UserInformationModel>();
            CreateMap<USP_GetUsers_Result, UserInformationModel>();
            CreateMap<Menu, MenuModel>();
            CreateMap<RoleWiseMenu, RoleWiseMenuModel>();
            CreateMap<Page, PageModel>();
            CreateMap<USP_GetUserInformationBySearch_Result, UserInformationModel>();
            ///CreateMap<List<BranchOfBank>, List<BranchOfBankModel>>();
            CreateMap<BranchOfBank, BranchOfBankModel>();
            CreateMap<USP_GetUsersByPagination_Result, UserInformationModel>();
            CreateMap<USP_GetRoleListByPagination_Result, RoleModel>();
            //CreateMap<USP_GetMerchantAuditHistory_Result, MerchantAuditHistory>();
            CreateMap<PersonalInformation, PersonalInformationModel>();
            //CreateMap<PersonalInformation, PersonalInformation_Audit>();
            //CreateMap<UserPINInformation, UserPINInformationModel>();
            CreateMap<USP_GetUserInformationForForgotPassword_Result, UserInformationModel>();
            CreateMap<PrinterInformation, PrinterInformationModel>();
        }
    }
}

