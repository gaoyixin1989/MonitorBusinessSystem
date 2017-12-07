// Create by 苏成斌 2012.12.14  "监测任务点位信息管理"功能

var objPointGrid = null;
var strPointId = "";
var strSubtaskID = "";
var strUrl = "SamplePointView.aspx";

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
//点位信息管理功能
$(document).ready(function () {
    $("#layout1").ligerLayout({ leftWidth: 670, height: '100%', topHeight: 200 });

    strSubtaskID = $.getUrlVar('strSubtaskID');

    objPointGrid = $("#PointGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        enabledEdit: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 8, 10],
        height: '95%',
        enabledEdit: true,
        url: strUrl + '?type=getPoint&strSubtaskID=' + strSubtaskID,
        columns: [
                 { display: '监测点位', name: 'POINT_ID', align: 'left', isSort: false, width: 120, render: function (record) {
                     return getPointName(record.POINT_ID);
                 }
                 },
                 { display: '样品名称', name: 'SAMPLE_NAME', align: 'left', isSort: false, width: 150,
                     editor: {
                         type: 'text'
                     }
                 },
                { display: '样品编号', name: 'SAMPLE_CODE', align: 'left', isSort: false, width: 100 },
                { display: '质控类型', name: 'QC_TYPE', align: 'left', isSort: false, width: 100, render: function (record) {
                    return GetQcType(record.QC_TYPE);
                }
                },
               { display: '特殊样说明', name: 'RemarkView', align: 'center', width: 100, render: function (items) {
                   return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
               }
               }
                ],
        toolbar: { items: [
                { text: '附件上传', click: upLoadFile, icon: 'fileup' },
                { line: true },
                { text: '附件下载', click: downLoadFile, icon: 'filedown' },
                { line: true },
                { text: '特殊样说明', click: SpecialSampleRemark, icon: 'bluebook' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            strPointId = parm.data.POINT_ID;
            objPointmenu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            strPointId = rowdata.POINT_ID;
            var strSampleID = rowdata.ID;
            ItemGrid.set('url', strUrl + "?type=GetItems&selPointID=" + strPointId + "&strSubtaskID=" + strSubtaskID + "&strSampleID=" + strSampleID);
        },
        onAfterEdit: SampleEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    $(".l-layout-header-toggle").css("display", "none");

    function SampleEdit(e) {
        var id = e.record.ID;
        var strSubtaskID = e.record.SUBTASK_ID;
        var strSampleCode = "";
        var strSampleName = e.record.SAMPLE_NAME
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
                    objPointGrid.cancelEdit(e.rowindex);
                }
            }
        });
    }

    ///附件上传
    function upLoadFile() {
        $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
            buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        $("iframe")[0].contentWindow.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../../OA/ATT/AttFileUpload.aspx?filetype=SubTask&id=' + strSubtaskID
        });
    }
    ///附件下载
    function downLoadFile() {
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../../OA/ATT/AttFileDownLoad.aspx?filetype=SubTask&id=' + strSubtaskID
        });
    }



    //特殊样品说明
    function SpecialSampleRemark() {
        if (objPointGrid.getSelectedRow() == null) {
            parent.$.ligerDialog.warn('请选择一条记录进行操作!');
            return;
        }
        else {
            var strValue = objPointGrid.getSelectedRow().ID;
            var oldRemark = objPointGrid.getSelectedRow().SPECIALREMARK;
            showDetailRemarkSrh(strValue, oldRemark, true);
        }
    }

    //获取字典项信息
    function getDictName(strDictCode, strDictType) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/getDictName",
            data: "{'strDictCode':'" + strDictCode + "','strDictType':'" + strDictType + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
    //获取监测类别信息
    function getMonitorName(strMonitorID) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/getMonitorName",
            data: "{'strValue':'" + strMonitorID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
});

//获取企业信息
function getCompanyName(strCompanyID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getCompanyName",
        data: "{'strValue':'" + strCompanyID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取排口信息
function getPointName(strPointID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getPointName",
        data: "{'strValue':'" + strPointID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取质控类型
function GetQcType(qc_type) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/GetQcType",
        data: "{'strValue':'" + qc_type + "'}",
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

function SaveRemark(strSubTaskId) {
    var strRemark = $("#SEA_REMARK").val();
    $.ajax({
        cache: false,
        type: "POST",
        url: strUrl + "/SaveRemark",
        data: "{'strValue':'" + strSubTaskId + "','strRemark':'" + strRemark + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "true") {
                objPointGrid.loadData();
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