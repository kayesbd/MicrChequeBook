var BankApprovedMerchantsController = [
    '$scope', '$http', 'subMenuService', function($scope, $http, subMenuService) {

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

        $scope.buildTable = function() {
            var tblURL = "/Administrator/Admin/GetMerchantByStatus?Id=" + $scope.BankId + "&status=UnderReview&bankStatus=Verified";
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
                        }
                    }
                },
                pageSize: 10,
                serverPaging: true,
                serverFiltering: false,
                serverSorting: false
            });
            jQuery("#tblBankApprovedMerchantList").kendoGrid({
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
                                return "<span><a href=\#/Administrator/Admin/MerchantDetails/" + dataItem.ClientId + "\> " + dataItem.ClientId + "</a></span>";
                            }
                        },
                        width: 60
                    },
                    { field: "FullName", title: "Full Name", width: 160 },
                    { field: "MobileNumber", title: "Mobile Number", width: 110, attributes: { style: "text-align:left;" } },
                    { field: "BankAccountNo", title: "Account No", width: 80, attributes: { style: "text-align:right;" } },
                    {
                        field: "ExpireDateStr",
                        title: "Expire Date Time",
                        width: 90,
                        filterable: false,
                        sortable: false
                    },
                    { field: "CustomerStatus", title: "Status", width: 80 },
                    {
                        width: 60,
                        field: "Id",
                        title: "Edit",
                        filterable: false,
                        sortable: false,
                        template: function(dataItem) {
                            if (dataItem != null) {
                                return "<span><a href=\#/Administrator/Admin/MerchantDetails/" + dataItem.Id + "\> <i class='glyphicon glyphicon-edit'></i></a></span>";
                            }
                        }
                    }
                ]
            });
        };
        $scope.buildTable();
    }
];