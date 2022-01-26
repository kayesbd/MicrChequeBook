var util = new GITS.Utils();
util.DbManager('BankAccountDetailsFactory', "/api/ApiBank/");

var customerList;
var BankAccountDetailsController =['$scope', 'BankAccountDetailsFactory', '$routeParams', '$http', '$window', 'subMenuService',function ($scope, BankAccountDetailsFactory, $routeParams, $http, $window, subMenuService) {
    $scope.subMenuService = subMenuService;
    alert($scope.subMenuService.BankId);

    if ($scope.subMenuService != undefined || $scope.subMenuService.BankId != "") {
        $scope.BankId = $scope.subMenuService.BankId;
    };

    var errorCallback = function (data, status, header, config) {
       // alert('error : ' + data);
        jQuery('#errorModalText').text(data);
        //$scope.errorModalText = "Data is not found.";               
        jQuery('#errorModal').modal('show');

    };

    if ($routeParams != undefined) {
        $scope.bankAccountVM = {};
        $scope.bankAccountVM.ClientId = $routeParams.Id;
        $scope.bankAccountVM.BankId = $scope.BankId;

        BankAccountDetailsFactory.create($scope.bankAccountVM, "GetBankAccountDetails")
            .success(function (data) {
                $scope.bankAccountVM = data;
            }).error(errorCallback);
    }

    //$scope.countryModel = null;
    //$http({
    //    method: 'GET',
    //    url: '/api/ApiCountry/GetCountryList',
    //}).success(function (result) {
    //    $scope.countryList = result;
    //});

    //$scope.GetStates = function (bankVM) {
    //    if (bankVM == undefined) {
    //        var countryId = this.bankVM.CountryId;
    //    } else {
    //        var countryId = $scope.bankVM.CountryId;
    //    }

    //    var states = [];
    //    if (countryId) {
    //        $http({
    //            method: 'Get',
    //            url: '/api/ApiCustomer/StateList?id=' + countryId
    //        })
    //            .success(function (data) {
    //                $scope.states = data;
    //            });
    //    } else {
    //        $scope.states = null;
    //    }
    //};

    //var successPostCallback = function (data, status, header, config) {
    //    //$location.url('/#/Administrator/Bank/Index');
    //    $window.history.go(-1);
    //};

    var successActiveCallback = function (data, status, header, config) {
       // alert('Customer Status Changed to approved!');
        
        jQuery('#successModalText').text("Customer Status Changed to approved!");
        jQuery('#successModal').modal('show');

        bankAccountVM = null;
        location.href = "/#/Administrator/Admin/ActiveCustomer/0";
        //$window.history.go(-1);
    };

    var successInActiveCallback = function (data, status, header, config) {
       // alert('Customer Status changed to declined!');

        jQuery('#successModalText').text("Customer Status changed to declined!");
        jQuery('#successModal').modal('show');

        bankAccountVM = null;
        location.href = "/#/Administrator/Admin/InActiveCustomer";
        //$window.history.go(-1);
    };

    //var getSuccessCallback = function (data, status) {
    //    debugger;
    //    $scope.bankVM = data
    //    $scope.personVM = data.PersonalInformations[0];
    //    $scope.GetStates();
    //};

    //var getSuccessCallbackForUserInformation =function(data,status) {
    //    $scople.bankUserList = data;
    //}

    $scope.update = function (isValid) {
        var id = $scope.bankAccountVM.Id;
        if (id != undefined && id > 0) {
            BankAccountDetailsFactory.update($scope.bankAccountVM, "ActiveBankAccount").success(successActiveCallback).error(errorCallback);
           // BankAccountDetailsFactory.update($scope.bankAccountVM, "ActiveCustomerStatus").success(successActiveCallback).error(errorCallback);
        }
    };

    $scope.inactive = function (isValid) {
        var id = $scope.bankAccountVM.Id;
        if (id != undefined && id > 0) {
            BankAccountDetailsFactory.update($scope.bankAccountVM, "InActiveBankAccount").success(successInActiveCallback).error(errorCallback);

            //BankAccountDetailsFactory.update($scope.bankAccountVM, "InActiveCustomerStatus").success(successInActiveCallback).error(errorCallback);
        }
    };

    jQuery('#successModal').one('hidden.bs.modal', function (e) {
        //var host = window.location.host;
        //if ($scope.returnURL != null) {
        //    location.href = $scope.returnURL;
        //}
        
    });

    $scope.cancel = function () {
        $window.history.go(-1);
    };
}];


