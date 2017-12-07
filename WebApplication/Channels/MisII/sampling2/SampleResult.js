// Create by 苏成斌 2012.12.13  "采样-现场项目结果填报"功能
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var objOneFrom = null;
var objTwoGrid = null;
var strUrl = "SampleResult.aspx";
var strSubtaskID = "", strMonitor_ID = "";
var strCCflowWorkId = '';
var strViewRemarkName = "备 注";
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
    strMonitor_ID = $.getUrlVar('strMonitor_ID');
    strCCflowWorkId = $.getUrlVar('ccflowWorkId');

    strViewRemarkName = strMonitor_ID == '000000004' ? "主要声源" : "备 注";
    objOneGrid = $("#oneGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        height: '96%',
        pageSizeOptions: [5, 10, 15, 20],
        url: strUrl + '?type=getOneGridInfo&strSubtaskID=' + strSubtaskID+ '&strCCflowWorkId=' + strCCflowWorkId,
        columns: [
                    { display: '监测点位', name: 'POINT_NAME', align: 'left', width: 150, minWidth: 60 },
                    { display: '样品名称', name: 'REMARK2', align: 'left', width: 100, minWidth: 60 },
                    { display: '样品编号', name: 'REMARK1', align: 'left', width: 180, minWidth: 60 }
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
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&OneGridId=" + rowdata.ID + "&strSubtaskID=" + strSubtaskID);
        },
        onAfterEdit: SampleEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    function SampleEdit(e) {
        var id = e.record.ID;
        var strSubtaskID = e.record.SUBTASK_ID;
        var strSampleCode = "";
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
                    { display: '结果值<a style="color:red;padding-left:5px;">*</a>', name: 'ITEM_RESULT', align: 'left', width: 120, minWidth: 60,
                        editor: {
                            type: 'text'
                        }
                    },
                    { display: '单位', name: 'ITEM_ID', align: 'left', width: 90, minWidth: 60, render: function (record) {
                        return getItemUnit(record.ITEM_ID);
                    }
                    },
                     { display: strViewRemarkName, name: 'REMARK_2', align: 'center', width: 80, render: function (items) {
                         if (items.REMARK_2 != "") {
                             return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.REMARK_2 + "\",false)'>查看</a>";
                         }
                     }
                     },
                     { display: '结果校核人', name: 'USER_DEFAULT', align: 'left', width: 100, minWidth: 60,
                         render: function (record, rowindex, value) {
                             if (record.IS_SAMPLEDEPT != "是") {
                                 var objUserArray = getAjaxUserName(record.ID);
                                 var strUserName = "";
                                 if (objUserArray.length > 0)
                                     strUserName = objUserArray[0]["UserName"];
                                 else
                                     strUserName = "请选择";
                                 return "<a href=\"javascript:getDefaultUserName('" + record.ID + "')\">" + strUserName + "</a> ";
                             }
                         }
                     },
                    { display: '分析协同人', name: 'USER_DEFAULT_EX', align: 'left', width: 150, minWidth: 60,
                        render: function (record, rowindex, value) {
                            if (record.IS_SAMPLEDEPT != "是") {
                                var objUserArray = getAjaxUseExName(record.ID);
                                var strUserName = "";
                                if (objUserArray.length > 0)
                                    strUserName = objUserArray[0]["UserName"];
                                else
                                    strUserName = "请选择";
                                return "<a href=\"javascript:getDefaultUserExName('" + record.ID + "')\">" + strUserName + "</a> ";
                            }
                        }
                    }
                ],
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        toolbar: { items: [
                { text: strViewRemarkName, click: ResultRemark, icon: 'bluebook' }
                ]
        },
        onAfterEdit: AfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");


    //特殊样品说明
    function ResultRemark() {
        if (objTwoGrid.getSelectedRow() == null) {
            parent.$.ligerDialog.warn('请选择一条记录进行操作!');
            return;
        }
        else {
            var strValue = objTwoGrid.getSelectedRow().ID;
            var oldRemark = objTwoGrid.getSelectedRow().REMARK_2;
            showDetailRemarkSrh(strValue, oldRemark, true);
        }
    }
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


//设置grid 的弹出特殊样说明录入对话框
var detailRemarkWinSrh = null;
function showDetailRemarkSrh(strResultId, oldRemark, isAdd) {
    //创建表单结构

    var mainRemarkform = $("#RemarkForm");
    mainRemarkform.ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: " 备 注", name: "SEA_REMARK", newline: true, type: "textarea" }
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
                  { text: '确定', onclick: function () { SaveRemark(strResultId); } },
                  { text: '返回', onclick: function () { clearRemarkDialogValue(); detailRemarkWinSrh.hide(); } }
                  ];
    }
    detailRemarkWinSrh = $.ligerDialog.open({
        target: $("#detailRemark"),
        width: 660, height: 170, top: 90, title: isAdd ? strViewRemarkName + "录入" : strViewRemarkName + "查看",
        buttons: ObjButton
    });
}
function clearRemarkDialogValue() {
    $("#SEA_REMAKR").val("");
}

function SaveRemark(strResultId) {
    var strRemark = $("#SEA_REMARK").val();
    $.ajax({
        cache: false,
        type: "POST",
        url: "SampleResult.aspx/SaveRemark",
        data: "{'strValue':'" + strResultId + "','strRemark':'" + strRemark + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == true) {
                objTwoGrid.loadData();
                clearRemarkDialogValue();
                detailRemarkWinSrh.hide();
                parent.$.ligerDialog.success('数据操作成功')
            }
            else {
                parent.$.ligerDialog.warn('数据操作失败');
            }
        }
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

//弹出选择默认负责人
function getDefaultUserName(strResultId) {
    $.ligerDialog.open({ title: "选择分析负责人", width: 400, height: 250, top: 20, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {


            //if ($("iframe:last")[0].contentWindow.UserSave()) {
            if (dialog.frame.UserSave()) {


                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objTwoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../Mis/Monitor/Result/DefaultUser.aspx?strResultId=" + strResultId
    });
}
//弹出选择默认协同人
function getDefaultUserExName(strResultId) {
    $.ligerDialog.open({ title: "选择分析协同人", width: 400, height: 250, top: 20, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            //if ($("iframe")[0].contentWindow.UserSave()) {

            if (dialog.frame.UserSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objTwoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../Mis/Monitor/Result/DefaultUserEx.aspx?strResultId=" + strResultId
    });
}