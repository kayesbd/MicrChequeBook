var util = new GITS.Utils();
util.DbManager('adminFactory', "/api/ApiAdmin/");

var ConstantValueController = [
    '$scope', 'adminFactory', '$routeParams', '$location', '$http', function($scope, adminFactory, $routeParams, $location, $http) {
        var onError = function(data, status, header, config) {
            //alert('error : ' + data);
            jQuery('#errorModalText').text(data);
            jQuery('#errorModal').modal('show');
        };
        // start loading//
        alert("hello constant values");
        $scope.loading = true;
        $scope.displayConstantValueList = function() {
            var dataSourceURL = "/Administrator/Admin/GetConstantValueList";
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
                            Title: { type: "string" },
                            ModuleName: { type: "string" },
                            DataTypeName: { type: "string" },
                            Value: { type: "string" }
                        }
                    }
                },
                sort: [
                    { field: "Title", dir: "asc" },
                    { field: "ModuleName", dir: "asc" },
                    { field: "DataTypeName", dir: "asc" },
                    { field: "Value", dir: "asc" }
                ],
                pageSize: 10,
                serverPaging: false,
                serverFiltering: false,
                serverSorting: false
            });
            jQuery("#tblConstantValueList").kendoGrid({
                selectable: "row",
                dataSource: dataSource,
                filterable: {
                    extra: true
                },
                sortable: true,
                pageable: true,
                reorderable: true,
                scrollable: false,
                columns: [
                    { field: "Id", title: "Id", hidden: "hidden" },
                    { field: "Title", title: "Name", },
                    { field: "ModuleName", title: "Module", },
                    { field: "DataTypeName", title: "Data Type", },
                    { field: "Value", title: "Value", }
                    //{
                    //    width: 60,
                    //    field: "Id",
                    //    title: "Edit",
                    //    sortable: false,
                    //    filterable: false,
                    //    template: function(dataItem) {
                    //        if (typeof dataItem.Id == "string") {
                    //            return "<span><a href=\#/Administrator/Admin/ConstantValueDetails/" + dataItem.Id + "\> <i class='glyphicon glyphicon-edit'></i></a></span>";
                    //        }
                    //    }
                    //}
                ]
            });
        };
        $scope.displayConstantValueList();

        // stop loading///
        $scope.loading = false;
        $scope.moduleList = null;
        $scope.dataTypeList = null;

        $http({
            method: 'GET',
            url: '/api/ApiAdmin/ModuleList',
        }).success(function(result) {
            $scope.moduleList = result;
        });

        $http({
            method: 'GET',
            url: '/api/ApiAdmin/DataTypeList',
        }).success(function(result) {
            $scope.dataTypeList = result;
        });

        var setConstantValueInformation = function(data, status) {
            $scope.constantValueVM = data;
        };

        if ($routeParams != undefined) {
            var roleId = $routeParams.Id;
        }

        if (roleId != undefined && roleId > 0) {
            adminFactory.getById(roleId, "GetConstantValueInformation").success(setConstantValueInformation).error(onError);
        }

        var goConstantValueListPage = function(data, status, header, config) {
            window.history.back();
            //location.href = "/#/Administrator/Admin/ConstantValueList";
        };
        var goAdminHome = function(data, status, header, config) {
            window.history.back();
            //location.href = "#/Administrator/Admin/Home";
        };

        $scope.saveConstantValue = function(constantValueForm) {
            if (constantValueForm.$invalid) {
                $scope.showValidation = true;
                $scope.submitted = true;
                $scope.toggleValidationSummary = function() {
                    $scope.showValidation = !$scope.showValidation;
                };
                return;
            } else {
                if ($scope.constantValueVM.Id == undefined || $scope.constantValueVM.Id <= 0) {
                    adminFactory.create($scope.constantValueVM, "CreateConstantValue").success(goConstantValueListPage).error(onError);
                } else {
                    adminFactory.update($scope.constantValueVM, "UpdateConstantValue").success(goConstantValueListPage).error(onError);
                }
            }
        };

        $scope.cancel = function() {
            goConstantValueListPage();
        };
    }
];
