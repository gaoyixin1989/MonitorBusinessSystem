﻿// Create by 苏成斌 2012.12.12  "采样-现状信息"

var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var strUrl = "SampleLocale.aspx"
var WeatherValue = [];
var AttributeTypeList = [];
var obj;
var strSubtaskID = "";

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
    strSubtaskID = $.getUrlVar('strSubtaskID');
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=getWeatherInfo&strSubtaskID=" + strSubtaskID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            AttributeTypeList = data;
        }
    });
    //获取任务对应的动态属性值

    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=getWeatherValue&strSubtaskID=" + strSubtaskID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            WeatherValue = data;
        }
    });


    obj = $("#divWeather").ligerForm({ inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right', fields: [] });

    managertmp = $.ligerui.managers.divWeather;
    initAttributeControl();
    initAttributeControlValue();

});
//根据指定的动态属性，初始化动态属性控件
function initAttributeControl() {
    var dataJson = GetAttributeControlJson(AttributeTypeList);

    $("#divWeather").html("");

    $.ligerui.managers["divWeather"] = [];
    $.ligerui.managers["divWeather"] = managertmp;

    var obj1 = $("#divWeather").ligerForm(dataJson);
    obj1._render();
}
//根据指定的动态属性集合，生成json，以动态生成控件
function GetAttributeControlJson(data) {
    var strdata = { inputWidth: 160, labelWidth: 100, space: 90, labelAlign: 'right', fields: [] };

    if (data.length > 0) {
        for (i = 0; i < data.length; i++) {
            var strNewLine = false;
            var strdata1 = "";
            if (i % 2 == 0) {
                strNewLine = true;
            }
            else {
                strNewLine = false;
            }

            if (data[i].DICT_TEXT == "有雨" || data[i].DICT_TEXT == "天气" || data[i].DICT_TEXT == "采样方式" || data[i].DICT_TEXT == "采样方法依据" || data[i].DICT_TEXT == "声级计型号") {
                strdata1 += "{ display: '" + data[i].DICT_TEXT + "',name: '" + data[i].DICT_CODE + "-1', newline:" + strNewLine + ", width: 160, type: 'select',";
                strdata1 += "comboboxName: '" + data[i].DICT_CODE + "',options:";

                if (data[i].DICT_TEXT == "有雨") {
                    strdata1 += "{ data: [{ text: '是', id: '0' }, { text: '否', id: '1'}]}";
                }
                if (data[i].DICT_TEXT == "天气") {
                    strdata1 += "{ data: [{ text: '晴', id: '0' }, { text: '阴', id: '1'}, { text: '多云', id: '2'}, { text: '雨', id: '3'}]}";
                }
                if (data[i].DICT_TEXT == "采样方式") {
                    strdata1 += "{ data: [{ text: '手工瞬时', id: '0' }, { text: '动压平衡法', id: '1'}]}";
                }
                if (data[i].DICT_TEXT == "采样方法依据" && data[i].DICT_TYPE == "noise_weather") {
                    strdata1 += "{ data: [{ text: 'GB 12348-2008', id: '0' }, { text: 'GB 22337-2008', id: '1'}]}";
                }
                if (data[i].DICT_TEXT == "采样方法依据" && data[i].DICT_TYPE == "gerenal_weather") {
                    strdata1 += "{ data: [{ text: 'HJ 494-2009', id: '0' }, { text: 'GB/T 16844-1996', id: 'GB/T 16844-1996'}]}";
                }
                if (data[i].DICT_TEXT == "声级计型号" && data[i].DICT_TYPE == "noise_weather") {
                    strdata1 += "{ data: [{ text: '噪声统计分析仪（AWA6218B）', id: '0' }, { text: '多功能声级计(AWA6228)', id: '1'}]}";
                }
                if (i == 0)
                    strdata1 += ", group: '现状信息', groupicon: groupicon";
                strdata1 += "}";
            }
            else {
                strdata1 += "{ display: '" + data[i].DICT_TEXT + "', name: '" + data[i].DICT_CODE + "', newline: " + strNewLine + ", width: 160, type: 'text'";
                if (i == 0)
                    strdata1 += ", group: '现状信息', groupicon: groupicon";
                strdata1 += "}";
            }
            var strJsonData = eval('(' + strdata1 + ')');
            strdata.fields.push(strJsonData);
        }
    }
    return strdata;
}
//给动态属性控件赋值
function initAttributeControlValue() {
    for (i = 0; i < AttributeTypeList.length; i++) {
        for (j = 0; j < WeatherValue.length; j++) {
            if (AttributeTypeList[i].DICT_CODE == WeatherValue[j].WEATHER_INFO) {
                $("#" + AttributeTypeList[i].DICT_CODE).val(WeatherValue[j].WEATHER_INFO);
            }
        }
    }
}
//得到动态属性保存参数
function GetAttrStr() {
    var strData = "";

    if (AttributeTypeList.length > 0) {
        for (i = 0; i < AttributeTypeList.length; i++) {
            var strdata1 = "";
            strdata1 += AttributeTypeList[i].DICT_CODE + ":" + $("#" + AttributeTypeList[i].DICT_CODE).val();

            strData += (strData.length > 0 ? ";" : "") + strdata1;
        }
    }
    if (strData.length > 0) {
        strData = "'strAttribute':'" + strData + "'";
    }
    else
        strData = "'strAttribute':''";

    return strData;
}

