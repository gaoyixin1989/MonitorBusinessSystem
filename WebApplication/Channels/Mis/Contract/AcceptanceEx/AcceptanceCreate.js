//验收监测委托书录入 Create by weilin
var managerContractType; //选择委托类型
var managerContractInfo; //委托书信息
var vContractList = null;
var vAutoYearItem = null, vMonitorType = null, strCreated = false, strStatus = 0, boolSave = false;
var strContratId = "", strContratTypeId = null, strAutoYear = null, strMonitorTypeId = null, strRpt_Way = null, strContractFee = null, strContract_Date = null, vRItems = null, vRptItems = null, strRemarktData = null, strProjectName = null, strMonitor_Purpose = null;
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var ArrReturnInfo = []; //返回数据集合
//委托企业JS
var vAreaItem = null, vAutoItem = null;
var strCompanyId = "", strCompanyName = "", strIndustryId = "", strAreaId = "", strContactName = "", strTelPhone = "", strAddress = "";
var cacheSave = false;
var isExistCompany = false, isSelect = false;

var ModifyCompanyInfor = null;
//受检企业JS
var vAreaItem = null, vAutoItem = null;
var strCompanyIdFrim = "", strCompanyNameFrim = "", strIndustryIdFrim = "", strAreaIdFrim = "", strContactNameFrim = "", strTelPhoneFrim = "", strAddressFrim = "";
var cacheSaveFrim = false, isExistCompanyFrm = false;

var isAdd = true, isView = false, strView = "", strType = "true", strWf = "";
var strContractCode = "";

