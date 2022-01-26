Gits.factory('LoginFactory', function ($http) {
    alert("RNY1");
    var handler = [];
    var url = "/Account/Login/";

    handler.Authenticate = function (dataModel) {
        return $http.post(url, dataModel);
    };
    return handler;
});
Gits.controller('LoginCrtl', ['$scope', 'LoginFactory', function ($scope, LoginFactory) {
    alert("RNY");
    $scope.list = [];
    var factory = LoginFactory;
    $scope.title = "";
    var getSuccessCallback = function (data, status, event) {
        
        var target = angular.element(event.target);
        //var target = element();
        var welcomeMessage = "Welcome !" + data;
        $('#welcomeMsg').show();
        $('#welcomeMsg').val(welcomeMessage);
      //  layoutService.modalInstance.close();
    };
    var errorCallback = function (data, status, header, config) {
        alert('error : ' + data);
    };
    $scope.getSecUser = function (id) {
        factory.getById(id).success(getSuccessCallback).error(errorCallback);
    };
    $scope.Authenticate = function (Login) {

        factory.Authenticate(Login).success(getSuccessCallback).error(errorCallback);
    };
    $scope.add = function () {
        factory.create($scope.currentModel).success(successPostCallback).error(errorCallback);
    };
}]);
//// For GITS//
  //$("[id*=btnContinuee]").live("click", function () {


//      $("#dialog").dialog({
//          minHeight: 100,minWidth:400,
//          title: "Login Information",
//          buttons: {
//              Close: function () {
//                  $(this).dialog('close');
//              }

//          }

//      });


//      var user = {};
//      var amount = $("[id*=hfOrderAmount]").val();
//      var RetUrl = $("[id*=hfurl]").val();
//      user.Amount = amount;
//      //user. = RetUrl;

//      $.ajax({
//          type: "POST",
//          url: "Checkout.aspx/ProcessAjaxRequest",
//          // data: "{}",
//          data: '{user: ' + JSON.stringify(user) + '}',
//          contentType: "application/json; charset=utf-8",
//          dataType: "json",
//          success: function (msg) {
//              // Do something interesting here.
//          }
//      });


    //  return false;
  //});
  //$(document).ready(function () {
  //    debugger;
  //    $(".openDialog").live("click", function (e) {
  //        e.preventDefault();

  //        $("<div></div>")
  //       .addClass("dialog")
  //       .attr("id", $(this).attr("data-dialog-id"))
  //       .appendTo("body")
  //       .dialog({
  //           title: $(this).attr("data-dialog-title"),
  //           close: function () { $(this).remove() },
  //           modal: true
  //       })
  //       .load(this.href);
  //    });

  //    $(".close").live("click", function (e) {
  //        e.preventDefault();
  //        $(this).closest(".dialog").dialog("close");
  //    });
  //})

