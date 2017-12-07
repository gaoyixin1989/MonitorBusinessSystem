// Create by 熊卫华 2012.12.11  "分析结果质控审核"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;
var objFiveGrid = null;

var strUrl = "AnalysisResultQcCheck.aspx";
var strOneGridTitle = "任务信息";
var strSubTaskID = "";

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
        height: '200px',
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 150, minWidth: 60 },
                { display: '合同编号', name: 'CONTRACT_CODE', align: 'left', width: 150, minWidth: 60 },
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

            //点击的时候加载样品信息
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID);
            objThreeGrid.set("data", emptyArray);
            objFourGrid.set("data", emptyArray);
            objFiveGrid.set("data", emptyArray);
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
        $.ligerDialog.prompt('退回意见', '', true, function (yes, value) {

            if (yes) {
                //退回该项目
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=GoToBack&strTaskId=" + objOneGrid.getSelectedRow().ID + "&strSuggestion=" + encodeURI(value),
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
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=SendToNext&strTaskId=" + objOneGrid.getSelectedRow().ID,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.result == "1") {
                            objOneGrid.loadData();
                            objTwoGrid.set("data", emptyArray);
                            objThreeGrid.set("data", emptyArray);
                            objFourGrid.set("data", emptyArray);
                            objFiveGrid.set("data", emptyArray);
                            
                            if (data.msg != "")
                                $.ligerDialog.success('任务发送成功，发送至【<a style="color:Red;font-weight:bold">' + data.msg + '</a>】环节');
                            else
                                $.ligerDialog.success('任务发送成功');
                            return;
                        }
                        else {
                            $.ligerDialog.warn('任务发送失败');
                            return;
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
        pageSizeOptions: [5, 10, 15, 20],
        height: $(window).height() - 380,
        columns: [
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 150, minWidth: 60 },
                 { display: '特殊样说明', name: 'RemarkView', align: 'center', width: 80, render: function (items) {
                     if (items.SPECIALREMARK != "") {
                         return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                     }
                 }
                 },
                { display: '子样', name: 'SUBSAMPLE', align: 'center', width: 80, render: function (items) {
                    if (getSubSample(items.ID) != null) {
                        return "<a href='javascript:ShowSubSample(\"" + items.ID + "\")'>明细</a>";
                    }
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
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID + "&strSubTaskID=" + rowdata.SUBTASK_ID);
            strSubTaskID = rowdata.SUBTASK_ID;
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
        pageSizeOptions: [5, 10, 15, 20],
        height: 180,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     return record.isQC == "1" ? "<span style=\"color:Red\">" + strItemName + "</span>" : strItemName;
                 }
                 },
                 { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60, render: function (items) {
                     //var isShow = getItemInfoName(items.ITEM_ID, "IS_ANYSCENE_ITEM");
                     //if (isShow == "1") {
                     //    var strSubTaskId = objTwoGrid.getSelectedRow();

                     //    return "<a href='javascript:SetTable(\"" + strSubTaskId.SUBTASK_ID + "\" ,\"" + items.ITEM_ID + "\")'>原始样结果</a>";
                     //}
                     return items.ITEM_RESULT;
                 }
                 },
                 { display: '质控手段', name: 'QC', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     return getQcName(record.QC);
                 }
                 },
                  { display: '方法依据', name: 'METHOD_CODE', align: 'left', width: 200, minWidth: 60 },
                 { display: '分析方法', name: 'ANALYSIS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '检出限', name: 'RESULT_CHECKOUT', align: 'left', width: 100, minWidth: 60 },
                 { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器编号', name: 'APPARATUS_CODE', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '前处理说明', name: 'REMARK_2', align: 'center', width: 80, render: function (items) {
                     if (items.REMARK_2 != "") {
                         return "<a href='javascript:showSampleRemarkSrh(\"" + items.ID + "\",\"" + items.REMARK_2 + "\",false)'>查看</a>";
                     }
                     return items.REMARK_2;
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
                { text: '原始记录信息', click: SetTable, icon: 'attibutes' },
                { text: '质控信息', click: QcInfo, icon: 'attibutes' },
                { text: '退回', click: ResultGoToBack, icon: 'add'}]//huangjinjun add 20141103]
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
            objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + rowdata.ID + "&strSubTaskID=" + strSubTaskID + "&strItemID=" + rowdata.ITEM_ID);
            //点击的时候加载实验室质控信息
            objFiveGrid.set('url', strUrl + "?type=getFiveGridInfo&threeGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //监测项目退回
    function ResultGoToBack() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('退回之前请先选择监测项目信息');
            return;
        }
        $.ligerDialog.prompt('退回意见', '', true, function (yes, value) {
            if (yes == true) {
                //退回该项目
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=ResultGoToBack&strResultId=" + objThreeGrid.getSelectedRow().ID + "&strSuggestion=" + encodeURI(value),
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
        height: $(window).height() - 500,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     return strItemName;
                 }
                 },
                 { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 120, minWidth: 60 },
                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     return getQcName(record.QC_TYPE);
                 }
                 },
                 { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 50, minWidth: 60 },
                 { display: '质控结果', name: 'REMARK', align: 'left', width: 320, minWidth: 60 },
                  { display: '是否合格', name: 'IS_OK', align: 'left', width: 50, minWidth: 60, render: function (record) {
                      if (record.IS_OK == "0")
                          return "<span style='color:red'>否</span>";
                      else if (record.IS_OK == "1")
                          return "是";
                      else
                          return "";
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
        height: $(window).height() - 500,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     return strItemName;
                 }
                 },
                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     return getQcName(record.QC_TYPE);
                 }
                 },
                 { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 50, minWidth: 60 },
                 { display: '质控结果', name: 'REMARK', align: 'left', width: 320, minWidth: 60 },
                  { display: '是否合格', name: 'IS_OK', align: 'left', width: 50, minWidth: 60, render: function (record) {
                      if (record.IS_OK == "0")
                          return "<span style='color:red'>否</span>";
                      else if (record.IS_OK == "1")
                          return "是";
                      else
                          return "";
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

    objFiveGrid.toggleCol("IS_OK");
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
//发送之前判断现场项目是否已
function IsCanSendTaskQcToNextFlowWithSence(strTaskId) {
    var isCanBack = false;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=IsCanSendTaskQcToNextFlowWithSence&strTaskId=" + strTaskId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            isCanBack = data;
        }
    });
    return isCanBack;
}

