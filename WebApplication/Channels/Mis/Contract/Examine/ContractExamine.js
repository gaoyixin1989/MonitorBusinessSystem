//Create By 胡方扬  2012-12-10
//获取流程业务ID

var wf_inst_task_id = "", wf_inst_id = "", wf_id = "";
var MonitorTypeName = "", ContractTypeName = "", Remarks = "";
var vFlowInfo = null, vContractInfor = null;
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
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: 1300 });
    //获取流程业务参数ID，得到业务ID 和流程ID
    wf_inst_task_id = $.getUrlVar("WF_INST_TASK_ID");
    wf_inst_id = $.getUrlVar("WF_INST_ID");
    wf_id = $.getUrlVar("WF_ID");
    if (wf_inst_task_id != "" & wf_inst_id != "") {

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../ProgrammingHandler.ashx?action=GetFlowTaskInfo&wf_inst_task_id=" + wf_inst_task_id + "&wf_inst_id=" + wf_inst_id + "&service_key_name=task_id",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vFlowInfo = data.Rows;
                    task_id = vFlowInfo[0].SERVICE_KEY_VALUE;
                    $("#hidTaskId").val(task_id);
                    //获取委托书信息
                    GetTaskInfo();
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    else {
        $.ligerDialog.warn('流程业务参数错误！'); return;
    }



    function GetTaskInfo() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../ProgrammingHandler.ashx?action=GetTaskInfor&task_id=" + task_id + "",
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