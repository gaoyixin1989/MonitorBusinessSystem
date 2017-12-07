//流程变量
var wf_inst_task_id = "", wf_inst_id = "", wf_id = "", vFlowInfo = null;
var isAdd = true,  isView=false, strView="",strType = "", strWf = "", strWFfirst = "", strIsWfStart = "1";
var rowdata = "", strContractCode = "", strIdShow = "";
var ClientCompanyInfor = null, TestedCompanyInfor = null,vContractList = null;
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

    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: 1200 });
    strIdShow = $.getUrlVar('idShow');
    strType = $.getUrlVar('type');
    strContratId = $.getUrlVar('strContratId');

    if (!strType) {

        if (strContratId != "") {
            $("#hidTaskId").val(strContratId);
            GetContractInfor();
            if (vContractList != null) {
                vContractList[0].CONTRACT_CODE;
            }
        }
        else {
            $.ligerDialog.warn('业务参数错误！'); return;
        }
        SetVauleModify();
    }
    else {

        if (strType == "true") {
            isAdd = true;
        }
        else {
            isAdd = false;
            strContratId = $.getUrlVar('strContratId');
            strView = $.getUrlVar('view');
            GetContractInfor();
            if (vContractList != null) {
                vContractList[0].CONTRACT_CODE;
            }
            if (strView == "true") {
                isView = true;
                $("#divContratSubmit").remove();
            }
            SetVauleModify();
        } 
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


    //初始化加载修改条件下的数据
    function SetVauleModify() {
        //alert($.getUrlVar('ID'))
        //如果为修改状态，则根据委托书ID 获取委托书企业信息 并填充
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../MethodHander.ashx?action=GetContractCompanyInfor&strContratId=" + strContratId + "&strCompanyId=" + vContractList[0].CLIENT_COMPANY_ID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    ClientCompanyInfor = data.Rows;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });


        //alert($.getUrlVar('ID'))
        //如果为修改状态，则根据委托书ID 获取委托书企业信息 并填充
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../MethodHander.ashx?action=GetContractCompanyInfor&strContratId=" + strContratId + "&strCompanyIdFrim=" + vContractList[0].TESTED_COMPANY_ID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    TestedCompanyInfor = data.Rows;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
        SetInputValue();
    }


    function SetInputValue() {

        if (ClientCompanyInfor.length > 0) {
            strCompanyId = "", strCompanyName = "", strIndustryId = "", strAreaId = "", strContactName = "", strTelPhone = "", strAddress = "";
            strCompanyId = vContractList[0].CLIENT_COMPANY_ID;
            strCompanyName = ClientCompanyInfor[0].COMPANY_NAME;
            strContactName = ClientCompanyInfor[0].CONTACT_NAME;
            strTelPhone = ClientCompanyInfor[0].PHONE;
            strAddress = ClientCompanyInfor[0].CONTACT_ADDRESS;
            strIndustryId = ClientCompanyInfor[0].INDUSTRY;
            strAreaId = ClientCompanyInfor[0].AREA;
        }
        else {
            $.ligerDialog.warn('该委托书委托企业数据信息不完整'); return;
        }
        if (TestedCompanyInfor.length > 0) {
            strCompanyIdFrim = "", strCompanyNameFrim = "", strIndustryIdFrim = "", strAreaIdFrim = "", strContactNameFrim = "", strTelPhoneFrim = "", strAddressFrim = "";

            strCompanyIdFrim = vContractList[0].TESTED_COMPANY_ID;
            strCompanyNameFrim = TestedCompanyInfor[0].COMPANY_NAME;
            strContactNameFrim = TestedCompanyInfor[0].CONTACT_NAME;
            strTelPhoneFrim = TestedCompanyInfor[0].PHONE;
            strAddressFrim = TestedCompanyInfor[0].MONITOR_ADDRESS;
            strIndustryIdFrim = TestedCompanyInfor[0].INDUSTRY;
            strAreaIdFrim = TestedCompanyInfor[0].AREA;
        }
        else {
            $.ligerDialog.warn('该委托书受检企业数据信息不完整'); return;
        }
        strContratTypeId = "", strAutoYear = "", strMonitorTypeId = "";
        strContratTypeId = vContractList[0].CONTRACT_TYPE;
        strAutoYear = vContractList[0].CONTRACT_YEAR;
        strMonitorTypeId = vContractList[0].TEST_TYPES;

        strRpt_Way = ""; strRemarks1 = "", strRemarks2 = "", strRemarks3 = "", strRemarks4 = "", strContract_Date = "", strMonitor_Purpose = "", strRemarktData = "";
        strRpt_Way = vContractList[0].RPT_WAY;
        strRemarks1 = vContractList[0].AGREE_OUTSOURCING;
        strRemarks2 = vContractList[0].AGREE_METHOD;
        strRemarks3 = vContractList[0].AGREE_NONSTANDARD;
        strRemarks4 = vContractList[0].AGREE_OTHER;

        if (strRemarks1 == '1') {
            strRemarktData += 'accept_subpackage' + ';';
        }
        if (strRemarks2 == '1') {
            strRemarktData += 'accept_useMonitorMethod' + ';';
        }
        if (strRemarks3 == '1') {
            strRemarktData += 'accept_usenonstandard' + ';';
        }
        if (strRemarks4 == '1') {
            strRemarktData += 'accept_other' + ';';
        }
        strRemarktData = strRemarktData.substring(0, strRemarktData.length - 1);
        strMonitor_Purpose = decodeURIComponent(vContractList[0].TEST_PURPOSE);
        //处理 日期函数
        var v = vContractList[0].ASKING_DATE;
        if (v == "") return;
        var createDate = new Date(Date.parse(v.replace(/-/g, '/')));
        var strData = createDate.getFullYear() + "-";
        strData += (createDate.getMonth() + 1) + "-";
        strData += createDate.getDate();

        strContract_Date = strData;
    }
});