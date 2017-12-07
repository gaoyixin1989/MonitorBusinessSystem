// Create by 苏成斌 2012.12.11  "采样任务"功能

var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strUrl = "SamplingView.aspx";

//委托书信息
$(document).ready(function () {
    //委托书信息
    //    $.post('Sampling.aspx?type=getContractInfo&strSubtaskID=' + $("#SUBTASK_ID").val(), function (data) {
    //        SetContractInfo(data);
    //    }, 'json');

});

//初始委托书信息模块
function SetContractInfo(data) {
    objOneFrom = $("#oneFrom").ligerForm({
        inputWidth: 160, labelWidth: 100, space: 30, labelAlign: 'right',
        fields: [
                { display: "合同编号", name: "CONTRACT_CODE", newline: true, type: "text", width: 330, group: "委托信息", groupicon: groupicon },
                { display: "监测类型", name: "MONITOR_ID", newline: false, width: 150, type: "text" },
                { display: "委托类型", name: "CONTRACT_TYPE", newline: false, width: 150, type: "text" },
                { display: "受检单位", name: "TESTED_COMPANY_ID", newline: true, width: 330, type: "text" },
                { display: "联系人", name: "CONTACT_NAME", newline: false, width: 150, type: "text" },
                { display: "联系电话", name: "LINK_PHONE", newline: false, width: 150, type: "text" },
                { display: "采样日期", name: "SAMPLE_ASK_DATE", space: 10, newline: true, width: 110, type: "date" },
                { display: "要求完成日期", name: "SAMPLE_FINISH_DATE", newline: false, width: 110, type: "date" },
                { display: "采样负责人", name: "SAMPLING_MANAGER_ID", newline: false, width: 150, type: "text" },
                { display: "采样人员", name: "SAMPLING_MAN", newline: false, width: 150, type: "text"}]
    });


    //赋值
    if (data) {
        $("#CONTRACT_CODE").val(data.REMARK1);
        $("#MONITOR_ID").val(data.MONITOR_ID);
        $("#CONTRACT_TYPE").val(data.REMARK2);
        $("#TESTED_COMPANY_ID").val(data.REMARK3);
        $("#CONTACT_NAME").val(data.REMARK4);
        $("#LINK_PHONE").val(data.REMARK5);
        $("#SAMPLE_ASK_DATE").val(data.SAMPLE_ASK_DATE);
        $("#SAMPLE_FINISH_DATE").val(data.SAMPLE_FINISH_DATE);
        $("#SAMPLING_MANAGER_ID").val(data.SAMPLING_MANAGER_ID);
        $("#SAMPLING_MAN").val(data.SAMPLING_MAN);
    }

    $("#CONTRACT_CODE").ligerGetComboBoxManager().setDisabled();
    $("#MONITOR_ID").ligerGetComboBoxManager().setDisabled();
    $("#CONTRACT_TYPE").ligerGetComboBoxManager().setDisabled();
    $("#TESTED_COMPANY_ID").ligerGetComboBoxManager().setDisabled();
    $("#CONTACT_NAME").ligerGetComboBoxManager().setDisabled();
    $("#LINK_PHONE").ligerGetComboBoxManager().setDisabled();
    $("#SAMPLE_ASK_DATE").ligerGetComboBoxManager().setDisabled();
    $("#SAMPLING_MANAGER_ID").ligerGetComboBoxManager().setDisabled();
    $("#SAMPLING_MAN").ligerGetComboBoxManager().setDisabled();
}
function getCompanyName(strTaskId, strCompanyId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getCompanyName",
        data: "{'strTaskId':'" + strTaskId + "','strCompanyId':'" + strCompanyId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取监测类别信息
function getMonitorTypeName(strMonitorTypeId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getMonitorTypeName",
        data: "{'strMonitorTypeId':'" + strMonitorTypeId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

function SendClick() {
    $.ligerDialog.confirm('您确定要将该任务发送至下一环节吗？', function (yes) {
        if (yes == true) {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/btnSendClick",
                data: "{'strSubtaskID':'" + $("#SUBTASK_ID").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    strValue = data.d;
                    if (strValue == "1") {
                        $.ligerDialog.success('发送成功');
                    }
                    else {
                        $.ligerDialog.warn('发送失败');
                    }
                }
            });
        }
    });
}