//设置grid 的弹出特殊样说明录入对话框
var detailRemarkWinSrh = null;
function showDetailRemarkSrh(strSubTaskId, oldRemark, isAdd) {
    //创建表单结构

    var mainRemarkform = $("#RemarkForm");
    mainRemarkform.ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: "特殊样说明", name: "SEA_REMARK", newline: true, type: "textarea" }
                    ]
    });
    $("#SEA_REMARK").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:400px");

    $("#SEA_REMARK").val(oldRemark);
    var ObjButton = [];
    if (!isAdd) {
        $("#SEA_REMARK").attr("disabled", true);
        ObjButton = [
                  { text: '返回', onclick: function () { clearRemarkDialogValue(); detailRemarkWinSrh.hide(); } }
                  ];
    } else {
        $("#SEA_REMARK").attr("disabled", false);
        ObjButton = [
                  { text: '确定', onclick: function () { SaveRemark(strSubTaskId); } },
                  { text: '返回', onclick: function () { clearRemarkDialogValue(); detailRemarkWinSrh.hide(); } }
                  ];
    }
    detailRemarkWinSrh = $.ligerDialog.open({
        target: $("#detailRemark"),
        width: 660, height: 170, top: 90, title: isAdd ? "特殊样说明录入" : "特殊样说明查看",
        buttons: ObjButton
    });
}
function clearRemarkDialogValue() {
    $("#SEA_REMAKR").val("");
}

