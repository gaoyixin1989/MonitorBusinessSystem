// Create by 熊卫华 2012.12.11  "分析结果质控审核"功能
var objOneGrid = null;
var objMonitorGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;
var objFiveGrid = null;

var strUrl = "AnalysisResultQcCheck.aspx";
var strOneGridTitle = "任务信息";

//监测任务管理
$(document).ready(function () {
    objOneGrid = $("#oneGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '240px',
        enabledSort: false,
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 150, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '退回', click: GoToBack, icon: 'add' },
                { text: '发送', click: SendToNext, icon: 'add'}]
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

            //点击的时候加载监测类别信息
            objMonitorGrid.set('url', strUrl + "?type=getMonitorGridInfo&oneGridId=" + rowdata.ID);
            objTwoGrid.set("data", emptyArray);
            objThreeGrid.set("data", emptyArray);
            //            objFourGrid.set("data", emptyArray);
            //            objFiveGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
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
                            objFourGrid.set("data", emptyArray);
                            objFiveGrid.set("data", emptyArray);
                            $.ligerDialog.success('监测任务退回成功')
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
                //如果不是环境质量类需要逻辑控制
                if (objOneGrid.getSelectedRow().TASK_TYPE != "1") {
                    if (IsCanSendTaskQcToNextFlow(objOneGrid.getSelectedRow().ID) == "0") {
                        $.ligerDialog.warn('该任务正在执行中，不允许发送');
                        return;
                    }
                }
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=SendToNext&strTaskId=" + objOneGrid.getSelectedRow().ID + "&strTaskType=" + objOneGrid.getSelectedRow().TASK_TYPE,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data == "1") {
                            objOneGrid.loadData();
                            objTwoGrid.set("data", emptyArray);
                            objThreeGrid.set("data", emptyArray);
                            objFourGrid.set("data", emptyArray);
                            objFiveGrid.set("data", emptyArray);
                            $.ligerDialog.success('任务发送成功')
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
//监测类别信息
$(document).ready(function () {
    objMonitorGrid = $("#MonitorGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        height: '100%',
        enabledSort: false,
        columns: [
                { display: '监测类别', name: 'MONITOR_ID', align: 'left', width: 150, minWidth: 60, render: function (record) {
                    return getMonitorTypeName(record.MONITOR_ID);
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

            //点击的时候加载样品信息
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&MonitorGridId=" + rowdata.ID);
            objThreeGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
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
        pageSizeOptions: [5, 10, 15, 20],
        height: '100%',
        enabledSort: false,
        columns: [
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '样品名称', name: 'SAMPLE_NAME', align: 'left', width: 150, minWidth: 60 }
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
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID + "&SubTaskID=" + objMonitorGrid.getSelectedRow().ID);
            //            objFourGrid.set("data", emptyArray);
            //            objFiveGrid.set("data", emptyArray);
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
        height: '100%',
        columns: [
                 { display: '分析项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     if (record.FINISH_DATE > record.ASKING_DATE) {
                         return "<div style='color:red;'>" + getItemInfoName(record.ITEM_ID, "ITEM_NAME") + "</div>";
                     }
                     else {
                         return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     }
                 }
                 },
        //                 { display: '质控手段', name: 'QC', align: 'left', width: 100, minWidth: 60, render: function (record) {
        //                     return getQcName(record.QC);
        //                 }
        //                 },
                  {display: '方法依据', name: 'METHOD_CODE', align: 'left', width: 200, minWidth: 60 },
                 { display: '分析方法', name: 'ANALYSIS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '检出限', name: 'LOWER_CHECKOUT', align: 'left', width: 100, minWidth: 60 },
                 { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                { display: '仪器名称及编码', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60,
                    render: function (record, rowindex, value) {
                        if (record.APPARATUS_NAME != "" || record.APPARATUS_CODE != "")
                            return "" + record.APPARATUS_NAME + "(" + record.APPARATUS_CODE + ")";
                    }
                },
                 { display: '分析负责人', name: 'HEAD_USERID', align: 'left', width: 100, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUserName(record.HEAD_USERID);
                 }
                 },
                 { display: '分析协同人', name: 'ASSISTANT_USERID', align: 'left', width: 200, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUseExName(record.ASSISTANT_USERID);
                 }
                 }
                ],
        toolbar: { items: [
                { text: '退回', click: ResultGoToBack, icon: 'add'}]
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

            //点击的时候加载质控信息
            //            objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + rowdata.ID);
            //            objFiveGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    function ResultGoToBack() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('退回之前请先选择监测项目信息');
            return;
        }
        $.ligerDialog.confirm('您确认退回该监测项目吗？', function (yes) {
            if (yes == true) {
                //退回该项目
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=ResultGoToBack&strResultId=" + objThreeGrid.getSelectedRow().ID,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data == "1") {
                            objThreeGrid.loadData();
                            objFourGrid.set("data", emptyArray);
                            objFiveGrid.set("data", emptyArray);
                            $.ligerDialog.success('监测项目退回成功')
                        }
                        else {
                            $.ligerDialog.warn('监测项目退回失败');
                        }
                    }
                });
            }
        });
    }
});
//质控信息
//$(document).ready(function () {
//    objFourGrid = $("#fourGrid").ligerGrid({
//        title: "",
//        dataAction: 'server',
//        usePager: false,
//        pageSize: 10,
//        alternatingRow: false,
//        checkbox: true,
//        onRClickToSelect: true,
//        sortName: "ID",
//        width: '100%',
//        pageSizeOptions: [5, 10, 15, 20],
//        height: 180,
//        columns: [
//                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, render: function (record) {
//                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
//                     return strItemName;
//                 }
//                 },
//                 { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 100, minWidth: 60 },
//                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
//                     return getQcName(record.QC_TYPE);
//                 }
//                 }
//                ],
//        onContextmenu: function (parm, e) {
//            for (var rowid in this.records)
//                this.unselect(rowid);
//            this.select(parm.rowindex);
//            return false;
//        },
//        onDblClickRow: function (data, rowindex, rowobj) {
//            for (var rowid in this.records)
//                this.unselect(rowid);
//            this.select(rowindex);
//        },
//        onCheckRow: function (checked, rowdata, rowindex) {
//            for (var rowid in this.records)
//                this.unselect(rowid);
//            this.select(rowindex);
//            //点击的时候加载实验室质控信息
//            objFiveGrid.set('url', strUrl + "?type=getFiveGridInfo&threeGridId=" + rowdata.ID);
//        },
//        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
//    });
//    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
//});
////实验室质控信息
//$(document).ready(function () {
//    objFiveGrid = $("#fiveGrid").ligerGrid({
//        title: "",
//        dataAction: 'server',
//        usePager: false,
//        pageSize: 10,
//        alternatingRow: false,
//        checkbox: true,
//        onRClickToSelect: true,
//        sortName: "ID",
//        width: '100%',
//        pageSizeOptions: [5, 10, 15, 20],
//        height: 180,
//        columns: [
//                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, render: function (record) {
//                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
//                     return strItemName;
//                 }
//                 },
//                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
//                     return getQcName(record.QC_TYPE);
//                 }
//                 }
//                ],
//        onContextmenu: function (parm, e) {
//            for (var rowid in this.records)
//                this.unselect(rowid);
//            this.select(parm.rowindex);
//            return false;
//        },
//        onDblClickRow: function (data, rowindex, rowobj) {
//            for (var rowid in this.records)
//                this.unselect(rowid);
//            this.select(rowindex);
//        },
//        onCheckRow: function (checked, rowdata, rowindex) {
//            for (var rowid in this.records)
//                this.unselect(rowid);
//            this.select(rowindex);
//        },
//        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
//    });
//    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
//});
//获取质控手段名称
function getQcName(strQcId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getQcName",
        data: "{'strQcId':'" + strQcId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
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