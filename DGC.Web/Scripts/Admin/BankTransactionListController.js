var BankTransactionListController = [
    '$scope', '$http', function($scope, $http) {
        //util.Menu();

        $scope.buildTable = function() {
            var dataSourceURL = "/Administrator/Bank/GetTransactionList";
            var dataSource = new kendo.data.DataSource({
                type: "aspnetmvc-ajax",
                transport: {
                    read: {
                        url: dataSourceURL,
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
                            CustomerId: { type: "int" },
                            CustomerName: { type: "string" },
                            MerchantId: { type: "int" },
                            CompanyName: { type: "string" },
                            TransactionId: { type: "int" },
                            OrderId: { type: "int" },
                            OrderAmount: { type: "decimal" },
                            TransactionStatus: { type: "int" },
                            OrderRequestedDate: { type: "string" },
                            TransactionTypeName: { type: "string" },
                            AccountTypeName: { type: "string" },
                            CurrencyCode: { type: "string" },
                        }
                    }
                },
                sort: [
                    { field: "customerId", dir: "asc" },
                    { field: "CustomerName", dir: "asc" },
                    { field: "MerchantId", dir: "asc" },
                    { field: "CompanyName", dir: "asc" },
                    { field: "TransactionId", dir: "asc" },
                    { field: "OrderId", dir: "asc" },
                    { field: "OrderAmount", dir: "asc" },
                    { field: "TransactionStatus", dir: "asc" },
                    { field: "OrderRequestedDate", dir: "asc" },
                    { field: "TransactionTypeName", dir: "asc" },
                    { field: "AccountTypeName", dir: "asc" },
                    { field: "CurrencyCode", dir: "asc" }
                ],
                pageSize: 10,
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true,
            });

            jQuery("#tblTransactionList").kendoGrid({
                selectable: "row",
                dataSource: dataSource,
                filterable: {
                    extra: true
                },
                sortable: true,
                pageable: true,
                reorderable: true,
                scrollable: true,
                groupable: true,
                columns: [
                    { field: "Id", title: "Id", hidden: "hidden" },
                    { field: "customerId", title: "Customer Id", hidden: "hidden" },
                    { field: "CustomerName", title: "Customer Name", width: 180 },
                    { field: "MerchantId", title: "Merchant Id", hidden: "hidden" },
                    { field: "CompanyName", title: "Merchant", width: 180 },
                    { field: "TransactionId", title: "Transaction Id", width: 150 },
                    { field: "OrderId", title: "Order Id", width: 120 },
                    { field: "OrderAmount", title: "Order Amount", width: 150 },
                    { field: "TransactionStatus", title: "Status", filterable: false, width: 150 },
                    {
                        field: "OrderRequestedDate",
                        title: "Request Date",
                        filterable: false,
                        width: 150,
                        template: function(dataItem) {
                            if (dataItem.OrderRequestedDate == undefined || dataItem.OrderRequestedDate == null) {
                                return "";
                            } else {
                                var date1 = kendo.parseDate(dataItem.OrderRequestedDate, "yyyy/MM/dd");
                                return kendo.toString(date1, "dd-MMM-yyyy hh:mmtt");
                            }
                        }
                    },
                    { field: "TransactionTypeName", title: "Transaction Type", width: 150 },
                    { field: "AccountTypeName", title: "Account Type", width: 150, hidden: "hidden" },
                    { field: "CurrencyCode", title: "Currency", width: 150 }
                ]
            });
        };
        $scope.buildTable();
    }
];