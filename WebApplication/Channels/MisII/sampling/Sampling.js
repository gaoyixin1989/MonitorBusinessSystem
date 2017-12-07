// Create by 苏成斌 2012.12.11  "采样任务"功能
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
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strUrl = "Sampling.aspx";
var strSourceID = "";
var iESample = "0";
var strCCflowWorkId = '';
//委托书信息
$(document).ready(function () {
    strSourceID = $("#SUBTASK_ID").val();
    strCCflowWorkId = $.getUrlVar('WorkID');
    //构建发送人表单
    $("#SendUser").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                            { display: "现场复核人", name: "ddlUser", newline: true, type: "select", comboboxName: "ddlUserBox", options: { valueFieldID: "hidUser", valueField: "ID", textField: "REAL_NAME", resize: false, url: strUrl + "?type=GetCheckUser&MonitorID=" + $("#MONITOR_ID").val()} }
                        ]
    });
    //判断是否存在现场项目
    //    $.ajax({
    //        cache: false,
    //        async: false,
    //        type: "POST",
    //        url: strUrl + "/isSendToCheck" + '&strCCflowWorkId=' + strCCflowWorkId,
    //        data: "{'strSubTaskID':'" + $("#SUBTASK_ID").val() + "'}",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (data, textStatus) {
    //            if (data.d != "1") {
    //                $("#SendUser").hide();
    //                iESample = "0";
    //            }
    //            else {
    //                iESample = "1";
    //            }
    //        }
    //    });
    
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=isSendToCheck2" + '&strCCflowWorkId=' + strCCflowWorkId + '&strSubTaskID=' + $("#SUBTASK_ID").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: function (data, textStatus) {

            
            if (data != "1") {
                $("#SendUser").hide();
                iESample = "0";
            }
            else {
                iESample = "1";
            }
        }
    });

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
var sendhDialog = null;

function Save() {

    var result = 1;
    var fn = $("#SampleLicaleID")[0].contentWindow.getSaveDate()

    if (iESample == "1") {
        if ($("#hidUser").val() == "") {
            $("#txtTip").val("请选择现场复核人");
            result = 0;
        }
        else {

            //更新复核人信息
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/UpdateSampleCheck",
                data: "{'strSubTaskID':'" + $("#SUBTASK_ID").val() + "', 'strCheckUser':'" + $("#hidUser").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        result = 1;
                    }
                    else {
                        result = 0;
                    }
                }
            });
        }
    }


    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/SaveLocaleInfo",
        data: "{'strSubtaskID':'" + $("#SUBTASK_ID").val() + "'" + fn + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            var strValue = data.d;
        }
    });

    return result;
}