var menulist = new BankMenu;
var ActiveCustomerController = [
    '$scope', '$http', 'subMenuService', 'loginInfoService', function($scope, $http, subMenuService, loginInfoService) {
        $scope.BankSubmenu = menulist.bankMenuList();
        $scope.subMenuService = subMenuService;

        if ($scope.subMenuService == undefined || $scope.subMenuService.BankId == "") {
            $scope.BankId = 0;
        } else {
            $scope.BankId = $scope.subMenuService.BankId;
        };

        if ($scope.subMenuService.BankId > 0) {
            jQuery("#bankMenu").show();
            jQuery("#bankDetails").removeClass("col-sm-12").addClass("col-sm-10");
        } else {
            jQuery("#bankMenu").hide();
            jQuery("#bankDetails").removeClass("col-sm-10").addClass("col-sm-12");
        }

        $scope.loginInfoService = loginInfoService;
        console.log($scope.loginInfoService);
        // if ($scope.loginInfoService.LoginInformation.UserType == "SystemAdmin" || $scope.loginInfoService.LoginInformation.UserType == "Customer") {

        if ($scope.loginInfoService.LoginInformation.UserType == "SystemAdmin") {
            $scope.IsAdmin = true;
        } else {
            $scope.IsAdmin = false;
        }

        $scope.buildTable = function() {
            var tblURL = "/Administrator/Admin/GetCustomerByStatus?Id=" + $scope.BankId + "&status=Approved";
            var dataSource = new kendo.data.DataSource({
                type: "aspnetmvc-ajax",
                transport: {
                    read: {
                        url: tblURL,
                        type: "POST"
                    }
                },
                schema: {
                    data: function(data) {
                        return data.Data;
                    },
                    total: function(data) {
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
                            //BankApprovalStatus: { type: "string" }
                        }
                    }
                },
                pageSize: 10,
                serverPaging: true,
                serverFiltering: false,
                serverSorting: false
            });
            jQuery("#tblActiveCustomerList").kendoGrid({
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
                        template: function(dataItem) {
                            if (dataItem.Id != null) {
                                return "<span><a href=\#/Client/Customer/Details/" + dataItem.ClientId + "\> " + dataItem.ClientId + "</a></span>";
                            }
                        },
                        width: 60
                    },
                    { field: "FullName", title: "Full Name", width: 150 },
                    { field: "MobileNumber", title: "Mobile Number", width: 100, attributes: { style: "text-align:left;" } },
                    { field: "BankAccountNo", title: "Account No", width: 100, attributes: { style: "text-align:right;" } },
                    {
                        field: "ExpireDateStr",
                        title: "Expire Date Time",
                        width: 130,
                        filterable: false,
                        sortable: false
                    },
                    { field: "CustomerStatus", title: "Status", width: 60 }
                    //{ field: "BankApprovalStatus", title: "Bank Account Verification", width: 160 }
                ]
            });
        };
        $scope.buildTable();
    }
];