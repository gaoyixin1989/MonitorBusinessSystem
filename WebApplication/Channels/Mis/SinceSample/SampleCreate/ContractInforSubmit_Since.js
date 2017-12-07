//Create By Castle (胡方扬) 2012-12-04

var strBtnStatus = "", strStatus = 0, strContractCode = "", strTaskCode = "", strTaskProjectName = "", strContratId = "",strTaskCompanyId = "";
var boolSave = false;
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

    //引用父页面vContractList 委托书信息
    if (vContractList != null) {
        strBtnStatus = vContractList[0].CONTRACT_STATUS;
        strContractCode = vContractList[0].CONTRACT_CODE;
        strTaskProjectName = vContractList[0].PROJECT_NAME;
        strTaskCompanyId = vContractList[0].TESTED_COMPANY_ID;
    }
    if (strContractCode != "") {
        $("#Contract_Code").html(strContractCode);
        SetHidInputValue();
    }
})
function SetHidInputValue() {
    $("#hidTaskId").val(strContratId);
    $("#hidTaskCode").val(strContractCode);
    $("#hidTaskProjectName").val(strTaskProjectName);
    $("#hidCompanyId").val(strTaskCompanyId);
    $("#hidBtnType").val("send");
}

function GetCheckContractInfor() {
    var strRequestData = "";
    strRequestData += '&strProjectName=' + encodeURI($("#Project_Name").val());
    strRequestData += '&strContract_Date=' + encodeURI($("#Contract_Date").val());
    strRequestData += '&strRpt_Way=' + $("#Rpt_Way_OP").val();
    strRequestData += ' &strFREQ='+$("#FREQ_OP").val();
    strRequestData += ' &strSampleAccept=' + $("#SAMPLE_ACCEPTER_OP").val();
    strRequestData += ' &strSampleMan=' + $("#SAMPLE_SEND_MAN").val();
    var strmark = "";
    strmark = $("#Remarks_OP").val().split(';');
    for (var i = 0; i < strmark.length; i++) {
        if (strmark[i] == 'accept_subpackage') {
            strRequestData += '&strAGREE_OUTSOURCING=1';
        }

        if (strmark[i] == 'accept_useMonitorMethod') {
            strRequestData += '&strAGREE_METHOD=1';
        }

        if (strmark[i] == 'accept_usenonstandard') {
            strRequestData += '&strAGREE_NONSTANDARD=1';
        }
        if (strmark[i] == 'accept_other') {
            strRequestData += '&strAGREE_OTHER=1';
        }
    }
    strRequestData += '&strMonitor_Purpose=' + encodeURI($("#Monitor_Purpose").val());
    //使用其他页面变量
    strRequestData += '&strContratId=' + strContratId + '&strCompanyIdFrim=' + strCompanyIdFrim + '&strContratType=' + strContratTypeId + '&strContratYear=' + strAutoYear + '&strMonitroType=' + strMonitorTypeId + '&strSelectTabMonitorTypeId=' + MonitorTypeId +'&strFREQ=' + strFreq + '&strSampleAccept=' + strSampleAccept + '&strSampleMan=' + strSampleMan;
    return strRequestData;
}


function SaveCheckContractInfor() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Contract/MethodHander.ashx?action=SaveCheckContractInfor&strStatus=" + strStatus + GetCheckContractInfor(),
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                var DataArr = data.split('|');
                strContractCode = DataArr[0];
                strContratId = DataArr[1];
                strTaskProjectName = $("#Project_Name").val();
                $("#Contract_Code").html(strContractCode);
                SetHidInputValue();
                if (boolSave == true) {
                    $.ligerDialog.success('数据保存成功！');
                }
            }
            else {
                $.ligerDialog.warn('数据保存失败！');
                return;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('AJAX数据请求失败！');
            return;
        }
    });
}

function SendSave() {
    if (strContratId == "") {
        $.ligerDialog.success('请确认以上录入信息！'); return;
    }
    boolSave = false;
    SaveCheckContractInfor();
    $("#divConst").attr("display", "");
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}

function Save() {
    if (strContratId == "") {
        $.ligerDialog.success('请确认以上录入信息！'); return;
    }
    boolSave = true;
    SaveCheckContractInfor();
    $("#divConst").attr("display", "");
}

function BackSend() {
    $("#hidTaskId").val(task_id);
    $("#hidBtnType").val("back");
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}