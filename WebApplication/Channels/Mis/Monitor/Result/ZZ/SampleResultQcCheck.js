// Create by 熊卫华 2013.05.03  "现场室主任审核"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;

var strUrl = "SampleResultQcCheck.aspx";
var strOneGridTitle = "任务信息";

//监测任务管理
$(document).ready(function () {
    objOneGrid = $("#oneGrid").ligerGrid({
        title: strOneGridTitle,
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '250px',
        enabledSort: false,
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 150, minWidth: 60 },
                { display: '委托类别', name: 'CONTRACT_TYPE', width: 100, minWidth: 60, render: function (record) {
                    return getDictName(record.CONTRACT_TYPE, "Contract_Type");
                }
                },
                { display: '项目名称', name: 'PROJECT_NAME', width: 300, minWidth: 60 },
                { display: '受检企业', name: 'TESTED_COMPANY_ID', width: 300, minWidth: 60, render: function (record) {
                    return getCompanyName(record.ID, record.TESTED_COMPANY_ID);
                }
                },
                { display: '报告要求完成时间', name: 'ASKING_DATE', width: 100, minWidth: 60 }
                ],
        toolbar: { items: [
                    { text: '退回', click: GoToBack, icon: 'add' },
                    { text: '发送', click: SendToNext, icon: 'add' },
                    { text: '下载汇总表', click: DownLoadSummaryTable, icon: 'excel' }
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
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载样品信息
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID);
            objThreeGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    //下载数据汇总表
    function DownLoadSummaryTable() {
        if (!objOneGrid.getSelectedRow()) {
            $.ligerDialog.warn('请先选择一条记录');
            return;
        }
        $.ligerDialog.open({ title: '数据汇总表', top: 0, width: 600, height: 380, buttons:
        [
            { text:
            '关闭', onclick: function (item, dialog) { dialog.close(); }
            }
        ]
        , url: 'DownLoadSummaryTable.aspx?id=' + objOneGrid.getSelectedRow().ID
        });
    }
    //退回监测监测项目 
    function GoToBack() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('退回之前请先选择一条任务信息');
            return;
        }
        $.ligerDialog.confirm('您确认退回该监测任务吗？', function (yes) {
            if (yes == true) {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=GoToBack&strTaskId=" + objOneGrid.getSelectedRow().ID,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data == "1") {
                            objOneGrid.loadData();
                            objTwoGrid.set("data", emptyArray);
                            objThreeGrid.set("data", emptyArray);
                            $.ligerDialog.success('监测任务退回成功！已退回到【<a style="color:Red;font-weight:bold">采样</a>】环节！')
                        }
                        else {
                            $.ligerDialog.warn('监测任务退回失败');
                        }
                    }
                });
            }
        });
    }
    //发送到下一环节
    function SendToNext() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('发送之前请选择一条任务信息');
            return;
        }
        $.ligerDialog.confirm('您确认发送该任务至下一环节吗？', function (yes) {
            if (yes == true) {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=SendToNext&strTaskId=" + objOneGrid.getSelectedRow().ID,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data == "1") {
                            objOneGrid.loadData();
                            objTwoGrid.set("data", emptyArray);
                            objThreeGrid.set("data", emptyArray);
                            $.ligerDialog.success('任务发送成功！')
                        }
                        else {
                            $.ligerDialog.warn('任务发送失败');
                        }
                    }
                });
            }
        });
    }
});
//样品信息
$(document).ready(function () {
    objTwoGrid = $("#twoGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        enabledSort: false,
        pageSizeOptions: [5, 10, 15, 20],
        height: 480,
        columns: [
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 120, minWidth: 60 },
                { display: '样品名称', name: 'SAMPLE_NAME', align: 'left', width: 120, minWidth: 60 }
        //                ,
        //                { display: '原始单号', name: 'REMARK2', align: 'left', width: 120, minWidth: 60 }
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
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//监测项目信息
$(document).ready(function () {
    objThreeGrid = $("#threeGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        enabledSort: false,
        pageSizeOptions: [5, 10, 15, 20],
        height: 480,
        columns: [
                 { display: '监测项目', name: 'ITEM_NAME', align: 'left', width: 200, minWidth: 60, frozen: true
                 },
                { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60 },
                { display: '单位', name: 'UNIT', align: 'left', width: 90, minWidth: 60
                },
                { display: '采样完成日期', name: 'SAMPLE_ACCESS_DATE', align: 'left', width: 100, minWidth: 60
                },
                { display: '采样负责人', name: 'SAMPLING_MANAGER_NAME', align: 'left', width: 100, minWidth: 60
                },
                { display: '采样协同人', name: 'SAMPLE_ACCESS_NAME', align: 'left', width: 200, minWidth: 60
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

//获取默认负责人用户名称
function getAjaxUserName(strUserId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDefaultUserName",
        data: "{'strUserId':'" + strUserId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取默认协同人名称
function getAjaxUseExName(strUserId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDefaultUserExName",
        data: "{'strUserId':'" + strUserId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//发送之前判断是否满足发送条件
function IsCanSendTaskQcToNextFlow(strTaskId) {
    var isCanBack = false;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=IsCanSendTaskQcToNextFlow&strTaskId=" + strTaskId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            isCanBack = data;
        }
    });
    return isCanBack;
}

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