// Create by 苏成斌 2012.12.11  "采样任务列表"功能

var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var strUrl = "SamplingList.aspx";

$(document).ready(function () {
    $("#layout1").ligerLayout({ leftWidth: 170, height: '100%' });

    //质控设置菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menudone', text: '办理', click: click_OfMenu, icon: 'modify' }
            ]
    });

    //grid
    window['g'] =
    manager = $("#maingridItem").ligerGrid({
        columns: [
//        { display: '企业', name: 'TESTED_COMPANY_ID', width: 180, align: 'left', render: function (record) {
//            return getCompanyName1(record.TESTED_COMPANY_ID);
//        }
//        },
        { display: '项目名称', name: 'PROJECT_NAME', width: 250, align: 'left' },
//        { display: '合同号', name: 'CONTRACT_CODE', width: 150, align: 'left' },
//        { display: '业务类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', render: function (record) {
//            return getDictName(record.CONTRACT_TYPE, "Contract_Type");
//        }
//        },
        { display: '监测类型', name: 'REMARK2', width: 90, align: 'left', render: function (record) {
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
    var surl = "../Channels/Mis/Monitor/sampling/QHD/Sampling.aspx?strSubtaskID=" + data.ID;
    top.f_addTab(tabid, '采样', surl);
}
//toolbar click
function click_OfToolbar(item) {
    switch (item.id) {
        case 'done':
            var selected = manager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }

            var tabid = "tabidSamplingNew"
            var surl = "../Channels/Mis/Monitor/sampling/QHD/Sampling.aspx?strSubtaskID=" + selected.ID;
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
            var surl = "../Channels/Mis/Monitor/sampling/QHD/Sampling.aspx?strSubtaskID=" + actionID;
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
                      { display: "监测类型", name: "MONITOR_ID", newline: true, type: "select", comboboxName: "SrhMONITOR_TYPE_ID", options: { valueFieldID: "MONITOR_ID", url: "../../../Base/MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} }
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