// Create by 熊卫华 2013.09.12  "技术负责人审核"功能

var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;
var objFiveGrid = null;

var strUrl = "AnalysisResultQcTechnicalManagerAudit.aspx";
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
        //                { display: '合同编号', name: 'CONTRACT_CODE', align: 'left', width: 150, minWidth: 60 },
                {display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 150, minWidth: 60 },
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
                { text: '审核', click: GoToBack, icon: 'add' },
                { text: '确认', click: SendToNext, icon: 'add' },
                { text: '下载汇总表', click: DownLoadSummaryTable, icon: 'excel' },
                { text: '上传', click: upLoadFile, icon: 'fileup' }
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
            objFourGrid.set("data", emptyArray);
            objFiveGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    ///附件上传
    function upLoadFile() {
        if (!objOneGrid.getSelectedRow()) {
            $.ligerDialog.warn('请先选择一条记录');
            return;
        }
        $.ligerDialog.open({ title: '附件上传', width: 500, height: 300, isHidden: false,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../../OA/ATT/AttFileUpload.aspx?'
        });
    }
    //下载数据汇总表
    function DownLoadSummaryTable() {
        if (!objOneGrid.getSelectedRow()) {
            $.ligerDialog.warn('请先选择一条记录');
            return;
        }
        $.ligerDialog.open({ title: '数据汇总表', top: 0, width: 600, height: 380, buttons:
        [
                { text: '上传', onclick: function (item, dialog) {
                    dialog.frame.upLoadFile();
                }
                },
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
        $.ligerDialog.open({ title: '审核意见', name: 'winaddtor', width: 700, height: 300, top: 90, url: 'AnalysisResultQcManagerAudit_RtnContent.aspx?', buttons: [
                { text: '确定', onclick: f_SaveDate },
                { text: '取消', onclick: f_Cancel }
            ]
        });
        //        $.ligerDialog.confirm('您确认退回该监测任务吗？', function (yes) {
        //            if (yes == true) {
        //                $.ajax({
        //                    cache: false,
        //                    async: false,
        //                    type: "POST",
        //                    url: strUrl + "?type=GoToBack&strTaskId=" + objOneGrid.getSelectedRow().ID,
        //                    contentType: "application/json; charset=utf-8",
        //                    dataType: "json",
        //                    success: function (data, textStatus) {
        //                        if (data == "1") {
        //                            objOneGrid.loadData();
        //                            objTwoGrid.set("data", emptyArray);
        //                            objThreeGrid.set("data", emptyArray);
        //                            objFourGrid.set("data", emptyArray);
        //                            objFiveGrid.set("data", emptyArray);
        //                            $.ligerDialog.success('监测任务退回成功')
        //                        }
        //                        else {
        //                            $.ligerDialog.warn('监测任务退回失败');
        //                        }
        //                    }
        //                });
        //            }
        //        });
    }
    function f_SaveDate(item, dialog) {
        var select_ID = objOneGrid.getSelectedRow().ID;
        var fn = dialog.frame.GetRequestValue || dialog.frame.window.GetRequestValue;
        var Rtn_Content = fn();
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "?type=GoToBack&strTaskId=" + select_ID + "&Rtn_Content=" + encodeURI(Rtn_Content),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data == "1") {
                    objOneGrid.loadData();
                    objTwoGrid.set("data", emptyArray);
                    objThreeGrid.set("data", emptyArray);
                    objFourGrid.set("data", emptyArray);
                    objFiveGrid.set("data", emptyArray);
                    $.ligerDialog.success('监测任务审核成功')
                }
                else {
                    $.ligerDialog.warn('监测任务审核失败');
                }
            }
        });
    }
    function f_Cancel(item, dialog) {
        dialog.close();
    }
    //发送到下一环节
    function SendToNext() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('发送之前请选择一条任务信息');
            return;
        }
        $.ligerDialog.confirm('您确认发送该任务至下一环节吗？', function (yes) {
            if (yes == true) {
                if (IsCanSendTaskQcToNextFlow(objOneGrid.getSelectedRow().ID) == "0") {
                    $.ligerDialog.warn('该任务正在执行中，不允许发送');
                    return;
                }
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
                            objFourGrid.set("data", emptyArray);
                            objFiveGrid.set("data", emptyArray);
                            $.ligerDialog.success('任务发送成功！已发送到【<a style="color:Red;font-weight:bold">业务室</a>】环节！')
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
        height: '100%',
        columns: [
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '全程质控', name: 'ENTIRE_QC', align: 'left', width: 50, minWidth: 60, render: function (record) {
                    if (record.ENTIRE_QC == "1")
                        return "是";
                    else
                        return "否 ";
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
            objFiveGrid.set("data", emptyArray);
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
        height: 260,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     return strItemName;
                 }
                 },
                 { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60 },
        //                 { display: '原始单号', name: 'REMARK_2', align: 'left', width: 150, minWidth: 60 },
                 {display: '方法依据', name: 'METHOD_CODE', align: 'left', width: 200, minWidth: 60 },
                 { display: '分析方法', name: 'ANALYSIS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '检出限', name: 'LOWER_CHECKOUT', align: 'left', width: 100, minWidth: 60 },
                 { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器编号', name: 'APPARATUS_CODE', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '分析负责人', name: 'HEAD_USERID', align: 'left', width: 100, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUserName(record.HEAD_USERID);
                 }
                 },
                 { display: '分析协同人', name: 'ASSISTANT_USERID', align: 'left', width: 200, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUseExName(record.ASSISTANT_USERID);
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

            //点击的时候加载质控信息
            objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + rowdata.ID);
            objFiveGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//质控信息
$(document).ready(function () {
    objFourGrid = $("#fourGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '180',
        enabledSort: false,
        columns: [
                 { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 100, minWidth: 60 },
                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     return getQcName(record.QC_TYPE);
                 }
                 },
                  { display: '结果值', name: 'ITEM_RESULT', align: 'left', width: 50, minWidth: 60 },
                 { display: '质控结果', name: 'REMARK', align: 'left', width: 250, minWidth: 60 },
                 { display: '是否合格', name: 'IS_OK', align: 'left', width: 50, minWidth: 60, render: function (record) {
                     if (record.IS_OK == "0")
                         return "<span style='color:red'>否</span>";
                     else
                         return "是";
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
            //点击的时候加载实验室质控信息
            objFiveGrid.set('url', strUrl + "?type=getFiveGridInfo&threeGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//实验室质控信息
$(document).ready(function () {
    objFiveGrid = $("#fiveGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '180',
        enabledSort: false,
        columns: [
                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     return getQcName(record.QC_TYPE);
                 }
                 },
                  { display: '结果值', name: 'ITEM_RESULT', align: 'left', width: 50, minWidth: 60 },
                 { display: '质控结果', name: 'REMARK', align: 'left', width: 250, minWidth: 60 },
                 { display: '是否合格', name: 'IS_OK', align: 'left', width: 50, minWidth: 60, render: function (record) {
                     if (record.IS_OK == "0")
                         return "<span style='color:red'>否</span>";
                     else
                         return "是";
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