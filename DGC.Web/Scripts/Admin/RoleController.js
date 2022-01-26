var util = new GITS.Utils();
util.DbManager('adminFactory', "/api/ApiAdmin/");

var RoleController = [
    '$scope', 'adminFactory', '$routeParams', '$location', '$http', '$window', function ($scope, adminFactory, $routeParams, $location, $http, $window) {
        //util.Menu();
        var onError = function (data, status, header, config) {
            jQuery('#errorModalText').text(data);
            jQuery('#errorModal').modal('show');

            //alert('error : ' + data);
        };

        // To Display Role List
        $scope.displayRoleList = function () {
            debugger;
            $scope.loading = true;
            var dataSourceURL = "/Administrator/Admin/GetRoleListByPagination";
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
                    model: {
                        fields: {
                            Id: { type: "string" },
                            RoleName: { type: "string" },
                            RoleDescription: { type: "string" }
                        }
                    }
                },
                sort: [
                    { field: "RoleName", dir: "asc" },
                    { field: "RoleDescription", dir: "asc" },
                ],
                pageSize: 10,
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true
            });

            jQuery("#tblRoleList").kendoGrid({
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
                    //{ field: "Id", title: "Id", hidden: "hidden" },
                    { field: "RoleName", title: "Role Name", width: 300 },
                    { field: "RoleDescription", title: "Role Description", },
                    {
                        width: 60,
                        field: "Id",
                        title: "Edit",
                        sortable: false,
                        filterable: false,
                        template: function (dataItem) {
                            if (typeof dataItem.Id == "string") {
                                if (dataItem.Id == 1) {
                                    return "";
                                }
                                return "<span><a href=\#/Administrator/Admin/RoleDetails/" + dataItem.Id + "\> <i class='glyphicon glyphicon-edit'></i></a></span>";
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
                    //            if (dataItem.Id == 1) {
                    //                return "";
                    //            }
                    //            return "<span><a href=\#/Administrator/Admin/DeleteRoleDetails/" + dataItem.Id + "\> <i class='glyphicon glyphicon-remove'></i></a></span>";
                    //        }
                    //    }
                    //}
                ]
            });
        };
        $scope.displayRoleList();

        // To Display Role Information
        var setRoleInformation = function (data, status) {
            $scope.roleVM = data;
        };

        if ($routeParams != undefined) {
            var roleId = $routeParams.Id;
        }

        if (roleId != undefined && roleId > 0) {
            adminFactory.getById(roleId, "GetRoleInformation").success(setRoleInformation).error(onError);
            $scope.IsAdmin = true;
        }

        // To Display Menu List
        $scope.displayMenuList = function () {
           
            var dataSourceURL = "/Administrator/Admin/GetMenuList/" + roleId;
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
                        var result = data.Data;
                        selectedMenus.length = 0;
                        for (var i = 0; i < result.length; i++) {
                            if (result[i].IsMapped) {
                                var menuId = result[i].Id.toString().trim();
                                if (selectedMenus.length > 0) {
                                    if (jQuery.inArray(menuId, selectedMenus) < 0) {
                                        selectedMenus.push(menuId);
                                    }
                                } else {
                                    selectedMenus.push(menuId);
                                }
                            }
                        }
                        return result;
                    },
                    total: function (data) {
                        return data.Total;
                    },
                    model: {
                        fields: {
                            Id: { type: "string" },
                            MenuName: { type: "string" },
                            MenuOrder: { type: "int" },
                            ParentId: { type: "int" },
                            PageId: { type: "int" },
                        }
                    }
                },
                //pageSize: 10,
                serverPaging: false,
                serverFiltering: false,
                serverSorting: false,
                serverOperation: false,
            });
            jQuery("#tblMenuList").kendoGrid({
                selectable: "row",
                dataSource: dataSource,
                filterable: {
                    extra: true
                },
                sortable: true,
                height: 400,
                pageable: false,
                reorderable: true,
                scrollable: true,
                columns: [
                    {
                        width: 70,
                        field: "IsMapped",
                        title: "Select",
                        sortable: false,
                        filterable: false,
                        template: function (dataItem) {
                            if (dataItem.IsMapped) {
                                return "<input type='checkbox' checked='checked' style='width: 15px; height: 15px;' onchange='collectSelectedMenus(" + dataItem.Id.toString().trim() + ", this)' />";
                            } else {
                                return "<input type='checkbox' style='width: 15px; height: 15px;' onchange='collectSelectedMenus(" + dataItem.Id.toString().trim() + ", this)' />";
                            }
                        }
                    },
                    { field: "Id", title: "Menu Id" },
                    { field: "MenuName", title: "Menu Name" },
                    { field: "MenuOrder", title: "Menu Order", filterable: false, width: 110 },
                    { field: "ParentId", title: "Parent Id", filterable: false, width: 110 },
                    {
                        field: "ParentMenu",
                        title: "Parent Menu",
                        sortable: false,
                        filterable: false,
                        template: function (dataItem) {
                            return dataItem.MenuName;
                        }
                    },
                    { field: "PageId", title: "Page Id", filterable: false, width: 110 },
                    {
                        field: "Page",
                        title: "Page",
                        sortable: false,
                        filterable: false,
                        template: function (dataItem) {
                            if (dataItem.Page != null) {
                                return dataItem.Page.PageName;
                            } else {
                                return "";
                            }
                        }
                    }
                ]
            });
        };
        $scope.loading = false;
        $scope.displayMenuList();

        // To Add, Update Role Information
        var goRoleListPageAdd = function (data, status, header, config) {
            if (data.Success) {
                jQuery('#successModalText').text('Role information is saved successfully');
                jQuery('#successModal').modal('show');
                //alert('Role information is saved successfully');
                //location.href = "/#/Administrator/Admin/RoleList";
            } else {
                jQuery('#errorModalText').text(data.ErrorMessage);
                jQuery('#errorModal').modal('show');

                //alert(JSON.stringify(data.ErrorMessage));
            }
        };

        var goRoleListPageUpdate = function (data, status, header, config) {
            if (data.Success) {
                jQuery('#successModalText').text('Role information is saved successfully');
                jQuery('#successModal').modal('show');
                //alert('Role information is Updated successfully');
                //location.href = "/#/Administrator/Admin/RoleList";
            } else {
                jQuery('#errorModalText').text(data.ErrorMessage);
                jQuery('#errorModal').modal('show');
                //alert(JSON.stringify(data.ErrorMessage));
            }
        };
        var goAdminHome = function (data, status, header, config) {
            location.href = "#/Administrator/Admin/Home";
        };

        $scope.saveRole = function (roleForm) {
            if (roleForm.$invalid) {
                $scope.showValidation = true;
                $scope.submitted = true;
                $scope.toggleValidationSummary = function () {
                    $scope.showValidation = !$scope.showValidation;
                };
                return;
            } else {
                debugger;
                $scope.roleVM.RoleWiseMenus = [];
                if (selectedMenus.length > 0) {
                    for (var i = 0, l = selectedMenus.length; i < l; i++) {
                        var obj = {
                            //RoleId: $scope.RoleId,
                            MenuId: selectedMenus[i]
                        };
                        $scope.roleVM.RoleWiseMenus.push(obj);
                    }
                }

                if ($scope.roleVM.Id == undefined || $scope.roleVM.Id <= 0) {
                    adminFactory.create($scope.roleVM, "CreateRole").success(goRoleListPageAdd).error(onError);
                } else {
                    adminFactory.update($scope.roleVM, "UpdateRole").success(goRoleListPageUpdate).error(onError);
                }
            }
        };

        $scope.cancel = function () {
            //goAdminHome();
            $window.history.go(-1);
        };

        $scope.delete = function () {
            var result = confirm("Are you sure want to delete this role information?");
            if (result == true) {
                adminFactory.delete(roleId, "DeleteRole").success(function () {
                    jQuery('#successModalText').text('Role information is deleted successfully');
                    jQuery('#successModal').modal('show');
                    //alert('Role information is deleted successfully');
                    //location.href = "/#/Administrator/Admin/RoleList";
                }).error(onError);
            }
        };
        jQuery('#successModal').one('hidden.bs.modal', function (e) {
            //var host = window.location.host;
            // window.history.back();
            location.href = "#/Administrator/Admin/RoleList";
        });
    }
];

//var selectedMenus = [];

//function collectSelectedMenus(menuId, chkBox) {
//    var $cb = jQuery(chkBox);
//    var isSelected = $cb.is(':checked');
//    if (isSelected === true) {
//        if ($scope.selectedMenus.length > 0) {
//            if (jQuery.inArray(menuId, $scope.selectedMenus) < 0) {
//                $scope.selectedMenus.push(menuId);
//            }
//        } else {
//            $scope.selectedMenus.push(menuId);
//        }
//    } else {
//        $scope.selectedMenus.splice($scope.selectedMenus.indexOf(menuId), 1);
//    }
//}

var selectedMenus = [];
var collectSelectedMenus = function (menuId, chkBox) {
    debugger;
    menuId = menuId.toString();
    if (jQuery(chkBox).is(':checked') === true) {
        if (selectedMenus.length > 0) {
            if (jQuery.inArray(menuId, selectedMenus) < 0) {
                selectedMenus.push(menuId);
            }
        } else {
            selectedMenus.push(menuId);
        }
    }
    else {

        var index = selectedMenus.indexOf(menuId.toString());
        if (index != -1) {
            selectedMenus.splice(index, 1);
        }

    }
};
