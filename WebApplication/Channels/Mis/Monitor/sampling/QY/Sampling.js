// Create by 苏成斌 2012.12.11  "采样任务"功能

var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strUrl = "Sampling.aspx";
var strSourceID = "";
//委托书信息
$(document).ready(function () {
    strSourceID = getQueryString('SOURCE_ID');
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

//弹出发送人窗口
var sendhDialog = null;
function showSend() {
    var strdate = $("#SampleLicaleID")[0].contentWindow.getNewAskDate(); //郭海华修改提醒功能
    if (!strdate) {
        $.ligerDialog.warn('采样日期不能为空！');
        return;
    }
    if (!$("#SamplePointID")[0].contentWindow.CheckPointData()) {
        $.ligerDialog.warn('样品采样时间和样品状态不能为空，请检查！');
        return;
    }
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/isSendToCheck",
        data: "{'strSubTaskID':'" + $("#SUBTASK_ID").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            
            if (data.d == "1") {
                if (sendhDialog) {
                    sendhDialog.show();
                } else {
                    //构建发送人表单
                    $("#sendForm").ligerForm({
                        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
                        fields: [
                            { display: "现场复核人", name: "ddlUser", newline: true, type: "select", comboboxName: "ddlUserBox", options: { valueFieldID: "hidUser", valueField: "ID", textField: "REAL_NAME", resize: false, url: strUrl + "?type=GetCheckUser&MonitorID=" + getQueryString("strMonitor_ID")} }
                        ]
                    });

                    sendhDialog = $.ligerDialog.open({
                        target: $("#sendDiv"),
                        width: 300, height: 160, top: 90, title: "选择现场复核人",
                        buttons: [
                        { text: '确定', onclick: function (item, dialog) {
                            if ($("#hidUser").val() == "") {
                                $.ligerDialog.warn('请选择现场复核人');
                                return;
                            }
                            dialog.hide();
                            SendClick();
                        }
                        },
                        { text: '取消', onclick: function (item, dialog) { dialog.hide(); } }
                    ]
                    });
                }
            }
            else {
                SendClick();
            }
        }
    });

}


function SendClick() {
    
    $.ligerDialog.confirm('您确定要将该任务发送至下一环节吗？', function (yes) {
        if (yes == true) {
            var fn = $("#SampleLicaleID")[0].contentWindow.getSaveDate();
            var btnClick = "";
            if ($("#IS_BACK").val() == "1")
                btnClick = "btnBackSendClick";
            else
                btnClick = "btnSendClick";

            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/" + btnClick,
                data: "{'strSubtaskID':'" + $("#SUBTASK_ID").val() + "','strSourceID':'" + strSourceID + "','strUserID':'" + $("#hidUser").val() + "'" + fn + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    strValue = data.d;
                    var objValue = eval(strValue)[0];
                    if (objValue.result == "1") {
                        //$.ligerDialog.success('发送成功');
                        $.ligerDialog.confirmWF('发送成功,发送至【<a style="color:Red;font-weight:bold">' + objValue.msg + '</a>】环节', '提示', function (result) { if (result) { top.f_removeSelectedTabs(); } })
                        return;
                    }
                    else {
                        $.ligerDialog.warn(objValue.msg, '发送失败');
                        return;
                    }
                }
            });
        }
    });
}

function BackClick() {
    $.ligerDialog.prompt('退回意见', '', true, function (yes, value) {
        if (yes) {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/btnBackClick",
                data: "{'strSubtaskID':'" + $("#SUBTASK_ID").val() + "','strSuggestion':'" + value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        $.ligerDialog.confirmWF('退回成功,退回至【<a style="color:Red;font-weight:bold">采样任务分配</a>】环节', '提示', function (result) { if (result) { top.f_removeSelectedTabs(); } })
                        return;
                    }
                    else {
                        $.ligerDialog.warn('退回失败');
                        return;
                    }
                }
            });
        }
    });
}