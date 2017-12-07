// Create by 苏成斌 2012.12.14  "监测任务点位信息管理"功能

var objPointGrid = null;
var strPointId = "";
var strSubtaskID = "";
var strUrl = "SamplePoint.aspx"
var objSubItems = null;
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

    //监测点位grid的菜单
    var objPointmenu = $.ligerMenu({ width: 120, items:
            [
            { text: '修改', click: updateData, icon: 'modify' },
            { line: true },
            { text: '删除', click: deleteData, icon: 'delete' }
            ]
    });

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
        height: '96%',
        enabledEdit: true,
        url: 'SamplePoint.aspx?type=getPoint&strSubtaskID=' + strSubtaskID,
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
                { display: '样品编号', name: 'SAMPLE_CODE', align: 'left', isSort: false, width: 150 },
                { display: '原样编号', name: 'QC_SOURCE_CODE', align: 'left', isSort: false, width: 100, render: function (record) {
                    return getQcSourceCode(record.QC_SOURCE_ID);
                }
                },
                { display: '质控类型', name: 'QC_TYPE', align: 'left', isSort: false, width: 100, render: function (record) {
                    return GetQcType(record.QC_TYPE);
                }
                },
               { display: '特殊样说明', name: 'RemarkView', align: 'center', width: 100, render: function (items) {
                   if (items.SPECIALREMARK != "") {
                       return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                   }
               }
               },
            { display: '子样', name: 'SUBSAMPLE', align: 'center', width: 100, render: function (items) {
                if (getSubSample(items.ID) != null) {
                    return "<a href='javascript:ShowSubSample(\"" + items.ID + "\")'>明细</a>";
                }
            }
            }
                ],
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { text: '停产', click: discontinued, icon: 'gridwarning' },
                { line: true },
                { text: '附件上传', click: upLoadFile, icon: 'fileup' },
                { line: true },
                { text: '附件下载', click: downLoadFile, icon: 'filedown' },
                { line: true },
                { text: '特殊样说明', click: SpecialSampleRemark, icon: 'bluebook' },
                { line: true },
                { text: '添加子样', click: AddSubSample, icon: 'database_wrench' }
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
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            strPointId = data.POINT_ID;
            updateData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            strPointId = rowdata.POINT_ID;
            var strSampleID = rowdata.ID;
            ItemGrid.set('url', "SamplePoint.aspx?type=GetItems&selPointID=" + strPointId + "&strSubtaskID=" + strSubtaskID + "&strSampleID=" + strSampleID);
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

    //增加数据
    function createData() {
        parent.$.ligerDialog.open({ title: '点位信息增加', top: 0, width: 780, height: 520, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'SamplePointEdit.aspx?strSubtaskID=' + strSubtaskID
        });
    }
    //修改数据
    function updateData() {
        if (strPointId == "") {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        parent.$.ligerDialog.open({ title: '点位信息编辑', top: 0, width: 780, height: 520, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'SamplePointEdit.aspx?strid=' + strPointId + '&strSubtaskID=' + strSubtaskID
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
            }], url: '../../../OA/ATT/AttFileUpload.aspx?filetype=SubTask&id=' + strSubtaskID
        });
    }
    ///附件下载
    function downLoadFile() {
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=SubTask&id=' + strSubtaskID
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

    function getSubSample(SampleId) {
        var objItems = null;
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "SubSample.aspx?action=GetSubSampleList&strSampleId=" + SampleId,
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
    //save函数
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "SamplePoint.aspx/SaveData",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    parent.$.ligerDialog.success('数据保存成功');
                    dialog.close();
                    objPointGrid.loadData();
                }
                else {
                    parent.$.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }

    //删除数据
    function deleteData() {
        if (objPointGrid.getSelectedRow() == null) {
            parent.$.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        parent.$.ligerDialog.confirm("确认删除点位信息吗？", function (yes) {
            if (yes == true) {
                var strValue = objPointGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "SamplePoint.aspx/deletePoint",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objPointGrid.loadData();
                            parent.$.ligerDialog.success('删除数据成功')
                        }
                        else {
                            parent.$.ligerDialog.warn('删除数据失败');
                        }
                    }
                });
            }
        });
    }


    function discontinued() {
        if (objPointGrid.getSelectedRow() == null) {
            parent.$.ligerDialog.warn('请选择一条记录');
            return;
        }
        parent.$.ligerDialog.confirm("确定所选点位停产吗？", function (yes) {
            if (yes == true) {
                var strValue = objPointGrid.getSelectedRow().ID;
                showDetailSrh(strValue);
            }
        });
    }

    function DelPlanStopPoint(strSubTaskId) {

        $.ajax({
            cache: false,
            type: "POST",
            url: "SamplePoint.aspx/discontinued",
            data: "{'strValue':'" + strSubTaskId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    objPointGrid.loadData();
                    parent.$.ligerDialog.success('停产成功')
                }
                else if (data.d == "2") {
                    var tabid = "tabidSamplingListNew"
                    var surl = "SamplingList.aspx";
                    top.f_addTab(tabid, '采样任务列表', surl);
                }
                else {
                    parent.$.ligerDialog.warn('停产失败');
                }
            }
        });
    }

    //设置grid 的弹出停产原因对话框
    var detailWinSrh = null;
    function showDetailSrh(strSubTaskId) {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构

            var mainform = $("#InputForm");
            mainform.ligerForm({
                inputWidth: 430, labelWidth: 90, space: 40,
                fields: [
                     { display: "停产原因", name: "SEA_REASON", newline: true, type: "textarea" }
                    ]
            });
            $("#SEA_REASON").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:400px");
            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 170, top: 90, title: "停产原因录入",
                buttons: [
                  { text: '确定', onclick: function () { SaveStopReason(strSubTaskId); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }
    function SaveStopReason(strSubTaskId) {
        var strReason = $("#SEA_REASON").val();
        $.ajax({
            cache: false,
            type: "POST",
            url: "SamplePoint.aspx/SaveStopReason",
            data: "{'strValue':'" + strSubTaskId + "','strReason':'" + strReason + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "true") {
                    clearSearchDialogValue();
                    detailWinSrh.hide();
                    DelPlanStopPoint(strSubTaskId);
                }
                else {
                    return;
                }
            }
        });
    }
    function clearSearchDialogValue() {
        $("#SEA_REASON").val("");
    }



    //获取字典项信息
    function getDictName(strDictCode, strDictType) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "SamplePoint.aspx/getDictName",
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
            url: "SamplePoint.aspx/getMonitorName",
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

//获取原样样品编号
function getQcSourceCode(sample_id) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePoint.aspx/getQcSourceCode",
        data: "{'strValue':'" + sample_id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取企业信息
function getCompanyName(strCompanyID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePoint.aspx/getCompanyName",
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
        url: "SamplePoint.aspx/getPointName",
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
        url: "SamplePoint.aspx/GetQcType",
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
        url: "SamplePoint.aspx/SaveRemark",
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

function AddSubSample() {
    if (objPointGrid.getSelectedRow() == null) {
        parent.$.ligerDialog.warn('请选择一条记录进行操作!');
        return;
    }

    else {
        var SelectedRow = objPointGrid.getSelectedRow();
        $.ligerDialog.open({ title: '新增子样', name: 'winaddtor', width: 700, height: 400, top: 0, url: 'SubSample.aspx?strSampleId=' + SelectedRow.ID + '&strSampleCode=' + SelectedRow.SAMPLE_CODE, buttons: [
                { text: '关闭', onclick: function (item, dialog) { objPointGrid.loadData(); dialog.close() } }
            ]
        });
    }
}


function ShowSubSample(strId) {
    $.ligerDialog.open({ title: '子样明细', name: 'winaddtor', width: 700, height: 400, top: 0, url: 'SubSample.aspx?strView=true&strSampleId=' + strId, buttons: [
                { text: '返回', onclick: function (item, dialog) { objPointGrid.loadData(); dialog.close() } }
            ]
    });
}