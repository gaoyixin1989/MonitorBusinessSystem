// Create by 熊卫华 2013.09.26  "汇总表下载列表"功能
var objOneGrid = null;

var strUrl = "DownLoadSummaryTable.aspx";
var strOneGridTitle = "汇总表下载";

//汇总表列表界面
$(document).ready(function () {
    objOneGrid = $("#oneGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20],
        height: '100%',
        enabledSort: false,
        url: strUrl + '?type=getOneGridInfo&formId=' + $("#cphData_formId").val(),
        columns: [
                { display: '监测类别', name: 'MONITOR_ID', align: 'left', width: 150, minWidth: 60, render: function (record) {
                    return getMonitorTypeName(record.MONITOR_ID);
                }
                },
                 { display: '采样汇总表', name: 'MONITOR_NAME', align: 'left', width: 130, minWidth: 60,
                     render: function (record, rowindex, value) {
                         return "<a href=\"javascript:downLoadFile('samplingSummaryTable','" + record.ID + "')\">下载</a> ";
                     }
                 },
                 { display: '分析汇总表', name: 'MONITOR_NAME', align: 'left', width: 130, minWidth: 60,
                     render: function (record, rowindex, value) {
                         return "<a href=\"javascript:downLoadFile('resultSummaryTable','" + record.ID + "')\">下载</a> ";
                     }
                 }
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
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});

///附件下载
function downLoadFile(fileType, strSubTaskId) {
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../../OA/ATT/AttFileDownLoad.aspx?filetype=' + fileType + '&id=' + strSubTaskId
    });
}