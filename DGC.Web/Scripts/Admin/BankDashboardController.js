var menulist = new BankMenu;
var BankDashboardController = [
    '$scope', '$http', 'subMenuService', '$routeParams', 'loginInfoService', function ($scope, $http, subMenuService, loginInfoService, $routeParams) {

    $scope.subMenuService = subMenuService;
    $scope.BankSubmenu = menulist.bankMenuList()
    if ($scope.subMenuService == undefined || $scope.subMenuService.BankId == "") {
        $scope.BankId = 0;
    } else {
        $scope.BankId = $scope.subMenuService.BankId;
    };

    if ($scope.subMenuService.BankId > 0) {
        jQuery("#bankMenu").show();
        jQuery("#bankDetails").removeClass("col-sm-12").addClass("col-sm-10");
    } else {
        $scope.marginleft = 'nomarginleft';
        jQuery("#bankMenu").hide();
        jQuery("#bankDetails").removeClass("col-sm-10").addClass("col-sm-12");
    }

    $scope.loginInfoService = loginInfoService;
        console.log($scope.loginInfoService);
    // if ($scope.loginInfoService.LoginInformation.UserType == "SystemAdmin" || $scope.loginInfoService.LoginInformation.UserType == "Customer") {

    //if ($scope.loginInfoService.LoginInformation.UserType == "SystemAdmin") {
    //    $scope.IsAdmin = true;
    //    } else {
    //    $scope.IsAdmin = false;
    //}


    //pending customer

    $scope.showPendingCustomersTable = function () {
        var tblURL = "/Administrator/Admin/GetCustomerByStatus?Id=" + $scope.BankId + "&status=New";
        var dataSource = new kendo.data.DataSource({
            type: "aspnetmvc-ajax",
            transport: {
                read: {
                    url: tblURL,
                    type: "POST"
                }
            },
            schema: {
                data: function (data) {
                    return data.Data;
                },
                total: function (data) {
                    return data.Total;
                },
                model: {
                    fields: {
                        Id: { type: "string" },
                        ClientId: { type: "string" },
                        FullName: { type: "string" },
                        MobileNumber: { type: "string" },
                        BankAccountNo: { type: "string" },
                        ExpireDate: { type: "date" },
                        CustomerStatus: { type: "string" }
                        //,
                        //BankApprovalStatus: { type: "string" }
                    }
                }
            },


                selectable: "row",
              //  dataSource: dataSource,
                filterable: {
                    extra: true
                },
                sortable: true,
                pageable: true,
                reorderable: true,
                scrollable: true,

            pageSize: 10,
            serverPaging: true,
            serverFiltering: false,


            serverSorting: false
        });
        jQuery("#tblNewCustomerList").kendoGrid({
            selectable: "row",
            dataSource: dataSource,
            filterable: {
                extra: true
            },
            sortable: true,
            pageable: true,
            reorderable: true,
            scrollable: true,
            columns: [
                { field: "Id", title: "Id", hidden: "hidden" },
                {
                    field: "ClientId", title: "Id",
                    template: function (dataItem) {
                        if (dataItem.Id != null) {
                            return "<span><a href=\#/Client/Customer/Details/" + dataItem.ClientId + "\> " + dataItem.ClientId + "</a></span>";
                        }
                    },
                    width: 60
                },
                { field: "FullName", title: "Full Name", width: 180 },
                { field: "MobileNumber", title: "Mobile Number", width: 110, attributes: { style: "text-align:left;" } },
                { field: "BankAccountNo", title: "Account No", width: 120, attributes: { style: "text-align:left;" } },
                {
                    field: "ExpireDateStr",
                    title: "Expire Date Time",
                    width: 90,
                    filterable: false,
                    sortable: false
                },

                { field: "CustomerStatus", title: "Status", width: 80 }
                //{ field: "BankApprovalStatus", title: "Bank Account Verification", width: 160 }
            ]
        });
    };






    $scope.buildTable = function () {
        var tblURL = "/Administrator/Bank/GetBankAccountInformation?Id=" + $scope.BankId;
        var dataSource = new kendo.data.DataSource({
            type: "aspnetmvc-ajax",
            transport: {
                read: {
                    url: tblURL,
                    type: "POST"
                }
            },
            schema: {
                    data: function (data) {
                    return data.Data; // <-- The result is just the data, it doesn't need to be unpacked.
                },
                    total: function (data) {
                    return data.Total; // <-- The total items count is the data length, there is no .Count to unpack.
                },
                model: {
                    fields: {
                        Id: { type: "string" },
                        ClientId: { type: "string" },
                        FullName: { type: "string" },
                        MobileNumber: { type: "string" },
                        //Designation: { type: "string" },
                        BankAccountNo: { type: "string" },
                        ExpireDateStr: { type: "string" },
                        CustomerStatus: { type: "string" }
                    }
                }
            },
            pageSize: 10,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true
        });
        jQuery("#tblBankAccountList").kendoGrid({
            selectable: "row",
            dataSource: dataSource,
            filterable: {
                extra: true
            },
            sortable: true,
            pageable: true,
            reorderable: true,
            scrollable: true,
            columns: [
                { field: "Id", title: "Id", hidden: "hidden" },
                {
                        field: "ClientId",
                        title: "Id",
                        template: function (dataItem) {
                        if (dataItem.Id != null) {
                            return "<span><a href=\#/Client/Customer/Details/" + dataItem.ClientId + "\> " + dataItem.ClientId + "</a></span>";
                        }
                    },
                     width: 60
                },
                { field: "FullName", title: "Full Name", width: 110 },
                { field: "MobileNumber", title: "Mobile Number", width: 130, attributes: { style: "text-align:left;" } },
                    { field: "Designation", title: "Designation", width: 85, filterable: false },
                { field: "BankAccountNo", title: "Account No", width: 110, attributes: { style: "text-align:left;" } },
                {
                    field: "ExpireDateStr",
                        title: "Expire Date",
                    type: "string",
                        width: 110,
                    filterable: false,
                    sortable: false,
                    format: "{0:MMM-dd-yyyy }",
                    parseFormats: ["yyyy-MM-dd "]
                },
                    { field: "CustomerStatus", title: "Status", width: 70, filterable: false },

            {
                        width: 90,
                field: "ClientId",
                        title: "Audit History",
                attributes: { style: "text-align: center;" },
                filterable: false,
                sortable: false,
                    template: function (dataItem) {
                    if (dataItem != null) {
                        return "<span><a href=\#/Administrator/Admin/CustomerAuditHistory/" + dataItem.ClientId + "\> <i class=\"glyphicon glyphicon-th-list\"></i></a></span>";
                    }
                }
                }
            ]
        });
    };
    $scope.buildTable();

   

    // Recent transaction
        var TransactionId = '',
    TransactionTypeId = '',
    TransactionStatusId = '',
    MerchantId = '',
    CompanyName = '',
    CompanyShortName = '',
    FromBankCode = '',
    CustomerId = '',
    CustomerMobileNumber = '',
    CustomerEmail = '',
    CustomerFirstName = '',
    CustomerLastName = '',
    FromDateTime = "04/22/2015",
    ToDateTime = "04/22/2015";

   // + "&fromDateTime=" + jQuery("#fromDateTime").val() + "&toDateTime=" + jQuery("#toDateTime").val();

    $scope.showRecentSuccessfulTransactionTable = function () {

      //  ValidateInput();
        var tblURL = "/api/ApiTransaction/RecentTransactions?status=success";
        var dataSource = new kendo.data.DataSource({
            type: "aspnetmvc-ajax",
            transport: {
                read: {
                    url: tblURL,
                    type: "POST"
                }
            },
            schema: {
                data: function (data) {
                    return data.Data;
                },
                total: function (data) {
                    return data.Total;
                },
                model: {
                    fields: {
                        Id: { type: "string" },

                    }
                }
            },
          
            pageSize: 20,
            serverPaging: true,
            serverFiltering: false,
            serverSorting: true,
            

            //filter: 
            //    //{field: "TransactionStatusName", operator: "eq", value: "Authorized"},
            //    { field: "TransactionStatusName", operator: "eq", value: "Settled " }
            
        

        });



        jQuery("#tblTransactionSearchResult").kendoGrid({
            selectable: "row",
           
            dataSource: dataSource,
            //dataBound: gridDataBound,
            filterable: {
                extra: true
            },
            sortable: false,
            pageable: false,
            reorderable: true,
            scrollable: true,
            columns: [
                { field: "Id", title: "Id", hidden: "hidden" },
                {
                    field: "TransactionId",
                    title: "Transaction Id",
                    width: 130,
                    filterable: false,
                    sortable: false,
                    template: function (dataItem) {
                        if (dataItem.TransactionId == null) {
                            return "<span><a title=\"Go for Transaction Details\" class=\"column-highlight\" href=\#/Transaction/Transaction/TransactionDetails/" + dataItem.Id + "\><i class='glyphicon glyphicon-edit'></i></a></span>";
                        } else {
                            return "<span><a title=\"Go for Transaction Details\" class=\"column-highlight\" href=\#/Transaction/Transaction/TransactionDetails/" + dataItem.Id + "\>" + dataItem.TransactionId + "</a></span>";
                        }
                    }
                },
                {
                    field: "StrDateCreated",
                    title: "Transaction Date Time",
                    type: "string",
                    width: 160,
                    filterable: false,
                    sortable: false,
                },
                { field: "TransactionTypeName", title: "Transaction Type", width: 120, filterable: false, sortable: false, },
                { field: "TransactionStatusName", title: "Transaction Status", width: 100, filterable: false, sortable: false, },

               {
                   field: "CurrencyName",
                   title: "Currency",
                   width: 50,
                   filterable: false,
                   sortable: false,
               },
                {

                    //title: "Amount",
                    //width: 100,
                    //filterable: false,
                    //sortable: false,
                    //attributes: { style: "text-align:right;" },
                    //template: "#= '(' +CurrencyName + ') ' + OrderAmount #"

                    field: "OrderAmount",
                    title: "Amount",
                    format: '{0:n2}',
                    width: 50,
                    filterable: false,
                    sortable: false,
                    attributes: { style: "text-align:right;" }
                }
                //{
                //    title: "Amount",
                //    width: 100,
                //    filterable: false,
                //    sortable: false,
                //    attributes: { style: "text-align:right;" },
                //    template: "#= '(' +CurrencyName + ') ' + OrderAmount #"
                //}




            ]

        });

    };


    //Unsuccessful transactions 

    $scope.showRecentUnsuccessfulTransactionTable = function () {

        //  ValidateInput();
        var tblURL = "/api/ApiTransaction/RecentTransactions?status=";
        var dataSource = new kendo.data.DataSource({
            type: "aspnetmvc-ajax",
            transport: {
                read: {
                    url: tblURL,
                    type: "POST"
                }
            },
            schema: {
                data: function (data) {
                    return data.Data;
                },
                total: function (data) {
                    return data.Total;
                },
                model: {
                    fields: {
                        Id: { type: "string" },

                    }
                }
            },
            pageSize: 20,
            serverPaging: true,
            serverFiltering: false,
            serverSorting: true,

            //filter: [
            //    {field: "TransactionStatusName", operator: "neq", value: "Authorized"},
            //    {field: "TransactionStatusName", operator: "neq", value: "Settled "
            //}]


        });



        jQuery("#showRecentUnsuccessfulTransactionTable").kendoGrid({
            selectable: "row",

            dataSource: dataSource,
            //dataBound: gridDataBound,
            filterable: {
                extra: true
            },
            sortable: false,
            pageable: false,
            reorderable: true,
            scrollable: true,
            columns: [
                { field: "Id", title: "Id", hidden: "hidden" },
                {
                    field: "TransactionId",
                    title: "Transaction Id",
                    width: 130,
                    filterable: false,
                    sortable: false,
                    template: function (dataItem) {
                        if (dataItem.TransactionId == null) {
                            return "<span><a title=\"Go for Transaction Details\" class=\"column-highlight\" href=\#/Transaction/Transaction/TransactionDetails/" + dataItem.Id + "\><i class='glyphicon glyphicon-edit'></i></a></span>";
                        } else {
                            return "<span><a title=\"Go for Transaction Details\" class=\"column-highlight\" href=\#/Transaction/Transaction/TransactionDetails/" + dataItem.Id + "\>" + dataItem.TransactionId + "</a></span>";
                        }
                    }
                },
                {
                    field: "StrDateCreated",
                    title: "Transaction Date Time",
                    type: "string",
                    width: 160,
                    filterable: false,
                    sortable: false,
                },
                { field: "TransactionTypeName", title: "Transaction Type", width: 120, filterable: false, sortable: false, },
                { field: "TransactionStatusName", title: "Transaction Status", width: 100, filterable: false, sortable: false, },

               {
                   field: "CurrencyName",
                   title: "Currency",
                   width: 50,
                   filterable: false,
                   sortable: false,
               },
                {

                    //title: "Amount",
                    //width: 100,
                    //filterable: false,
                    //sortable: false,
                    //attributes: { style: "text-align:right;" },
                    //template: "#= '(' +CurrencyName + ') ' + OrderAmount #"

                    field: "OrderAmount",
                    title: "Amount",
                    format: '{0:n2}',
                    width: 50,
                    filterable: false,
                    sortable: false,
                    attributes: { style: "text-align:right;" }
                }
                //{
                //    title: "Amount",
                //    width: 100,
                //    filterable: false,
                //    sortable: false,
                //    attributes: { style: "text-align:right;" },
                //    template: "#= '(' +CurrencyName + ') ' + OrderAmount #"
                //}




            ]

        });

    };

    if ($routeParams != undefined && typeof $routeParams.Id == 'undefined') {
        console.log("Bank dashboard");
        $scope.showPendingCustomersTable();
        $scope.showRecentSuccessfulTransactionTable();
        $scope.showRecentUnsuccessfulTransactionTable();

    }
    jQuery(document).ready(function () {
        jQuery("#tabstrip").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            }
        });
    });
}
];