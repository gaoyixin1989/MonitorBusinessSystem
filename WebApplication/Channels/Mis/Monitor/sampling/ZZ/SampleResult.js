// Create by 苏成斌 2012.12.13  "采样-现场项目结果填报"功能
var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var objOneFrom = null;
var objTwoGrid = null;
var strUrl = "SampleResult.aspx";
var strSubtaskID = "";

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
//监测点位信息
$(document).ready(function () {
    strSubtaskID = $.getUrlVar('strSubtaskID');
    objOneGrid = $("#oneGrid").ligerGrid({
        dataAction: 'server',
        usePager: false,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        height: '96%',
        pageSizeOptions: [5, 10, 15, 20],
        url: strUrl + '?type=getOneGridInfo&strSubtaskID=' + strSubtaskID,
        columns: [
                    { display: '监测点位', name: 'POINT_NAME', align: 'left', width: 150, minWidth: 60 },
                    { display: '样品号', name: 'REMARK1', align: 'left', width: 100, minWidth: 60 },
                    { display: '样品名称', name: 'REMARK2', align: 'left', width: 100, minWidth: 60 }
                ],
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

        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载监测项目信息
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&OneGridId=" + rowdata.REMARK3 + "&strSubtaskID=" + strSubtaskID);
        },
        onAfterEdit: SampleEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    function SampleEdit(e) {
        var id = e.record.ID;
        var strSubtaskID = e.record.SUBTASK_ID;
        var strSampleCode = e.record.REMARK1;
        var strSampleName = e.record.REMARK2
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/saveSample",
            data: "{'id':'" + id + "','strSubtaskID':'" + strSubtaskID + "','strSampleCode':'" + strSampleCode + "','strSampleName':'" + strSampleName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == 1) {
                    objTwoGrid.cancelEdit(e.rowindex);
                }
            }
        });
    }
});
//现场项目
$(document).ready(function () {
    objTwoGrid = $("#twoGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 8,
        alternatingRow: false,
        checkbox: false,
        onRClickToSelect: true,
        enabledEdit: true,
        width: '100%',
        height: '96%',
        pageSizeOptions: [5, 8, 10],
        columns: [
                    { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 130, minWidth: 60, render: function (record) {
                        return getItemInfoName(record.ITEM_ID);
                    }
                    },
                    { display: '结果值', name: 'ITEM_RESULT', align: 'left', width: 120, minWidth: 60,
                        editor: {
                            type: 'text'
                        }
                    },
                    { display: '单位', name: 'ITEM_ID', align: 'left', width: 90, minWidth: 60, render: function (record) {
                        return getItemUnit(record.ITEM_ID);
                    }
                    }
                ],
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onAfterEdit: AfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    function AfterEdit(e) {
        var id = e.record.ID;
        var strItemResult = e.record.ITEM_RESULT;
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/saveResult",
            data: "{'id':'" + id + "','strItemResult':'" + strItemResult + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == 1) {
                    objTwoGrid.cancelEdit(e.rowindex);
                }
            }
        });
    }
});
//获取字典项信息
function getItemUnit(strItemID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getItemUnit",
        data: "{'strItemID':'" + strItemID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取监测项目资料信息
function getItemInfoName(strItemID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getItemInfoName",
        data: "{'strItemID':'" + strItemID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}