//验收监测委托书录入 Create by SSZ
var managerContractType; //选择委托类型
var managerContractInfo; //委托书信息
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
    //状态判断
    strType = $.getUrlVar('type');
    strWf = $.getUrlVar('WF_ID');
    if (strType == "true") {
        isAdd = true;
    }
    else {
        isAdd = false;
        strContratId = $.getUrlVar('strContratId');
        strView = $.getUrlVar('view');
        GetContractInfor();
        if (vContractList != null) {
            strContractCode = vContractList[0].CONTRACT_CODE;
        }
        if (strView == "true") {
            isView = true;
        }
    }
    //获取核对委托信息的 备注信息
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetDict&type=Contract_Remarks",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vRItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.error('Ajax加载数据失败！' + msg);
        }
    });
    //获取核对委托信息的 报告领取方式
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetDict&type=RPT_WAY",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vRptItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.error('Ajax加载数据失败！' + msg);
        }
    });
    //构造委托书类别信息表单
    $("#divContractType").ligerForm({
        inputWidth: 160, labelWidth: 80, space: 0, labelAlign: 'right',
        fields: [
        { display: "委托类型", name: "CONTRACT_TYPE", newline: true, width: 300, type: "select", group: "选择委托类型", groupicon: groupicon, comboboxName: "dropContractType", options: { valueFieldID: "CONTRACT_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "ContractInfor_QuicklyAcceptance.aspx?type=GetDict&dict_type=Contract_Type"} },
        { display: "委托年度", name: "CONTRACT_YEAR", newline: false, type: "select", comboboxName: "dropContractYear", options: { isMultiSelect: false, valueFieldID: "CONTRACT_YEAR", valueField: "ID", textField: "YEAR", url: "ContractInfor_QuicklyAcceptance.aspx?type=GetContratYear"} },
        { display: "监测类型", name: "TEST_TYPE", newline: true, width: 300, type: "select", comboboxName: "dropTestType", options: { isShowCheckBox: true, isMultiSelect: true, valueFieldID: "TEST_TYPE", valueField: "ID", textField: "MONITOR_TYPE_NAME", url: "ContractInfor_QuicklyAcceptance.aspx?type=GetMonitorType"} },
        { display: "委托费用", name: "CONTRACT_FEE", newline: false, type: "text" }
        ]
    });
    //构造委托书信息表单
    $("#divContractInfo").ligerForm({
        inputWidth: 160, labelWidth: 100, space: 90, labelAlign: 'right',
        fields: [
        { display: "项目名称", name: "PROJECT_NAME", width: 300, newline: true, type: "text", group: "核对委托书信息", groupicon: groupicon },
        { display: "签订日期", name: "CONTRACT_DATE", newline: false, type: "date" },
        { display: "监测目的", name: "TEST_PURPOSE", width: 300, newline: true, type: "text" },
        { display: "报告领取", name: "RPT_TYPE", newline: false, type: "select", comboboxName: "dropRptType", options: { valueFieldID: "RPT_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vRptItems, initValue: SetRptboxInitValue()} },
        { display: "备注", name: "REMARK", width: 650, newline: true, type: "select", comboboxName: "dropRemark", options: { isShowCheckBox: true, isMultiSelect: true, valueFieldID: "REMARK", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vRItems, initValue: SetboxInitValue()} }
        ]
    });
    //监测ID寄存
    $("#CONTRACT_ID").val(strContratId);
    //默认赋值 委托类型为验收监测
    $("#CONTRACT_TYPE").val($("#Contract_Type").val());
    $("#dropContractType").ligerGetTextBoxManager().setDisabled();
    //签订日期默认为当天
    if (strContract_Date != "" && strContract_Date != null) {
        $("#CONTRACT_DATE").val(strContract_Date);
    }
    else {
        $("#CONTRACT_DATE").val(TogetDate(new Date(), ""));
    }
    //报告单号
    if (strContractCode != "") {
        $("#Contract_Code").html(strContractCode);
    }
    else {
        $("#Contract_Code").html('委托单号尚未生成');
    }
    //备注
    if (strRemarktData != "" && strRemarktData != null) {
        $("#REMARK").val(strRemarktData);
    }
    //监测费用
    $.post("ContractInfor_QuicklyAcceptance.aspx?type=getContractFee&contract_id=" + strContratId, function (data) {
        if (data != null) {
            strContractFee = data;
            //费用
            $("#CONTRACT_FEE").val(strContractFee);
        }
    });

    //监测类型确认
    $("#btn_OkSelect").bind("click", function () {
        //先检查委托单位信息，如果不存在根据用户需求 进行是否增加
        if (isAdd) {
            InsertCompanyInfor();
        }
        else {
            GetPageSelectValue();
            SaveData();
        }
    });
    if (isAdd == false) {
        SetInputValue();
    }
});

function GetPageSelectValue() {
    strContratTypeId = $("#CONTRACT_TYPE").val();
    strAutoYear = $("#CONTRACT_YEAR").val();
    strMonitorTypeId = $("#TEST_TYPE").val();
    strContractFee = $("#CONTRACT_FEE").val();
}

//后期做统一保存
function SaveData() {
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
                        strContratId = data;
                        isAdd = false;
                        cacheSelectSave = true;
                        SetInputDisable();
                        ShowNextPage();
                        return;
                    }
                    else {
                        if (data == "True") {
                            $.ligerDialog.success('保存成功！');
                            SetInputDisable();
                            return;
                        }
                        else {
                            $.ligerDialog.warn('保存失败！');
                            return;
                        }
                    }
                }
                else {
                    $.ligerDialog.warn('保存失败！');
                    return;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('AJAX数据请求失败！');
                return;
            }
        });
    }
}
//信息验证
function vailValue() {
    if (strCompanyName == "") {
        $.ligerDialog.warn('请选择委托单位！');
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
    if (strMonitorTypeId == "") {
        $.ligerDialog.warn('请选择监测类别！');
        return true;
    }
    if (strMonitorTypeId == "") {
        $.ligerDialog.warn('请填写监测费用！');
        return true;
    }
}
//构造返回URL
function createQueryString() {
    GetPageValue()
    GetPageValueFrim();
    //获取控件的值
    strMethod = isAdd ? "InsertInfo" : "EditInfo";
    strReqContratId = isAdd ? "" : "&strContratId=" + strContratId + ""
    strParme = strReqContratId + "&strCompanyId=" + strCompanyId + "&strCompanyName=" + strCompanyName + "&strIndustryId=" + strIndustryId + "&strAreaId=" + strAreaId + "&strContactName=" + strContactName + "&strTelPhone=" + strTelPhone + "&strAddress=" + strAddress + "";
    strParme += "&strCompanyIdFrim=" + strCompanyIdFrim + "&strCompanyNameFrim=" + strCompanyNameFrim + "&strIndustryIdFrim=" + strIndustryIdFrim + "&strAreaIdFrim=" + strAreaIdFrim + "&strContactNameFrim=" + strContactNameFrim + "&strTelPhoneFrim=" + strTelPhoneFrim + "&strAddressFrim=" + strAddressFrim + "";
    strParme += "&strContratType=" + strContratTypeId + "&strContratYear=" + strAutoYear + "&strMonitroType=" + strMonitorTypeId + "&strContractFee=" + strContractFee + "&strBookType=1";
    return strParme;
}
//委托书类别信息不可用
function SetInputDisable() {
    if (isAdd == false) {
        $("#dropContractYear").ligerGetComboBoxManager().setDisabled();
        $("#dropTestType").ligerGetComboBoxManager().setDisabled();
        $("#CONTRACT_FEE").ligerGetTextBoxManager().setDisabled();
        $("#btn_OkSelect").remove();
    }

    if (isView == true) {
        $("#btn_OkSelect").remove();
        $("#divContractSubmit").remove();
    }
}
//动态生成委托书信息
function ShowNextPage() {
    if (!strCreated) {
        CreateDiv();
        strCreated = true;
    }
    //移除style display:none 属性 显示核对委托书信息 DIV层
    $("#divContractInfo").attr("style", "");
    $("#divContractPlan").attr("style", "");
    $("#divContractCode").attr("style", "");
    $("#divContratSubmit").attr("style", "");
}
//构造委托书确认模块
function CreateDiv() {
    //填充核对委托书信息 委托项目
    var strYear = $("#CONTRACT_YEAR").val();
    var strMonitorTypeName = $("#CONTRACT_TYPE").val();
    //项目生成
    if (strProjectName != "" && strProjectName != null) {
        $("#PROJECT_NAME").val(strProjectName);
    }
    else {
        $("#PROJECT_NAME").val(strCompanyName + strYear + "年度验收监测");
    }
    //监测目的生成
    if (strMonitor_Purpose != "" && strMonitor_Purpose != null) {
        $("#TEST_PURPOSE").val(strMonitor_Purpose);
    }
    else {
        $("#TEST_PURPOSE").val("受" + strCompanyName + "的委托对" + strCompanyNameFrim + "进行验收监测。");
    }
}

//备注信息填充
function SetboxInitValue() {
    var strValue = "";
    if (isAdd == true) {
        for (var i = 0; i < vRItems.length; i++) {
            strValue += vRItems[i].DICT_CODE + ";";
        }
        strValue = strValue.substring(0, strValue.length - 1);
    }
    else {
        strValue = strRemarktData;
    }
    return strValue;
}
//报告领取方式填充
function SetRptboxInitValue() {
    var strValue = "";
    if (isAdd == true) {
        strValue = vRptItems[0].DICT_CODE;
    }
    else
        strValue = strRpt_Way;
    return strValue;
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

function SaveCheckContractInfor() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=SaveCheckContractInfor&strQuck=1&strStatus=9" + GetCheckContractInfor(),
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strContractCode = data;
                if (strContractCode.length > 0) {
                    $("#Contract_Code").html(strContractCode.split('|')[0]);
                }
                // $("#btnSave").attr("disabled", "disabled");
                if (boolSave == true) {
                    $.ligerDialog.confirm('数据保存成功,是否跳转到委托书列表页面！', function (result) {
                        if (result == true) {
                            var surl = '../Channels/Mis/Contract/ContractList_Quickly.aspx';
                            top.f_overTab('新增委托书', surl);
                        } else {
                            return;
                        }
                    });
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
//构造保存表单信息
function GetCheckContractInfor() {
    var strRequestData = "";
    strRequestData += '&strProjectName=' + encodeURI($("#PROJECT_NAME").val());
    strRequestData += '&strContract_Date=' + encodeURI($("#CONTRACT_DATE").val());
    strRequestData += '&strRpt_Way=' + $("#RPT_TYPE").val();
    var strmark = "";
    strmark = $("#REMARK").val().split(';');
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
    strRequestData += '&strMonitor_Purpose=' + encodeURI($("#TEST_PURPOSE").val());
    //使用其他页面变量
    strRequestData += '&strContratId=' + strContratId + '&strCompanyIdFrim=' + strCompanyIdFrim + '&strContratType=' + strContratTypeId + '&strContratYear=' + strAutoYear + '&strMonitroType=' + strMonitorTypeId + '&strSelectTabMonitorTypeId=';
    return strRequestData;
}

//编辑状态赋值 补充
function SetInputValue() {
    if (vContractList == null) {
        $.ligerDialog.warn('数据获取失败！');
        return;
    }
    strContratTypeId = "", strAutoYear = "", strMonitorTypeId = "";
    //监测类型
    strContratTypeId = vContractList[0].CONTRACT_TYPE;
    //年度
    strAutoYear = vContractList[0].CONTRACT_YEAR;
    $("#CONTRACT_YEAR").val(strAutoYear);
    //监测类别
    strMonitorTypeId = vContractList[0].TEST_TYPES;
    $("#TEST_TYPE").val(strMonitorTypeId);

    strMonitor_Purpose = "", strRemarktData = "", strProjectName = "";
    strMonitor_Purpose = decodeURIComponent(vContractList[0].TEST_PURPOSE); //监测目的
    strProjectName = decodeURIComponent(vContractList[0].PROJECT_NAME); //监测名称
    SetInputDisable(); //控件不可用
    ShowNextPage();
}

function GetContractInfor() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetContractInfor&strContratId=" + strContratId,
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

//新增委托企业信息
function InsertCompanyInfor() {
    strCompanyName = $("#Company_Name").val();
    if (strCompanyName == "" || strCompanyName == "请选择委托企业") {
        $.ligerDialog.warn('委托企业不能为空！');
        return;
    }
    checkIsExistValue();
    if (strCompanyId == "") {
        $.ligerDialog.confirm('当前【<a style="color:Red; font-size:12px; font-weight:bold;">委托企业</a>】不存在，是否新增？\r\n', function (result) {
            if (result) {
                GetPageValue();
                InsertCompanyValue();
                if (strCompanyId != "") {
                    GetPageValue();
                    SetData();
                    //如果受检企业不是选择的 那么其strCompanyIdFrim为空，则提示是否新增受检企业信息，如果是 则新增加
                    InsertCompanyFrimInfor();
                }
            }
            else {
                return;
            }
        });
    }
    else {
        GetPageValue();
        setBluValue();
        SetData();
        InsertCompanyFrimInfor();
    }
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
    $("#btn_Ok").bind("click", function () {
        strCompanyName = $("#Company_Name").val();
        if (strCompanyName == "") {
            $.ligerDialog.warn('委托企业不能为空！');
            return;
        }
        checkIsExistValue();
        if (strCompanyId == "") {
            $.ligerDialog.confirm('当前委托企业不存在，是否新增？\r\n', function (result) {
                if (result) {
                    GetPageValue();
                    InsertCompanyValue();
                    if (strCompanyId != "") {
                        GetPageValue();
                        SetData();
                    }
                }
                else {
                    return;
                }
            });
        }
        else {
            GetPageValue();
            setBluValue();
            SetData();
        }
    });

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
        strIndustryId = "";
        strTelPhone = "";
        strAddress = "";
    }

    function InsertCompanyValue() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: encodeURI("../MethodHander.ashx?action=InsertCompany&strCompanyName=" + strCompanyName + "&strAreaId=" + strAreaId + "&strContactName=" + strContactName + "&strTelPhone=" + strTelPhone + "&strAddress=" + strAddress + ""),
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    strCompanyId = data;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('AJAX数据请求失败！');
                return;
            }
        });
    }
});

