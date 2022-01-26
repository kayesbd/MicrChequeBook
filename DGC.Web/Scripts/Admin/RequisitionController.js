var util = new GITS.Utils();
util.DbManager('RequisitionFactory', "/Account/");



var RequisitionController = [
    '$scope', 'RequisitionFactory', '$routeParams', '$location', '$http', '$window', function ($scope, RequisitionFactory, $routeParams, $location, $http, $window) {
       
        var onError = function (data, status, header, config) {
            jQuery('#warningModalText').text(data);
            jQuery('#warningModal').modal('show');

            //alert('error : ' + data);
        };

        $http({
            method: 'GET',
            url: '/Branch/BranchListForTable'
        }).success(function (result) {
            
            $scope.BranchList = result;
           

        });
        $http({
            method: 'GET',
            url: '/Account/GetNoOfLeafs'
        }).success(function (result) {
            $scope.NoOfLeafs = result;
        });
        // To Display Menu List

        $scope.loading = false;

        // To Add, Update Role Information
        var goAdd = function (data, status, header, config) {
        
            
                //jQuery('#successModalText').text('Fund Receiver Profile is saved successfully');
                //jQuery('#successModal').modal('show');
               
                location.href = "/#/Account/RequisitionComplete";
           
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

       

        //var goDelete = function (data, status, header, config) {
        //    if (data.Success) {
        //        //var resultObject = search(data.ID, $scope.BranchList);

        //    } else {

        //    }
        //};
        //var goAdminHome = function (data, status, header, config) {
        //    location.href = "#/Administrator/Admin/Home";
        //};
        

        $scope.edit = function (branch) {
            $scope.requisitionDetailsVM = branch;
            jQuery('#branchForm').modal('show');
        };
        $scope.addBranch = function () {
            jQuery('#branchForm').modal('show');
            $scope.requisitionDetailsVM = {};
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

        $scope.saveRequisitionForm = function (requisitionForm) {
            alert(requisitionForm.AccountNumber);

            if (requisitionForm.$invalid) {
                $scope.showValidation = true;
                $scope.submitted = true;
                $scope.toggleValidationSummary = function () {
                    $scope.showValidation = !$scope.showValidation;
                };
                return;
            } else {
                if (requisitionForm.ID == undefined || requisitionForm.ID <= 0) {
                    RequisitionFactory.create(requisitionForm, "InsertChequeRequsitionStepOneService").success(goAdd).error(onError);
                } else {
                    RequisitionFactory.update(requisitionDetailsVM, "UpdateBranch").success(goUpdate).error(onError);
                }
            }
        };

        //$scope.cancel = function () {
        //    $window.history.go(-1);
        //};
        //$scope.closeModel = function () {
        //    jQuery('#branchForm').modal('hide');
        //    $scope.requisitionDetailsVM = {};
        //}
        //jQuery('#successModal').one('hidden.bs.modal', function (e) {
        //    $scope.$apply(function () {
        //        $scope.BranchList.push($scope.requisitionDetailsVM);
        //        $scope.requisitionDetailsVM = {};
        //        $scope.showValidation = false;
        //    });
        //});
    }
];



