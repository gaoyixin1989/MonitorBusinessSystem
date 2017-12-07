var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strCompanyID = "";
var strID = "";
var strurlContractId = "", strurlMonitorTypeId = "", strurlProject = "", strurlContractTypeId = "", strurlPlanId = "";
var objDivAttr;
var intSelectCount = 0;
var AttributeTypeAndInfoLst = [];
var PointValue = [];
var managertmp;
var obj;

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
    strCompanyID = $.getUrlVar('CompanyID');
    strID = $.getUrlVar('strid');
    strurlContractId = $.getUrlVar('ContractId');
    strurlMonitorTypeId = $.getUrlVar('MonitorTypeId');
    strurlProject = $.getUrlVar('strProject');
    strurlContractTypeId = $.getUrlVar('ContractTypeId');
    strurlPlanId = $.getUrlVar('strPlanId');
    if (!strurlContractId)
        strurlContractId = "";
    if (!strID)
        strID = "";


    //创建表单结构 --样品基本信息
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 120, space: 10, labelAlign: 'right', modal: true,
        fields: [
                { name: "ID", type: "hidden" },
                { display: "原编码/名称", name: "SRC_CODEORNAME", newline: true, type: "text", width: 200, group: "基本信息", groupicon: groupicon },
                { display: "样品数量", name: "SAMPLE_COUNT", newline: false, type: "text", width: 200 },
                { display: "样品类型", name: "SAMPLE_TYPE", newline: true, type: "text", width: 200 },
                //{ display: "采样地点", name: "SAMPLE_NAME", newline: false, type: "text", width: 200 },
                //{ display: "采样日期/位置", name: "SAMPLE_ACCEPT_DATEORACC", newline: true, type: "text", width: 200 },
                { display: "样品状态", name: "SAMPLE_STATUS", newline: false, type: "text", width: 200 },
                { display: "备注", name: "REMARK1", newline: true, type: "text", width: 530 }
                ]
    });

    $("#SRC_CODEORNAME").attr("validate", "[{required:true, msg:'请输入原编码/名称'}]");
    $("#SAMPLE_COUNT").val('1');
    $("#SAMPLE_COUNT").attr("validate", "[{isnumber:true, msg:'请输入正确的数据类型'}]");


    if (strID != "") {
        var InitDataList = [];
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../Contract/MethodHander.ashx?action=GetContractSample&strSampleId=" + strID + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InitDataList = data.Rows;
            }
        });
        SetInitValue();
    }




    function SetInitValue() {
        //编辑模式下 初始化基本信息
        //$("#SAMPLE_NAME").val(InitDataList[0].SAMPLE_NAME);
        $("#SAMPLE_COUNT").val(InitDataList[0].SAMPLE_COUNT);
        $("#SAMPLE_TYPE").val(InitDataList[0].SAMPLE_TYPE);
        $("#SRC_CODEORNAME").val(InitDataList[0].SRC_CODEORNAME);
        //$("#SAMPLE_ACCEPT_DATEORACC").val(InitDataList[0].SAMPLE_ACCEPT_DATEORACC);
        $("#SAMPLE_STATUS").val(InitDataList[0].SAMPLE_STATUS);
        $("#REMARK1").val(InitDataList[0].REMARK1);
    }
});

//得到基本信息保存参数
function GetBaseInfoStr() {
    var strData = "";
    var isTrue = $("#form1").validate();
    if (isTrue) {
        strData += "&strSampleId=" + strID;

        strData += "&strContratId=" + strurlContractId;
        strData += "&strPlanId=" + strurlPlanId;
        strData += "&strMonitroType=" + strurlMonitorTypeId;
        strData += "&strSampleName=" + encodeURIComponent($("#SRC_CODEORNAME").val());
        strData += "&strSampleType=" + encodeURIComponent($("#SAMPLE_TYPE").val());
        strData += "&strSampleCount=" + encodeURIComponent($("#SAMPLE_COUNT").val());
        strData += "&strSrcCodeOrName=" + encodeURIComponent($("#SRC_CODEORNAME").val());
        //strData += "&strSampleDateOrAcc=" + encodeURIComponent($("#SAMPLE_ACCEPT_DATEORACC").val());
        strData += "&strSampleStatus=" + encodeURIComponent($("#SAMPLE_STATUS").val());
        strData += "&strtxtRemarks=" + encodeURIComponent($("#REMARK1").val());
    }
    return strData;
}


