var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var manager = null, manager2 = null, manager3 = null, tempGrid = null;
var gridName = "0";
var vContratTypeItem = null, vMonitorType = null, vCompanyItems = null, vAutoYearItem = null;
var isFisrt = true;
var strWf = "", wfArr = null, isExport = "", strYsShowStatus = "";
$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});

$(document).ready(function () {

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MethodHander.ashx?action=GetWebConfigValue&strKey=Contract_Export",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                isExport = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    //是否现在验收类委托书启用流程
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MethodHander.ashx?action=GetWebConfigValue&strKey=YSTypeShow",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strYsShowStatus = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    var menu1 = { width: 120, items:
            [
            { id: 'addRoutine', text: '通用类委托书', click: AddData, icon: 'bookadd' },
             { line: true },
            { id: 'addCheck', text: '验收类', click: AddData, icon: 'book_tabs' },
            { line: true },
            { id: 'addSample', text: '送样类委托书', click: AddData, icon: 'book_red' }
            ]
    };

    if (strYsShowStatus == "1") {
        menu1 = { width: 120, items:
            [
            { id: 'addRoutine', text: '通用类委托书', click: AddData, icon: 'bookadd' },
             { line: true },
            { id: 'addCheck', text: '验收类委托书', click: AddData, icon: 'book_tabs'}//,
            //{ line: true },
            //{ id: 'addSample', text: '送样类委托书', click: AddData, icon: 'book_red' }
            ]
        };
    }

    if (strYsShowStatus == "2") {
        menu1 = { width: 120, items:
            [
            { id: 'addRoutine', text: '通用类委托书', click: AddData, icon: 'bookadd' },
            { line: true },
            { id: 'addCheck', text: '验收类委托书', click: AddData, icon: 'book_tabs' },
             { line: true },
            { id: 'addEnvCheck', text: '环评类委托书', click: AddData, icon: 'book_red' }
            ]
        };
    }

    if (isExport == "1") {
        $("#topmenu").ligerMenuBar({ items: [
                 { id: 'add', text: '增加', menu: menu1, icon: 'add' },
                { id: 'modify', text: '修改', click: ModifData, icon: 'modify' },
                { id: 'del', text: '删除', click: DeleteData, icon: 'delete' },
                { id: 'excel', text: '导出委托协议书', click: f_ExportTaskExcle, icon: 'excel' },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
            ]
        });
    } else {
        $("#topmenu").ligerMenuBar({ items: [
                 { id: 'add', text: '增加', menu: menu1, icon: 'add' },
                { id: 'modify', text: '修改', click: ModifData, icon: 'modify' },
                { id: 'del', text: '删除', click: DeleteData, icon: 'delete' },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
            ]
        });
    }

    strWf = $.getUrlVar('WF_ID') || $.getUrlVar('wf_id');
    if (strWf != "") {
        wfArr = strWf.split('|');
        if (wfArr.length < 3) {
            for (var i = 0; i <= 3 - wfArr.length; i++) {
                wfArr.push("");
            }
        }
    }
    $("#navtab1").ligerTab({ contextmenu: false, onBeforeSelectTabItem: function (tabid) {
    },
        //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
        onAfterSelectTabItem: function (tabid) {
            navtab = $("#navtab1").ligerGetTabManager();
            navtab.reload(navtab.getSelectedTabItemID());
            if (tabid == "home") {
                gridName = "0";
                GetStartDataList();
            }
            if (tabid == 'tabitem1') {
                gridName = "1";
                GetData();
            }
            if (tabid == 'tabitem2') {
                gridName = "2";
                GetFinishList();
            }
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MethodHander.ashx?action=GetDict&type=Contract_Type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vContratTypeItem = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });


    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MethodHander.ashx?action=GetMonitorType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vMonitorType = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MethodHander.ashx?action=GetContratYear",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAutoYearItem = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    function GetVCompanyItem(strContractId) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MethodHander.ashx?action=GetContractCompany&strContratId=" + strContractId + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    vCompanyItems = data;
                    // return vCompanyItems;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    //    GetData();
    //    GetFinishList();
    GetStartDataList();
    function GetStartDataList() {
        window['g1'] =
    manager = $("#maingrid1").ligerGrid({
        columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 300, minWidth: 180 },
                { display: '合同号', name: 'CONTRACT_CODE', align: 'left', width: 160, minWidth: 100 },
                { display: '合同年度', name: 'CONTRACT_YEAR', width: 60, minWidth: 40 },
                { display: '委托类型', name: 'CONTRACT_TYPE', wdth: 100, minWidth: 80, data: vContratTypeItem, render: function (item) {
                    for (var i = 0; i < vContratTypeItem.length; i++) {
                        if (vContratTypeItem[i]['DICT_CODE'] == item.CONTRACT_TYPE)
                            return vContratTypeItem[i]['DICT_TEXT'];
                    }
                    return item.CONTRACT_TYPE;
                }
                },
                { display: '监测类型', name: 'TEST_TYPES', align: 'left', width: 140, minWidth: 100, data: vMonitorType, render: function (item) {
                    var typeName = "";
                    for (var i = 0; i < vMonitorType.length; i++) {
                        var strTestTypes = item.TEST_TYPES.split(';');
                        for (var n = 0; n < strTestTypes.length; n++) {
                            if (vMonitorType[i]['ID'] == strTestTypes[n]) {
                                typeName += vMonitorType[i]['MONITOR_TYPE_NAME'] + ";";
                            }
                        }
                    }
                    if (typeName != "") { return typeName = typeName.substring(0, typeName.length - 1); }
                    else
                    { return item.TEST_TYPES; }
                }
                },
                { display: '委托单位', name: 'CLIENT_COMPANY_ID', align: 'left', width: 120, minWidth: 180, render: function (item) {
                    GetVCompanyItem(item.ID);
                    if (vCompanyItems != null) {
                        for (var i = 0; i < vCompanyItems.length; i++) {
                            if (vCompanyItems[i].ID == item.CLIENT_COMPANY_ID)
                                return vCompanyItems[i].COMPANY_NAME;
                        }
                    }
                    return item.CLIENT_COMPANY_ID;
                }
                },
                { display: '受检单位', name: 'TESTED_COMPANY_ID', align: 'left', width: 120, minWidth: 180, render: function (item) {
                    GetVCompanyItem(item.ID);
                    if (vCompanyItems != null) {
                        for (var i = 0; i < vCompanyItems.length; i++) {
                            if (vCompanyItems[i].ID == item.TESTED_COMPANY_ID)
                                return vCompanyItems[i].COMPANY_NAME;
                        }
                    }
                    return item.TESTED_COMPANY_ID;
                }
                },
            { display: '是否快捷录入', name: 'ISQUICKLY', width: 100, minWidth: 80, render: function (item) {
                if (item.ISQUICKLY == '1') {
                    return "<a style='color:Red'>是</a>";
                } else {
                    return "否";
                }
                return item.ISQUICKLY;
            }
            },
            { display: '流程状态', name: 'CONTRACT_SATAUS', width: 100, minWidth: 80, render: function (item) {
                if (item.CONTRACT_STATUS == '0') {
                    return "<a style='color:Red'>未提交</a>";
                }
                return item.CONTRACT_STATUS;
            }
            }
                ],
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20, 25, 30],
        url: 'MethodHander.ashx?action=GetContractListData&strStatus=0',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 25,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        onDblClickRow: function (data, rowindex, rowobj) {
            ModifData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );

        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    }


    function GetData() {
        var objToobar1 = null;
        if (isExport == "1") {
            objToobar1 = { items: [
                { id: 'view', text: '查看', click: ViewData, icon: 'archives' },
                { line: true },
                { id: 'excel', text: '导出委托协议书', click: f_ExportTaskExcle, icon: 'excel' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
                ]
            }
        } else {
            objToobar1 = { items: [
                { id: 'view', text: '查看', click: ViewData, icon: 'archives' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
                ]
            }
        }
        window['g2'] =
    manager2 = $("#maingrid2").ligerGrid({
        columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 300, minWidth: 120 },
                { display: '合同号', name: 'CONTRACT_CODE', align: 'left', width: 160, minWidth: 100 },
                { display: '合同年度', name: 'CONTRACT_YEAR', width: 60, minWidth: 40 },
                { display: '委托类型', name: 'CONTRACT_TYPE', wdth: 100, minWidth: 80, data: vContratTypeItem, render: function (item) {
                    for (var i = 0; i < vContratTypeItem.length; i++) {
                        if (vContratTypeItem[i]['DICT_CODE'] == item.CONTRACT_TYPE)
                            return vContratTypeItem[i]['DICT_TEXT'];
                    }
                    return item.CONTRACT_TYPE;
                }
                },
                { display: '监测类型', name: 'TEST_TYPES', align: 'left', width: 140, minWidth: 100, data: vMonitorType, render: function (item) {
                    var typeName = "";
                    for (var i = 0; i < vMonitorType.length; i++) {
                        var strTestTypes = item.TEST_TYPES.split(';');
                        for (var n = 0; n < strTestTypes.length; n++) {
                            if (vMonitorType[i]['ID'] == strTestTypes[n]) {
                                typeName += vMonitorType[i]['MONITOR_TYPE_NAME'] + ";";
                            }
                        }
                    }
                    if (typeName != "") { return typeName = typeName.substring(0, typeName.length - 1); }
                    else
                    { return item.TEST_TYPES; }
                }
                },
                { display: '委托单位', name: 'CLIENT_COMPANY_ID', align: 'left', width: 120, minWidth: 180, render: function (item) {
                    GetVCompanyItem(item.ID);
                    for (var i = 0; i < vCompanyItems.length; i++) {
                        if (vCompanyItems[i]['ID'] == item.CLIENT_COMPANY_ID)
                            return vCompanyItems[i]['COMPANY_NAME'];
                    }
                    return item.CLIENT_COMPANY_ID;
                }
                },
                { display: '受检单位', name: 'TESTED_COMPANY_ID', align: 'left', width: 120, minWidth: 180, render: function (item) {
                    GetVCompanyItem(item.ID);
                    for (var i = 0; i < vCompanyItems.length; i++) {
                        if (vCompanyItems[i]['ID'] == item.TESTED_COMPANY_ID)
                            return vCompanyItems[i]['COMPANY_NAME'];
                    }
                    return item.TESTED_COMPANY_ID;
                }
                },
            { display: '是否快捷录入', name: 'ISQUICKLY', width: 100, minWidth: 80, render: function (item) {
                if (item.ISQUICKLY == '1') {
                    return "<a style='color:Red'>是</a>";
                } else {
                    return "否";
                }
                return item.ISQUICKLY;
            }
            },
            { display: '流程状态', name: 'CONTRACT_SATAUS', width: 100, minWidth: 80, render: function (item) {
                if (item.CONTRACT_STATUS == '1') {
                    return "<a style='color:Red'>流转中</a>";
                }
                return item.CONTRACT_STATUS;
            }
            }
                ],
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20, 25, 30],
        url: "MethodHander.ashx?action=GetContractListData&strStatus=1,2,3,4,5,6,7,8",
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 25,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: objToobar1,
        onDblClickRow: function (data, rowindex, rowobj) {
            ViewData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );

        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    }
    function GetFinishList() {
        var objToobar2 = null;
        if (isExport == "1") {
            objToobar2 = { items: [
                { id: 'view', text: '查看', click: ViewData, icon: 'archives' },
                { line: true },
                 { id: 'excel', text: '导出委托协议书', click: f_ExportTaskExcle, icon: 'excel' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
                ]
            }
        } else {
            objToobar2 = { items: [
                { id: 'view', text: '查看', click: ViewData, icon: 'archives' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
                ]
            }
        }
        window['g3'] =
     manager3 = $("#maingrid3").ligerGrid({
         columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 300, minWidth: 120 },
                { display: '合同号', name: 'CONTRACT_CODE', align: 'left', width: 160, minWidth: 100 },
                { display: '合同年度', name: 'CONTRACT_YEAR', width: 60, minWidth: 40 },
                { display: '委托类型', name: 'CONTRACT_TYPE', wdth: 100, minWidth: 80, data: vContratTypeItem, render: function (item) {
                    for (var i = 0; i < vContratTypeItem.length; i++) {
                        if (vContratTypeItem[i]['DICT_CODE'] == item.CONTRACT_TYPE)
                            return vContratTypeItem[i]['DICT_TEXT'];
                    }
                    return item.CONTRACT_TYPE;
                }
                },
                { display: '监测类型', name: 'TEST_TYPES', align: 'left', width: 140, minWidth: 100, data: vMonitorType, render: function (item) {
                    var typeName = "";
                    for (var i = 0; i < vMonitorType.length; i++) {
                        var strTestTypes = item.TEST_TYPES.split(';');
                        for (var n = 0; n < strTestTypes.length; n++) {
                            if (vMonitorType[i]['ID'] == strTestTypes[n]) {
                                typeName += vMonitorType[i]['MONITOR_TYPE_NAME'] + ";";
                            }
                        }
                    }
                    if (typeName != "") { return typeName = typeName.substring(0, typeName.length - 1); }
                    else
                    { return item.TEST_TYPES; }
                }
                },
                { display: '委托单位', name: 'CLIENT_COMPANY_ID', align: 'left', width: 120, minWidth: 180, render: function (item) {
                    GetVCompanyItem(item.ID);
                    for (var i = 0; i < vCompanyItems.length; i++) {
                        if (vCompanyItems[i]['ID'] == item.CLIENT_COMPANY_ID)
                            return vCompanyItems[i]['COMPANY_NAME'];
                    }
                    return item.CLIENT_COMPANY_ID;
                }
                },
                { display: '受检单位', name: 'TESTED_COMPANY_ID', align: 'left', width: 120, minWidth: 180, render: function (item) {
                    GetVCompanyItem(item.ID);
                    for (var i = 0; i < vCompanyItems.length; i++) {
                        if (vCompanyItems[i]['ID'] == item.TESTED_COMPANY_ID)
                            return vCompanyItems[i]['COMPANY_NAME'];
                    }
                    return item.TESTED_COMPANY_ID;
                }
                },
            { display: '是否快捷录入', name: 'ISQUICKLY', width: 100, minWidth: 80, render: function (item) {
                if (item.ISQUICKLY == '1') {
                    return "<a style='color:Red'>是</a>";
                } else {
                    return "否";
                }
                return item.ISQUICKLY;
            }
            },
                  { display: '流程状态', name: 'CONTRACT_SATAUS', width: 100, minWidth: 80, render: function (item) {
                      if (item.CONTRACT_STATUS == '9') {
                          return "<a style='color:Red'>已办结</a>";
                      }
                      return item.CONTRACT_STATUS;
                  }
                  }
                ],
         width: '100%',
         height: '100%',
         pageSizeOptions: [5, 10, 15, 20, 25, 30],
         url: 'MethodHander.ashx?action=GetContractListData&strStatus=9',
         dataAction: 'server', //服务器排序
         usePager: true,       //服务器分页
         pageSize: 25,
         alternatingRow: false,
         checkbox: true,
         whenRClickToSelect: true,
         toolbar: objToobar2,
         onDblClickRow: function (data, rowindex, rowobj) {
             ViewData();
         },
         onCheckRow: function (checked, rowdata, rowindex) {
             for (var rowid in this.records)
                 this.unselect(rowid);
             this.select(rowindex);
         },
         onBeforeCheckAllRow: function (checked, grid, element) { return false; }
     }
    );

        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    }
    function AddData(item) {
        switch (item.id) {
            case 'addRoutine':
                //                var tabid = "tabidContractRoutine";
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true&WF_ID=' + wfArr[0] + '';
                top.f_overTab('新增通用类委托书', surl);
                //                top.f_addTab(tabid, '新增常规类委托书', surl);
                break;
            case 'addCheck':
                //                var tabid = "tabidContractCheck";
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true&WF_ID=' + wfArr[1] + '';
                if (strYsShowStatus == "1") {
                    surl = '../Sys/WF/WFStartPage.aspx?action=|type=true|idShow=1&WF_ID=' + wfArr[1] + '';
                }
                top.f_overTab('新增验收类委托书', surl);
                //top.f_addTab(tabid, '新增验收类委托书', surl);
                break;
            case 'addSample':
                var tabid = "tabidContractSample";
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true&WF_ID=' + wfArr[2] + '';
                top.f_overTab('新增送样类委托书', surl);
                //                top.f_addTab(tabid, '新增自送样类委托书', surl);
                break;
            case 'addEnvCheck':
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true&WF_ID=' + wfArr[3] + '';
                if (strYsShowStatus == "1") {
                    surl = '../Sys/WF/WFStartPage.aspx?action=|type=true|idShow=1&WF_ID=' + wfArr[3] + '';
                }
                top.f_overTab('新增环评类委托书', surl);
                break;
            default:
                break;
        }
    }
    function ModifData() {
        var rowSelected = manager.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！'); return;
        }
        //        var strQuestrow = jQuery.param(rowSelected);
        var strQuestrow = 'strContratId=' + rowSelected.ID;
        //        strQuestrow = strQuestrow.replace(/&/g, "|"); //将所有的&符号转化成|，将在后台进行解析
        if (rowSelected.BOOKTYPE == '0') {
            //            var tabid = "tabidContractModify0"
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strYsShowStatus=' + strYsShowStatus + '|' + strQuestrow + '&WF_ID=' + wfArr[0] + '';
            top.f_overTab('通用类委托书修改', surl);
            //            top.f_addTab(tabid, '常规类委托书修改', surl);
        }
        if (rowSelected.BOOKTYPE == '1') {

            //            var tabid = "tabidContractModify1"
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strYsShowStatus=' + strYsShowStatus + '|' + strQuestrow + '&WF_ID=' + wfArr[1] + '';
            top.f_overTab('验收类委托书修改', surl);
            //top.f_addTab(tabid, '验收类委托书修改', surl);
        }
        if (rowSelected.BOOKTYPE == '2') {
            //            var tabid = "tabidContractModify2"
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strYsShowStatus=' + strYsShowStatus + '|' + strQuestrow + '&WF_ID=' + wfArr[2] + '';
            top.f_overTab('送样类委托书修改', surl);
            //            top.f_addTab(tabid, '自送样类委托书修改', surl);
        }
    }

    function ViewData() {
        var rowSelected = null;
        if (gridName == "0") {
            rowSelected = manager.getSelectedRow();
            tempGrid = manager;
        }
        if (gridName == "1") {
            rowSelected = manager2.getSelectedRow();
            tempGrid = manager2;
        }
        if (gridName == "2") {
            rowSelected = manager3.getSelectedRow();
            tempGrid = manager3;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！'); return;
        }
        var strQuestrow = 'strContratId=' + rowSelected.ID;
        strQuestrow = strQuestrow.replace(/&/g, "|"); //将所有的&符号转化成|，将在后台进行解析
        if (rowSelected.BOOKTYPE == '0') {
            var tabid = "tabidContractView0"
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strYsShowStatus=' + strYsShowStatus + '|' + strQuestrow + '&WF_ID=' + wfArr[0] + '';
            top.f_addTab(tabid, '通用类委托书查看', surl);
        }
        if (rowSelected.BOOKTYPE == '1') {
            var tabid = "tabidContractView1"
            //var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strYsShowStatus=' + strYsShowStatus + '|' + strQuestrow + '&WF_ID=' + wfArr[1] + '';
            var surl = '../Channels/Mis/Contract/AcceptanceEx/AcceptanceAudit.aspx?WF_ID=' + wfArr[1] + '&' + strQuestrow + '&type=false&view=true';
            top.f_addTab(tabid, '验收类委托书查看', surl);
        }
        if (rowSelected.BOOKTYPE == '2') {
            var tabid = "tabidContractView2"
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strYsShowStatus=' + strYsShowStatus + '|' + strQuestrow + '&WF_ID=' + wfArr[2] + '';
            top.f_addTab(tabid, '送样类委托书查看', surl);
        }
    }

    function DeleteData() {
        var rowSelected = manager.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！'); return;
        }
        $.ligerDialog.confirm('确定要删除该条记录？\r\n', function (result) {
            if (result == true) {
                $.ajax({
                    cache: false,
                    async: false, //设置是否为异步加载,此处必须
                    type: "POST",
                    url: "MethodHander.ashx?action=DeleteContractInfor&strContratId=" + rowSelected.ID + "",
                    contentType: "application/text; charset=utf-8",
                    dataType: "text",
                    success: function (data) {
                        if (data != "") {
                            manager.loadData();
                        }
                        else {
                            $.ligerDialog.warn('删除失败！');
                        }
                    },
                    error: function (msg) {
                        $.ligerDialog.warn('Ajax访问失败！' + msg);
                    }
                });
            }
            else {
                return;
            }
        })

    }


    function f_ExportTaskExcle() {
        var rowSelected = null;
        if (gridName == "0") {
            rowSelected = manager.getSelectedRow();
            tempGrid = manager;
        }
        if (gridName == "1") {
            rowSelected = manager2.getSelectedRow();
            tempGrid = manager2;
        }
        if (gridName == "2") {
            rowSelected = manager3.getSelectedRow();
            tempGrid = manager3;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！');
        } else {
            var strTask_id = rowSelected.ID;
            $("#cphData_hidTaskId").val(strTask_id);
            if (strYsShowStatus == "0") {
                $("#cphData_btnExport_QY").click();   //清远导出委托书
            }
            else {
                $("#cphData_btnExport").click();
            }
        }
    }

    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构

            var mainform = $("#SrhForm");
            mainform.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                     { display: "项目名称", name: "SEA_PROJECT_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "合同编号", name: "SEA_CONTRACT_CODE", newline: false, type: "text" },
                     { display: "合同年度", name: "SEA_CONTRACT_YEAR", newline: true, type: "select", comboboxName: "SEA_CONTRACT_YEAR_BOX", options: { valueFieldID: "SEA_CONTRACT_YEAR_OP", valueField: "ID", textField: "YEAR", data: vAutoYearItem} },
                     { display: "委托类型", name: "SEA_CONTRACT_TYPE", newline: false, type: "select", comboboxName: "SEA_CONTRACT_TYPE_BOX", options: { valueFieldID: "SEA_CONTRACT_TYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vContratTypeItem} },
                     { display: "委托单位", name: "SEA_CLIENT_COMPANYNAME", newline: true, type: "text" },
                     { display: "受检单位", name: "SEA_TEST_COMPANYNAME", newline: false, type: "text" }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 240, top: 90, title: "委托书查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_PROJECT_NAME = encodeURI($("#SEA_PROJECT_NAME").val());
            var SEA_CONTRACT_CODE = encodeURI($("#SEA_CONTRACT_CODE").val());
            var SEA_CONTRACT_YEAR_OP = encodeURI($("#SEA_CONTRACT_YEAR_BOX").val());
            var SEA_CONTRACT_TYPE_OP = $("#SEA_CONTRACT_TYPE_OP").val();
            var SEA_CLIENT_COMPANYNAME = encodeURI($("#SEA_CLIENT_COMPANYNAME").val());
            var SEA_TEST_COMPANYNAME = encodeURI($("#SEA_TEST_COMPANYNAME").val());

            if (gridName == "0") {
                strstatus = "&strStatus=0";
                tempGrid = manager;
            }
            if (gridName == "1") {
                strstatus = "&strStatus=1";
                tempGrid = manager2;
            }
            if (gridName == "2") {
                strstatus = "&strStatus=9";
                tempGrid = manager3;
            }
            var url = "MethodHander.ashx?action=GetContractListData&strProjectName=" + SEA_PROJECT_NAME + "&strContractCode=" + SEA_CONTRACT_CODE + "&strContratYear=" + SEA_CONTRACT_YEAR_OP + "&strContratType=" + SEA_CONTRACT_TYPE_OP + "&strCompanyName=" + SEA_CLIENT_COMPANYNAME + "&strCompanyNameFrim=" + SEA_TEST_COMPANYNAME + strstatus;
            tempGrid.set('url', url)
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_PROJECT_NAME").val("");
        $("#SEA_CONTRACT_CODE").val("");
        $("#SEA_TEST_COMPANYNAME").val("");
        $("#SEA_CLIENT_COMPANYNAME").val("");
        $("#SEA_CONTRACT_YEAR_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_CONTRACT_TYPE_BOX").ligerGetComboBoxManager().setValue("");
    }

});
