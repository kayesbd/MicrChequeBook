var util = new GITS.Utils();
util.DbManager('adminFactory', "/Api/ApiAdmin/");

var UserSearchController = [
    '$scope', 'adminFactory', '$routeParams', '$location', '$http', function($scope, adminFactory, $routeParams, $location, $http) {
        $http({
            method: 'GET',
            url: '/api/ApiList/BankListByUserType',
        }).success(function(result) {
            $scope.bankList = result;
        });

        $http({
            method: 'GET',
            url: '/api/ApiList/UserTypeList',
        }).success(function(result) {
            $scope.userTypeList = result;
        });

        if ($routeParams != undefined) {
            var id = $routeParams.Id;
        };
        var getSuccessCallback = function (data, status) {
            jQuery("#tblUserSearchList").kendoGrid({
                selectable: "row",
                dataSource: {
                    data: data,
                    serverPaging: false,
                    serverFiltering: false,
                    serverSorting: false,
                    serverOperation: false,
                    pageSize: 10,
                    schema: {
                        model: {
                            fields: {
                                TransactionId: { type: "string" },
                                SettlementDate: { type: "date" },
                                FromBank: { type: "string" },
                                ToBank: { type: "string" },
                                OrderAmount: { type: "string" },
                            }
                        }
                    },
                },
                pageSize: 10,
                filterable: {
                    extra: true
                },

                sortable: true,
                pageable: true,
                reorderable: true,
                scrollable: false,
                columns: [
                    {
                        width: 60,
                        field: "Id",
                        title: "Id",
                        sortable: false,
                        filterable: false,
                        template: function(dataItem) {
                            if (dataItem.Id != null) {
                                return "<span><a href=\#/Administrator/Admin/UserDetails/" + dataItem.Id + "\> " + dataItem.Id + "</a></span>";
                            } else {
                                return null;
                            }
                        }
                    },
                    { field: "FirstName", title: "First Name" },
                    { field: "LastName", title: "Last Name" },
                    { field: "MobileNumber", title: "Mobile Number", attributes: { style: "text-align:left;" } },
                    { field: "Email", title: "Email" },
                    { field: "UserType", title: "User Type" },
                    { field: "RoleName", title: "Role Name" },
                    { field: "BankCode", title: "BankCode" },
                    {
                        field: "IsApproved",
                        title: "Status",
                        filterable: false,
                        template: function(dataItem) {
                            if (dataItem.IsApproved) {
                                return "Approved";
                            } else {
                                return "Not Approved";
                            }
                        }
                    },
                ]
            });
        };

        var errorCallback = function(data, status, header, config) {
            jQuery('#errorModalText').text(data);
            jQuery('#errorModal').modal('show');
            //alert('error : ' + data);
        };

        var factory = adminFactory;

        //factory.get("GetSearchUserInformation").success(getSuccessCallback).error(errorCallback);

        $scope.userSearch = function (isValid) {
            try {
                jQuery("#tblUserSearchList").data("kendoGrid").destroy();
                
            } catch (e) {

            } 
            factory.create($scope.userSearchVM, "SearchUserInformation").success(getSuccessCallback).error(errorCallback);
        };

        var goAdminHome = function (data, status, header, config) {

            location.href = "#/Administrator/Admin/Home";
        };

        $scope.cancel = function() {
            // goAdminHome();
            history.go(-1);
        };
    }
];


