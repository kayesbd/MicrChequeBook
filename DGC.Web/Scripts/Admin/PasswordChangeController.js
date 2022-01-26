var util = new GITS.Utils();
util.DbManager('ChangePasswordFactory', "/api/ApiAdmin/");

var customerList;

var PasswordChangeController =['$scope', 'ChangePasswordFactory', '$routeParams', '$httpfunction', function ($scope, ChangePasswordFactory, $routeParams, $http) {
    //alert("hi");
    //util.Menu();
    //var Bank;
    //if ($routeParams != undefined) {
    //    var id = $routeParams.Id;
    //}

    ////Country Dropdown List
    //$scope.countryModel = null;
    //var countryList = [];

    //$http({
    //    method: 'POST',
    //    url: '/api/ApiAdmin/ChangePassword',
    //}).success(function (result) {
    //    $scope.countryList = result;
    //});


    ////State Dropdown List
    //$scope.GetStates = function (bankVM) {
    //    if (bankVM == undefined) {
    //        var countryId = this.bankVM.CountryId;
    //    }
    //    else {
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
    //    }
    //    else {
    //        $scope.states = null;
    //    }
    //}
    //var successPostCallback = function (data, status, header, config) {
    //    alert('Data has been saved successfully!');
    //    $scope.bankVM = null;
    //    $scope.personVM = null;
    //    //$location.url('/#/Administrator/Bank/Index');
    //};

    var successPutCallback = function (data, status, header, config) {
        //alert('Data has been updated successfully!');
        jQuery('#successModalText').text('Data has been updated successfully!');

        jQuery('#successModal').modal('show');
        $scope.bankVM = null;
        $scope.personVM = null;
        location.href = "/#/Client/Customer/Home";
    };

    ////var getSuccessCallback = function (data, status) {
    ////    $scope.bankVM = data
    ////    $scope.personVM = data.PersonalInformations[0];
    ////    $scope.GetStates();
    ////};

    ////var getSuccessCallbackForUserInformation =function(data,status) {
    ////    $scople.bankUserList = data;
    ////}
    var errorCallback = function (data, status, header, config) {
        jQuery('#errorModalText').text(data);
        jQuery('#errorModal').modal('show');
        //alert('error : ' + data);
    };

    //if (id != undefined && id > 0) {
    //    BankDetailsFactory.getById(id, "GetBankDetails")
    //        .success(function (data, status) {
    //            $scope.bankVM = data
    //            $scope.personVM = data.PersonalInformations[0];
    //            $scope.bankUserList = data.BankUsers;
    //            $scope.GetStates();
    //        }).error(errorCallback);
    //}

    ////if (id != undefined && id > 0) {

    ////    BankDetailsFactory.getById(id, "GetBankUserList")
    ////        .success(function (data) {
    ////            $scope.bankUserList = data;
    ////        }).error(errorCallback);
    ////}


    $scope.changePassword = function (isValid) {
        if ($scope.passwordVM.NewPassword == $scope.passwordVM.ConfirmPassword) {
            ChangePasswordFactory.update($scope.passwordVM, "ChangePassword").success(successPutCallback).error(errorCallback);
        }
        else {


            jQuery('#errorModalText').text("Please enter correct new and confirm password!!");
            jQuery('#errorModal').modal('show');
            //alert("Please enter correct new and confirm password!!")
        }


        //var localPersonalInformation = [];
        //localPersonalInformation[0] = $scope.personVM;
        //if ($scope.bankVM.Id == undefined) {
        //    $scope.bankVM.PersonalInformations = localPersonalInformation;
        //    BankDetailsFactory.create($scope.bankVM, "CreateBank").success(successPostCallback).error(errorCallback);
        //}
        //else {
        //    BankDetailsFactory.update($scope.bankVM, "UpdateBank").success(successPutCallback).error(errorCallback);
        //}

    };
    // for reset password//
    //$scope.resetPassword = function (isValid) {
    //    debugger;
    //    if ($scope.passwordVM.NewPassword == $scope.passwordVM.ConfirmPassword) {
    //        ChangePasswordFactory.update($scope.passwordVM, "ResetPassword").success(successPutCallback).error(errorCallback);
    //    }
    //    else {
    //        alert("Please enter correct new and confirm password!!")
    //    }



    //};
 
    $scope.cancel = function() {
        $window.history.go(-1);
    };
}];


