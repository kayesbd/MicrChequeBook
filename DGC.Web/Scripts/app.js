$.noConflict(); // jquery no conflict with angular or '$'. 
// Please use jQuery to use jquery function
var Gits = angular.module('NusPayApp', ['ngRoute', 'ui.bootstrap', 'ngCookies', 'flow', 'angular-timeline']);

Gits.controller('LayoutPageCtrl', function ($scope, $modal) {
    //var modalInstance;
    //// $scope.welcomeMessage = layoutService.welcomeMessage;

    //$scope.signIn = function () {
    //    // layoutService.AddTab($scope.tabs);
    //    modalInstance = $modal.open({
    //        templateUrl: 'Home/LoginPartial',
    //        controller: ModalInstanceCtrl,http://localhost:4145/#/Administrator/Branch/UpdateBranch/2    //        resolve: {
    //            items: function () {
    //                return [{ abc: '1' }];
    //            }
    //        }
    //    });
    //    //  layoutService.modalInstance = modalInstance;
    //    modalInstance.result.then(function (selectedItem) {
    //        $scope.selected = selectedItem;
    //    }, function () {

    //    });
    //};
    //$scope.navType = 'pills';
});

Gits.service('myservice', function () {
    this.TransactionFeeInformationId = "";
});

Gits.service('subMenuService', function () {
    this.MerchantId = "";
    this.CustomerId = "";
    this.BankId = "";
});

Gits.service('loginInfoService', function () {
    this.LoginInformation = [];
});



//// Please note that $modalInstance represents a modal window (instance) dependency.
//// It is not the same as the $modal service used above.   

var ModalInstanceCtrl = function ($scope, $modalInstance, items) {
    $scope.ok = function () {
        $modalInstance.close($scope.selected.item);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

//===========================Routing Information of GITS============================
function RouteConfig($routeProvider, $httpProvider) {
    $routeProvider
        .when("/Administrator/Admin/Home", { templateUrl: "Administrator/Admin/Home", controller: AdminHomeController })
        .when("/Administrator/Admin/ConstantValueList", { templateUrl: "Administrator/Admin/ConstantValueList", controller: ConstantValueController })
        .when("/Administrator/Admin/ConstantValueDetails/:id",
            {
                templateUrl: function (param) {
                   return "Administrator/Admin/ConstantValueDetails?id=" + param.id
                }, controller: ConstantValueController
            })
        .when("/Administrator/Branch/BranchCreate", { templateUrl: "Administrator/Branch/BranchCreate", controller: BankBranchAddController })
        .when("/Administrator/Branch/BranchList", { templateUrl: "Administrator/Branch/BranchList", controller: BranchController })
        .when("/Administrator/Branch/UpdateBranch/:id", {
            templateUrl: function (param) {
                return "Administrator/Branch/UpdateBranch?id=" + param.id
            }, controller: BankBranchAddController
        })
        .when("/Administrator/Branch/ToggleStatus/:id", {
            templateUrl: function (param) {                
                return "Administrator/Branch/ToggleStatus?id=" + param.id
            }, controller: BankBranchAddController
        })
        .when("/Account/Requisition", { templateUrl: "Account/Requisition", controller: RequisitionController })
        .when("/Account/RequisitionList", { templateUrl: "Account/RequisitionList", controller: RequisitionListController })
    
    
      
        .when("/Administrator/Admin/RoleList", { templateUrl: "Administrator/Admin/RoleList", controller: RoleController })
        //.when("/Administrator/Admin/BankWiseClientPolicy", { templateUrl: "Administrator/Admin/BankWiseClientPolicy", controller: BankWiseClientPolicyController })
        .when("/Administrator/Admin/RoleDetails/:Id", {
            templateUrl: function (param) { return "Administrator/Admin/RoleDetails?id=" + param.id }, controller: RoleController
        })
        //.when("/Administrator/Admin/DeleteRoleDetails/:Id", { templateUrl: "Administrator/Admin/DeleteRoleDetails", controller: DeleteRoleDetailsController })
        .when("/Administrator/Admin/UserList", { templateUrl: "Administrator/Admin/UserList", controller: UserController })
        .when("/Administrator/Admin/UserDetails/:Id", {
            templateUrl: function (param) {
                return "Administrator/Admin/UserDetails?id=" + param.id
            }, controller: UserController 
        })
    
        .when("/Account/RequisitionToggleStatus/:id", {
            templateUrl: function (param) {
                return "Account/RequisitionToggleStatus?id=" + param.id
            }, controller: RequisitionStatusController
        })
        .when("/Administrator/Admin/UserPasswordChange", { templateUrl: "Administrator/Admin/UserPasswordChange", controller: ChangePasswordController })
       
        .when("/Administrator/Admin/PasswordReset", { templateUrl: "Administrator/Admin/PasswordReset", controller: PasswordResetController })
       


       


       
        
        .when("/Error", { templateUrl: "/Home/ErrorPageNotFound" })
        .otherwise({ redirectTo: '/Error' });
   
    $httpProvider.defaults.withCredentials = true;
    $httpProvider.defaults.crossDomain = true;
    // delete $httpProvider.defaults.headers.common["X-Requested-With"];

    //Loading spinner
    $httpProvider.responseInterceptors.push('myHttpInterceptor');

    var spinnerFunction = function spinnerFunction(data, headersGetter) {
        jQuery("#spinner").show();

        return data;
    };

    $httpProvider.defaults.transformRequest.push(spinnerFunction);
    //Loading spinner end

    //responseInterceptors For Checking the session time out
    $httpProvider.responseInterceptors.push(['$q', '$location', '$window', function ($q, $location, $window) {
        return function (promise) {
            return promise.then(function (response) {


                // response.status >= 200 && response.status <= 299
                // The http request was completed successfully.
                //  console.log(response);
                //if(response.status == 302)
                //  {
                //      window.location = "/Account/Login";
                //}

                if (typeof response.data === 'string') {
                    if (response.data.indexOf instanceof Function &&
                        response.data.indexOf('<body class="login example2">') != -1) {
                        // $location.path("http://" + window.location.host + "/Account/LogOff");
                        //$window.location.href = "http://www.domain.com/login"; // just in case
                        // window.location.replace("http://" + window.location.host + "/Account/LogOff");
                        // window.location = "http://www.domain.com/login";
                        window.location = "http://" + window.location.host + "/Account/LogOff";
                        return;
                    }
                }

                return response;

            }, function (response) {

            

                if (response.status === 302 || response.status === 401) {
                 
                 

                    window.location = "/Account/Login";
                    return response;
                }
                /*
                 $q.reject creates a promise that is resolved as
                 rejectedwith the specified reason. 
                 In this case the error callback will be executed.
                */
                return $q.reject(response);
            });
        }
    }]);
}
//END responseInterceptors For Checking the session time out



Gits.config(RouteConfig);


//===========================End of Routing ==========================================
/************Factory for Loading spinner show hide********/
Gits.factory('myHttpInterceptor', function ($q, $window) {
    return function (promise) {
        return promise.then(function (response) {
            jQuery("#spinner").hide();
            return response;
        }, function (response) {
            jQuery("#spinner").hide();
            return $q.reject(response);
        });
    };
});


