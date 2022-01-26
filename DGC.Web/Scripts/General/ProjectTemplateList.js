var projectTemplateList = {};
projectTemplateList = {
    heightOfBrowser: function () {
        return $(window).height();
    },
    AddNew: function (referenceId, tableToReolad) {
        util.ShowIDPDialog(util.FullURLByAction('Project/ProjectTemplate/AddEdit'), 980, 'Project Template', '', referenceId, 0);
    },
    Edit: function (node) {
        if (node) {
            util.ShowIDPDialog(util.FullURLByAction('Project/ProjectTemplate/AddEdit/' + node.Id), 980, 'Project  Template', 'ProjectIdaaa', node.Id, 0);
        }
        else {
            $.messager.alert('Information', 'Please select a row to edit.');
        }
    },
    Delete: function () {
        var node = util.getSelectedSingleKendoNode($("#tblProjectTemplate"));
        if (node) {
            $.messager.confirm('Confirm', 'Are you sure you want to delete record?', function (r) {
                if (r) {
                    $.ajax({
                        url: util.FullURLByAction('api/ApiProjectTemplate/DeleteProjectTemplate?ProjectId=' + node.Id),
                        type: 'POST',
                        async: false,
                        success: function (result) {
                            if (result) {
                                $('#tblProjectTemplate').data('kendoGrid').dataSource.read();
                            }
                        }
                    });
                }
            });

        }
        else {
            $.messager.alert('Information', 'Please select a row to delete.');
        }
    },
    getRowIndex: function (target) {
        var tr = $(target).closest('tr.datagrid-row');
        return parseInt(tr.attr('datagrid-row-index'));
    },
    LoadViewCombo: function () {

        var arr = [{ DisplayText: "ALL", Value: "0" }, { DisplayText: "Active", Value: "Active" }, { DisplayText: "Inactive", Value: "Inactive" }];
        arr.forEach(function (obj) {
            $("#selProjectView").append($("<option></option>").val(obj.Value).html(obj.DisplayText));
        });
    },
    BuildTable:function()
    {
        var tblURL = util.FullURLByAction("Project/ProjectTemplate/GetDataByPaging");
        var dataSource = new kendo.data.DataSource({
            type: "aspnetmvc-ajax",
            transport: {
                read: {
                    url: tblURL,
                    type: "POST"
                }
            },
            schema: {
                data: function (data) {
                 
                    return data.Data; // <-- The result is just the data, it doesn't need to be unpacked.
                },
                total: function (data) {
                 
                    return data.Total; // <-- The total items count is the data length, there is no .Count to unpack.
                },
                model: {
                    fields: {
                        Id: { type: "string" },
                        ProjectTemplateName: { type: "string" },
                        AccountTypeName: { type: "string" },
                        ProjectTypeName: { type: "string" },
                        ProjectSubTypeName: { type: "string" },
                        ProjectStatus: { type: "string" }
                    }
                }



            },
            pageSize: 100,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true
        });
        $("#tblProjectTemplate").kendoGrid({
            selectable: "row",
            //change: function (e) {
            //    debugger;
            //    var selectedRows = this.select();
            //    var selectedDataItems = [];
            //    for (var i = 0; i < selectedRows.length; i++) {
            //        var dataItem = this.dataItem(selectedRows[i]);
            //        selectedDataItems.push(dataItem);
            //    }
            //    listOperation.Edit(selectedDataItems[0]);
            //    // selectedDataItems contains all selected data items
            //},
            dataSource: dataSource,
            filterable: {
                extra: true
            },
            sortable: true,
            pageable: true,
            reorderable: true,
            scrollable: true,
            columns: [
                  { field: "Id", title: "Id", hidden: "hidden" },
                  { field: "ProjectTemplateName", title: "Project Template Name", width: 200 },
                  { field: "AccountTypeName", title: "Account Type", width: 150},
                  { field: "ProjectTypeName", title: "Project Type", width: 150 },
                  { field: "ProjectSubTypeName", title: "Project Sub Type", width: 250 },
                  { field: "ProjectStatus", title: "Status", width: 100 }

            ]
        });
       
      
    },
    init : function () {
      
        projectTemplateList.BuildTable();
        $("#projectTemplateAdd").click(function () {
            projectTemplateList.AddNew($("#ProjectTemplateId"));
        });
        $("#projectTemplateEdit").click(function () {
            var node = util.getSelectedSingleKendoNode($("#tblProjectTemplate"));
            projectTemplateList.Edit(node);
        });
        $("#projectTemplateDelete").click(function () {
            projectTemplateList.Delete();
        });

        $("#reload-projectTemplate").click(function () {
            $('#tblProjectTemplate').data('kendoGrid').dataSource.read();
        });
       

    },
    ReloadProjectTemplateTable: function () {
        var grid = $("#tblProjectTemplate").data("kendoGrid");
        if (grid != null && grid != undefined) {
            grid.dataSource.read();
        }

    },
    GenerateAccountTAB: function (nodeId) {
        
        //$("#").tabs({
        //    border: false,
        //    onSelect: function (title) {
        //        var baseURL = util.GetBaseUrl();
        //        if (title == 'Overview') {

        //            //AccountUI.AccountContactRole(nodeId);
        //            //AccountUI.AccountProvisionList(nodeId);

        //            var url = 'Client/XAccountContact/GetAccountContactRole?parentId=' + nodeId;
        //            var res = util.GetRemoteData(util.GenerateFullURL(baseURL, url));
        //            var url1 = 'Client/PlanProvision/GetAccountProvisionListByAccount?parentId=' + nodeId;
        //            var res1 = util.GetRemoteData(url1);
        //            $("div[id=divAccountContactRole]").html(res);
        //            $("div[id=divAccountProvisionList]").html(res1);

        //            planProvisionContent.init();


        //        }
        //        else if (title == 'Portal Settings') {
        //            AccountUI.LoadPotalConfiguaration(nodeId);
        //            AccountUI.LoadPotalFileConfiguaration(nodeId);
        //        } else if (title == 'Funds') {
        //            AccountUI.LoadFundList(nodeId);
        //        } else if (title == 'Task/Project') {
        //            //    var url =  'Project/Task/GetChildTaskList?parentId=' + nodeId + "&&AccountId=" + nodeId;
        //            //  var res = util.GetRemoteData(util.GenerateFullURL(baseURL,url));

        //            //   $("div[id=divAccountTask]").html(res);
        //            var url1 = 'Project/Project/GetProjectTabData?parentId=' + nodeId;
        //            var res1 = util.GetRemoteData(url1);
        //            $("div[id=divProjects]").html(res1);

        //            ProjectTab.init();
        //        }
        //        else if (title == 'Alerts') {
        //            var url = 'Note/AlertNote/GetAlertNote?parentId=' + nodeId;
        //            var fullUrl = util.FullURLByAction(url);
        //            $("div[id=divAccountAlertNote]").load(fullUrl, function () {
        //                alertNoteContent.Initialize();
        //            });
        //        }
        //        else if (title == 'Billing') {
        //            // ParentEntityId =f26e5734-4dc4-e211-9725-0011258f6fb9. This id is static for Account.This is inserted into DB.


        //            var url = 'Fee/FeeAgreement/GetFeeAgreement?parentId=' + nodeId;
        //            $("div[id=divAccountFeeAgreement]").load(util.FullURLByAction(url), function () {
        //                // bankaccountcontent.init();
        //            });


        //            var url = 'Client/BankAccount/BankAccountList?parentId=' + nodeId + '&&ParentEntityId=' + "f26e5734-4dc4-e211-9725-0011258f6fb9";
        //            $("div[id=divAccountBankAccuontList]").load(util.FullURLByAction(url), function () {
        //                bankaccountcontent.init();
        //            });

        //        }
        //        else if (title == 'Services') {
        //            //var url =  'Client/AccountServiceElection/GetAccountServiceElection?parentId=' + nodeId;
        //            //var res = util.GetRemoteData(url);
        //            //$("#divServiceElection").html(res);
        //            //serviceElection.init();

        //            var url1 = 'Client/XAccountCompany/GetAccountCompany?parentId=' + nodeId;
        //            //$("#XAccountCompany").load(util.GenerateFullURL(baseURL, url1), function () {
        //            //    serviceProviderContent.init();
        //            //});

        //            var res = util.GetRemoteData(url1);
        //            $("#XAccountCompany").html(res);
        //            //   serviceProviderContent.init();

        //        }
        //        else if (title == 'Correspondence') {
        //            var url = 'Client/AccountPlan/GetFaxItems?parentId=' + nodeId;fSet
        //            var res = util.GetRemoteData(url);
        //            $("div[id=divAccountFax]").html(res);
        //        }
        //    }
        //});
    }
}

$(document).ready(function () {
    projectTemplateList.init();
    $("#tblProjectTemplate", this).dblclick(function (e) {
        var node=util.getSelectedSingleKendoNode(this);
        projectTemplateList.Edit(node);

    });
});
