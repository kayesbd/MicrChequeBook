var util = new GITS.Utils();
util.DbManager('RequisitionListFactory', "/Account/");

var RequisitionListController = [
    '$scope', 'RequisitionListFactory', '$routeParams', '$location', '$http', '$window', function ($scope,
        RequisitionListFactory, $routeParams, $location, $http, $window) {
        
        var onError = function (data, status, header, config) {
            jQuery('#warningModalText').text(data);
            jQuery('#warningModal').modal('show');

          
        };
       
     
        $http({
            method: 'GET',
            url: '/Account/GetRequisitionList'
        }).success(function (result) {

            //===================
            $scope.RequisitionList = result;
            jQuery("#tblRequisitionhData").kendoGrid({
                selectable: "row",
                dataSource: result,

                //dataBound: gridDataBound,
                filterable: {
                    extra: false
                },
                sortable: false,
                pageable: false,
                reorderable: true,
                scrollable: true,

                height: 410,
                columns: [
                    { field: "Id", title: "Id", hidden: "hidden" },
                    {
                        field: "AccountNumber",
                        title: "Account Number",

                        filterable: false,
                        sortable: false,
                        //template: function (dataItem) {
                        //    if (dataItem.TransactionId == null) {
                        //        return "<span><a title=\"Go for Transaction Details\" class=\"column-highlight\" href=\#/Transaction/Transaction/TransactionDetails/" + dataItem.Id + "\><i class='glyphicon glyphicon-edit'></i></a></span>";
                        //    } else {
                        //        return "<span><a title=\"Go for Transaction Details\" class=\"column-highlight\" href=\#/Transaction/Transaction/TransactionDetails/" + dataItem.Id + "\>" + dataItem.TransactionId + "</a></span>";
                        //    }
                        //},
                        width: 130
                    },
                    {
                        field: "CreatedDateString",
                        title: "Requisition Date",
                        type: "string",
                        width: 160,
                        filterable: false,
                        sortable: false,
                    },
                    //{ field: "BranchLocation", title: "Branch Location", width: 120, filterable: false, sortable: false, },
                    //{
                    //    field: "TotalNumberOfLeafsPrinted",
                    //    title: "TotalNumberOfLeafsPrinted",
                    //    width: 50,
                    //    filterable: false,
                    //    sortable: false,
                    //},
                    //{
                    //    field: "TotalNumberOfBooks",
                    //    title: "TotalNumberOfBooks",
                    //    width: 50,
                    //    filterable: false,
                    //    sortable: false,
                    //    attributes: { style: "text-align:right;" }
                    //},
                    //{
                    //    width: 60, //#?q=string
                    //    field: "Id",
                    //    title: "Edit",
                    //    filterable: false,
                    //    sortable: false,
                    //    template: function (dataItem) {
                    //        if (dataItem != null) {
                    //            return "<span ><a href='#/Administrator/Branch/UpdateBranch/" + dataItem.Id + "'> <i class='glyphicon glyphicon-edit'></i></a></span>";
                    //        }
                    //    }
                    //},
                    {
                        width: 60,
                        field: "Id",
                        title: "Status",
                        filterable: false,
                        sortable: false,
                        template: function (dataItem) {
                            alert(dataItem);
                            if (dataItem != null) {
                                if (dataItem.status=='Approved') {
                                    return "<span><a name='ChangeStatus' ng-class='ActiveStatus' href='#/Account/RequisitionToggleStatus/" + dataItem.Id + "'> <i class='glyphicon glyphicon-edit'></i></a></span>";
                                } else {
                                    return "<span><a name='ChangeStatus' ng-class='InactiveStatus' href='#/Account/RequisitionToggleStatus/" + dataItem.Id + "'> <i class='glyphicon glyphicon-edit'></i></a></span>";
                                }

                            }
                        }
                    }
                ]

            });

            //=====================

        });

        $scope.save = function (bc) {
            $scope.fromDateTime = jQuery("#fromDateTime").val();
            $scope.toDateTime = jQuery("#toDateTime").val();

            if ($scope.customerApprovalInfoVM == undefined) {
                $scope.customerApprovalInfoVM = {};
            }
            $scope.customerApprovalInfoVM.FromDate = $scope.fromDateTime;
            $scope.customerApprovalInfoVM.ToDate = $scope.toDateTime;

            if (customerApprovalInformationForm.$invalid || validateFromDate() == false || validateToDate() == false || validateDate() == false || validateCurrentdate() == false || validateDateFormat($scope.fromDateTime, "FromDate") == false || validateDateFormat($scope.toDateTime, "Todate") == false) {
                $scope.showValidation = true;
                $scope.submitted = true;
                $scope.toggleValidationSummary = function () {
                    $scope.showValidation = !$scope.showValidation;
                };
                return;
            } else {
                $scope.showValidation = false;
                adminFactory.create($scope.customerApprovalInfoVM, "GetCustomerApprovalInformation").success(function (data) {
                    $scope.searchResult = data;
                    displaySearchResult(data);

                }).error(errorCallback);
            }
        };









        $scope.AddNewBranch = function () {
            var id = $scope.BranchData.Id;
            if (id != undefined && id == 0) {
                RequisitionListFactory.Create($scope.BranchData, "Create").success(successActiveCallback).error(errorCallback);
            }
        };

        $scope.update = function (isValid) {
            var id = $scope.BranchData.Id;
            if (id != undefined && id > 0) {
                RequisitionListFactory.update($scope.BranchData, "Update").success(successActiveCallback).error(errorCallback);
            }
        };
        var successActiveCallback = function (data, status, header, config) {
            jQuery('#successModalText').text("Virtual account inactivated successfully!");

            jQuery('#successModal').modal('show');

            //alert('Virtual account inactivated successfully!');
            bankAccountVM = null;
            //$window.history.go(-1);
        }

        var errorCallback = function (data, status, header, config) {
            jQuery('#errorModalText').text(data);
            jQuery('#errorModal').modal('show');
            //alert('error : ' + data);
        };


    }];



