
var util = new GITS.Utils();
util.DbManager('adminFactory', "/Account/");


var UserResetPasswordController=  ['$scope', 'adminFactory', '$routeParams', '$location', '$http',function ($scope, adminFactory, $routeParams, $location, $http) {

    
    
  //  alert(MyVariableFromViewBag);

    $http({
        method: 'GET',
        url: '/Account/GetQuestionList?Key=c2hvaGFudWxAbnVzcGF5LmNvbQ==',
    }).success(function (result) {
        $scope.questionVM = result;

    });

}];