//设置grid 的弹出特殊样说明录入对话框
var SampleRemarkWinSrh = null;
function showSampleRemarkSrh(strSubTaskId, oldRemark, isAdd) {
    //创建表单结构

    var sampleRemarkform = $("#SampleRemarkForm");
    sampleRemarkform.ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: "前处理说明", name: "SEA_SAMPLEREMARK", newline: true, type: "textarea" }
                    ]
    });
    $("#SEA_SAMPLEREMARK").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:400px");

    $("#SEA_SAMPLEREMARK").val(oldRemark);
    var ObjButton = [];
    if (!isAdd) {
        $("#SEA_SAMPLEREMARK").attr("disabled", true);
        ObjButton = [
                  { text: '返回', onclick: function () { clearSampleRemarkDialogValue(); SampleRemarkWinSrh.hide(); } }
                  ];
    } else {
        $("#SEA_SAMPLEREMARK").attr("disabled", false);
        ObjButton = [
                  { text: '确定', onclick: function () { SaveSampleRemark(strSubTaskId); } },
                  { text: '返回', onclick: function () { clearSampleRemarkDialogValue(); SampleRemarkWinSrh.hide(); } }
                  ];
    }
    SampleRemarkWinSrh = $.ligerDialog.open({
        target: $("#detailSampleRemark"),
        width: 660, height: 170, top: 90, title: isAdd ? "前处理说明录入" : "前处理说明查看",
        buttons: ObjButton
    });
}
function clearSampleRemarkDialogValue() {
    $("#SEA_SAMPLEREMARK").val("");
}

function SaveSampleRemark(strSubTaskId) {
    var strRemark = $("#SEA_SAMPLEREMARK").val();
    $.ajax({
        cache: false,
        type: "POST",
        url: "AnalysisResult.aspx/SaveRemark",
        data: "{'strValue':'" + strSubTaskId + "','strSampleRemark':'" + strRemark + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "true") {
                objTwoGrid.loadData();
                clearSampleRemarkDialogValue();
                SampleRemarkWinSrh.hide();
                $.ligerDialog.success('数据操作成功')
            }
            else {
                $.ligerDialog.warn('数据操作失败');
            }
        }
    });
}

function getSubSample(SampleId) {
    var objItems = null;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../sampling/QY/SubSample.aspx?action=GetSubSampleList&strSampleId=" + SampleId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != "0") {
                objItems = data.Rows;
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax请求数据失败！');
        }
    });
    return objItems;
}

function ShowSubSample(strId) {
    $.ligerDialog.open({ title: '子样明细', name: 'winaddtor', width: 700, height: 400, top: 90, url: '../../sampling/QY/SubSample.aspx?strView=true&strSampleId=' + strId, buttons: [
                { text: '返回', onclick: function (item, dialog) { dialog.close() } }
            ]
    });
}
//获取监测项目信息
function getItemInfor(strItemID) {
    var strValue = null;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../sampling/QY/SamplePoint.aspx?type=getBaseItemInfor&strItemId=" + strItemID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.Total != '0') {
                strValue = data.Rows;
            }
        }
    });
    return strValue;
}

