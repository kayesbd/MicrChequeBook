var GITS = {};

var util = {};
var loadingCounter = 1;
util = {
    
    GetDataByAjax: function (url) {
        var res;
        url = util.FullURLByAction(url);
        $.ajax({
            url: url,
            type: 'GET',
            async: false,
            success: function (result) {

                res = result;
            }
        });
        return res;
    },

    GenerateKendoAutoCompleteBox: function (element, data, optAuto) {
        var options = {
            dataSource: data,
            dataTextField: "Text",
            dataValueField: "Value",
            filter: "startswith",
            placeholder: "Please Select..."
        };
        options = $.extend({}, options, optAuto);
        $(element).kendoAutoComplete(options);
    },
    getSelectedSingleKendoNode: function (element) {
        var grid = $(element).data("kendoGrid");
        var selectedRows = grid.select();
        var selectedDataItems = [];
        for (var i = 0; i < selectedRows.length; i++) {
            var dataItem = grid.dataItem(selectedRows[i]);
            selectedDataItems.push(dataItem);
        }
        return selectedDataItems[0];
    },
    getSelectedKendoNodes: function (element) {
        var grid = $(element).data("kendoGrid");
        var selectedRows = grid.select("tr");
        var selectedDataItems = [];
        for (var i = 0; i < selectedRows.length; i++) {
            var dataItem = grid.dataItem(selectedRows[i]);
            selectedDataItems.push(dataItem);
        }
        return selectedDataItems;
    },
    getSelectedKendoNodeIds: function (element) {
        var grid = $(element).data("kendoGrid");
        var selectedRows = grid.select();
        var selectedDataItems = [];
        for (var i = 0; i < selectedRows.length; i++) {
            var dataItem = grid.dataItem(selectedRows[i]);
            selectedDataItems.push(dataItem);
        }
        return selectedDataItems;
    },
    getUrlVars: function () {
        var id = location.pathname.substring(location.pathname.lastIndexOf("/") + 1);
        return id;
    },
    GetParentFooterDiv: function (name) {
        var footerDiv = $(name, window.parent.document);
        if (footerDiv.length > 0) {
            return footerDiv;
        } else {
            return $(name);
        }
    },
    KendoNumericSetValue: function (element, value) {
        var numerictextbox = $(element).data("kendoNumericTextBox");
        numerictextbox.value(value);
    },

    GetElement_ById: function (id) {
        var currentFrame = util.getFrameElement();
        if (window.parent == null)
            return null;

        var iframes = $('iframe', top.document);//.getElementsByTagName('iframe');
        for (var i = iframes.length; i-- > 0;) {
            var iframe = iframes[i];
            if ($(iframe).attr("id") === $(currentFrame).attr("_parentId")) {
                var tempElement = $('#' + id, $(iframe).contents());
                //  $('#' + id, $(iframe).contents()).treegrid('reload');
                ///testing

                ///
                return $(tempElement);
            }
        }


    },




    GetUniqueId: function (divIdprefix, className) {
        var objGuid = divIdprefix + util.guid();
        var divGuid = "div#" + objGuid;
        var div = $("<div class='" + className + "' id=" + objGuid + "></div>");
        var li = $("<li></li>").append(div);
        li.appendTo($("#ulMinimizeBar"));
        return divGuid;
    },

    GetURLParameter: function (sParam) {
        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        for (var i = 0; i < sURLVariables.length; i++) {
            var sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] == sParam) {
                return sParameterName[1];
            }
        }
    },
    GetRemoteData: function (url) {
        var htmlData;

        if (url.indexOf("http") < 0) {
            var baseUrl = util.GetBaseUrl();
            url = "http://" + baseUrl + "/" + url;
        }
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'html',
            context: document.body,
            processData: false,
            async: false,
            success: function (result) {

                // var result = $.parseHTML(result,document,true);
                htmlData = result;

            }
        });

        return htmlData;
    },
    FormateJSDate: function (dt) {

        if (dt === undefined)
            return;
        var numDate = new Date(parseInt(dt.substr(6)));
        var val = (numDate.getMonth() + 1) + "/" + numDate.getDate() + "/" + numDate.getFullYear();
        return val;


    },
    FormateDate: function (dt) {
        numDate = new Date(dt);
        var formatedDate = (numDate.getMonth() + 1) + "/" + numDate.getDate() + "/" + numDate.getFullYear();
        return formatedDate;
    },
    FormateDateForQuery: function (dt) {

        numDate = new Date(dt);
        var formatedDate = (numDate.getMonth() + 1) + "/" + numDate.getDate() + "/" + numDate.getFullYear();
        return formatedDate;
    },

    convertTrueFalse: function (val, row) {

        if (val == 'true') {
            return 'Yes';
        } else {

            return 'No';
        }

    },
    FormateDates: function (val, row) {

        if (val == null) {
            val = "";
            return val;
        }
        var numDate = eval("new " + val.slice(1, -1));
        var val = numDate.getDate() + "/" + (numDate.getMonth() + 1) + "/" + numDate.getFullYear();
        return val;
    },
    OnlyDate: function (val, row) {

        if (!val) {
            val = "";
            return val;
        }
        if (val == null) {
            val = "";
            return val;
        }
        var numDate = new Date(val);

        var val = numDate.getDate() + "/" + (numDate.getMonth() + 1) + "/" + numDate.getFullYear();
        return val;
    },
    FormateAmount: function (val, row) {
        if (val == 0) {
            return '0.00';
        }
        val = "$" + val;
        return val;
    },
    SetKendoComboByData: function (element, asyncBool, optsKendoDdl) {
        var asBoolData = false;
        if (asyncBool == undefined || asyncBool === null) {
            asBoolData = false;
        } else {
            asBoolData = asyncBool;
        }

        var baseUrl = util.GetBaseUrl();

        optsKendoDdl.url = "http://" + baseUrl + "/" + optsKendoDdl.url;
        $.ajax({
            url: optsKendoDdl.url,
            type: 'GET',
            async: asBoolData,
            success: function (result) {

                util.SetKendoComboByArrayData_ByOptions(element, result, optsKendoDdl);
            }
        });
    },
    SetKendoComboByArrayData_ByOptions: function (element, arr, opts) {

        var checkElement = $(element).data("kendoDropDownList");
        if (checkElement != undefined || checkElement != null) {

            $(element).data("kendoDropDownList").setDataSource(arr);
        }
        else {
            var options = {
                dataTextField: "Text",
                dataValueField: "Value",
                dataSource: arr,
                filter: "contains",
                suggest: true

            };
            options = $.extend({}, options, opts);
            $(element).kendoDropDownList(options);
            return $(element).data("kendoDropDownList");
        }
    },

    SetComboByData: function (action, element, asyncBool, placeholder) {
        var asBoolData = false;
        if (asyncBool == undefined || asyncBool === null) {
            asBoolData = false;
        } else {
            asBoolData = asyncBool;
        }

        var baseUrl = util.GetBaseUrl();
        baseUrl = "http://" + baseUrl + "/" + action;
        $.ajax({
            url: baseUrl,
            type: 'GET',
            async: asBoolData,
            success: function (result) {

                util.SetEasyComboByArrayData(element, result, placeholder);
            }
        });
    },
    SetComboByDataTemplate: function (action, element, asyncBool, placeholder, template) {

        var asBoolData = false;
        if (asyncBool == undefined || asyncBool === null) {
            asBoolData = false;
        } else {
            asBoolData = asyncBool;
        }

        var baseUrl = util.GetBaseUrl();
        baseUrl = "http://" + baseUrl + "/" + action;
        $.ajax({
            url: baseUrl,
            type: 'GET',
            async: asBoolData,
            success: function (result) {

                util.GenerateKendoGridWithTemplate(element, result, placeholder, template);
            }
        });
    },
    GenerateKendoGridWithTemplate: function (element, arr, _placeholder, _template) {

        if (_placeholder == undefined)
            _placeholder = '';
        $(element).kendoDropDownList({
            dataTextField: "Text",
            dataValueField: "Value",
            dataSource: arr,
            filter: "contains",
            suggest: true,
            placeHolder: _placeholder,
            template: _template

        });
        return $(element).data("kendoDropDownList");
    },
    GenerateFullURL: function (baseUrl, action) {
        baseUrl = "http://" + baseUrl + "/" + action;
        return baseUrl;
    },
    FullURLByAction: function (action) {

        if (action.indexOf("http") > -1)
            return action;
        else {
            var baseUrl = util.GetBaseUrl();
            baseUrl = "http://" + baseUrl + "/" + action;
            return baseUrl;
        }
    },


    SetKendoCombo: function (element, arr, opts) {

        var options = {
            dataTextField: "Text",
            dataValueField: "Value",
            dataSource: arr,
            filter: "contains",
            suggest: true

        };
        options = $.extend({}, options, opts);
        $(element).kendoComboBox(options);
        return $(element).data("kendoComboBox");
    },
    SetKendoValue: function (element, val) {
        var elementKendo = $(element).data("kendoDropDownList");
        if (elementKendo != null) {
            elementKendo.value(val);
        }
    },
    SetKendoComboValue: function (element, val) {
        var elementKendo = $(element).data("kendoComboBox");
        if (elementKendo != null) {
            elementKendo.value(val);
        }
    },
    RefreshKendoComboBox: function (form) {
        $(form).find('input').each(function () {
            if ($(this).data("kendoDropDownList")) {
                $(this).data("kendoDropDownList").value('Please Select...');
            }
        });
    },
    RefreshTextBox: function (form) {

        $(form).find('input').each(function () {
            if ($(this).data("text")) {

                $(this).value('');

            }

        });
    },


    buildTreeGrid: function ($el, opts) {

        var options = {
            idField: 'Id',
            height: 350,
            // treeField: 'Units',
            rownumbers: true,
            animate: true,
            fitColumns: true,
            //striped: true,
            loadMsg: "Loading data...",
            //loadFilter: util.pagerFilter,

            displayMsg: "",

            onBeforeLoad: function (row, param) {
                if (!row) { // load top level rows  
                    param.id = 0;   // set id=0, indicate to load new page rows  
                }
            }

        };


        options = $.extend({}, options, opts);

        $el.treegrid(options);
    },
    buildDataGrid: function ($el, opts) {

        var options = {
            idField: 'Id',
            height: 350,



            fitColumns: true,
            striped: true,

            striped: true,
            method: "get"



            //onBeforeLoad: function (row, param) {
            //    if (!row) { // load top level rows  
            //        param.id = 0;   // set id=0, indicate to load new page rows  
            //    }
            //}

        };


        options = $.extend({}, options, opts);

        $el.datagrid(options);
    },
    pagerFilter: function (data) {
        if ($.isArray(data)) {
            data = {
                total: data.length,
                rows: data
            }
        }
        var dg = $(this);
        var state = dg.data('treegrid');
        var opts = dg.treegrid('options');
        var pager = dg.treegrid('getPager');
        pager.pagination({
            displayMsg: "",
            onSelectPage: function (pageNum, pageSize) {
                opts.pageNumber = pageNum;
                opts.pageSize = pageSize;
                pager.pagination('refresh', {
                    pageNumber: pageNum,
                    pageSize: pageSize,
                    displayMsg: ""
                });
                dg.treegrid('loadData', data);
            }
        });
        if (!data.topRows) {
            data.topRows = [];
            data.childRows = [];
            for (var i = 0; i < data.rows.length; i++) {
                var row = data.rows[i];
                row._parentId ? data.childRows.push(row) : data.topRows.push(row);
            }
            data.total = (data.topRows.length);
        }
        var start = (opts.pageNumber - 1) * parseInt(opts.pageSize);
        var end = start + parseInt(opts.pageSize);
        data.rows = $.extend(true, [], data.topRows.slice(start, end).concat(data.childRows));
        return data;
    },
    formatDollar: function (value, row) {

        if (value) {
            return '$' + value;
        } else {
            return '';
        }
    }, OpenModal: function (elementId) {


        $(elementId).window({
            collapsible: false,
            title: 'Portal Configuration',
            width: 450,
            height: 260,
            closed: false,
            cache: false,
            modal: true,
            onLoad: function () {
                // AccountDialog.RefreshPage();
            }


        });
    },
    s4: function () {
        return Math.floor((1 + Math.random()) * 0x10000)
                   .toString(16)
                   .substring(1);
    },

    guid: function () {
        return util.s4() + util.s4();
    },
    ShowErrorMessage: function (title, msg) {


    },
    CloseIFrame: function () {

        var currIframe = util.getFrameElement();
        $(currIframe).closest('.iframe-wrapper').remove();
    },
    GetBaseUrl: function () {
        try {
            var url = location.href;

            var start = url.indexOf('//');
            if (start < 0)
                start = 0
            else
                start = start + 2;

            var end = url.indexOf('/', start);
            if (end < 0) end = url.length - start;

            var baseURL = url.substring(start, end);
            return baseURL;
        }
        catch (arg) {
            return null;
        }
    }



}

