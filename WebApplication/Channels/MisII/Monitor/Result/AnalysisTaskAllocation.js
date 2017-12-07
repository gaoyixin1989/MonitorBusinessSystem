// Create by 魏林 2014.01.20  "分析任务分配"功能
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

var objSampleGrid = null;
var objItemGrid = null;

var strUrl = "AnalysisTaskAllocation.aspx";

var strSampleGridTitle = "样品信息列表";
var strItemGridTitle = "监测项目信息";

var strSampleID = "";
var objSubTaskID = "";
var objTaskID = "";
var strCCflowWorkId = '';
function Save() {

    return true;
}

//样品号管理
$(document).ready(function () {
    objSubTaskID = $("#cphData_SUBTASK_ID").val();
    objTaskID = $("#cphData_TASK_ID").val();

    strCCflowWorkId = $.getUrlVar('WorkID');


    objSampleGrid = $("#SampleGrid").ligerGrid({
        dataAction: 'server',
        usePager: false,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        height: '100%',
        enabledEdit: false,
        columns: [
                    //{ display: '任务编号', name: 'TICKET_NUM', align: 'left', width: 180, minWidth: 60 },
                    { display: '样品编号', name: 'SAMPLE_CODE', align: 'left', width: 150, minWidth: 60 },
                    { display: '分析任务数', name: 'ANAYSIS_COUNT', align: 'left', width: 100, minWidth: 60 }
                ],
                toolbar: { items: [
                { text: '查看时限要求', click: ChkTimeMgm, icon: 'add' }
                ]
                },

                url: strUrl + '?type=getSampleGridInfo&strSubTaskID=' + objSubTaskID + '&strTaskID=' + objTaskID + '&strCCflowWorkId=' + strCCflowWorkId,
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
            strSampleID = rowdata.ID;
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            strSampleID = rowdata.ID;
            //点击的时候加载监测项目信息
            objItemGrid.set('url', strUrl + "?type=getItemGridInfo&SampleID=" + rowdata.ID   );
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
 
    });

    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    function ChkTimeMgm() {

        if (objSampleGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('导出之前请先选择任务信息');
            return;
        }

            $.ligerDialog.open({ title: "时限管理", width: 800, height: 300, buttons:
                [
                { text:
                '关闭', onclick: function (item, dialog) { dialog.close(); }
                }], url: "../../ProcessMgm/ProcessMgm.aspx?strPlanId=" + $("#PLAN_ID").val()
            });
    }
});

//监测项目信息
$(document).ready(function () {
    
    objItemGrid = $("#ItemGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [5, 8, 10],
        height: '100%',
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
                            return "<a href=\"javascript:getAskingDate('" + strSampleID + "','" + record.ID + "')\">" + strAskingDateTemp + "</a> ";
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
    $.ligerDialog.open({ title: "选择分析负责人", width: 400, height: 250, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.UserSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objItemGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../../Mis/Monitor/Result/DefaultUser.aspx?strResultId=" + strResultId
    });
}
//弹出选择默认协同人
function getDefaultUserExName(strResultId) {
    $.ligerDialog.open({ title: "选择分析协同人", width: 400, height: 250, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.UserSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objItemGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../../Mis/Monitor/Result/DefaultUserEx.aspx?strResultId=" + strResultId
    });
}
//弹出选择分析完成时间
function getAskingDate(strSampleID, strResultId) {
    $.ligerDialog.open({ title: "录入分析完成时间", width: 400, height: 300, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.UserSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objItemGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../../Mis/Monitor/Result/AskingDateSelected.aspx?strSampleID=" + strSampleID + "&strResultId=" + strResultId
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
