// Create by 苏成斌 2012.12.11  "采样任务列表"功能

var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var strUrl = "SamplingList.aspx";
var tab = null, selecttabindex = null,strExportMonitorName = "所有";
var manager; //未办理任务
var backmanager; //回退任务

$(document).ready(function () {
    $("#layout1").ligerLayout({ leftWidth: 170, height: '100%' });
    //tab
    $("#navtab1").ligerTab({ onAfterSelectTabItem: function (tabid) {

        tab = $("#navtab1").ligerGetTabManager();
        selecttabindex = tab.getSelectedTabItemID();
        changeTab(selecttabindex);
    }
    });
    //Tab标签切换事件
    function changeTab(tabid) {
        switch (tabid) {
            // 未办理任务                                                                                                                                                                                                                                                                                                                                                                                         
            case "tabitem1":
                manager.set('url', strUrl + "?type=getSampleTask");
                break;
            // 退回任务                                                                                                                                                                                                                                                                                                                                                                                         
            case "tabitem2":
                backmanager.set('url', strUrl + "?type=getBackSampleTask");
                break;
            default:
                manager.set('url', strUrl + "?type=getSampleTask");
                break;
        }
    }

    /*================================未办理任务 Start========================================*/
    //质控设置菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menudone', text: '办理', click: click_OfMenu, icon: 'modify' }
            ]
    });

    //grid
    manager = $("#maingridItem").ligerGrid({
        columns: [
//        { display: '企业', name: 'TESTED_COMPANY_ID', width: 180, align: 'left', render: function (record) {
//            return getCompanyName1(record.TESTED_COMPANY_ID);
//        }
        //        },
//        { display: '合同号', name: 'CONTRACT_CODE', width: 150, align: 'left' },
//        { display: '业务类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', render: function (record) {
//            return getDictName(record.CONTRACT_TYPE, "Contract_Type");
//        }
        //        },
        //        { display: '监测类型', name: 'REMARK2', width: 90, align: 'left', render: function (record) {
        //            return getMonitorTypeName(record.REMARK2);
        //        }
        //        },
        // Modify By 胡方扬 2013-06-03 原因：修改为树形模式显示
        {display: '任务号', name: 'TICKET_NUM', width: 150, align: 'center' },
        { display: '项目名称', name: 'PROJECT_NAME', width: 350, align: 'left' },
        {display: '监测类型', name: 'MONITOR_NAME', width: 160, align: 'left'},

        {display: '采样时间', name: 'REMARK1', width: 90, align: 'left' },
        { display: '要求完成时间', name: 'ASKING_DATE', width: 90, align: 'left' }, //黄进军修改20140901 ASKING_DATE SAMPLE_FINISH_DATE
        { display: '采样人', name: 'SAMPLING_MANAGER_ID', width: 70, align: 'center', render: function (record) {
            return getUserName(record.SAMPLING_MANAGER_ID);
        }
        }
        ], width: '100%', height: '100%', pageSizeOptions: [10, 15, 20, 50],
        url: strUrl + '?type=getSampleTask',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        //checkbox: true,
        tree: { columnName: 'TICKET_NUM' },
        onAfterShowData: f_hasChildren,
        sortName: "ID",
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'done', text: '办理', click: click_OfToolbar, icon: 'modify' },
                { line: true },
                { id: 'excel', text: '导出监测任务单', click: click_OfToolbar, icon: 'excel' },
                { line: true },
                { id: 'srh', text: '查询', click: click_OfToolbar, icon: 'search' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            click_OfDblclick(data);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
   // $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});


//数据加载完毕后对有子节点的数据进行初始化折叠  胡方扬
function f_hasChildren(data) {
    if (data.Rows.length > 0) {
        for (var i = 0; i < data.Rows.length; i++) {
            //判断是否有子节点
            if (manager.hasChildren(data.Rows[i])) {
                manager.collapse(data.Rows[i]);
            }
        }
    }
}
//Dblclick
function click_OfDblclick(data) {
    var selected = manager.getSelectedRow();
    if (selected.children) {
        $.ligerDialog.warn('父节点无法办理！'); return;
    }
    var tabid = "tabidSamplingNew"
    var surl = "../Channels/Mis/Monitor/sampling/QY/Sampling.aspx?strSubtaskID=" + data.ID;
    top.f_addTab(tabid, '采样', surl);
}
//toolbar click
function click_OfToolbar(item) {
    switch (item.id) {
        case 'done':
            var selected = manager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择一条记录！'); return }
            else {

                if (selected.children) {
                    $.ligerDialog.warn('父节点无法办理！'); return;
                }
                var tabid = "tabidSamplingNew";
                var surl = "";
                var isBack = selected.TASK_TYPE == '退回' ? '1' : '0';

                if (selected.REMARK2 == "000000004" || selected.REMARK2 == "FunctionNoise" || selected.REMARK2 == "AreaNoise" || selected.REMARK2 == "EnvRoadNoise")
                    surl = "../Channels/Mis/Monitor/sampling/QY/Sampling_Noise.aspx?Link=Sample&strSubtaskID=" + selected.ID + "&strMonitor_ID=" + selected.REMARK2 + "&IS_BACK=" + isBack;
                else
                    surl = "../Channels/Mis/Monitor/sampling/QY/Sampling.aspx?strSubtaskID=" + (selected.SOURCE_ID.length > 0 ? selected.SOURCE_ID : selected.ID) + "&SOURCE_ID=" + selected.ID + "&strMonitor_ID=" + selected.REMARK2 + "&IS_BACK=" + isBack;
                top.f_addTab(tabid, '采样', surl);
            }

            break;
        case 'excel':
            var selected = manager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择一条记录！'); return }
            else {
                $("#hidASK_DATE").val(selected.REMARK1);
                $("#hidFINISH_DATE").val(selected.ASKING_DATE);
                $("#hidTaskId").val(selected.CONTRACT_ID);
                $("#hidWorkTaskId").val(selected.TASK_ID);
                $("#hidPlanId").val(selected.PLAN_ID);
                if (selected.children) {
                    $("#hidMonitorId").val("");
                    strExportMonitorName = "所有";
                } else {
                    $("#hidMonitorId").val(selected.REMARK2);
                    strExportMonitorName = selected.MONITOR_NAME;
                }
                Export();
            }
            break;
        case 'srh':
            showDetailSrh();
            break;
        default:
            break;
    }
}

//右键菜单
function click_OfMenu(item) {
    switch (item.id) {
        case 'menudone':
            var tabid = "tabidSamplingNew"
            var surl = "../Channels/Mis/Monitor/sampling/QY/Sampling.aspx?strSubtaskID=" + actionID;
            top.f_addTab(tabid, '采样', surl);
            break;
        default:
            break;
    }
}

//弹出查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#SrhForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "企业", name: "TESTED_COMPANY_ID", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                      { display: "合同编号", name: "CONTRACT_CODE", newline: true, type: "text", validate: { required: true, minlength: 3} },
                      { display: "监测类型", name: "MONITOR_ID", newline: true, type: "select", comboboxName: "SrhMONITOR_TYPE_ID", options: { valueFieldID: "MONITOR_ID", url: "../../../../Base/MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 350, height: 200, top: 90, title: "查询监测项目",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhMONITOR_ID = $("#MONITOR_ID").val();
        var SrhCONTRACT_CODE = escape($("#CONTRACT_CODE").val());
        var SrhTESTED_COMPANY_ID = escape($("#TESTED_COMPANY_ID").val());

        manager.set('url', strUrl + "?type=getSampleTask&TestedCompanyID=" + SrhTESTED_COMPANY_ID + "&ContractCode=" + SrhCONTRACT_CODE + "&MonitorID=" + SrhMONITOR_ID);
    }
}

//质控设置grid 的弹出查询对话框 清空
function clearSearchDialogValue() {
    $("#TESTED_COMPANY_ID").val("");
    $("#CONTRACT_CODE").val("");
    $("#MONITOR_ID").val("");
    $("#SrhMONITOR_TYPE_ID").val("");
}
/*================================未办理任务 End========================================*/

/*==============================回退任务列表 Start========================================*/
$(document).ready(function () {
    //回退任务列表
    backmanager = $("#backgridItem").ligerGrid({
        columns: [
//        { display: '企业', name: 'TESTED_COMPANY_ID', width: 180, align: 'left', render: function (record) {
//            return getCompanyName1(record.TESTED_COMPANY_ID);
//        }
        //        },
//        { display: '合同号', name: 'CONTRACT_CODE', width: 150, align: 'left' },
//        { display: '业务类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', render: function (record) {
//            return getDictName(record.CONTRACT_TYPE, "Contract_Type");
//        }
//        },
//        { display: '监测类型', name: 'REMARK2', width: 90, align: 'left', render: function (record) {
//            return getMonitorTypeName(record.REMARK2);
//        }
        //        },
        // Modify By 胡方扬 2013-06-03 原因：修改为树形模式显示
        {display: '任务号', name: 'TICKET_NUM', width: 150, align: 'center' },
        { display: '项目名称', name: 'PROJECT_NAME', width: 350, align: 'left' },
        {display: '监测类型', name: 'MONITOR_NAME', width: 160, align: 'left' },
        { display: '采样时间', name: 'REMARK1', width: 90, align: 'left' },
        { display: '要求完成时间', name: 'ASKING_DATE', width: 90, align: 'left' },
        { display: '采样人', name: 'SAMPLING_MANAGER_ID', width: 70, align: 'center', render: function (record) {
            return getUserName(record.SAMPLING_MANAGER_ID);
        }
        }
        ], width: '100%', height: '100%', pageSizeOptions: [10, 15, 20, 50],
        url: strUrl + '?type=getBackSampleTask',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        //checkbox: true,
        sortName: "ID",
        whenRClickToSelect: true,
        tree: { columnName: 'TICKET_NUM' },
        onAfterShowData: f_BackhasChildren,
        toolbar: { items: [
                { id: 'done', text: '办理', click: click_OfToolbarBack, icon: 'modify' },
                { line: true },
                { id: 'excel', text: '导出监测任务单', click: click_OfToolbarBack, icon: 'excel' },
                { line: true },
                { id: 'srh', text: '查询', click: click_OfToolbarBack, icon: 'search' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            click_OfDblclickBack(data);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    //$(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

function f_BackhasChildren(data) {
    if (data.Rows.length > 0) {
        for (var i = 0; i < data.Rows.length; i++) {
            //判断是否有子节点
            if (backmanager.hasChildren(data.Rows[i])) {
                backmanager.collapse(data.Rows[i]);
            }
        }
    }
}
//Dblclick
function click_OfDblclickBack(data) {
    var selected = backmanager.getSelectedRow();
    if (selected.children) {
        $.ligerDialog.warn('父节点无法办理！'); return;
    }
    var tabid = "tabidSamplingNew"
    var surl = "../Channels/Mis/Monitor/sampling/QY/SamplingView.aspx?strSubtaskID=" + data.ID + "&IS_SEND=2";
    top.f_addTab(tabid, '采样', surl);
}
//toolbar click
function click_OfToolbarBack(item) {
    switch (item.id) {
        case 'done':
            var selected = backmanager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择一条记录！'); ; return }
            else {


                if (selected.children) {
                    $.ligerDialog.warn('父节点无法办理！'); return;
                }
                var tabid = "tabidSamplingNew";
                var surl = "";
                //var surl = "../Channels/Mis/Monitor/sampling/QY/SamplingView.aspx?strSubtaskID=" + selected.ID + "&IS_SEND=2";
                if (selected.REMARK2 == "000000004")
                    surl = "../Channels/Mis/Monitor/sampling/QY/Sampling_Noise.aspx?Link=Sample&strSubtaskID=" + selected.ID + "&strMonitor_ID=" + selected.REMARK2 + "&IS_BACK=1";
                else
                    surl = "../Channels/Mis/Monitor/sampling/QY/Sampling.aspx?strSubtaskID=" + selected.ID + "&strMonitor_ID=" + selected.REMARK2 + "&IS_BACK=1";
                top.f_addTab(tabid, '采样', surl);
            }



            break;
        case 'excel':
            var selected = backmanager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择一条记录！'); return }
            else {
                $("#hidASK_DATE").val(selected.REMARK1);
                $("#hidFINISH_DATE").val(selected.ASKING_DATE);
                $("#hidTaskId").val(selected.CONTRACT_ID);
                $("#hidWorkTaskId").val(selected.TASK_ID);
                $("#hidPlanId").val(selected.PLAN_ID);
                if (selected.children) {
                    $("#hidMonitorId").val("");
                    strExportMonitorName = "所有";
                } else {
                    $("#hidMonitorId").val(selected.REMARK2);
                    strExportMonitorName = selected.MONITOR_NAME;
                }
                Export();
            }
            break;
        case 'srh':
            showDetailSrhBack();
            break;
        default:
            break;
    }
}

//右键菜单
function click_OfMenuBack(item) {
    switch (item.id) {
        case 'menudone':

            var tabid = "tabidSamplingNew"
            var surl = "../Channels/Mis/Monitor/sampling/QY/SamplingView.aspx?strSubtaskID=" + actionID + "&IS_SEND=2";
            top.f_addTab(tabid, '采样', surl);

            break;
        default:
            break;
    }
}

//弹出查询对话框
var detailWinSrhBack = null;
function showDetailSrhBack() {
    if (detailWinSrhBack) {
        detailWinSrhBack.show();
    }
    else {
        //创建表单结构
        var mainformback = $("#SrhFormBack");
        mainformback.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "企业", name: "BACK_TESTED_COMPANY_ID", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                      { display: "合同编号", name: "BACK_CONTRACT_CODE", newline: true, type: "text", validate: { required: true, minlength: 3} },
                      { display: "监测类型", name: "BACK_MONITOR_ID", newline: true, type: "select", comboboxName: "BACK_SrhMONITOR_TYPE_ID", options: { valueFieldID: "MONITOR_ID", url: "../../../../Base/MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} }
                    ]
        });

        detailWinSrhBack = $.ligerDialog.open({
            target: $("#detailSrhBack"),
            width: 350, height: 200, top: 90, title: "查询监测项目",
            buttons: [
                  { text: '确定', onclick: function () { searchBack(); clearSearchDialogValueBack(); detailWinSrhBack.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValueBack(); detailWinSrhBack.hide(); } }
                  ]
        });
    }

    function searchBack() {
        var SrhMONITOR_ID = $("#MONITOR_ID").val();
        var SrhCONTRACT_CODE = escape($("#BACK_CONTRACT_CODE").val());
        var SrhTESTED_COMPANY_ID = escape($("#BACK_TESTED_COMPANY_ID").val());

        backmanager.set('url', strUrl + "?type=getBackSampleTask&TestedCompanyID=" + SrhTESTED_COMPANY_ID + "&ContractCode=" + SrhCONTRACT_CODE + "&MonitorID=" + SrhMONITOR_ID);
    }
}

//质控设置grid 的弹出查询对话框 清空
function clearSearchDialogValueBack() {
    $("#BACK_TESTED_COMPANY_ID").val("");
    $("#BACK_CONTRACT_CODE").val("");
    $("#BACK_MONITOR_ID").val("");
    $("#BACK_SrhMONITOR_TYPE_ID").val("");
}
/*==============================回退任务列表 End========================================*/

/*==============================基础资料获取===========================================*/
//获取监测点位信息
function getUserName(strUserID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getUserName",
        data: "{'strUserID':'" + strUserID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取企业名称信息
function getCompanyName1(strCompanyId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getCompanyName",
        data: "{'strCompanyId':'" + strCompanyId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取字典项信息
function getDictName(strDictCode, strDictType) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDictName",
        data: "{'strDictCode':'" + strDictCode + "','strDictType':'" + strDictType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取监测类别信息
function getMonitorTypeName(strMonitorTypeId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getMonitorTypeName",
        data: "{'strMonitorTypeId':'" + strMonitorTypeId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//导出数据
function Export() {
    $.ligerDialog.confirm('您确定要导出【<a style="color:Red; font-weight:bolder">' + strExportMonitorName + '</a>】监测类的工作任务通知单吗？', function (yes) {
        if (yes == true) {
            $("#btnExport").click();
        }
    });
}