function checkIsExistValue() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: encodeURI("../MethodHander.ashx?action=checkCompany&strCompanyName=" + strCompanyName + ""),
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                isExistCompany = true;
                strCompanyId = data;
                return strCompanyId;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('AJAX数据请求失败！');
            return;
        }
    });
}
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
        $("#btn_Ok").remove();
    }

    if (isView == true) {
        $("#Company_Name").unautocomplete();
        $("#Company_Name").ligerTextBox({ disabled: true });
        $("#Contacts_Name").ligerTextBox({ disabled: true });
        $("#Tel_Phone").ligerTextBox({ disabled: true });
        $("#Address").ligerTextBox({ disabled: true });
        $("#Area_Name").ligerGetComboBoxManager().setDisabled();
        $("#btn_Ok").remove();
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
            strIndustryIdFrim = data["INDUSTRY"];
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
        if (strCompanyName == "") {
            $.ligerDialog.warn('【<a style="color:Red; font-size:12px; font-weight:bold;">委托单位</a>】为空,请选择后再复制！');
            return;
        }
        else {
            $("#Company_NameFrim").val(strCompanyName);
            $("#Contacts_NameFrim").val(strContactName);
            $("#Tel_PhoneFrim").val(strTelPhone);
            $("#AddressFrim").val(strAddress);
            $("#Area_NameFrim").ligerGetComboBoxManager().setValue(strAreaId);
        }
    });

    function SetComFrimInputValue() {
        $("#Company_NameFrim").val(strCompanyName);
        if (isAdd == false) {
            $("#Company_NameFrim").unautocomplete();
            $("#Company_NameFrim").ligerTextBox({ disabled: true });
            $("#btn_OkFrim").remove();
            $("#btn_Copy").remove();
        }
        if (isView == true) {
            $("#Company_NameFrim").ligerTextBox({ disabled: true });
            $("#Contacts_NameFrim").ligerTextBox({ disabled: true });
            $("#Tel_PhoneFrim").ligerTextBox({ disabled: true });
            $("#AddressFrim").ligerTextBox({ disabled: true });
            $("#Area_NameFrim").ligerGetComboBoxManager().setDisabled();
            $("#btn_OkFrim").remove();
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
        strIndustryIdFrim = "";
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

//新增受检企业信息
function InsertCompanyFrimInfor() {

    strCompanyNameFrim = $("#Company_NameFrim").val();
    if (strCompanyNameFrim == "" || strCompanyNameFrim == "请选择受检企业") {
        $.ligerDialog.warn('请选择受检企业！');
        return;
    }
    checkIsExistFrimValue();
    if (strCompanyIdFrim == "") {
        $.ligerDialog.confirm('当前【<a style="color:Red; font-size:12px; font-weight:bold;">受检企业</a>】不存在，是否新增？\r\n', function (result) {
            if (result) {
                GetPageValueFrim();
                InsertCompanyFrimValue();
                if (strCompanyIdFrim != "") {
                    GetPageValueFrim();
                    SetFrimData();
                    GetPageSelectValue();
                    SaveData();
                }
            }
            else {
                return;
            }
        })
    }

    else {
        GetPageValueFrim();
        SetFrimData();
        setBluFrimValue();

        //如果添加成功，则检查委托企业
        GetPageSelectValue();
        SaveData();
    }
}

function checkIsExistFrimValue() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: encodeURI("../MethodHander.ashx?action=checkCompany&strCompanyName=" + strCompanyNameFrim + ""),
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                isExistCompanyFrm = true;
                strCompanyIdFrim = data;
                return strCompanyIdFrim;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('AJAX数据请求失败！');
            return;
        }
    });
}

