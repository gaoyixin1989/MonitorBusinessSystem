//Create By Castle (胡方扬) 2012-12-04

var strBtnStatus = "", strStatus = 0, strContractCode = "", strTaskCode = "", strTaskProjectName = "", strContratId = "", strTaskCompanyId = "";
var boolSave = false;
var isExport = "";
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

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetWebConfigValue&strKey=Contract_Export",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                isExport = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    //引用父页面vContractList 委托书信息
    if (vContractList != null) {
        strBtnStatus = vContractList[0].CONTRACT_STATUS;
        strContractCode = vContractList[0].CONTRACT_CODE;
        strTaskProjectName = vContractList[0].PROJECT_NAME;
        strTaskCompanyId = vContractList[0].TESTED_COMPANY_ID;
    }

    if (strContractCode != "") {
        var jsExport = "";
        if (isExport == "1") {
            jsExport += "  <a href='javascript:ExportContract();'>导出委托书</a>"
            $("#Contract_Fee").html("<a href='javascript:ExportContractFee();'>导出费用明细</a>");
        }
        $("#Contract_Code").html(strContractCode + jsExport);
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
        strRequestData += "&strProData=" + encodeURI($("#txtProData").val()) + "&strOtherAsk=" + encodeURI($("#txtOtherAsk").val()) + "&strAccording=" + encodeURI($("#txtAccording").val()) + "&strtxtRemarks=" + encodeURI($("#txtRemarks").val());
        
        //使用其他页面变量
        strRequestData += '&strContratId=' + strContratId + '&strCompanyIdFrim=' + strCompanyIdFrim + '&strContratType=' + strContratTypeId + '&strContratYear=' + strAutoYear + '&strMonitroType=' + strMonitorTypeId + '&strSelectTabMonitorTypeId=' + MonitorTypeId;

        GetPageValue()
        GetPageValueFrim();
        strRequestData += "&strAreaId=" + strAreaId + "&strContactName=" + encodeURI(strContactName) + "&strTelPhone=" + encodeURI(strTelPhone) + "&strAddress=" + encodeURI(strAddress);
        strRequestData += "&strAreaIdFrim=" + strAreaIdFrim + "&strContactNameFrim=" + encodeURI(strContactNameFrim) + "&strTelPhoneFrim=" + encodeURI(strTelPhoneFrim) + "&strAddressFrim=" + encodeURI(strAddressFrim);

        return strRequestData;
    }


function SaveCheckContractInfor() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=SaveCheckContractInfor&strStatus=" + strStatus + GetCheckContractInfor(),
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
        $.ligerDialog.warn('请确认以上录入信息！'); return;
    }
    boolSave = false;
    SaveCheckContractInfor();
    $("#divConst").attr("display", "");
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none"; 
}
function BackSend() {
    $("#hidTaskId").val(task_id);
    $("#hidBtnType").val("back");
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}
function Save() {
    if (strContratId == "") {
        $.ligerDialog.warn('请确认以上录入信息！'); return;
    }
    boolSave = true;
    SaveCheckContractInfor();
    $("#divConst").attr("display", "");
    return false;
//    var surl = '../Channels/Mis/Contract/ContractList.aspx?WF_ID=WT_FLOW|WF_A|SAMPLE_WT';
//    top.f_overTab('委托书列表', surl);
}
function ExportContract() {
    $.ligerDialog.confirm('确定导出委托监测协议书？\r\n', function (yes) {
        if (yes == true) {
            if ($.getUrlVar('strYsShowStatus') == "0")
                $("#btnExport_QY").click();
            else
                $("#btnExport").click();
        }
        else {
        }
    });
}