var showInput = [
        { display: "委托类型", name: "CONTRACT_TYPE", newline: true, width: 300, type: "select", group: "选择委托类型", groupicon: groupicon, comboboxName: "dropContractType", options: { valueFieldID: "CONTRACT_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "AcceptanceCreate.aspx?type=GetDict&dict_type=Contract_Type"} },
        { display: "委托年度", name: "CONTRACT_YEAR", newline: false, type: "select", comboboxName: "dropContractYear", options: { isMultiSelect: false, valueFieldID: "CONTRACT_YEAR", valueField: "ID", textField: "YEAR", url: "AcceptanceCreate.aspx?type=GetContratYear"} }
        ];
//路径解析
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

$(function () {
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="验收企业自查内容"/><span>验收企业自查内容</span>';
    $(strdivImg).appendTo(divImgContent);
    strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="勘查结果"/><span>勘查结果</span>';
    $(strdivImg).appendTo(divImgPro);

    //状态判断
    strType = $.getUrlVar('type');
    strWf = $.getUrlVar('WF_ID');
    if (strType == "true") {
        isAdd = true;
    }
    else {
        isAdd = false;

        if ($.getUrlVar('strContratId') != null)
            strContratId = $.getUrlVar('strContratId');
        else
            strContratId = $("#CONTRACT_ID").val();

        strView = $.getUrlVar('view');
        GetContractInfor();
        if (vContractList != null) {
            strContractCode = vContractList[0].CONTRACT_CODE;
            strStatus = vContractList[0].CONTRACT_STATUS;
        }
        if (strView == "true") {
            isView = true;
        }

    }

    //构造委托书类别信息表单
    $("#divContractType").ligerForm({
        inputWidth: 160, labelWidth: 80, space: 0, labelAlign: 'right',
        fields: showInput//动态生成输入框
    });

    //监测ID寄存
    $("#CONTRACT_ID").val(strContratId);
    //默认赋值 委托类型为验收监测
    $("#CONTRACT_TYPE").val($("#Contract_Type").val());
    $("#dropContractType").ligerGetTextBoxManager().setDisabled();
    $("#CONTRACT_YEAR").val(new Date().getFullYear());

    //委托单号
    if (strContractCode != "") {
        $("#Contract_Code").html(strContractCode);
    }
    else {
        $("#Contract_Code").html('委托单号尚未生成');
    }

    if (isAdd == false) {
        SetInputValue();
        SetInputDisable();
    }
});
//编辑状态赋值 补充
function SetInputValue() {
    if (vContractList == null) {
        $.ligerDialog.warn('数据获取失败！A');
        return;
    }
    strContratTypeId = vContractList[0].CONTRACT_TYPE;
    strAutoYear = vContractList[0].CONTRACT_YEAR;
    strCompanyId = vContractList[0].COMPANYID;
    strCompanyName = vContractList[0].COMPANYNAME;
    strAreaId = vContractList[0].AREA;
    strContactName = vContractList[0].CONTRACTNAME;
    strAddress = vContractList[0].CONTRACTADDRESS;
    strTelPhone = vContractList[0].PHONE;
    strCompanyIdFrim = vContractList[0].COMPANYIDFRIM;
    strCompanyNameFrim = vContractList[0].COMPANYNAMEFRIM;
    strAreaIdFrim = vContractList[0].AREAFRIM;
    strContactNameFrim = vContractList[0].CONTRACTNAMEFRIM;
    strAddressFrim = vContractList[0].CONTRACTADDRESSFRIM;
    strTelPhoneFrim = vContractList[0].PHONEFRIM;
    strProjectName = vContractList[0].PROJECT_NAME;

    $("#PROJECT_NAME").val(strProjectName);
    $("#CONTRACT_YEAR").val(strAutoYear);
    $("#txtPFYQ").val(vContractList[0].PROVIDE_DATA);
    $("#txtSZCL").val(vContractList[0].OTHER_ASKING);
    $("#txtBL").val(vContractList[0].MONITOR_ACCORDING);
    $("#txtGasNum").val(vContractList[0].QC_STEP);
    $("#txtRemark").val(vContractList[0].REMARK1);
    
    var objRadio = vContractList[0].REMARK2.split('|');
    if (objRadio[0] == "true") {
        $("#rbYS_0")[0].checked = true;
        $("#rbYS_1")[0].checked = false;
    }
    else {
        $("#rbYS_0")[0].checked = false;
        $("#rbYS_1")[0].checked = true;
    }
    if (objRadio[1] == "true") {
        $("#rbCP_0")[0].checked = true;
        $("#rbCP_1")[0].checked = false;
    }
    else {
        $("#rbCP_0")[0].checked = false;
        $("#rbCP_1")[0].checked = true;
    }
    if (objRadio[2] == "true") {
        $("#rbWater_0")[0].checked = true;
        $("#rbWater_1")[0].checked = false;
    }
    else {
        $("#rbWater_0")[0].checked = false;
        $("#rbWater_1")[0].checked = true;
    }
    if (objRadio[3] == "true") {
        $("#rbWaterRun_0")[0].checked = true;
        $("#rbWaterRun_1")[0].checked = false;
    }
    else {
        $("#rbWaterRun_0")[0].checked = false;
        $("#rbWaterRun_1")[0].checked = true;
    }
    if (objRadio[4] == "true") {
        $("#rbWaterPWK_0")[0].checked = true;
        $("#rbWaterPWK_1")[0].checked = false;
    }
    else {
        $("#rbWaterPWK_0")[0].checked = false;
        $("#rbWaterPWK_1")[0].checked = true;
    }
    if (objRadio[5] == "true") {
        $("#rbGas_0")[0].checked = true;
        $("#rbGas_1")[0].checked = false;
    }
    else {
        $("#rbGas_0")[0].checked = false;
        $("#rbGas_1")[0].checked = true;
    }
    if (objRadio[6] == "true") {
        $("#rbGasRun_0")[0].checked = true;
        $("#rbGasRun_1")[0].checked = false;
    }
    else {
        $("#rbGasRun_0")[0].checked = false;
        $("#rbGasRun_1")[0].checked = true;
    }
    if (objRadio[7] == "true") {
        $("#rbGasPWK_0")[0].checked = true;
        $("#rbGasPWK_1")[0].checked = false;
    }
    else {
        $("#rbGasPWK_0")[0].checked = false;
        $("#rbGasPWK_1")[0].checked = true;
    }
    
    if (vContractList[0].REMARK3 != "") {
        $("#divProspecting")[0].style.display = "";

        var objRadio = vContractList[0].REMARK3.split('|');
        $("#txtPPFYQ").val(objRadio[0]);
        $("#txtPSZCL").val(objRadio[1]);
        $("#txtPBL").val(objRadio[2]);
        $("#txtPGasNum").val(objRadio[3]);
        if (objRadio[4] == "true") {
            $("#rbPYS_0")[0].checked = true;
            $("#rbPYS_1")[0].checked = false;
        }
        else {
            $("#rbPYS_0")[0].checked = false;
            $("#rbPYS_1")[0].checked = true;
        }
        if (objRadio[5] == "true") {
            $("#rbPCP_0")[0].checked = true;
            $("#rbPCP_1")[0].checked = false;
        }
        else {
            $("#rbPCP_0")[0].checked = false;
            $("#rbPCP_1")[0].checked = true;
        }
        if (objRadio[6] == "true") {
            $("#rbPWater_0")[0].checked = true;
            $("#rbPWater_1")[0].checked = false;
        }
        else {
            $("#rbPWater_0")[0].checked = false;
            $("#rbPWater_1")[0].checked = true;
        }
        if (objRadio[7] == "true") {
            $("#rbPWaterRun_0")[0].checked = true;
            $("#rbPWaterRun_1")[0].checked = false;
        }
        else {
            $("#rbPWaterRun_0")[0].checked = false;
            $("#rbPWaterRun_1")[0].checked = true;
        }
        if (objRadio[8] == "true") {
            $("#rbPWaterPWK_0")[0].checked = true;
            $("#rbPWaterPWK_1")[0].checked = false;
        }
        else {
            $("#rbPWaterPWK_0")[0].checked = false;
            $("#rbPWaterPWK_1")[0].checked = true;
        }
        if (objRadio[9] == "true") {
            $("#rbPGas_0")[0].checked = true;
            $("#rbPGas_1")[0].checked = false;
        }
        else {
            $("#rbPGas_0")[0].checked = false;
            $("#rbPGas_1")[0].checked = true;
        }
        if (objRadio[10] == "true") {
            $("#rbPGasRun_0")[0].checked = true;
            $("#rbPGasRun_1")[0].checked = false;
        }
        else {
            $("#rbPGasRun_0")[0].checked = false;
            $("#rbPGasRun_1")[0].checked = true;
        }
        if (objRadio[11] == "true") {
            $("#rbPGasPWK_0")[0].checked = true;
            $("#rbPGasPWK_1")[0].checked = false;
        }
        else {
            $("#rbPGasPWK_0")[0].checked = false;
            $("#rbPGasPWK_1")[0].checked = true;
        }
    }
}

function GetPageSelectValue() {
    strContratTypeId = $("#CONTRACT_TYPE").val();
    strAutoYear = $("#CONTRACT_YEAR").val();
}

//后期做统一保存
function SaveData() {
    var b = false;
    if (!vailValue()) {
        //创建参数字符串
        createQueryString();
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: encodeURI("../MethodHander.ashx?action=" + strMethod + strParme),
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    if (isAdd == true) {
                        strContratId = data.split('|')[0];
                        $("#CONTRACT_ID").val(strContratId);
                        $("#Contract_Code").html(data.split('|')[1]);
                        isAdd = false;
                        cacheSelectSave = true;
                        b = true;
                    }
                    else {
                        if (data == "True") {
                            if (boolSave)
                                $.ligerDialog.success('保存成功！');
                            b = true;
                        }
                        else {
                            $.ligerDialog.warn('保存失败！');
                            b = false;
                        }
                    }
                }
                else {
                    $.ligerDialog.warn('保存失败！');
                    b = false;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('AJAX数据请求失败！');
                b = false;
            }
        });
        return b;
    }
    else {
        return false;
    }
}
//信息验证
function vailValue() {
    if ($("#Company_Name").val() == "") {
        $.ligerDialog.warn('请选择委托单位！');
        return true;
    }
    if ($("#Company_NameFrim").val() == "") {
        $.ligerDialog.warn('请选择受检单位！');
        return true;
    }
    if (strContratTypeId == "") {
        $.ligerDialog.warn('请填写委托类型！');
        return true;
    }
    if (strAutoYear == "") {
        $.ligerDialog.warn('请选择委托年度！');
        return true;
    }
    if ($("#PROJECT_NAME").val() == "") {
        $.ligerDialog.warn('请填写项目名称！');
        return true;
    }
}
//构造返回URL
function createQueryString() {
    GetPageValue()
    GetPageValueFrim();
    GetPageSelectValue();
    //获取控件的值
    strMethod = isAdd ? "InsertAcceptInfo" : "EditAcceptInfo";
    //alert(strMethod);
    strReqContratId = isAdd ? "" : "&strContratId=" + strContratId + ""
    strParme = strReqContratId + "&strCompanyId=" + strCompanyId + "&strCompanyName=" + strCompanyName + "&strAreaId=" + strAreaId + "&strContactName=" + strContactName + "&strTelPhone=" + strTelPhone + "&strAddress=" + strAddress + "";
    strParme += "&strCompanyIdFrim=" + strCompanyIdFrim + "&strCompanyNameFrim=" + strCompanyNameFrim + "&strAreaIdFrim=" + strAreaIdFrim + "&strContactNameFrim=" + strContactNameFrim + "&strTelPhoneFrim=" + strTelPhoneFrim + "&strAddressFrim=" + strAddressFrim + "";
    strParme += "&strContratType=" + strContratTypeId + "&strContratYear=" + strAutoYear + "&strProjectName=" + $("#PROJECT_NAME").val() + "&strPFYQ=" + $("#txtPFYQ").val() + "&strSZCL=" + $("#txtSZCL").val() + "&strBL=" + $("#txtBL").val() + "&strNUM=" + $("#txtGasNum").val() + "&strtxtRemarks=" + $("#txtRemark").val();
    strParme += "&strRadioInfo=" + $("#rbYS_0")[0].checked + "|" + $("#rbCP_0")[0].checked + "|" + $("#rbWater_0")[0].checked + "|" + $("#rbWaterRun_0")[0].checked + "|" + $("#rbWaterPWK_0")[0].checked + "|" + $("#rbGas_0")[0].checked + "|" + $("#rbGasRun_0")[0].checked + "|" + $("#rbGasPWK_0")[0].checked
    
    //alert(strParme);
    return strParme;
}
//委托书类别信息不可用
function SetInputDisable() {
    if (isAdd == false) {
        if (strStatus == "1" || strStatus == "2") {
            $("#Company_Name").ligerTextBox({ disabled: true });
            $("#Contacts_Name").ligerTextBox({ disabled: true });
            $("#Tel_Phone").ligerTextBox({ disabled: true });
            $("#Address").ligerTextBox({ disabled: true });
            $("#Company_NameFrim").ligerTextBox({ disabled: true });
            $("#Contacts_NameFrim").ligerTextBox({ disabled: true });
            $("#Tel_PhoneFrim").ligerTextBox({ disabled: true });
            $("#AddressFrim").ligerTextBox({ disabled: true });

            $("#PROJECT_NAME").ligerTextBox({ disabled: true });
            $("#txtPFYQ").attr("disabled", "true");
            $("#txtSZCL").attr("disabled", "true");
            $("#txtBL").attr("disabled", "true");
            $("#txtGasNum").attr("disabled", "true");
            $("#txtRemark").attr("disabled", "true");
            $("#rbYS_0")[0].disabled = true;
            $("#rbYS_1")[0].disabled = true;
            $("#rbCP_0")[0].disabled = true;
            $("#rbCP_1")[0].disabled = true;
            $("#rbWater_0")[0].disabled = true;
            $("#rbWater_1")[0].disabled = true;
            $("#rbWaterRun_0")[0].disabled = true;
            $("#rbWaterRun_1")[0].disabled = true;
            $("#rbWaterPWK_0")[0].disabled = true;
            $("#rbWaterPWK_1")[0].disabled = true;
            $("#rbGas_0")[0].disabled = true;
            $("#rbGas_1")[0].disabled = true;
            $("#rbGasRun_0")[0].disabled = true;
            $("#rbGasRun_1")[0].disabled = true;
            $("#rbGasPWK_0")[0].disabled = true;
            $("#rbGasPWK_1")[0].disabled = true;

            $("#dropContractYear").ligerGetComboBoxManager().setDisabled();
        }
    }

    if (isView == true) {
        $("#divContractSubmit").remove();
    }
}

