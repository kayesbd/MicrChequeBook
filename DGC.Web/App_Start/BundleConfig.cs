using System.Web.Optimization;

namespace GITS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //#if RELEASE
            //                 BundleTable.EnableOptimizations = true;
            //#else
            //            BundleTable.EnableOptimizations = false;
            //#endif
            //            BundleTable.EnableOptimizations = false;

            BundleTable.EnableOptimizations = false;

            bundles.Add(new StyleBundle("~/Content/assets").Include(
                "~/assets/plugins/bootstrap/css/bootstrap.min.css",
                "~/assets/fonts/style.css",
                "~/assets/css/main.css",
                "~/assets/css/main-responsive.css",
                "~/assets/plugins/iCheck/skins/all.css",
                "~/assets/css/main.css",
                "~/assets/css/main-responsive.css",
                "~/assets/css/theme_navy.css",
                "~/assets/css/MyraidPro.css",
                "~/assets/plugins/bootstrap-fileupload/bootstrap-fileupload.min.css"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/assets").Include(
                "~/assets/plugins/jquery-ui/jquery-ui-1.10.2.custom.min.js",
                "~/assets/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                "~/assets/plugins/blockUI/jquery.blockUI.js",
                "~/assets/plugins/less/less-1.5.0.min.js",
                "~/assets/js/main.js"
             ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                 "~/Content/Admin.css",
                "~/Content/validation.css",
                "~/Content/intlTelInput.css",
                "~/Content/dd.css",
                "~/Content/flags.css",
                "~/Content/Site.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/Scripts/Directive").Include(
                "~/Scripts/appDirectives.js",
                "~/Scripts/utils.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/Scripts/Client").Include(
                "~/Scripts/Client/CustomerController.js",
                "~/Scripts/Client/CustomerDropdownController.js",
                "~/Scripts/Client/CustomerDetailsController.js",
                "~/Scripts/Client/CustomerHomeController.js",
                "~/Scripts/Client/MerchantHomeController.js",
                "~/Scripts/Client/MerchantPendingOrdersController.js",
                "~/Scripts/Client/MerchantCapturedOrdersController.js",
                "~/Scripts/Client/MerchantSettledOrdersController.js",
                "~/Scripts/Client/MerchantBatchTransactionsController.js",
                "~/Scripts/Client/MerchantVoidTransactionsController.js",
                "~/Scripts/Client/MerchantDeclinedOrdersController.js",
                "~/Scripts/Client/FundTransferController.js",
                "~/Scripts/Client/MerchantCategoryCodeController.js",
                "~/Scripts/Client/CustomerActivitySearchController.js",
                "~/Scripts/Client/ReceiveFundTransferController.js",
                "~/Scripts/Client/GeneratePinForSkyShopController.js",
                 "~/Scripts/Client/GeneratePinController.js",
                "~/Scripts/Client/CustomerStep2RegistrationController.js",
                "~/Scripts/Client/CustomerRenewalController.js",
                "~/Scripts/Client/NewMerchantMenuController.js",
                "~/Scripts/Client/UnderReviewMerchantMenuController.js",
                "~/Scripts/Client/ApprovedMerchantMenuController.js",
                "~/Scripts/Client/DeclinedMerchantMenuController.js",
                "~/Scripts/Client/CancelMerchantMenuController.js",
                "~/Scripts/Client/NewCustomerMenuController.js",
                "~/Scripts/Client/ApprovedCustomerMenuController.js",
                "~/Scripts/Client/DeclinedCustomerMenuController.js",
                "~/Scripts/Client/UnderReviewCustomerMenuController.js",
                "~/Scripts/Client/CancelCustomerMenuController.js",
                "~/Scripts/Client/NewMerchantMenuController.js",
                "~/Scripts/Client/UnderReviewMerchantMenuController.js",
                "~/Scripts/Client/CancelMerchantMenuController.js",
                "~/Scripts/Client/BankApprovedMerchantsMenuController.js",
                "~/Scripts/Client/DeclinedMerchantMenuController.js",
                "~/Scripts/Client/DeclinedMerchantMenuController.js",
                "~/Scripts/Client/ApprovedMerchantMenuController.js",
                "~/Scripts/Client/CustomerVirtualAccountReportController.js",
                "~/Scripts/Client/BankApprovedCustomersMenuController.js",
                "~/Scripts/Client/MerchantRefundTransactionsController.js",
                "~/Scripts/Client/FundTransferAnonymousCustomerController.js",
                "~/Scripts/Client/FundReceiverProfileController.js",
                "~/Scripts/Client/VerifiedCustomerMenuController.js",
                "~/Scripts/Client/BankVerifiedCustomerMenuController.js",
                "~/Scripts/Client/CachReceiverController.js",
                "~/Scripts/Client/CompleteTransactionReport.js",
                "~/Scripts/Client/FundTransferReportController.js",
                 "~/Scripts/Client/InstantBillPaymentReportController.js",
                 "~/Scripts/Client/MerchantVerificationController.js",
                  "~/Scripts/Client/CashOutReportController.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Scripts/Admin").Include(
                "~/Scripts/Admin/ConstantValueController.js",
                 "~/Scripts/Admin/AdminHomeController.js",
                   "~/Scripts/Admin/RequisitionController.js",
                    "~/Scripts/Admin/RequisitionListController.js",
                    "~/Scripts/SaveRegisterBeforeOTPController.js",
                     "~/Scripts/Admin/RoleController.js",
                     "~/Scripts/Admin/UserController.js",
                     "~/Scripts/Admin/CustomerAuditHistoryController.js",
                     "~/Scripts/Admin/AuditHistoryByUserController.js",
                     "~/Scripts/Admin/PasswordResetController.js",
                     "~/Scripts/Admin/RequisitionStatusController.js"


                    ));

            

            bundles.Add(new ScriptBundle("~/bundles/Scripts/Transaction").Include(
                "~/Scripts/BankAdmin/BranchController.js",
                 "~/Scripts/BankAdmin/BankBranchAddController.js"

                     ));
            
            bundles.Add(new StyleBundle("~/Content/KendoCss").Include(
                //"~/Content/styles/kendo.common.min.css",
                "~/Content/styles/kendo.common-bootstrap.min.css",
                "~/Content/styles/kendo.bootstrap.min.css",
                "~/Content/styles/kendo.rtl.min.css",
                "~/Content/styles/kendo.dataviz.min.css",
                "~/Content/styles/kendo.dataviz.default.min.css"
           ));
            //bundles.Add(new StyleBundle("~/Content/KendoCss").Include(
            //    "~/Content/styles/kendo.common.min.css",
            //    "~/Content/styles/kendo.rtl.min.css",
            //    "~/Content/styles/kendo.dataviz.min.css",
            //    "~/Content/styles/kendo.dataviz.default.min.css"
            //));

            bundles.Add(new ScriptBundle("~/Scripts/KendoJs").Include(
                 "~/Scripts/Library/kendo.all.min.js"
              ));
            //bundles.Add(new ScriptBundle("~/bundles/angularui").Include(
            //     "~/Scripts/Library/ui-grid-stable.js"                 
            //  ));
        }
    }
}
