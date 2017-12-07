// Create by 熊卫华 2013.05.08  "分析任务分配"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;

var strUrl = "AnalysisTaskAllocation.aspx";
var strOneGridTitle = "";
var strTwoGridTitle = "监测项目信息";
var strThreeGridTitle = "监测项目信息";

var strFlowCode = "duty_analyse";
var strResultStatus = "01";

//监测任务管理
$(document).ready(function () {
    topHeight = 2 * $(window).height() / 5;

    objOneGrid = $("#oneGrid").ligerGrid({
        title: strOneGridTitle,
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: 260,
        enabledSort: false,
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 150, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '退回', click: GoToBack, icon: 'add' },
                { line: true },
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
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID);
            objThreeGrid.set("data", emptyArray);
            objFourGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //返回到上一环节
    function GoToBack() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('退回之前请先选择任务信息');
            return;
        }
        $.ligerDialog.confirm('您确认退回该任务吗？', function (yes) {
            if (yes == true) {
                if (IsCanGoToBack(objOneGrid.getSelectedRow().ID) == "0") {
                    $.ligerDialog.warn('该任务正在执行中，不允许退回');
                    return;
                }
                //退回该任务
                if (subTaskGoToBack(objOneGrid.getSelectedRow().ID) == "1") {
                    objOneGrid.loadData();
                    objTwoGrid.set("data", emptyArray);
                    objThreeGrid.set("data", emptyArray);
                    objFourGrid.set("data", emptyArray);
                    $.ligerDialog.success('任务回退成功')
                }
                else {
                    $.ligerDialog.warn('任务回退失败');
                }
            }
        });
    }
    //发送到下一环节
    function SendToNext() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('发送之前请先选择任务信息');
            return;
        }
        $.ligerDialog.confirm('您确定要将该任务发送至下一环节吗？', function (yes) {
            if (yes == true) {
                if (SendToNextFlow(objOneGrid.getSelectedRow().ID) == "1") {
                    objOneGrid.loadData();
                    objTwoGrid.set("data", emptyArray);
                    objThreeGrid.set("data", emptyArray);
                    objFourGrid.set("data", emptyArray);
                    $.ligerDialog.success('任务发送成功')
                }
                else {
                    $.ligerDialog.warn('任务发送失败,请检查默认负责人是否全部已设置');
                }
            }
        });
    }
});
//监测类别管理
$(document).ready(function () {
    bottomHeight = 3 * $(window).height() / 5 - 35;

    objTwoGrid = $("#twoGrid").ligerGrid({
        dataAction: 'server',
        usePager: false,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        enabledSort: false,
        height: bottomHeight,
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

            //点击的时候加载监测项目信息
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID);
            objFourGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});

//样品号管理
$(document).ready(function () {
    bottomHeight = 3 * $(window).height() / 5 - 35;

    objThreeGrid = $("#threeGrid").ligerGrid({
        dataAction: 'server',
        usePager: false,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        enabledSort: false,
        height: bottomHeight,
        columns: [
                    { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 150, minWidth: 60 }
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
            objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});

//监测项目信息
$(document).ready(function () {
    bottomHeight = 3 * $(window).height() / 5 - 35;

    objFourGrid = $("#fourGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        enabledSort: false,
        pageSizeOptions: [5, 8, 10],
        height: bottomHeight,
        columns: [
                    { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, render: function (record) {
                        return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                    }
                    },
                    { display: '默认负责人', name: 'USER_DEFAULT', align: 'left', width: 100, minWidth: 60,
                        render: function (record, rowindex, value) {
                            var objUserArray = getAjaxUserName(record.ID);
                            var strUserName = "";
                            if (objUserArray.length > 0)
                                strUserName = objUserArray[0]["UserName"];
                            else
                                strUserName = "请选择";
                            return "<a href=\"javascript:getDefaultUserName('" + record.ID + "')\">" + strUserName + "</a> ";
                        }
                    },
                    { display: '分析协同人', name: 'USER_DEFAULT_EX', align: 'left', width: 150, minWidth: 60,
                        render: function (record, rowindex, value) {
                            var objUserArray = getAjaxUseExName(record.ID);
                            var strUserName = "";
                            if (objUserArray.length > 0)
                                strUserName = objUserArray[0]["UserName"];
                            else
                                strUserName = "请选择";
                            return "<a href=\"javascript:getDefaultUserExName('" + record.ID + "')\">" + strUserName + "</a> ";
                        }
                    },
                    { display: '分析要求完成时间', name: 'ASKING_DATE', align: 'left', width: 150, minWidth: 60,
                        render: function (record, rowindex, value) {
                            var strAskingDate = getAjaxAskingDate(record.ID);
                            var strAskingDateTemp = "";
                            if (strAskingDate != "")
                                strAskingDateTemp = strAskingDate;
                            else
                                strAskingDateTemp = "请选择";
                            return "<a href=\"javascript:getAskingDate('" + record.ID + "')\">" + strAskingDateTemp + "</a> ";
                        }
                    }
                ],
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});

//弹出选择默认负责人
function getDefaultUserName(strResultId) {
    $.ligerDialog.open({ title: "选择分析负责人", width: 400, height: 250,isHidden:false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.UserSave()) {
                dialog.close();
                //$.ligerDialog.success('数据保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objFourGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../DefaultUser.aspx?strResultId=" + strResultId
    });
}
//弹出选择默认协同人
function getDefaultUserExName(strResultId) {
    $.ligerDialog.open({ title: "选择分析协同人", width: 400, height: 250,isHidden:false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.UserSave()) {
                dialog.close();
                //$.ligerDialog.success('数据保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objFourGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../DefaultUserEx.aspx?strResultId=" + strResultId
    });
}
//弹出选择分析完成时间
function getAskingDate(strResultId) {
    $.ligerDialog.open({ title: "录入分析完成时间", width: 400, height: 300, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.UserSave()) {
                dialog.close();
                //$.ligerDialog.success('数据保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objFourGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../AskingDateSelected.aspx?strResultId=" + strResultId
    });
}
//获取默认负责人用户名称
function getAjaxUserName(strResultId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDefaultUserName",
        data: "{'strResultId':'" + strResultId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return jQuery.parseJSON(strValue);
}
//获取默认协同人名称
function getAjaxUseExName(strResultId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDefaultUserExName",
        data: "{'strResultId':'" + strResultId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return jQuery.parseJSON(strValue);
}
//获取分析完成时间
function getAjaxAskingDate(strResultId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getAskingDate",
        data: "{'strResultId':'" + strResultId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取已经分配的监测项目
function getAssignedDefaultItemName(strTaskId, strDefaultUser) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getAssignedDefaultItemName",
        data: "{'strTaskId':'" + strTaskId + "','strDefaultUser':'" + strDefaultUser + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取已经分配的协同人信息
function getAssignedDefaultItemNameEx(strTaskId, strDefaultUser) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getAssignedDefaultItemNameEx",
        data: "{'strTaskId':'" + strTaskId + "','strDefaultUser':'" + strDefaultUser + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取已经分配的样品号
function getAssignedSampleCode(strTaskId, strDefaultUser) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getAssignedSampleCode",
        data: "{'strTaskId':'" + strTaskId + "','strDefaultUser':'" + strDefaultUser + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//退回之前判断是否满足回退条件
function IsCanGoToBack(strTaskId) {
    var isCanBack = false;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=IsCanGoToBack&strTaskId=" + strTaskId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            isCanBack = data;
        }
    });
    return isCanBack;
}

//退回任务
function subTaskGoToBack(strTaskId) {
    var isSuccess = false;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=GoToBack&strTaskId=" + strTaskId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            isSuccess = data;
        }
    });
    return isSuccess;
}
//发送任务
function SendToNextFlow(strTaskId) {
    var isSuccess = false;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=SendToNext&strTaskId=" + strTaskId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            isSuccess = data;
        }
    });
    return isSuccess;
}