function InsertCompanyFrimValue() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: encodeURI("../MethodHander.ashx?action=InsertCompany&strCompanyName=" + strCompanyNameFrim + "&strIndustryId=" + strIndustryIdFrim + "&strAreaId=" + strAreaIdFrim + "&strContactName=" + strContactNameFrim + "&strTelPhone=" + strTelPhoneFrim + "&strAddress=" + strAddressFrim + ""),
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strCompanyIdFrim = data;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('AJAX数据请求失败！');
            return;
        }
    });
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
                        strIndustryIdFrim = data[i].INDUSTRY;
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
    if (strContratId == "") {
        $.ligerDialog.success('请确认以上录入信息！'); return;
    }
    boolSave = true;
    SaveCheckContractInfor();
    //修改委托书状态
    ChangeStatus();
}
//保存
function Save() {
    if (strContratId == "") {
        $.ligerDialog.success('请确认以上录入信息！'); return;
    }
    //检查项目名称为空
    if ($("#PROJECT_NAME").val() == "") {
        $.ligerDialog.success('请填写项目名称！'); return;
    }
    boolSave = true;
    SaveCheckContractInfor();
    return false;
}

//修改委托书状态
function ChangeStatus() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "ContractInfor_QuicklyAcceptance.aspx?type=changeStatus&strContratId=" + strContratId,
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != null) {
                //委托书ID
                $("#CONTRACT_ID").val(data);
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        }
    });
}