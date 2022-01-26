var util = new GITS.Utils();
util.DbManager('adminFactory', "/api/ApiAdmin/");

var DeleteUserDetailsController = [
    '$scope', 'adminFactory', '$routeParams', '$location', '$http', function($scope, adminFactory, $routeParams, $location, $http) {
        var onError = function(data, status, header, config) {
            //alert('error : ' + data);
            jQuery('#errorModalText').text(data);
            jQuery('#errorModal').modal('show');
        };

        $http({
            method: 'GET',
            url: '/api/ApiUserType/UserTypeList',
        }).success(function(result) {
            $scope.userTypeList = result;
        });

        $http({
            method: 'GET',
            url: '/api/ApiRole/RoleList',
        }).success(function(result) {
            $scope.roleList = result;
        });

        $http({
            method: 'GET',
            url: '/api/ApiBank/GetBankList',
        }).success(function(result) {
            $scope.bankList = result;
        });

        var setUserInformation = function(data, status) {
            $scope.userVM = data;
        };

        if ($routeParams != undefined) {
            var userId = $routeParams.Id;
        }

        if (userId != undefined && userId > 0) {
            adminFactory.getById(userId, "GetUserInformation").success(setUserInformation).error(onError);
        }

        var goUserListPage = function(data, status, header, config) {
            location.href = "/#/Administrator/Admin/UserList";
        };

        $scope.delete = function() {
            var result = confirm("Are you sure want to delete the selected user information?");
            if (result == true) {
                adminFactory.delete(userId, "DeleteUser").success(function() {
                    jQuery('#successModalText').text('User information is deleted successfully');

                    jQuery('#successModal').modal('show');
                    //alert('User information is deleted successfully');
                    //goUserListPage();
                }).error(onError);
            }
        };

        jQuery('#successModal').one('hidden.bs.modal', function (e) {
            //var host = window.location.host;
            window.history.back();
        });


        $scope.cancel = function() {
            goUserListPage();
        };
    }
];