//日期格式化
function TogetDate(date, formart) {
    var strD = "";
    var thisYear = date.getYear();
    thisYear = (thisYear < 1900) ? (1900 + thisYear) : thisYear;
    var thisMonth = date.getMonth() + 1;
    //如果月份长度是一位则前面补0    
    if (thisMonth < 10) thisMonth = "0" + thisMonth;
    var thisDay = date.getDate();
    //如果天的长度是一位则前面补0    
    if (thisDay < 10) thisDay = "0" + thisDay;
    {

        if (formart) {
            strD = thisYear + "年" + thisMonth + "月" + thisDay + '日';
        }
        else {
            strD = thisYear + "-" + thisMonth + "-" + thisDay;
        }
    }
    return strD;
}

function GetContractInfor() {
    
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetAcceptContractInfor&strContratId=" + strContratId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vContractList = data.Rows;
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

//委托企业JS
$(document).ready(function () {
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="添加委托单位"/><span>添加委托单位</span>';
    $(strdivImg).appendTo(divImgCom);

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetDict&type=administrative_area",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAreaItem = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    $("#Area_Name").ligerComboBox({ data: vAreaItem, width: 200, valueFieldID: 'AREA_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });
    
    if (isAdd == false) {
        SetComInputValue();
    }

    //自动检索加载
    if (isAdd == true & cacheSave == false) {
        $("#Company_Name").unautocomplete();

        $("#Company_Name").autocomplete("../MethodHander.ashx?action=GetCompanyInfo",
    {
        max: 12,     // 列表里的条目数 
        minChars: 0,     // 自动完成激活之前填入的最小字符 
        matchContains: true,     // 包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示 
        autoFill: false,     // 自动填充 
        width: 250,
        max: 20,
        dataType: "json",
        scrollHeight: 150,
        parse: function (data) {
            return $.map(eval(data), function (row) {
                return {
                    data: row,
                    value: row.ID + " <" + row.COMPANY_NAME + ">",
                    result: row.COMPANY_NAME
                }
            });
        },
        formatItem: function (item) {
            return item.COMPANY_NAME;
        }
    });
        $("#Company_Name").result(findValueCallback); //加这个主要是联动显示id
        function findValueCallback(event, data, formatted) {
            strCompanyId = data["ID"]; //获取选择的ID
            strCompanyName = data["COMPANY_NAME"];
            strAreaId = data["AREA"];
            strContactName = data["CONTACT_NAME"];
            strTelPhone = data["PHONE"];
            strAddress = data["CONTACT_ADDRESS"];
            SetComInputValue();
        }
    }

    $("#Company_Name").bind("change", function () {
        if ($("#Company_Name").val() == "") {
            ClearValue();
            SetComInputValue();
        }
    });

    function ClearValue() {
        strCompanyId = "";
        strCompanyName = "";
        strContactName = "";
        strAreaId = "";
        strTelPhone = "";
        strAddress = "";
    }

});

function GetPageValue() {
    strCompanyId = strCompanyId.toString();
    strCompanyName = $("#Company_Name").val();
    strContactName = $("#Contacts_Name").val();
    strTelPhone = $("#Tel_Phone").val();
    strAddress = $("#Address").val();
    strAreaId = $("#AREA_OP").val();
}

//设置委托单位为不可编辑
function SetData() {
    $("#Company_Name").unautocomplete();
    $("#Company_Name").ligerTextBox({ disabled: true })
    GetPageValue();
    cacheSave = true;
}

function setBluValue() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetCompanyInfo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    if (strCompanyId == data[i].ID) //获取选择的ID
                    {
                        strCompanyName = data[i].COMPANY_NAME;
                        strAreaId = data[i].AREA;
                        strContactName = data[i].CONTACT_NAME;
                        strTelPhone = data[i].PHONE;
                        strAddress = data[i].CONTACT_ADDRESS;
                        SetComInputValue();
                    }
                }
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}

