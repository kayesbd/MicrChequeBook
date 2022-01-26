

var util = new GITS.Utils();
util.DbManager('BankBranchFactory', "/Administrator/Branch/");


var BankBranchAddController = [
    '$scope', 'BankBranchFactory',  '$routeParams', '$location', '$http', '$window',
    function ($scope, BankBranchFactory,  $routeParams, $location, $http, $window) {
        $scope.Id = $routeParams.id;
        $scope.Status = $routeParams.Status;
        //$scope.bc = {};
       // ============ Load Printer Location===============
        $http({
            method: 'GET',
            url: '/Administrator/Branch/GetPrinterLocation'
        }).success(function (result) {
          
            $scope.PrinterLocationList = result;
        });
        //============ End Load Printer Location===============
        var onError = function (data, status, header, config) {
                  jQuery('#warningModalText').text(data);
                  jQuery('#warningModal').modal('show');
        };
       
        $scope.branchToEdit = function () {
            $http({
                method: 'GET',
                url: '/Administrator/Branch/GetUpdateData/' + $routeParams.id
            }).success(function (result) {
                
              
                $scope.bc = result;
                
               
               
                //alert(result);

            });

        };      
        $scope.edit = function () {
            jQuery('#branchCreateForm').modal('show');
        };
        $scope.addBranch = function () {
            jQuery('#branchCreateForm').modal('show');
            $scope.bc = {};
            $scope.submitted = false;
        };
       
        alert("Inside the edit");
        if ($routeParams.id > 0 && ($routeParams.Status == undefined || $routeParams.Status == '')) {
            $scope.branchToEdit();
           // $scope.edit();
        } else if ($routeParams.Status != undefined && $routeParams.Status.length > 0 && $routeParams.id > 0) {
            $scope.branchToEdit();
           // $scope.edit();
        } else if ($routeParams.id == 0 && ($routeParams.Status == undefined || $routeParams.Status == '')) {
            $scope.addBranch();
        }
     
        
       
            
      
         
      
          //    // To Add, Update Role Information
              var goAdd = function (data, status, header, config) {
                  jQuery('#branchCreateForm').modal('hide');
                  if (data.Success) {
                      jQuery('#successModalText').text('Fund Receiver Profile is saved successfully');
                      jQuery('#successModal').modal('show');
                      
                  } else {
                      
      
                      jQuery('#warningModalText').text(data.ErrorMessage);
                      jQuery('#warningModal').modal('show');
      
                      
                  }
              };
     
              var goUpdate = function (data, status, header, config) {
      
                  debugger;
                 // jQuery('#branchForm').modal('hide');
                  //if (data.Success) {
                      jQuery('#successModalText').text('Fund Receiver Profile information is Updated successfully');
                      jQuery('#successModal').modal('show');
                      $window.history.go(-1);
                  //} else {
                  //    jQuery('#errorModalText').text(data.ErrorMessage);
                  //    jQuery('#errorModal').modal('show');
                      
                  //}
              };
      
             
      
       
     
        
      
        $scope.ToggleStatus = function () {
            $http({
                method: 'GET',
                url: '/Administrator/Branch/ToggleStatus/' + $routeParams.Id
            }).success(function (result) {
              
            });
        };
       
        //if ($routeParams.id > 0 && ($routeParams.Status == undefined || $routeParams.Status == '')) {
            
        //    $scope.edit();
        //} else if ($routeParams.Status.length > 0 && $routeParams.id > 0) {

        //} else if ($routeParams.id == 0 && ($routeParams.Status == undefined || $routeParams.Status == '')) {
        //    $scope.addBranch();
        //}
        

        $scope.saveBranchForm = function (branchCreateForm) {

            $scope.fromDateTime = jQuery("#PrintStartDate").val();
            $scope.toDateTime = jQuery("#LastPrintingDateTime").val();

            
            if ($scope.bc == undefined) {
                $scope.bc = {};
            };
            if (branchCreateForm.$invalid) {
                $scope.showValidation = true;
                $scope.submitted = true;
                $scope.toggleValidationSummary = function () {
                    $scope.showValidation = !$scope.showValidation;
                };
                return;
            } else {
                if ($scope.bc.Id == undefined || $scope.bc.Id <= 0) {
                    BankBranchFactory.create($scope.bc, "CreateBranch").success(goAdd).error(onError);
                } else {
                    debugger;
                    BankBranchFactory.update($scope.bc, "BranchUpdateByChangedData").success(goUpdate).error(onError);
                }
            }

        };
        

            $scope.cancel = function () {
                  $window.history.go(-1);
              };
        //$scope.closeModel = function () {
        //    jQuery('#branchCreateForm').modal('hide');
        //    $scope.branchDetailsVM = {};
        //};
        //      jQuery('#successModal').one('hidden.bs.modal', function (e) {
        //          $scope.$apply(function () {
        //              $scope.BranchList.push($scope.bc);
        //              $scope.bc = {};
        //              $scope.showValidation = false;
        //          });
        //      });
          
    }];




































































































