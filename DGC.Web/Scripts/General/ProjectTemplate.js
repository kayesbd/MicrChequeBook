var validator;
var PersonData;
var ContactData;
var blnLoadedDropdown = false;
var dialogProjectTemplate = {};
dialogProjectTemplate = {
    PrjectOnChange: function (e) {
        var value = this.value();
        util.SetKendoComboByData($("#ddlProjectSubType"), false,
            {
                url: 'api/ApiCommon/GetProjectSubTypeData?projectTypeId=' + value
            });
    },
    AssignOnChange: function (e) {
        var data = {};
        var value = $("#ddlAssignType").data("kendoDropDownList").value();
        if (value == "Person") {
            data = util.GetDataByAjax('api/ApiCommon/GetPersonData');
        } else if (value == "Queue") {
            data = util.GetDataByAjax('api/ApiCommon/GetQueueDropDownList');
        }
        else if (value == "Role") {
            data = util.GetDataByAjax('api/ApiCommon/GetContactRole');
        }
        util.SetKendoCombo($("#autoTextOwner"), data, {
            minLength: 1,
            filter: "startswith",
            change: function (e) {
                var value = this.value();
            }
        });

    },
    CascadeProjectSubType: function (element) {
        var value = '', ele;
        ele = $(element).data("kendoDropDownList");
        if (ele != undefined) {
            value = ele.value();
        }
        util.SetKendoComboByData($("#ddlProjectSubType"), false,
            {
                url: 'api/ApiCommon/GetProjectSubTypeData?projectTypeId=' + value
            });
    },
    FillComboData: function () {
        var baseUri = util.GetBaseUrl();


        util.SetKendoComboByData($("#ddlAccountType"), false, {
            url: 'api/ApiCommon/GetAccountTypeList'
        });

        util.SetKendoComboByData($("#ddlProjectType"), true,
            {
                url: 'api/ApiCommon/GetProjectTypeData',
                change: dialogProjectTemplate.PrjectOnChange

            });
        util.SetKendoComboByData($("#ddlProjectSubType"), false,
            {
                url: 'api/ApiCommon/GetProjectSubTypeData?projectTypeId='
            });

        util.SetAssignTypeData($("#ddlAssignType"), 'Please Select Assign Type');
        util.SetFrequencyMethodData($("#ddlFrequency"), 'Please Select Frequency Type');
        var assign = $("#ddlAssignType").data("kendoDropDownList");
        assign.bind("change", dialogProjectTemplate.AssignOnChange);
    },
    RefreshPage: function () {
        $("#txtActivityPlanName").val("");
        $("#txtDescription").val("");
        $("#txtStatus").val("");
        $("#autoTextOwner").val("");
        util.RefreshKendoComboBox($('#projectTemplateForm'));
    },
    Save: function (action) {
        if (!validator.form()) {
            return false;
        }
        var baseUrl = util.GetBaseUrl();
        baseUrl = "http://" + baseUrl + "/" + action;
        var blnStatus = false;
        $.ajax({
            url: baseUrl,
            type: 'POST',
            data: $('#frmProjectTemplate').serialize(),
            async: false,
            success: function (result) {
                if (result.Success) {
               
                    $("#Id").val(result.Id);
                    blnStatus = true;
                    util.CallParentFunction('projectTemplateList.ReloadProjectTemplateTable');
                    
                }

            }
        });
        return blnStatus;
    },
    SetData: function (model) {

        if (model != undefined || model != null) {
            $("#Id").val(model.Id);
            if (model.Id == "00000000-0000-0000-0000-000000000000" || model.Id == "") {
                dialogProjectTemplate.RefreshPage();
            }
            else {
                //=========Combobox selection starts....
                util.SetKendoValue($("#ddlAccountType"), model.AccountTypeId);

                util.SetKendoValue($("#ddlProjectType"), model.ProjectTypeId);
                dialogProjectTemplate.CascadeProjectSubType($("#ddlProjectType"));
                util.SetKendoValue($("#ddlAssignType"), model.FrequencyMethod);
                util.SetKendoValue($("#ddlFrequency"), model.FrequencyMethod);


                util.SetKendoValue($("#ddlProjectSubType"), model.ProjectSubTypeId);
                util.SetKendoValue($("#ddlAssignType"), model.DefaultOwnerType);

                //==============Combobox selection end=======
                //==============texbox starts================
                $("#txtActivityPlanName").val(model.ProjectTemplateName);
                $("#txtDescription").val(model.ProjectTemplateDescription);
                dialogProjectTemplate.AssignOnChange(null);

                if (model.DefaultProjectOwnerId != "00000000-0000-0000-0000-000000000000") {
                    $("#autoTextOwner").data("kendoComboBox").value(model.DefaultProjectOwnerId);
                } else {
                    if ($("#autoTextOwner").data("kendoComboBox") != undefined) {
                        $("#autoTextOwner").data("kendoComboBox").select();
                    }
                }
                // $("#txtStatus").val(model.PercentComplete);
                //  $("#autoTextOwner").val("");
                //===== texbox starts...============



                // $("#txtProjectAccountId").val(model.AccountId);
            }
        }
    },
    ValidationInit: function () {

        validator = $("#frmProjectTemplate").validate({
            onfocusout: function (element) {
                if (!this.checkable(element)) {
                    this.element(element);
                } else {
                    $(element).valid();
                }
            },
            rules: {
                ProjectTemplateName: "required"
            },
            messages: {
                ProjectTemplateName: "Please enter task name."
            },

            errorPlacement: function (error, element) {
                util.ErrorPlacement(error, element);
            },
            debug: true,
            // specifying a submitHandler prevents the default submit, good for the demo
            submitHandler: function () {
                alert("submitted!");
            },
            // set this class to error-labels to indicate valid fields
            success: function (label, element) {
                $(label).addClass("checked");
                label.html("&nbsp;").addClass("checked");
                $(element).removeClass('on-error');
            },
            highlight: function (element, errorClass) {
                $(element).next().find("." + errorClass).removeClass("checked");
            }
        });


    },
    OpenTracker: function () {

        $("#projectTracking").css('display', 'inline-block');
        $('#projectTracking').dialog({
            title: 'Task Assignment History',
            width: 850,
            height: 500,
            closed: false,
            cache: false,
            modal: false,
            onCollapse: function () {
                alert("closed");
            }
        });

    },
    Assign: function (projectId) {
        $('#divAssignment').dialog({
            href: 'Project/ProjectAssignment/Index?projectId=' + projectId,
            title: 'Project Assignment History',
            width: 800,
            height: 600,
            closed: false,
            cache: false,
            modal: false
        });
    },
    OnTabSelect: function (e) {
        var selectedIndex = $(e.item).index();
        dialogProjectTemplate.ReloadTabs(selectedIndex);
       
      
    },
    ReloadTabs: function (selectedIndex)
    {
      
        if (selectedIndex == 1) {
            $($('#divProjectTemplateTab').find('a.k-link')[1]).data('contentUrl', util.FullURLByAction('Project/XProjectTemplateProduct/Index?projectTempId=' + $("#Id").val()));
        } else if (selectedIndex == 2) {
            $($('#divProjectTemplateTab').find('a.k-link')[2]).data('contentUrl', util.FullURLByAction('Project/XProjectTemplateAccountSubType/Index?projectTempId=' + $("#Id").val()));
        }
        else if (selectedIndex == 0) {
            
            $($('#divProjectTemplateTab').find('a.k-link')[0]).data('contentUrl', util.FullURLByAction('Project/ProjectTemplateTask/Index'));
        }
        else if (selectedIndex == 3) {
            $($('#divProjectTemplateTab').find('a.k-link')[3]).data('contentUrl', util.FullURLByAction('Project/XProjectTemplateProjectSubType/Index'));
        }
    },
    OnTabActivated: function (e) {
        //debugger;
        //var item = $(e.item);
        ////var link = $(item).find('.t-link');
        ////var contentUrl = $link.data('ContentUrl');
    },
    deleteProjectTempleteItem: function (id) {

        $.messager.confirm('Confirm', 'Are you sure you want to delete record?', function (r) {
            if (r) {
                $.ajax({
                    url: util.FullURLByAction('api/ApiProjectTemplate/DeleteProjectTemplate?ProjectId=' + id),
                    type: 'POST',
                    async: false,
                    success: function (result) {
                        if (result) {
                            
                            util.CallParentFunction('projectTemplateList.ReloadProjectTemplateTable');
                            util.CloseIFrame();
                        }
                    }
                });
            }
        });
    },
    SaveProjectTemplate:function(blnSave)
    {
        if (blnSave == undefined)
            blnSave = false;

        $.messager.confirm('Confirm', 'Are you sure you want to save record?', function (r) {
            if (r) {
                var res = dialogProjectTemplate.Save('api/ApiProjectTemplate/Save');
               
                if (res) {

                    if (blnSave) {
                        util.CloseIFrame();
                    }
                    debugger;
                    $("#divProjectTemplateTab").data("kendoTabStrip").select();
                    util.HideAllChildActionButton();
                }
            } else {
                return false;
            }
        });
    },
    onTabLoaded:function()
    {
       
        var paramId = util.getUrlVars();
        var baseId =  $("#Id").val();
        if (paramId === undefined || paramId == null || paramId.length != 36) {
            if (baseId === undefined || baseId == null || baseId.length != 36) {
                util.HideAllChildActionButton();
            }
        }
    },
    LoadProjectTemplateTabs: function () {
        $("#divProjectTemplateTab").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            },
            select: dialogProjectTemplate.OnTabSelect,
            activate: dialogProjectTemplate.OnTabActivated,
            contentLoad: dialogProjectTemplate.onTabLoaded,
            contentUrls: [
                util.FullURLByAction('Project/ProjectTemplateTask/Index'),
                util.FullURLByAction('Project/XProjectTemplateProduct/Index?projectTempId=' + $("#Id").val()),
                util.FullURLByAction('Project/XProjectTemplateAccountSubType/Index?projectTempId=' + $("#Id").val()),
                util.FullURLByAction('Project/XProjectTemplateProjectSubType/Index')]
        });
       
    },
    init: function () {
        dialogProjectTemplate.FillComboData();

        var paramId = util.getUrlVars();
        if (paramId.length === 36) {
            $.ajax({
                url: util.FullURLByAction('api/ApiProjectTemplate/GetProjectTemplateForEdit?Id=' + paramId),
                type: 'GET',
                dataType: 'json',
                async: false,
                success: function (result) {
                    dialogProjectTemplate.SetData(result, '');
                    dialogProjectTemplate.LoadProjectTemplateTabs();
                }
            });
            //  AccountUI.GenerateAccountTAB(paramId);
        }
        else {
            dialogProjectTemplate.RefreshPage();
            dialogProjectTemplate.LoadProjectTemplateTabs();
            
           
        }
     

        $("#lnkProjectTemplateDelete").click(function () {
            dialogProjectTemplate.deleteProjectTempleteItem($("#Id").val());
        });

        $("#lnkProjectTemplateSave").click(function () {
            dialogProjectTemplate.SaveProjectTemplate(true);
        });
        $("#lnkProjectTemplateSaveOnly").click(function () {
            dialogProjectTemplate.SaveProjectTemplate(false);
        });


        $("#lnkCopyTaskTemplate").click(function () {
            var url = util.FullURLByAction('Project/ProjectTemplate/CopyTask');
            $("#divCopyTaskFrom").kendoWindow({
                animation: {
                    open: false
                },
                actions: ["Close"],
                draggable: true,
                height: "250px",
                modal: true,
                pinned: false,
                position: {
                    top: 100,
                    left: 100
                },
                resizable: false,
                title: "Modal Window",
                width: "300px",
                content: url,
                iframe: false

            });



        });

    
    },

}






$(document).ready(function () {
    dialogProjectTemplate.init();
    dialogProjectTemplate.ValidationInit();

   // $(".").
});

