var util = new GITS.Utils();
util.DbManager('BankDetailsFactory', "/api/ApiBank/");
//util.DbManager('adminFactory', "/api/ApiAdmin/");
var menulist = new BankMenu;
var customerList;
var BankDetailsController = ['$scope', 'BankDetailsFactory', '$routeParams', '$http', '$window', 'subMenuService', function ($scope, BankDetailsFactory, $routeParams, $http, $window, subMenuService) {
    $scope.subMenuService = subMenuService;
    //jQuery("#bankDetails").removeClass("col-sm-12").addClass("col-sm-10");
    $scope.BankSubmenu = menulist.bankMenuList();
    if (typeof ($routeParams) != undefined && typeof ($location) != undefined && $routeParams != undefined && $routeParams.Id > 0) {
        $scope.BankId = $scope.subMenuService.BankId = $routeParams.Id;
       
       
    } else if ($scope.subMenuService.BankId > 0) {
        $scope.BankId = $scope.subMenuService.BankId;
       
       // $scope.marginleft = 'nomarginleft';
    }
    

    if ($scope.subMenuService.BankId > 0 && $routeParams.Id == -1) {
      
        $scope.BankId = -1;
        jQuery("#bankMenu").hide();
        jQuery("#bankDetails").removeClass("col-sm-10").addClass("col-sm-12");
        $scope.marginleft = 'nomarginleft';
    }
    if ($scope.subMenuService.BankId.length == 0 && $routeParams.Id == 0) {
        $scope.BankId = 0;
        jQuery("#bankMenu").hide();
        jQuery("#bankDetails").removeClass("col-sm-10").addClass("col-sm-12");
        $scope.marginleft = 'nomarginleft';
    }

    if ($scope.subMenuService.BankId == 0 && $routeParams.Id == -1) {
        $scope.marginleft = 'nomarginleft';
    }


    $scope.onlyNumbers = /^\d+$/;

    $http({
        method: 'GET',
        url: '/api/ApiBank/GetBankList',
    }).success(function (result) {
        $scope.bankList = result;
    });

    $http({
        method: 'GET',
        url: '/api/ApiBank/GetCommunicationTypeList',
    }).success(function (result) {
        $scope.communicationTypeList = result;
    });

    //Country Dropdown List
    $scope.countryModel = null;
    var countryList = [];

    $http({
        method: 'GET',
        url: '/api/ApiCountry/GetCountryList',
    }).success(function (result) {
        $scope.countryList = result;
    });

    //TimeZone Dropdown List
    var utcTimeZoneList = [];

    $http({
        method: 'GET',
        url: '/api/ApiCustomer/TimeZoneList',
    }).success(function (result) {
        $scope.utcTimeZoneList = result;
    });

    // Country and Country
    jQuery("#countries").change(function () {
        var countryId = jQuery('#countries option:selected').val();
        $scope.GetStates(countryId);
    });
    $scope.GetStates = function (countryId) {
        if (countryId == undefined || countryId == '' || countryId.length < 1) {
            $scope.stateList = null;
        }
        if (countryId > 0) {
            $http({
                method: 'Get',
                url: '/api/ApiCountry/StateList?id=' + countryId
            })
                .success(function (data) {
                    var element = {};
                    element.Id = 'Others';
                    element.StateName = 'Others';
                    element.CountryId = '';
                    element.IsActive = '';
                    element.IsDeleted = '';
                    element.DateCreated = '';
                    element.CreatedBy = '';
                    element.DateModified = '';
                    element.ModifiedBy = '';
                    data.push(element);
                    $scope.stateList = data;
                });
        } else {
            $scope.stateList = null;
        }
    };
    $scope.onStateChange = function () {
        if ($scope.personVM.StateId == 'Others') {
            $scope.AddStateMode = true;
            $scope.StateVM = null;
        }
    };
    $scope.saveState = function () {
        if ($scope.StateVM == undefined || $scope.StateVM.StateName == undefined || $scope.StateVM.StateName == '') {
            $scope.AddStateMode = false;
        }
        else {
            $scope.StateVM.CountryId = jQuery('#countries option:selected').val();
            BankDetailsFactory.create($scope.StateVM, "CreateState").success(
                function (data) {
                    $scope.AddStateMode = false;
                    $scope.GetStates($scope.StateVM.CountryId);
                    $scope.personVM.StateId = data.Id;
                }
            ).error(onError);
        }
    };
    //-------------------------------------------

    var successPostCallback = function (data, status, header, config) {
        if (data.Success) {
            jQuery('#successModalText').text('Data has been saved successfully!');

            jQuery('#successModal').modal('show');
            //alert('Data has been saved successfully!');
            //$scope.bankVM = null;
            //$scope.personVM = null;
            //location.href = "/#/Administrator/Bank/Index";
        } else {
            jQuery('#errorModalText').text(data.ErrorMessage);
            jQuery('#errorModal').modal('show');
            //alert(JSON.stringify(data.ErrorMessage));
        }
    };

    var successPutCallback = function (data, status, header, config) {
        if (data.Success) {
            jQuery('#successModalText').text('Data has been updated successfully!');

            jQuery('#successModal').modal('show');
            //alert('Data has been updated successfully!');
           // $scope.bankVM = null;
            //$scope.personVM = null;
            //location.href = "/#/Administrator/Bank/Index";
        } else {
            jQuery('#errorModalText').text(data.ErrorMessage);
            jQuery('#errorModal').modal('show');
            //alert(JSON.stringify(data.ErrorMessage));
        }
    };

    var errorCallback = function (data, status, header, config) {
        jQuery('#errorModalText').text(data);
        jQuery('#errorModal').modal('show');
        //alert('error : ' + data);
    };

    if ($scope.BankId != undefined && $scope.BankId >= 0) {
        BankDetailsFactory.getById($scope.BankId, "GetBankDetails").success(function (data, status) {
            $scope.bankVM = data;
            $scope.personVM = data.PersonalInformations[0];
            $scope.bankUserList = data.PersonalInformations;
            var countryDropdown = jQuery("#countries").msDropdown().data("dd");
            countryDropdown.set("value", data.PersonalInformations[0].State.CountryId);
            $scope.personVM.StateId = data.PersonalInformations[0].StateId;
            $scope.GetStates(data.PersonalInformations[0].State.CountryId);
            jQuery("#ContactPersonMobileNumber").intlTelInput("setNumber", $scope.personVM.MobileNumber);
        }).error(errorCallback);
    }

    function validate() {
        $scope.IsRequiredCustomerMobileNo = false;
        $scope.IsExceededLengthCustomerMobileNo = false;
        $scope.IsRequiredStateId = false;
        $scope.IsLessThanCustomerMobileNo = false;

        if ($scope.ContactPersonMobileNumber == undefined || $scope.ContactPersonMobileNumber.length == 0) {
            $scope.IsRequiredCustomerMobileNo = true;
            return false;
        }
        if ($scope.ContactPersonMobileNumber.length > 15) {
            $scope.IsExceededLengthCustomerMobileNo = true;
            return false;
        }
        if ($scope.ContactPersonMobileNumber.length <8) {
            $scope.IsLessThanCustomerMobileNo = true;
            return false;
        }
        if ($scope.personVM == undefined || $scope.personVM.StateId.length < 1 || $scope.personVM.StateId == "Others") {
            $scope.IsRequiredStateId = true;
            return false;
        }
        return true;
    }

    $scope.submitForm = function (bankForm) {
        $scope.ContactPersonMobileNumber = jQuery("#ContactPersonMobileNumber").intlTelInput().val();
        $scope.CountryId = jQuery('#countries option:selected').val();
        $scope.bankVM.StateId = $scope.personVM.StateId;
        if ($scope.CountryId != undefined && $scope.CountryId.length > 1) {
            $scope.bankVM.CountryId = $scope.CountryId;
        }

        if (bankForm.$invalid || (validate() == false)) {
            $scope.showValidation = true;
            $scope.submitted = true;
            $scope.toggleValidationSummary = function () {
                $scope.showValidation = !$scope.showValidation;
            };
            return;
        } else {
            var localPersonalInformation = [];
            $scope.personVM.MobileNumber = $scope.ContactPersonMobileNumber;
            localPersonalInformation[0] = $scope.personVM;

            if ($scope.bankVM.Id == undefined) {
                $scope.bankVM.PersonalInformations = localPersonalInformation;
                BankDetailsFactory.create($scope.bankVM, "CreateBank").success(successPostCallback).error(errorCallback);
            } else {
                BankDetailsFactory.update($scope.bankVM, "UpdateBank").success(successPutCallback).error(errorCallback);
            }
        }
    };

    jQuery('#successModal').one('hidden.bs.modal', function (e) {
        //var host = window.location.host;
        // window.history.back();
        $scope.bankVM = null;
        $scope.personVM = null;
        // $window.history.go(-1);
        location.href = "/#/Administrator/Bank/Index";
      //  $window.history.pop();
    });

    $scope.cancel = function () {
        $window.history.go(-1);
    };

    //jQuery("#ContactPersonMobileNumber").intlTelInput({
    //    //preferredCountries: ["us", "gb"]
    //});
    jQuery("#countries").msDropdown();
}];


