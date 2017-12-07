// Create by 魏林 2014.04.11  "点位项目拆分"功能

var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strUrl = "PointItemSplit.aspx";
var strSampleID = "";
var strSampleCode = "";
var strSampleName = "";
var strMonitorID = "";
var strSubTaskID = "";
var objItemGrid = null;

$(document).ready(function () {
    strSampleID = getQueryString("SampleID");
    strSampleCode = getQueryString("SampleCode");
    strSampleName = getQueryString("SampleName");
    strMonitorID = getQueryString("MonitorID");
    strSubTaskID = getQueryString("SubTaskID");

    $("#divPoint").ligerForm({
        inputWidth: 160, labelWidth: 100, space: 30, labelAlign: 'right', Height: 500,
        fields: [
                { display: "样品编号", name: "SAMPLE_CODE", newline: true, type: "text", width: 150, group: "样品信息", groupicon: groupicon },
                { display: "样品名称", name: "SAMPLE_NAME", newline: false, width: 150, type: "text" }]
    });


    //赋值
    $("#SAMPLE_CODE").val(strSampleCode);
    $("#SAMPLE_NAME").val(strSampleName);

    $("#SAMPLE_CODE").ligerGetComboBoxManager().setDisabled();

    objItemGrid = $("#divItem").ligerGrid({
        dataAction: 'server',
        usePager: false,
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        enabledEdit: false,
        sortName: "ID",
        width: '98%',
        pageSizeOptions: [5, 8, 10],
        height: '95%',
        url: strUrl + '?type=getItems&strSampleID=' + strSampleID,
        columns: [
                    { display: '监测项目', name: 'ITEM_NAME', width: 280, align: 'left', isSort: false, render: function (record) {
                        return getItemName(record.ITEM_ID);
                    }
                    }
                 ],
        title: "选择需要拆分的项目",
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            //for (var rowid in this.records)
            //    this.unselect(rowid);
            //this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    $(".l-layout-header-toggle").css("display", "none");
});

//获取监测项目信息
function getItemName(strItemID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getItemName",
        data: "{'strValue':'" + strItemID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

function SaveItemSplit() {
    var strReturn = "true";
    if (objItemGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('至少选择一条项目进行拆分!');
        return "false";
    }
    if (objItemGrid.getSelectedRows().length == objItemGrid.rows.length) {
        $.ligerDialog.warn('至少保留一条项目不用拆分!');
        return "false";
    }
    var strPointName = $("#SAMPLE_NAME").val();
    var strResultItems = "";
    var strItems = "";
    for (var i = 0; i < objItemGrid.getSelectedRows().length; i++) {
        strResultItems += objItemGrid.getSelectedRows()[i].ID + ",";
        strItems += objItemGrid.getSelectedRows()[i].ITEM_ID + ",";
    }

    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/SaveItemSplit",
        data: "{'strSampleID':'" + strSampleID + "','strPointName':'" + strPointName + "','strResultItems':'" + strResultItems + "','strItems':'" + strItems + "','strMonitorID':'" + strMonitorID + "','strSubTaskID':'" + strSubTaskID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strReturn = data.d;
        }
    });

    return strReturn;
}