GITS.Utils = function () {
    return {      
       
        Menu:function()
        {
            //alert("called from Utils.menu");
            jQuery(".page-sidebar-menu").kendoMenu({
                animation: { open: { effects: "fadeIn" } },
                orientation: 'horizontal'

            });
        },
        
        DbManager: function (factoryName, url) {
            
            Gits.factory(factoryName,['$http', '$cookieStore','$cookies', function ($http, $cookieStore,$cookies) {
                var handler = [];
                handler.get = function (actionName) {
                      
                        return $http.get(url+actionName);
                },
                 handler.getByParameter = function (parameterName,parameterValue, actionName) {
                     debugger;
                     return $http.get(url + actionName + "?" + parameterName + "=" + parameterValue);
                 },
                    handler.getById = function (id, actionName) {
                    
                 
                        return $http.get(url +actionName +"?Id=" + id);
                    },
                    handler.update = function (dataModel, actionName) {
                    debugger;
                        return $http.post(url + actionName + "/" + dataModel.Id, dataModel, {

                            headers: {
                                'content-type': "application/json",
                                'RequestVerificationToken': jQuery("#RequestVerificationKey").val()
                                
                            }
                        });
                    },
                    handler.delete = function (id, actionName) {
                        return $http.post(url + actionName + "?id=" + id, {

                            headers: {
                                'content-type': "application/json",
                                'RequestVerificationToken': jQuery("#RequestVerificationKey").val()

                            }
                        });

                    },
                    handler.create = function (dataModel, actionName) {
                        return $http.post(url + actionName, dataModel, {
                            headers: {
                                'content-type': "application/json",
                                'RequestVerificationToken':jQuery("#RequestVerificationKey").val()
                            }
                        });
                    };
                return handler;
            }]);
        }
    };
}

var BankMenu = function () {
    var Id = 0;
    return {
        
        bankMenuList: function () {
            var menuobject = [
                 {
                    'URL': '/#/Administrator/Bank/Details/0',
                    'Title': 'Details',
                    'Icon': 'glyphicon glyphicon-edit',
                    'Order': 1
                },
                {
                    'URL': '/#/Administrator/Bank/BankDashboard/0',
                    'Title': 'All Customer',
                    'Icon': 'glyphicon glyphicon-edit',
                    'Order': 2
                },
               

            ];

            return menuobject;
          }

    };
    

}

