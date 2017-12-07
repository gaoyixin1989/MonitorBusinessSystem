//Create By 胡方扬  2012-12-10
//获取流程业务ID

var MonitorTypeName = "", ContractTypeName = "", Remarks = "";
var vFlowInfo = null, vContractInfor = null;
var CCFLOW_WORKID = "";
var objTaskInfo = null;
//业务ID
var task_id = "";
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

$(document).ready(function () {
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: 740 });

    CCFLOW_WORKID = $.getUrlVar('WorkID');

    if (objTaskInfo == null) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../../../Mis/Contract/MethodHander.ashx?action=GetTaskInfor&strCCFLOW_WORKID=" + CCFLOW_WORKID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    objTaskInfo = data.Rows;
                }
                else {
                    $.ligerDialog.warn('数据加载错误！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    task_id = objTaskInfo[0].CONTRACT_ID;
    $("#hidtask_id").val(task_id);
    $("#txtTASK_CODE").val(objTaskInfo[0].TICKET_NUM);

    //获取委托书信息
    GetTaskInfo();

    function GetTaskInfo() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../../../Mis/Contract/ProgrammingHandler.ashx?action=GetTaskInfor&task_id=" + task_id + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vContractInfor = data.Rows;
                    //调用用户控件，为用户控件赋值
                    GetInivalue();
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
})

function Save() {
    var strTaskNum = $("#txtTASK_CODE").val();

    var result = 0;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../Mis/MonitoringPlan/MonitoringPlan.ashx?action=UpdateTask&task_id=" + objTaskInfo[0].ID + "&strTaskNum=" + encodeURI(strTaskNum),
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {


            if (data == "true") {
                result = 1;
            }

        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！');
        }
    });

    return result;
}