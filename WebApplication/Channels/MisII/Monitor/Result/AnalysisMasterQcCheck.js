﻿// Create by 魏林 2015.01.21  "分析室主任审核"功能

var objSampleGrid = null;
var objItemGrid = null;
var objQCGrid1 = null;
var objQCGrid2 = null;
var objResultID = "";
var a = "", b = "";
var strUrl = "AnalysisMasterQcCheck.aspx";

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

function Save() {
    return true;
}

//样品信息
$(document).ready(function () {
    objResultID = $("#RESULT_ID").val();

    objSampleGrid = $("#SampleGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: $(window).height() - 30,
        columns: [
        //{ display: '样品条码', name: 'SAMPLE_BARCODE', align: 'left', width: 100, minWidth: 60 },
                {display: '样品编号', name: 'SAMPLE_CODE', align: 'left', width: 120, minWidth: 60 }
        //{ display: '待审核任务数', name: 'SAMPLE_COUNT', align: 'left', width: 80, minWidth: 60 },
        //{ display: '全程质控', name: 'IsQC', align: 'left', width: 100, minWidth: 60 }
                ],
        toolbar: { items: [
                 { text: '查看时限要求', click: ChkTimeMgm, icon: 'add' }
                ]
        },


        url: strUrl + '?type=getSampleGridInfo&strResultID=' + objResultID,
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
            a = rowdata.ID;
            objItemGrid.set('url', strUrl + "?type=getItemGridInfo&SampleGridId=" + rowdata.ID + "&strResultID=" + objResultID);
            //objQCGrid1.set("data", emptyArray);
            objQCGrid2.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");


    //by yinchengyi 2015-9-21 查看时限要求
    function ChkTimeMgm() {

        var selectedSample = objSampleGrid.getSelectedRow();
        if (!selectedSample) {
            $.ligerDialog.warn('请先选择样品！');
            return;
        }

        $.ligerDialog.open({ title: "时限管理", width: 800, height: 300, buttons:
                [
                { text:
                '关闭', onclick: function (item, dialog) { dialog.close(); }
                }], url: "../../ProcessMgm/ProcessMgm.aspx?strSampleID=" + selectedSample.ID
        });
    }
});
//监测项目信息
$(document).ready(function () {
    objItemGrid = $("#ItemGrid").ligerGrid({
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: 300,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     return strItemName;
                 }
                 },
        //                 { display: '样品记录单', name: 'DATAINFO', align: 'left', width: 140, minWidth: 40,
        //                     render: function (record, rowindex, value) {
        //                         return "<a href=\"javascript:EditOriginalInfo()\">查看</a> ";
        //                     }
        //                 },
                 {display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60, render: function (items) {
                     return items.ITEM_RESULT;
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
                 { display: '实际完成时间', name: 'FINISH_DATE', align: 'left', width: 100, minWidth: 60 },
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
                //{ text: '获取仪器数据', icon: 'attibutes' },
                //{ text: '查看仪器结果', icon: 'add' },
                //{ text: '上传附件', click: Upload, icon: 'attibutes' },
                { text: '下载附件', click: DownLoad, icon: 'attibutes' }
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

            //点击的时候加载质控信息
            //objQCGrid1.set('url', strUrl + "?type=getQCGrid1Info&ResultId=" + rowdata.ID + "&strItemID=" + rowdata.ITEM_ID);
            //点击的时候加载实验室质控信息
            b = rowdata.ID;
            objQCGrid2.set('url', strUrl + "?type=getQCGrid2Info&ResultId=" + rowdata.ID);
            
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

});

function Upload() {
    var selectedItemSample = objSampleGrid.getSelectedRow();
    if (!selectedItemSample) {
        $.ligerDialog.warn('请先选择样品编号！');
        return;
    }
    var selectedItem = objItemGrid.getSelectedRow();
    if (!selectedItem) {
        $.ligerDialog.warn('请先选择监测项目！');
        return;
    }
    var filetype = a; //任务ID
    var Doc_ID = getItemId(b); //项目ID
    //alert(filetype + "|" + Doc_ID);
    $.ligerDialog.open({ title: '附件上传', width: 500, height: 250, isHidden: false,
        buttons: [
             { text: '上传', onclick: function (item, dialog) {
                 dialog.frame.upLoadFile();
                 alert("上传成功");
                 //GetData(filetype);
                 dialog.close();
             }
             },
                { text: '关闭',
                    onclick: function (item, dialog) { dialog.close(); }
                }], url: '../../../OA/ATT/AttFileUpload.aspx?id=' + Doc_ID + '&filetype=' + filetype
    });
}


function DownLoad() {
    var selectedItemSample = objSampleGrid.getSelectedRow();
    if (!selectedItemSample) {
        $.ligerDialog.warn('请先选择监测项目！');
        return;
    }
    var selectedItem = objItemGrid.getSelectedRow();
    if (!selectedItem) {
        $.ligerDialog.warn('请先选择监测项目！');
        return;
    }
    var filetype = a; //任务ID
    var Doc_ID = getItemId(b); //项目ID
    //alert(filetype + "|" + Doc_ID);
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?id=' + Doc_ID + '&filetype=' + filetype
    });
}




//质控信息
//$(document).ready(function () {
//    
//    objQCGrid1 = $("#QCGrid1").ligerGrid({
//        title: "现场质控",
//        dataAction: 'server',
//        usePager: false,
//        pageSize: 10,
//        alternatingRow: false,
//        checkbox: true,
//        onRClickToSelect: true,
//        sortName: "ID",
//        width: '100%',
//        pageSizeOptions: [5, 10, 15, 20],
//        height: '60%',
//        columns: [
//                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 100, minWidth: 60, render: function (record) {
//                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
//                     return strItemName;
//                 }
//                 },
//                 { display: '样品条码', name: 'SAMPLE_BARCODE', align: 'left', width: 120, minWidth: 60 },
//                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
//                     return getQcName(record.QC_TYPE);
//                 }
//                 },
//                 { display: '质控结果', name: 'REMARK', align: 'left', width: 320, minWidth: 60 },
//                  { display: '是否合格', name: 'IS_OK', align: 'left', width: 50, minWidth: 60, render: function (record) {
//                      if (record.IS_OK == "0")
//                          return "<span style='color:red'>否</span>";
//                      else if (record.IS_OK == "1")
//                          return "是";
//                      else
//                          return "";
//                  }
//                  }
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
//            objQCGrid2.set('url', strUrl + "?type=getQCGrid2Info&QCGridId=" + rowdata.ID);
//        },
//        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
//    });
//    
//    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
//});
//实验室质控信息
$(document).ready(function () {
    objQCGrid2 = $("#QCGrid2").ligerGrid({
        title: "实验质控",
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '40%',
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

function EditOriginalInfo() {

    //window.open("AnalysisOriginalInfo2.aspx?ccflowWorkId=" + $.getUrlVar('WorkID') + "&ccflowFid=" + $.getUrlVar('FID'));
    window.open("AnalysisOriginalInfo2.aspx?ccflowWorkId=" + $.getUrlVar('WorkID') + "&ccflowFid=" + $.getUrlVar('FID') + "&ccflowUserNo=" + $.getUrlVar('UserNo'));
}


function getTaskID(strSubtask) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getTaskID",
        data: "{'strSubtask':'" + strSubtask + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

function getItemId(strSubtask) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getItemId",
        data: "{'strSubtask':'" + strSubtask + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}


