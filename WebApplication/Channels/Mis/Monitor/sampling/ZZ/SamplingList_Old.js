// Create by 苏成斌 2012.12.11  "采样任务列表"功能

var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var strUrl = "SamplingList.aspx";
var tab = null, selecttabindex = null;
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
         {display: '领取', name: 'IS_RECEIVE', align: 'left', width: 50, minWidth: 60, render: function (record) {
             if (record.IS_RECEIVE == "1")
                 return "已领取";
             else
                 return "<a href=\"javascript:setReceiveSubTask('" + record.ID + "')\">未领取</a> ";
         }
     },
        { display: '项目名称', name: 'PROJECT_NAME', width: 250, align: 'left' },
        { display: '任务单号', name: 'TICKET_NUM', width: 150, align: 'center' },
        //        { display: '业务类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', render: function (record) {
        //            return getDictName(record.CONTRACT_TYPE, "Contract_Type");
        //        }
        //        },
        {display: '监测类型', name: 'REMARK2', width: 90, align: 'left', render: function (record) {
            return getMonitorTypeName(record.REMARK2);
        }
    },
        { display: '采样时间', name: 'REMARK1', width: 90, align: 'left' },
        { display: '采样人', name: 'SAMPLING_MANAGER_ID', width: 70, align: 'left', render: function (record) {
            return getUserName(record.SAMPLING_MANAGER_ID);
        }
        }
        ], width: '100%', height: '100%', pageSizeOptions: [10, 15, 20, 50],
    url: strUrl + '?type=getSampleTask',
    dataAction: 'server', //服务器排序
    usePager: true,       //服务器分页
    pageSize: 15,
    alternatingRow: false,
    checkbox: true,
    sortName: "ID",
    whenRClickToSelect: true,
    toolbar: { items: [
                { id: 'done', text: '办理', click: click_OfToolbar, icon: 'modify' },
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
$(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

//Dblclick
function click_OfDblclick(data) {
    var tabid = "tabidSamplingNew"
    var surl = "../Channels/Mis/Monitor/sampling/ZZ/Sampling.aspx?strSubtaskID=" + data.ID;
    top.f_addTab(tabid, '采样', surl);
}
//toolbar click
function click_OfToolbar(item) {
    switch (item.id) {
        case 'done':
            var selected = manager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }

            var tabid = "tabidSamplingNew"
            var surl = "../Channels/Mis/Monitor/sampling/ZZ/Sampling.aspx?strSubtaskID=" + selected.ID;
            top.f_addTab(tabid, '采样', surl);

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
            var surl = "../Channels/Mis/Monitor/sampling/ZZ/Sampling.aspx?strSubtaskID=" + actionID;
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
        {display: '项目名称', name: 'PROJECT_NAME', width: 250, align: 'left' },
        { display: '任务单号', name: 'TICKET_NUM', width: 150, align: 'center' },
        //        { display: '业务类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', render: function (record) {
        //            return getDictName(record.CONTRACT_TYPE, "Contract_Type");
        //        }
        //        },
        {display: '监测类型', name: 'REMARK2', width: 90, align: 'left', render: function (record) {
            return getMonitorTypeName(record.REMARK2);
        }
    },
        { display: '采样时间', name: 'REMARK1', width: 90, align: 'left' },
        { display: '采样人', name: 'SAMPLING_MANAGER_ID', width: 70, align: 'left', render: function (record) {
            return getUserName(record.SAMPLING_MANAGER_ID);
        }
        }
        ], width: '100%', height: '100%', pageSizeOptions: [10, 15, 20, 50],
    url: strUrl + '?type=getBackSampleTask',
    dataAction: 'server', //服务器排序
    usePager: true,       //服务器分页
    pageSize: 15,
    alternatingRow: false,
    checkbox: true,
    sortName: "ID",
    whenRClickToSelect: true,
    toolbar: { items: [
                { id: 'done', text: '办理', click: click_OfToolbarBack, icon: 'modify' },
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
$(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});
//Dblclick
function click_OfDblclickBack(data) {
    var tabid = "tabidSamplingNew"
    var surl = "../Channels/Mis/Monitor/sampling/ZZ/SamplingView.aspx?strSubtaskID=" + data.ID + "&IS_SEND=2";
    top.f_addTab(tabid, '采样', surl);
}
//toolbar click
function click_OfToolbarBack(item) {
    switch (item.id) {
        case 'done':
            var selected = backmanager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }

            var tabid = "tabidSamplingNew"
            var surl = "../Channels/Mis/Monitor/sampling/ZZ/SamplingView.aspx?strSubtaskID=" + selected.ID + "&IS_SEND=2";
            top.f_addTab(tabid, '采样', surl);

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
            var surl = "../Channels/Mis/Monitor/sampling/ZZ/SamplingView.aspx?strSubtaskID=" + actionID + "&IS_SEND=2";
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
//对未领取的任务设置领取
function setReceiveSubTask(strSubTaskId) {
    $.ligerDialog.confirm('您确认领取该任务吗？', function (yes) {
        if (yes == true) {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/setReceiveSubTask",
                data: "{'strSubTaskId':'" + strSubTaskId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        manager.loadData();
                    }
                }
            });
        }
    });
}