var util = new GITS.Utils();
util.DbManager('adminFactory', "/api/ApiAdmin/");
var PasswordResetController = ['$scope', 'adminFactory', '$routeParams', '$location', '$http', '$window', function ($scope, adminFactory, $routeParams, $location, $http, $window) {

   
    $scope.cancel = function ()
    {
        $location.url('/Administrator/Admin/Home');
       
        //$window.history.go(-1);
    };
    $scope.save = function (passwordResetForm) {
     

        if (passwordResetForm.$invalid) {
            $scope.passwordResetForm.$invalid = true;
            $scope.submitted = true;
            return;
        }
        else {
            if ($scope.userVM == undefined) {
                $scope.userVM = {};
            }
            $scope.loading = true;
            adminFactory.create($scope.userVM, "ResetPasswordByAdmin").success(successPostCallback).error(errorCallback);
        }
    };

    var successPostCallback = function () {
        $scope.loading = false;
        jQuery('#successModal').modal('show');
        jQuery('#successModalText').text("Password has been reset successfully.");
       
    };
    var errorCallback = function (data) {
        $scope.loading = false;
        if (data == null) {
            jQuery('#errorModalText').text("Data is not found.");
        } else {
            jQuery('#errorModalText').text(data.Message);
        }
        jQuery('#errorModal').modal('show');
    };

    
    
}];