//得到保存信息
function getSaveDate() {
    var strData = GetAttrStr();
    return strData;
}

//得到动态属性保存参数
function GetAttrStr() {
    var strData = "";

    var newData = new Array();
    for (i = 0; i < AttributeTypeList.length; i++) {
        newData.push(AttributeTypeList[i]);
    }

    if (newData.length > 0) {
        for (i = 0; i < newData.length; i++) {
            var strdata1 = "";
            strdata1 += newData[i].DICT_CODE + "|" + $("#" + newData[i].DICT_CODE).val();

            strData += (strData.length > 0 ? "-" : "") + strdata1;
        }
    }
    if (strData.length > 0) {
        strData = ",'strAttribute':'" + strData + "'";
    }
    else
        strData = "'strAttribute':''";

    return strData;
}

//委托书信息
$(document).ready(function () {
    //委托书信息
    $.post('Sampling.aspx?type=getContractInfo&strSubtaskID=' + $("#SUBTASK_ID").val(), function (data) {
        SetContractInfo(data);

        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "Sampling.aspx/getEntire_QC",
            data: "{'strSubTaskId':'" + $("#SUBTASK_ID").val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                $("#ENTIRE_QC").val(data.d);
                $("#ENTIRE_QC").ligerGetComboBoxManager().setDisabled();
            }
        });

    }, 'json');
});
//初始委托书信息模块
function SetContractInfo(data) {
    objOneFrom = $("#oneFrom").ligerForm({
        inputWidth: 160, labelWidth: 100, space: 30, labelAlign: 'right',
        fields: [
                { display: "合同编号", name: "CONTRACT_CODE", newline: true, type: "text", width: 180, group: "委托信息", groupicon: groupicon },
                { display: "监测类型", name: "MONITOR_ID", newline: false, width: 150, type: "text" },
                { display: "受检单位", name: "TESTED_COMPANY_ID", newline: true, width: 180, type: "text" },
                { display: "委托类型", name: "CONTRACT_TYPE", newline: false, width: 150, type: "text" },
                { display: "联系人", name: "CONTACT_NAME", newline: true, width: 180, type: "text" },
                { display: "联系电话", name: "LINK_PHONE", newline: false, width: 150, type: "text" },
                { display: "采样日期", name: "SAMPLE_ASK_DATE", space: 30, newline: true, width: 180, type: "date" },
                { display: "要求完成日期", name: "SAMPLE_FINISH_DATE", newline: false, width: 150, type: "date" },
                { display: "采样负责人", name: "SAMPLING_MANAGER_ID", newline: true, width: 180, type: "text" },
                { display: "采样人员", name: "SAMPLING_MAN", newline: false, width: 150, type: "text" },
                { display: "是否全程质控", name: "ENTIRE_QC", newline: true, width: 180, type: "text" }
               ]
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
    //huangjinjun 20140509
    //$("#SAMPLE_ASK_DATE").ligerGetComboBoxManager().setDisabled();
    //end
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