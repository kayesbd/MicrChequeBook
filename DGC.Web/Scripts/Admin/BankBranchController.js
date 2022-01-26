var util = new GITS.Utils();
util.DbManager('BankBranchFactory', "/api/ApiBank/");


var BankBranchController = [
    '$scope', 'BankBranchFactory', '$routeParams', '$location', '$http', '$window', function ($scope, BankBranchFactory, $routeParams, $location, $http, $window) {
        //util.Menu();
        var onError = function (data, status, header, config) {
            jQuery('#warningModalText').text(data);
            jQuery('#warningModal').modal('show');

            //alert('error : ' + data);
        };

        $http({
            method: 'GET',
            url: '/api/ApiBank/GetBranchByBankCode'
        }).success(function (result) {
            debugger;
            $scope.BranchList = result;
            var self = this;
            self.tableParams = result;

        });
        // To Display Menu List

        $scope.loading = false;

        // To Add, Update Role Information
        var goAdd = function (data, status, header, config) {
            jQuery('#branchForm').modal('hide');
            if (data.Success) {
                jQuery('#successModalText').text('Fund Receiver Profile is saved successfully');
                jQuery('#successModal').modal('show');
                //alert('Role information is saved successfully');
                //location.href = "/#/Administrator/Admin/RoleList";
            } else {
                //jQuery('#errorModalText').text(data.ErrorMessage);
                //jQuery('#errorModal').modal('show');

                jQuery('#warningModalText').text(data.ErrorMessage);
                jQuery('#warningModal').modal('show');

                //alert(JSON.stringify(data.ErrorMessage));
            }
        };

        var goUpdate = function (data, status, header, config) {


            jQuery('#branchForm').modal('hide');
            if (data.Success) {
                jQuery('#successModalText').text('Fund Receiver Profile information is Updated successfully');
                jQuery('#successModal').modal('show');
                //alert('Role information is Updated successfully');
                //location.href = "/#/Administrator/Admin/RoleList";
            } else {
                jQuery('#errorModalText').text(data.ErrorMessage);
                jQuery('#errorModal').modal('show');
                //alert(JSON.stringify(data.ErrorMessage));
            }
        };

       

        var goDelete = function (data, status, header, config) {
            if (data.Success) {
                //var resultObject = search(data.ID, $scope.BranchList);

            } else {

            }
        };
        var goAdminHome = function (data, status, header, config) {
            location.href = "#/Administrator/Admin/Home";
        };
        

        $scope.edit = function (branch) {
            $scope.branchDetailsVM = branch;
            jQuery('#branchForm').modal('show');
        };
        $scope.addBranch = function () {
            jQuery('#branchForm').modal('show');
            $scope.branchDetailsVM = {};
            $scope.submitted = false;
        }
        $scope.delete = function (branchForm, index) {
            debugger;
            var result = confirm("are you sure to delete");
            if (result) {
                $scope.BranchList.splice(index, 1);
                BankBranchFactory.update(branchForm, "DeleteBranch").success(goDelete).error(onError);
            }
        }

        $scope.saveBranchForm = function (branchForm) {
            debugger;
            if (branchForm.$invalid) {
                $scope.showValidation = true;
                $scope.submitted = true;
                $scope.toggleValidationSummary = function () {
                    $scope.showValidation = !$scope.showValidation;
                };
                return;
            } else {
                if ($scope.branchDetailsVM.Id == undefined || $scope.branchDetailsVM.Id <= 0) {
                    BankBranchFactory.create($scope.branchDetailsVM, "CreateBranch").success(goAdd).error(onError);
                } else {
                    BankBranchFactory.update($scope.branchDetailsVM, "UpdateBranch").success(goUpdate).error(onError);
                }
            }
        };

        $scope.cancel = function () {
            $window.history.go(-1);
        };
        $scope.closeModel = function () {
            jQuery('#branchForm').modal('hide');
            $scope.branchDetailsVM = {};
        }
        jQuery('#successModal').one('hidden.bs.modal', function (e) {
            $scope.$apply(function () {
                $scope.BranchList.push($scope.branchDetailsVM);
                $scope.branchDetailsVM = {};
                $scope.showValidation = false;
            });
        });
    }
];



