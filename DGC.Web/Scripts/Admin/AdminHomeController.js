
var util = new GITS.Utils();
util.DbManager('adminFactory', "/api/ApiAdmin/");

var AdminHomeController = [
    '$scope', 'adminFactory', '$routeParams', '$location', '$http', 'loginInfoService', function($scope, adminFactory, $routeParams, $location, $http, loginInfoService) {
        jQuery(function() {
            var pgurl = window.location.href.substr(window.location.href.lastIndexOf("/") + 1);
            jQuery("page-sidebar-menu li a ").each(function() {
                if (jQuery(this).attr("href") == pgurl || $(this).attr("href") == '')
                    jQuery(this).addClass("active");
                if (jQuery(this).attr("ng-href") == pgurl || $(this).attr("href") == '')
                    jQuery(this).addClass("active");
            });
        });
        jQuery(function() {
            var pgurl = window.location.href.substr(window.location.href.lastIndexOf("/") + 1);
            jQuery("page-sidebar-menu li a ").each(function() {
                if (jQuery(this).attr("href") == pgurl || $(this).attr("href") == '')
                    jQuery(this).addClass("active");
                if (jQuery(this).attr("ng-href") == pgurl || $(this).attr("href") == '')
                    jQuery(this).addClass("active");
            });
        });


        // Set LoginInformation Service
        $scope.loginInfoService = loginInfoService;

        var onError = function(data, status, header, config) {
            // alert('error : ' + data);
            jQuery('#errorModalText').text(data);
            jQuery('#errorModal').modal('show');
        };

        var setLoginInformation = function(data, status) {
            $scope.loginInfoService.LoginInformation = data;
        };

        adminFactory.get("GetLoginInformation").success(setLoginInformation).error(onError);
    }
];