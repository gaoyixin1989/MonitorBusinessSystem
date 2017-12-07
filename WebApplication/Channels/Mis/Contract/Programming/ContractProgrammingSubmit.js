//Create By 胡方扬 2012-12-11
//委托编制阶段数据保存提交

//引用父页面vContractInfor变量
var SavCompany = true;
var isfrim = false;
var strLeader = "", strRequest = "";

$(document).ready(function () {
    //获取当前页面URL参数（包含了流程信息的参数）
    strRequest = window.location.search;
    strRequest = strRequest.substring(1,strRequest.length);
})

function BackSend() {
    $("#hidTaskId").val(task_id);
    $("#hidCompanyId").val(vContractInfor[0].TESTED_COMPANY_ID);
    $("#hidBtnType").val("back");
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}
function SendSave() {
    if (vContractInfor != null) {
        if (vContractInfor[0].CLIENT_COMPANY_ID != "") {
            EditCompanyInfo(vContractInfor[0].CLIENT_COMPANY_ID);
        }
        if (vContractInfor[0].TESTED_COMPANY_ID != "") {
            isfrim = true;
            EditCompanyInfo(vContractInfor[0].TESTED_COMPANY_ID);
        }
        if (SavCompany == true) {
            //UpdateLeader();
            $("#hidQcStep").val($("#txtQcStep").val());
            $("#hidQcList").val($("#QCLIST_BOX").val());
            $("#hidTaskId").val(task_id);
            $("#hidContractType").val(vContractInfor[0].CONTRACT_TYPE);
            $("#hidBtnType").val("send");
        }
    }
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}
function GetValueDate(strComanyId) {
    var strRequest = "";
    strRequest += "&strCompanyId=" + strComanyId;
    if (isfrim == false) {
        strRequest += "&task_id=" + task_id;
        strRequest += "&strContactName=" + encodeURI($("#Contacts_Name").val());
        strRequest += "&strIndustryId=" + $("#INDUSTRY_OP").val();
        strRequest += "&strAreaId=" + $("#AREA_OP").val();
        strRequest += "&strTelPhone=" + $("#Tel_Phone").val();
        strRequest += "&strAddress=" + encodeURI($("#Address").val());
        strRequest += "&strisFrim=" + isfrim;
    }
    else {
        strRequest += "&task_id=" + task_id;
        strRequest += "&strContactName=" + encodeURI($("#Contacts_NameFrim").val());
        strRequest += "&strIndustryId=" + $("#INDUSTRYFrim_OP").val();
        strRequest += "&strAreaId=" + $("#AREAFrim_OP").val();
        strRequest += "&strTelPhone=" + $("#Tel_PhoneFrim").val();
        strRequest += "&strAddress=" + encodeURI($("#AddressFrim").val());
        strRequest += "&strisFrim=" + isfrim;
    }
    return strRequest;
}


function EditCompanyInfo(comPanyId) {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../ProgrammingHandler.ashx?action=EditContractCompanyInfo" + GetValueDate(comPanyId),
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
            }
            else {
                SavCompany = false;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}

//function UpdateLeader() {
//    strLeader = $("#hidleader").val();
////    var strQcStep = $("#txtQcStep").val();
//    $.ajax({
//        cache: false,
//        async: false, //设置是否为异步加载,此处必须
//        type: "POST",
//        url: "../ProgrammingHandler.ashx?action=UpdateContractLeader&task_id=" + task_id + "&strLeader=" + strLeader,
//        contentType: "application/text; charset=utf-8",
//        dataType: "text",
//        success: function (data) {
//            if (data != "") {
////                $.ligerDialog.success('项目负责人更新成功！');
//            }
//            else {
//                $.ligerDialog.warn('数据保存失败！');
//            }
//        },
//        error: function (msg) {
//            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
//        }
   // });
//}