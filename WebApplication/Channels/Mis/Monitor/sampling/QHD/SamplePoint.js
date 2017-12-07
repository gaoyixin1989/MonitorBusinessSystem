// Create by 苏成斌 2012.12.14  "监测任务点位信息管理"功能

var objPointGrid = null;
var strPointId = "";
var strTaskID = "";
var strSubtaskID = "";
var strMonitorID = "";
var strUrl = "SamplePoint.aspx"

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

    strTaskID = $.getUrlVar('strTaskID');
    strSubtaskID = $.getUrlVar('strSubtaskID');
    strMonitorID = $.getUrlVar('strMonitorID');

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
        rownumbers: true,
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        enabledEdit: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 8, 10],
        height: '96%',
        url: 'SamplePoint.aspx?type=getPoint&strSubtaskID=' + strSubtaskID,
        columns: [
                 { display: '监测点位', name: 'POINT_ID', align: 'left', isSort: false, width: 120, render: function (record) {
                     return getPointName(record.POINT_ID);
                 }
                 },
                 { display: '样品名称(监测频次)', name: 'SAMPLE_NAME', align: 'left', isSort: false, width: 238,
                     editor: {
                         type: 'text'
                     }
                 },
                 { display: '监测时间', name: 'SAMPLE_REMARK', align: 'left', isSort: false, width: 120,
                     editor: {
                         type: 'text'
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
                { text: '方案下载', click: downLoadScheme, icon: 'filedown' },
                { line: true },
                { text: '样品交接表', click: downSampleHandover, icon: 'filedown' }
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
            //for (var rowid in this.records)
            //    this.unselect(rowid);
            //this.select(rowindex);

            strPointId = rowdata.POINT_ID;
            var strSampleID = rowdata.ID;
            ItemGrid.set('url', "SamplePoint.aspx?type=GetItems&selPointID=" + strPointId + "&strSubtaskID=" + strSubtaskID + "&strSampleID=" + strSampleID);
        },
        onAfterEdit: SampleEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return true; }
    });
    //$(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    $(".l-layout-header-toggle").css("display", "none");

    function SampleEdit(e) {
        var id = e.record.ID;
        var strSubtaskID = e.record.SUBTASK_ID;
        var strSampleCode = "";
        var strSampleName = e.record.SAMPLE_NAME
        var strSampleRemark = e.record.SAMPLE_REMARK
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/saveSample",
            data: "{'id':'" + id + "','strSubtaskID':'" + strSubtaskID + "','strSampleCode':'" + strSampleCode + "','strSampleName':'" + strSampleName + "','strSampleRemark':'" + strSampleRemark + "'}",
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
        if (objPointGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('附件上传之前请先选择监测点位');
            return;
        }
        $.ligerDialog.open({ title: '附件上传', width: 500, height: 270, top: 50,
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
            }], url: '../../../../OA/ATT/AttFileUpload.aspx?filetype=SamplingPointAttachment&id=' + objPointGrid.getSelectedRow().POINT_ID
        });
    }
    ///附件下载
    function downLoadFile() {
        if (objPointGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('附件下载之前请先选择监测点位');
            return;
        }
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270, top: 50,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../../OA/ATT/AttFileDownLoad.aspx?filetype=SamplingPointAttachment&id=' + objPointGrid.getSelectedRow().POINT_ID
        });
    }
    ///方案下载
    function downLoadScheme() {
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270, top: 50,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../../OA/ATT/AttFileDownLoad.aspx?filetype=Scheme&id=' + strTaskID
        });
    }

    //导出样品交接表
    function downSampleHandover() {
        $.ligerDialog.confirm('您确定要导出样品交接单？', function (yes) {
            if (yes == true) {
                $("#cphData_strPrintId").val(strTaskID);
                $("#cphData_btnImport").click();
            }
        })
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
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "SamplePoint.aspx/discontinued",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objPointGrid.loadData();
                            parent.$.ligerDialog.success('停产成功')
                        }
                        else if (data.d == "2") {
                            var tabid = "tabidSamplingListNew"
                            var surl = "../SamplingList.aspx";
                            top.f_addTab(tabid, '采样任务列表', surl);
                        }
                        else {
                            parent.$.ligerDialog.warn('停产失败');
                        }
                    }
                });
            }
        });
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