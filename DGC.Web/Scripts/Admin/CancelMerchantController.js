var menulist = new BankMenu;
var CancelMerchantController = [
    '$scope', '$http', 'subMenuService', function($scope, $http, subMenuService) {
        $scope.subMenuService = subMenuService;
        $scope.BankSubmenu = menulist.bankMenuList();
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

        $scope.buildTable = function() {
            var tblURL = "/Administrator/Admin/GetMerchantByStatus?Id=" + $scope.BankId + "&status=Canceled";
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
                            CompanyName: { type: "string" },
                            CompanyEmail: { type: "string" },
                            CompanyShortName: { type: "string" },
                            TradeLicenseNo: { type: "string" },
                        }
                    }
                },
                
                pageSize: 10,
                serverPaging: true,
                serverFiltering: false,
                serverSorting: false
            });
            jQuery("#tblCancelMerchantList").kendoGrid({
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
                    {
                        field: "Id",
                        title: "Id",
                        width: 80,
                        template: function(dataItem) {

                            if (typeof dataItem.Id == "string") {
                                return "<span><a href=\#/Administrator/Admin/MerchantDetails/" + dataItem.Id + "\>" + dataItem.Id + "</a></span>";
                            }
                        },
                    },
                    { field: "CompanyName", title: "Company Name", width: 180 },
                    { field: "CompanyEmail", title: "Email", width: 160 },
                    { field: "CompanyShortName", title: "Short Name", width: 140 },
                    { field: "TradeLicenseNo", title: "Trade License No", width: 180 },
                    { field: "ProductsOrServicesDetails", title: "Services", width: 110, sortable: false, filterable: false },
                    {
                        field: "Id",
                        title: "Edit",
                        width: 60,
                        sortable: false,
                        filterable: false,
                        template: function(dataItem) {
                            if (typeof dataItem.Id == "string") {
                                return "<span><a href=\#/Administrator/Admin/MerchantDetails/" + dataItem.Id + "\> <i class='glyphicon glyphicon-edit'></i></a></span>";
                            }
                        },
                    },
                    {
                        field: "Id",
                        title: "Allow Transaction Type",
                        width: 200,
                        sortable: false,
                        filterable: false,
                        template: function(dataItem) {
                            if (typeof dataItem.Id == "string") {
                                return "<span><a href=\#/Administrator/Admin/AllowTransactionType/" + dataItem.Id + "\> <i class='glyphicon glyphicon-edit'></i></a></span>";
                            }
                        },
                    }
                    //,
                    //{
                    //    field: "Id",
                    //    title: "Delete",
                    //    width: 60,
                    //    sortable: false,
                    //    filterable: false,
                    //    template: function (dataItem) {
                    //        if (typeof dataItem.Id == "string") {
                    //            return "<span><a href=\#/Administrator/Admin/DeleteMerchantDetails/" + dataItem.Id + "\> <i class='glyphicon glyphicon-remove'></i></a></span>";
                    //        }
                    //    }
                    //}
                ]
            });
        };
        $scope.buildTable();
    }
];