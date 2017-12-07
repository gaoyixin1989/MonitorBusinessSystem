var formContract;
var formClientCompany;
var formTestedCompany;
var CompanyInfo = null, ContractInfo = null;
var strContractId = "";
var jsonContractInfo; //委托书信息
var jsonClientCompanyInfo; //委托企业信息
var jsonTestedCompanyInfo; //受检企业信息
var strContractFee = "", strYsShowStatus = "0";  //费用
var strTestType=""; //监测类型

var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(function () {
    strContractId = $("#hdnContracID").val();
    //委托书 编制
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "AcceptanceSchedule.aspx?type=getContractInfo&contract_id=" + strContractId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                jsonContractInfo = data;
            }
        }
    });
    //委托企业信息获取
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "AcceptanceSchedule.aspx?type=getClientCompanyInfo&contract_id=" + strContractId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                jsonClientCompanyInfo = data;
            }
        }
    });
    //受检企业信息获取
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "AcceptanceSchedule.aspx?type=getTestedCompanyInfo&contract_id=" + strContractId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                jsonTestedCompanyInfo = data;
            }
        }
    });
    //监测费用获取
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "AcceptanceCreate.aspx?type=getContractFee&contract_id=" + jsonContractInfo.ID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strContractFee = data;
            }
        }
    });

    //是否现在验收类委托书启用流程
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetWebConfigValue&strKey=YSTypeShow",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strYsShowStatus = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    formContract = $("#divContractInfo");


    var objFile = [
        { display: "项目名称", name: "PROJECT_NAME", newline: true, width: 350, type: "label" },
        { display: "委托类型", name: "CONTRACT_TYPE", newline: false, type: "select", comboboxName: "dropContractType", options: { valueFieldID: "CONTRACT_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "AcceptanceCreate.aspx?type=GetDict&dict_type=Contract_Type"} },
        { display: "合同编号", name: "CONTRACT_CODE", space: 10, newline: true, type: "label" },
        { display: "费用", name: "CONTRACT_FEE", width: 80, newline: false, type: "label" },
        { display: "委托年度", name: "CONTRACT_YEAR", newline: false, type: "label" },
        { display: "监测类型", name: "TEST_TYPES", space: 190, newline: true, type: "label" },
        { display: "签订日期", name: "ASKING_DATE", newline: false, type: "label" },
        { display: "监测目的", name: "TEST_PURPOSE", width: 610, newline: true, type: "label" },
        { display: "备注", name: "REMARK", newline: true, width: 610, type: "label" }
        ];

    if (strYsShowStatus == "2") {
        objFile = [
        { display: "项目名称", name: "PROJECT_NAME", newline: true, width: 350, type: "label" },
        { display: "委托类型", name: "CONTRACT_TYPE", newline: false, type: "select", comboboxName: "dropContractType", options: { valueFieldID: "CONTRACT_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "AcceptanceCreate.aspx?type=GetDict&dict_type=Contract_Type"} },
        { display: "合同编号", name: "CONTRACT_CODE", space: 10, newline: true, type: "label" },
        { display: "委托年度", name: "CONTRACT_YEAR", newline: false, type: "label" },
        { display: "监测类型", name: "TEST_TYPES", space: 190, newline: true, type: "label" },
        { display: "签订日期", name: "ASKING_DATE", newline: false, type: "label" },
        { display: "监测目的", name: "TEST_PURPOSE", width: 610, newline: true, type: "label" },
        { display: "备注", name: "REMARK", newline: true, width: 610, type: "label" }
        ];
    }
    formContract.ligerForm({
        inputWidth: 160, labelWidth: 100, space: 0, labelAlign: 'right',
        fields: objFile
    });
    if (jsonContractInfo != null) {
        $("#PROJECT_NAME").val(jsonContractInfo.PROJECT_NAME);
        $("#CONTRACT_TYPE").val(jsonContractInfo.CONTRACT_TYPE);
        $("#dropContractType").ligerGetComboBoxManager().setDisabled();
        $("#CONTRACT_CODE").val(jsonContractInfo.CONTRACT_CODE);
        if (strYsShowStatus != "2") {
            $("#CONTRACT_FEE").val(strContractFee);
        }
        $("#CONTRACT_YEAR").val(jsonContractInfo.CONTRACT_YEAR);
        $("#TEST_TYPES").val(jsonContractInfo.TEST_TYPES);
        $("#ASKING_DATE").val(jsonContractInfo.ASKING_DATE);
        $("#TEST_PURPOSE").val(jsonContractInfo.TEST_PURPOSE);
        $("#REMARK").val(jsonContractInfo.REMARK1);
    }
    //委托企业信息
    formClientCompany = $("#divClientCompanyInfo");
    formClientCompany.ligerForm({
        inputWidth: 160, labelWidth: 100, space: 0, labelAlign: 'right',
        fields: [
        { display: "委托单位", name: "CLIENT_COMPANY", newline: true, width: 350, type: "label" },
        { display: "所在区域", name: "AREA1", newline: false, type: "select", comboboxName: "dropArea1", options: { valueFieldID: "AREA1", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "AcceptanceCreate.aspx?type=GetDict&dict_type=administrative_area"} },
        { display: "联系人", name: "CONTACT_NAME1", space: 190, newline: true, type: "label" },
        { display: "联系电话", name: "PHONE1", newline: false, type: "label" },
        { display: "通讯地址", name: "CONTACT_ADDRESS", newline: true, width: 610, type: "label" }
        ]
    });
    formTestedCompany = $("#divTestedCompanyInfo");
    formTestedCompany.ligerForm({
        inputWidth: 160, labelWidth: 100, space: 0, labelAlign: 'right',
        fields: [
        { display: "受检单位", name: "TESTED_COMPANY", width: 350, newline: true, type: "label" },
        { display: "所在区域", name: "AREA2", newline: false, type: "select", comboboxName: "dropArea2", options: { valueFieldID: "AREA2", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "AcceptanceCreate.aspx?type=GetDict&dict_type=administrative_area"} },
        { display: "联系人", name: "CONTACT_NAME2", space: 190, newline: true, type: "label" },
        { display: "联系电话", name: "PHONE2", newline: false, type: "label" },
        { display: "监测地址", name: "MONITOR_ADDRESS", newline: true, width: 610, type: "label" }
        ]
    });
    //委托企业赋值
    if (jsonClientCompanyInfo != null) {
        $("#CLIENT_COMPANY").val(jsonClientCompanyInfo.COMPANY_NAME);
        $("#AREA1").val(jsonClientCompanyInfo.AREA);
        $("#dropArea1").ligerGetComboBoxManager().setDisabled();
        $("#CONTACT_NAME1").val(jsonClientCompanyInfo.CONTACT_NAME);
        $("#PHONE1").val(jsonClientCompanyInfo.PHONE);
        $("#CONTACT_ADDRESS").val(jsonClientCompanyInfo.CONTACT_ADDRESS);
    }
    //受检企业赋值
    if (jsonTestedCompanyInfo != null) {
        $("#TESTED_COMPANY").val(jsonTestedCompanyInfo.COMPANY_NAME);
        $("#AREA2").val(jsonTestedCompanyInfo.AREA);
        $("#dropArea2").ligerGetComboBoxManager().setDisabled();
        $("#CONTACT_NAME2").val(jsonTestedCompanyInfo.CONTACT_NAME);
        $("#PHONE2").val(jsonTestedCompanyInfo.PHONE);
        $("#MONITOR_ADDRESS").val(jsonTestedCompanyInfo.MONITOR_ADDRESS);
    }
});
///方案附件上传
function upload() {
    $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
        buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        $("iframe")[0].contentWindow.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileUpload.aspx?filetype=AcceptanceContract&id=' + strContractId
    });
}
///附件下载
function downLoad() {
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=AcceptanceContract&id=' + strContractId
    });
}

function SendSave() {
}