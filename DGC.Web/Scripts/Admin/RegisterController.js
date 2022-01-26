var util = new GITS.Utils();
util.DbManager('RegisterFactory', "/Account/");
alert("I am from controller");
function go_to_item(display) {

    window.location = display;

    location.reload();
}

var RegisterController = [
    '$scope', 'RegisterFactory', '$routeParams', '$location', '$http', '$window', function ($scope,
        RegisterFactory, $routeParams, $location, $http, $window) {
        //util.Menu();
 
        //var onError = function (data, status, header, config) {
        //    jQuery('#warningModalText').text(data);
        //    jQuery('#warningModal').modal('show');

        //    //alert('error : ' + data);
        //};
        //$http({
        //    method: 'GET',
        //    url: '/Branch/GetPrinterLocation',
        //}).success(function (result) {
        //    $scope.PrinterLocation = result;
        //});
        //$scope.test = function () {
        //    alert('test');
        //}
        //$scope.BranchList = [];
        //$http({
        //    method: 'GET',
        //    url: '/Branch/BranchListForTable'
        //}).success(function (result) {

     

           

        //});

        //$scope.save = function (bc) {
        //    $scope.fromDateTime = jQuery("#fromDateTime").val();
        //    $scope.toDateTime = jQuery("#toDateTime").val();

        //    if ($scope.customerApprovalInfoVM == undefined) {
        //        $scope.customerApprovalInfoVM = {};
        //    }
        //    $scope.customerApprovalInfoVM.FromDate = $scope.fromDateTime;
        //    $scope.customerApprovalInfoVM.ToDate = $scope.toDateTime;

        //    if (customerApprovalInformationForm.$invalid || validateFromDate() == false || validateToDate() == false || validateDate() == false || validateCurrentdate() == false || validateDateFormat($scope.fromDateTime, "FromDate") == false || validateDateFormat($scope.toDateTime, "Todate") == false) {
        //        $scope.showValidation = true;
        //        $scope.submitted = true;
        //        $scope.toggleValidationSummary = function () {
        //            $scope.showValidation = !$scope.showValidation;
        //        };
        //        return;
        //    } else {
        //        $scope.showValidation = false;
        //        adminFactory.create($scope.customerApprovalInfoVM, "GetCustomerApprovalInformation").success(function (data) {
        //            $scope.searchResult = data;
        //            displaySearchResult(data);

        //        }).error(errorCallback);
        //    }
        //};









        //$scope.AddNewBranch = function () {
        //    var id = $scope.BranchData.Id;
        //    if (id != undefined && id == 0) {
        //        BankBranchFactory.Create($scope.BranchData, "Create").success(successActiveCallback).error(errorCallback);
        //    }
        //};

        //$scope.update = function (isValid) {
        //    var id = $scope.BranchData.Id;
        //    if (id != undefined && id > 0) {
        //        BankBranchFactory.update($scope.BranchData, "Update").success(successActiveCallback).error(errorCallback);
        //    }
        //};
        //var successActiveCallback = function (data, status, header, config) {
        //    jQuery('#successModalText').text("Virtual account inactivated successfully!");

        //    jQuery('#successModal').modal('show');

        //    //alert('Virtual account inactivated successfully!');
        //    bankAccountVM = null;
        //    //$window.history.go(-1);
        //}

        //var errorCallback = function (data, status, header, config) {
        //    jQuery('#errorModalText').text(data);
        //    jQuery('#errorModal').modal('show');
        //    //alert('error : ' + data);
        //};


    }];



