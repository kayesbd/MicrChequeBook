var util = new GITS.Utils();
util.DbManager('adminFactory', "/api/ApiAdmin/");

var UserController = [
    '$scope', 'adminFactory', '$routeParams', '$location', '$http', function ($scope, adminFactory, $routeParams, $location, $http) {

        $scope.cancel = function () {
            window.history.back();
        };

        jQuery('#successModal').one('hidden.bs.modal', function (e) {
            //window.history.back();
            location.href = "#/Administrator/Admin/UserList";
        });

        var onError = function (data, status, header, config) {
            jQuery('#errorModalText').text(data);
            jQuery('#errorModal').modal('show');
        };

        var goDestination = function (data, status, header, config) {
            if (data.Success) {
                jQuery('#successModalText').text('User information is saved successfully.');
                jQuery('#successModal').modal('show');
            } else {
                jQuery('#errorModalText').text(JSON.stringify(data.ErrorMessage));
                jQuery('#errorModal').modal('show');
            }
        };

        jQuery("#UserMobileNumber").intlTelInput({
            //preferredCountries: ["us", "gb"]
        });

        if ($routeParams != undefined) {
            $scope.userIdForPassword = $routeParams.Id;
        }

        $scope.EMAIL_REGEXP = /^[a-zA-Z0-9-_.]+@[a-zA-Z0-9]+?\.[a-zA-Z]{2,3}$/;

        $scope.UserMobileNumberKeyPress = function () {
            $scope.UserMobileNumber = jQuery("#UserMobileNumber").intlTelInput().val();
            if ($scope.UserMobileNumber == undefined || $scope.UserMobileNumber.length == 0) {
                $scope.IsRequiredUserMobileNo = true;
            } else {
                $scope.IsRequiredUserMobileNo = false;
            }
            if ($scope.UserMobileNumber.length > 15) {
                $scope.IsExceededLengthUserMobileNo = true;
            } else {
                $scope.IsExceededLengthUserMobileNo = false;
            }
            if ($scope.UserMobileNumber.length < 8) {
                $scope.IsLessThanMinLengthUserOwnerMobileNo = true;
            } else {
                $scope.IsLessThanMinLengthUserOwnerMobileNo = false;
            }
        }

        $scope.displayUserList = function () {
            $scope.loading = true;
            var dataSourceURL = "/Administrator/Admin/GetUserListByPagination";
            var dataSource = new kendo.data.DataSource({
                type: "aspnetmvc-ajax",
                transport: {
                    read: {
                        url: dataSourceURL,
                        type: "POST"
                    }
                },
                schema: {
                    data: function (data) {
                        return data.Data;
                    },
                    total: function (data) {
                        return data.Total;
                    },

                },
                sort: [
                    { field: "UserName", dir: "asc" },
                    { field: "Email", dir: "asc" },
                    { field: "UserTypeName", dir: "asc" },
                    { field: "RoleName", dir: "asc" }
                ],
                pageSize: 10,
                serverPaging: true,

            });
            jQuery("#tblUserList").kendoGrid({
                selectable: "row",
                dataSource: dataSource,
                filterable: {
                    extra: true
                },
                sortable: true,
                pageable: true,
                reorderable: true,
                scrollable: true,
                columns: [
                    { field: "UserName", title: "Username", },
                    { field: "Email", title: "Email", },
                    { field: "UserType", title: "User Type", },
                    { field: "RoleName", title: "Role", },
                    {
                        field: "IsApproved",
                        title: "Approved Status",
                        filterable: false,
                        template: function (dataItem) {
                            if (dataItem.IsApproved) {
                                return "Yes";
                            } else {
                                return "No";
                            }
                        }
                    },
                    {
                        field: "StrLastLoginDate",
                        title: "Last Login",
                        filterable: false,
                        //template: function (dataItem) {
                        //    if (dataItem.LastLoginDate == undefined || dataItem.LastLoginDate == null) {
                        //        return "";
                        //    } else {
                        //        //return new Date(parseInt(dataItem.LastLoginDate.substr(6)));
                        //        //return kendo.parseDate(dataItem.LastLoginDate);
                        //        // dd-MMM-yyyy hh:mmtt
                        //        var date = kendo.parseDate(dataItem.LastLoginDate, "yyyy/MM/dd");
                        //        return kendo.toString(date, "MM/dd/yy hh:mmtt");
                        //        //return kendo.toString(date1, "dddd dd MMM, yyyy hh:mmtt");
                        //        //return kendo.toString(date1, "MMM dd");
                        //    }
                        //}
                    },
                    {
                        width: 60,
                        field: "Id",
                        title: "Edit",
                        sortable: false,
                        filterable: false,
                        template: function (dataItem) {
                            if (dataItem.Id != null) {
                                return "<span><a href=\#/Administrator/Admin/UserDetails/" + dataItem.Id + "\> <i class='glyphicon glyphicon-edit'></i></a></span>";
                            } else {
                                return null;
                            }
                        }
                    },
                    {
                        width: 100,
                        field: "Id",
                        title: "Audit History",
                        sortable: false,
                        filterable: false,
                        attributes: { style: "text-align: center;" },
                        template: function (dataItem) {
                            if (dataItem.Id != null) {
                                return "<span><a href=\#/Administrator/Admin/AuditHistoryByUser/" + dataItem.Id + "\> <i class='glyphicon glyphicon-th-list'></i></a></span>";
                            } else {
                                return null;
                            }
                        }
                    }
                    //,
                    //{
                    //    field: "Id",
                    //    title: "Delete",
                    //    width: 60,
                    //    sortable: false,
                    //    filterable: false,
                    //    template: function (dataItem) {
                    //        if (typeof dataItem.Id == "string") {
                    //            return "<span><a href=\#/Administrator/Admin/DeleteUserDetails/" + dataItem.Id + "\> <i class='glyphicon glyphicon-remove'></i></a></span>";
                    //        }
                    //    }
                    //}
                ]
            });
        };
        $scope.displayUserList();

        $scope.loading = false;

        // To Display User Information
        $scope.userTypeList = null;
        $scope.roleList = null;

        $http({
            method: 'GET',
            url: '/api/ApiList/UserTypeList'
        }).success(function (result) {
            $scope.userTypeList = result;
        });

        $http({
            method: 'GET',
            url: '/api/ApiRole/RoleList'
        }).success(function (result) {
            $scope.roleList = result;
        });

        $http({
            method: 'GET',
            url: '/api/ApiList/InstitutionList'
        }).success(function (result) {
            $scope.institutionList = result;
        });

        $http({
            method: 'GET',
            url: '/api/ApiBank/GetBankList'
        }).success(function (result) {
            $scope.bankList = result;
        });

        $http({
            method: 'GET',
            url: '/api/ApiList/TimeZoneList'
        }).success(function (result) {
            $scope.utcTimeZoneList = result;
        });

        var setUserInformation = function (data) {
            $scope.personalInfoVM = data;
            $scope.userVM = data.UserInformation;
            jQuery("#UserMobileNumber").intlTelInput("setNumber", $scope.personalInfoVM.MobileNumber);
        };

        if ($routeParams != undefined) {
            var userId = $routeParams.Id;
        }

        if (userId != undefined && userId > 0) {
            adminFactory.getById(userId, "GetUserInformation").success(setUserInformation).error(onError);
            $scope.IsAdmin = true;
        }

        function validate() {
            var result = true;
            $scope.IsPasswordAgeZero = false;
            $scope.IsRequiredUserMobileNo = false;
            $scope.IsLessThanMinLengthUserOwnerMobileNo = false;
            $scope.IsUserNameInvalid = false;
            $scope.IsPasswordInvalid = false;

            if ($routeParams.Id < 1) {
                if ($scope.UserName != undefined && $scope.UserName.length < 8) {
                    $scope.IsUserNameIsLess = true;
                    result = false;
                }
                if ($scope.UserName.toLowerCase() === "admin" || $scope.UserName.toLowerCase() === "administrator") {
                    $scope.IsUserNameInvalid = true;
                    result = false;
                }
            }

            if ($scope.UserMobileNumber == undefined || $scope.UserMobileNumber.length === 0) {
                $scope.IsRequiredUserMobileNo = true;
                result = false;
            }
            if ($scope.UserMobileNumber.length > 15) {
                $scope.IsExceededLengthUserMobileNo = true;
                result = false;
            }
            if ($scope.UserMobileNumber.length < 8) {
                $scope.IsLessThanMinLengthUserOwnerMobileNo = true;
                result = false;
            }

            if ($scope.PasswordAge <= 0) {
                $scope.IsPasswordAgeZero = true;
                result = false;
            }
            return result;
        }

        $scope.saveUser = function (userForm) {
            $scope.UserMobileNumber = jQuery("#UserMobileNumber").intlTelInput().val();
            $scope.PasswordAge = jQuery("#PasswordAge").val();
            $scope.UserName = jQuery("#UserName").val();
            $scope.isValidated = validate();
         
            if (($scope.isValidated === false) || userForm.$invalid) {
                $scope.submitted = true;
                return;
            } else {
                $scope.personalInfoVM.MobileNumber = $scope.UserMobileNumber;
                $scope.personalInfoVM.UserInformation = $scope.userVM;
                if ($scope.userVM.Id == undefined || $scope.userVM.Id <= 0) {
                    adminFactory.create($scope.personalInfoVM, "CreateUser").success(goDestination).error(onError);
                } else {
                    adminFactory.update($scope.personalInfoVM, "UpdateUser").success(goDestination).error(onError);
                }
            }
        };

        $scope.delete = function () {
            var result = confirm("Are you sure want to delete this user information?");
            if (result === true) {
                adminFactory.delete(userId, "DeleteUser").success(function () {
                    jQuery('#successModalText').text('User information is deleted successfully');
                    jQuery('#successModal').modal('show');
                }).error(onError);
            }
        };
    }
];

var ChangePasswordController = ['$scope', 'adminFactory', '$routeParams', '$http', function ($scope, adminFactory, $routeParams, $http) {
    var onError = function (data, status, header, config) {
        jQuery('#errorModalText').text(data);
        jQuery('#errorModal').modal('show');
    };

    var goToHome = function (data, status, header, config) {
        if (data.Success) {
            jQuery('#successModalText').text('Password is changed successfully');
            jQuery('#successModal').modal('show');
        } else {
            jQuery('#errorModalText').text(JSON.stringify(data.ErrorMessage));
            jQuery('#errorModal').modal('show');
        }
    };

    $scope.changePassword = function (changePasswordForm) {
        if (changePasswordForm.$invalid) {
            $scope.showValidation = true;
            $scope.submitted = true;
            $scope.toggleValidationSummary = function () {
                $scope.showValidation = !$scope.showValidation;
            };
            return;
        } else {
            if ($scope.userInfoVM != undefined) {
                adminFactory.update($scope.userInfoVM, "ChangePassword").success(goToHome).error(onError);
            }
        };
    };

    //jQuery('#successModal').one('hidden.bs.modal', function (e) {
    //    location.href = "/Account/LogOff";
    //});
}];