function SetComInputValue() {
    $("#Company_Name").val(strCompanyName);
    if (isAdd == false) {
        $("#Company_Name").unautocomplete();
        $("#Company_Name").ligerTextBox({ disabled: true });
        $("#Area_Name").ligerGetComboBoxManager().setDisabled();
    }

    if (isView == true) {
        $("#Company_Name").unautocomplete();
        $("#Company_Name").ligerTextBox({ disabled: true });
        $("#Contacts_Name").ligerTextBox({ disabled: true });
        $("#Tel_Phone").ligerTextBox({ disabled: true });
        $("#Address").ligerTextBox({ disabled: true });
        $("#Area_Name").ligerGetComboBoxManager().setDisabled();
    }
    $("#Contacts_Name").val(strContactName);
    $("#Tel_Phone").val(strTelPhone);
    $("#Address").val(strAddress);
    $("#Area_Name").ligerGetComboBoxManager().setValue(strAreaId);
}

//受检企业JS
$(document).ready(function () {
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="添加受检单位"/><span>添加受检单位</span>  ';
    $(strdivImg).appendTo(divImgComFrim);

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetDict&type=administrative_area",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAreaItem = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    //自动检索加载
    if (isAdd == true & cacheSaveFrim == false) {
        $("#Company_NameFrim").unautocomplete();

        $("#Company_NameFrim").autocomplete("../MethodHander.ashx?action=GetCompanyInfo",
    {
        max: 12,     // 列表里的条目数 
        minChars: 0,     // 自动完成激活之前填入的最小字符 
        matchContains: true,     // 包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示 
        autoFill: false,     // 自动填充 
        width: 250,
        max: 20,
        dataType: "json",
        scrollHeight: 150,
        parse: function (data) {
            return $.map(eval(data), function (row) {
                return {
                    data: row,
                    value: row.ID + " <" + row.COMPANY_NAME + ">",
                    result: row.COMPANY_NAME
                }
            });
        },
        formatItem: function (item) {
            return item.COMPANY_NAME;
        }
    }
    );
        $("#Company_NameFrim").result(findValueCallback); //加这个主要是联动显示id


        function findValueCallback(event, data, formatted) {
            strCompanyIdFrim = data["ID"]; //获取选择的ID
            strCompanyNameFrim = data["COMPANY_NAME"];
            strAreaIdFrim = data["AREA"];
            strContactNameFrim = data["CONTACT_NAME"];
            strTelPhoneFrim = data["PHONE"];
            strAddressFrim = data["CONTACT_ADDRESS"];
            SetComFrimInputValue();
        }
    }
    $("#Area_NameFrim").ligerComboBox({ data: vAreaItem, width: 200, valueFieldID: 'AREA_Frim_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });
    if (isAdd == false) {
        SetComFrimInputValue();
    }

    $("#Company_NameFrim").bind("change", function () {
        if ($("#Company_NameFrim").val() == "") {
            ClearValue();
            SetComFrimInputValue();
        }
    })

    $("#btn_Copy").bind("click", function () {
        if ($("#Company_Name").val() == "") {
            $.ligerDialog.warn('【<a style="color:Red; font-size:12px; font-weight:bold;">委托单位</a>】为空,请选择后再复制！');
            return;
        }
        else {
            strCompanyIdFrim = strCompanyId;
            strCompanyNameFrim = strCompanyName;
            $("#Company_NameFrim").val($("#Company_Name").val());
            $("#Contacts_NameFrim").val($("#Contacts_Name").val());
            $("#Tel_PhoneFrim").val($("#Tel_Phone").val());
            $("#AddressFrim").val($("#Address").val());
            $("#Area_NameFrim").ligerGetComboBoxManager().setValue($("#AREA_OP").val());

        }
    });

    function SetComFrimInputValue() {
        $("#Company_NameFrim").val(strCompanyName);
        if (isAdd == false) {
            $("#Company_NameFrim").unautocomplete();
            $("#Company_NameFrim").ligerTextBox({ disabled: true });
            $("#Area_NameFrim").ligerGetComboBoxManager().setDisabled();
            $("#btn_Copy").remove();
        }
        if (isView == true) {
            $("#Company_NameFrim").ligerTextBox({ disabled: true });
            $("#Contacts_NameFrim").ligerTextBox({ disabled: true });
            $("#Tel_PhoneFrim").ligerTextBox({ disabled: true });
            $("#AddressFrim").ligerTextBox({ disabled: true });
            $("#Area_NameFrim").ligerGetComboBoxManager().setDisabled();
            $("#btn_Copy").remove();
        }
        $("#Company_NameFrim").val(strCompanyNameFrim);
        $("#Contacts_NameFrim").val(strContactNameFrim);
        $("#Tel_PhoneFrim").val(strTelPhoneFrim);
        $("#AddressFrim").val(strAddressFrim);
        $("#Area_NameFrim").ligerGetComboBoxManager().setValue(strAreaIdFrim);
    }

    function ClearValue() {
        strCompanyIdFrim = "";
        strCompanyNameFrim = "";
        strContactNameFrim = "";
        strAreaIdFrim = "";
        strTelPhoneFrim = "";
        strAddressFrim = "";
    }

    //设置委托单位为不可编辑
    function SetData() {
        $("#Company_NameFrim").unautocomplete();
        $("#Company_NameFrim").ligerTextBox({ disabled: true });
        cacheSaveFrim = true;
    }
});

function GetPageValueFrim() {
    strCompanyIdFrim = strCompanyId.toString();
    strCompanyNameFrim = $("#Company_NameFrim").val();
    strContactNameFrim = $("#Contacts_NameFrim").val();
    strTelPhoneFrim = $("#Tel_PhoneFrim").val();
    strAddressFrim = $("#AddressFrim").val();
    strIndustryIdFrim = $("#INDUSTRY_Frim_OP").val();
    strAreaIdFrim = $("#AREA_Frim_OP").val();
}


//设置委托单位为不可编辑
function SetFrimData() {
    $("#Company_NameFrim").unautocomplete();
    $("#Company_NameFrim").ligerTextBox({ disabled: true });
    cacheSaveFrim = true;
}

function setBluFrimValue() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetCompanyInfo",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                for (var i = 0; i < data.length; i++) {
                    if (strCompanyIdFrim == data[i].ID) //获取选择的ID
                    {
                        strCompanyIdFrim = data[i].ID;
                        strCompanyNameFrim = data[i].COMPANY_NAME;
                        strAreaIdFrim = data[i].AREA;
                        strContactNameFrim = data[i].CONTACT_NAME;
                        strTelPhoneFrim = data[i].PHONE;
                        strAddressFrim = data[i].CONTACT_ADDRESS;
                        SetComFrimInputValue();
                    }
                }
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}

//发送保存
function SendSave() {
    boolSave = false;
    if (SaveData()) {
        $("#hidBtnType").val("send");
        if (vContractList != null) {
            $("#hidCompanyId").val(vContractList[0].TESTED_COMPANY_ID);
        }
        //防止多次点击按钮产生多个任务
        $("#divContractSubmit")[0].style.display = "none";
        return true;
    }
    else {
        return false;
    }
}
//保存
function Save() {
    boolSave = true;
    SaveData();
    return false;
}

function BackSend() {
    $("#hidBtnType").val("back");
}