function SetTable() {

    var selectedSample = objTwoGrid.getSelectedRow();
    if (!selectedSample) {
        $.ligerDialog.warn('请先选择样品！');
        return;
    }
    var selectedItem = objThreeGrid.getSelectedRow();
    if (!selectedItem) {
        $.ligerDialog.warn('请先选择监测项目！');
        return;
    }
    //结果类型，Poll：污染源 Air：大气
    var strPA = selectedItem.REMARK_5;
    var ItemInfor = getItemInfor(selectedItem.ITEM_ID);
    var strCataLogName = ItemInfor[0].ORI_CATALOG_TABLEID;
    //获取监测项目的监测类型 废气：000000002
    var strMONITORID = ItemInfor[0].MONITOR_ID;
    var strTitle = "原始记录表", strPageUrl = "", strKeyTableName = "", strBaseTableName = "";
    if (strCataLogName != "" || strMONITORID == "000000002") {
        if (strPA != "Air") {
            //固定污染源原始记录表
            switch (strCataLogName) {
                //烟尘类的 使用该表作为原始记录主表             
                case "T_MIS_MONITOR_DUSTATTRIBUTE":
                    if (ItemInfor[0].ITEM_NAME.indexOf("油烟") != -1) {
                        strTitle = "饮食业油烟分析原始记录表";
                        strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_YY.aspx";
                    }
                    else {
                        strTitle = "固定污染源排气中颗粒物采样分析原始记录表";
                        strPageUrl = "../../sampling/QY/OriginalTable/DustyTable.aspx";
                    }
                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE";
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                    break;
                //除了标明原始记录表名的监测项目外其他废气的监测项目也使用该表作为原始记录主表  
                case "":
                    strTitle = "污染源采样原始记录表";
                    strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_PM.aspx";
                    strKeyTableName = "";
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                    break;
                //PM的和总悬浮物项目类的 使用该表作为原始记录主表              
                case "T_MIS_MONITOR_DUSTATTRIBUTE_PM":
                    strTitle = "污染源采样原始记录表";
                    strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_PM.aspx";
                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_PM";
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                    break;
                //SO2和NOX类的 使用该表作为原始记录主表              
                case "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX":
                    strTitle = "固定污染源排气中气态污染物采样分析原始记录表";
                    strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_So2OrNox.aspx";
                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX";
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                    break;
                default:
                    break;
            }
        }
        else {
            strTitle = "大气采样原始记录表";
            strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_Air.aspx";
            strKeyTableName = strCataLogName;
            strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
            OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
        }
    } else {
        return;
    }

    //    $.ligerDialog.open({ title: getItemInfoName(strItemId, "ITEM_NAME") + '样品原始记录表', top: 0, width: 1100, height: 680, buttons:
    //         [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
    //         }], url: '../../sampling/QY/OriginalTable/DustyTable.aspx?strSubTask_Id=' + strSubId + '&strIsView=true&strItem_Id=' + strItemId
    //    });
}
//strLinkCode环节编号，01：采样环节；02：监测分析环节；03：分析结果复核环节；04：质控审核环节；05：现场项目结果核录环节；06：分析主任审核环节；07：现场结果复核环节；08：现场室主任审核环节
function OpenDialog(Title, PageUrl, KeyTable, BaseTable, ItemID, SubTaskID) {
    $.ligerDialog.open({ Title: Title, top: 0, width: 1100, height: 680, buttons:
         [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: PageUrl + '?strSubTask_Id=' + SubTaskID + '&strIsView=true&strItem_Id=' + ItemID + '&strKeyTableName=' + KeyTable + '&strBaseTableName=' + BaseTable + '&strLinkCode=04'
    });
}
//function SetTable(strSubId, strItemId) {
//    $.ligerDialog.open({ title: getItemInfoName(strItemId, "ITEM_NAME") + '样品原始记录表', top: 0, width: 1100, height: 680, buttons:
//         [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
//         }], url: '../../sampling/QY/OriginalTable/DustyTable.aspx?strSubTask_Id=' + strSubId + '&strIsView=true&strItem_Id=' + strItemId
//    });
//}
//查看质控信息
function QcInfo() {
    if (objThreeGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('请选择一个项目进行查看');
        return;
    }
    var strResultID = objThreeGrid.getSelectedRow().ID;
    var strItemID = objThreeGrid.getSelectedRow().ITEM_ID;
    $.ligerDialog.open({ title: "质控信息查看", width: 780, height: 530, isHidden: false, buttons:
        [
        { text:
        '返回', onclick: function (item, dialog) { dialog.close(); }
        }], url: "QcInfo.aspx?strSubTaskID=" + strSubTaskID + "&strResultID=" + strResultID + "&strItemID=" + strItemID + "&strMark=0"
    });
}