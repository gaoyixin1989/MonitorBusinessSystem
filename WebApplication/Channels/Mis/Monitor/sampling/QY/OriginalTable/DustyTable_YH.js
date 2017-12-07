/// <summary>
/// 创建原因：实现烟气黑度动态属性（原始记录表）数据保存功能-- 也可根据配置配置其他
/// 创建人：魏林
/// 创建时间：2014-11-12 11：10
/// </summary>
var groupicon = "../../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strUrl = "DustyTable_YH.aspx";
var strID = "";
var intSelectCount = 1;
var strAttrID = "000000208", strAttrName = "";
var AttributeTypeAndInfoLst = [], PointValue = [];
var managertmp;
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
    strID = $.getUrlVar('strID');

    //获取所有属性类别及属性信息关联数据
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../SamplePointEdit.aspx?type=GetAttrrbute",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            AttributeTypeAndInfoLst = data;
        }
    });
    //获取点位对应的动态属性值
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../SamplePointEdit.aspx?type=GetAttrValue&strid=" + strID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            PointValue = data;
        }
    });

    managertmp = $.ligerui.managers.divAttr;

    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../SamplePointEdit.aspx?type=loadData&strid=" + strID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.DYNAMIC_ATTRIBUTE_ID != "") {
                initAttributeControl(data.DYNAMIC_ATTRIBUTE_ID);
                initAttributeControlValue(data.DYNAMIC_ATTRIBUTE_ID);
            }
            else {
                initAttributeControl(strAttrID);
                initAttributeControlValue(strAttrID);
            }
        }
    });
})

//根据指定的动态属性，初始化动态属性控件
function initAttributeControl(strDYNAMIC_ATTRIBUTE_ID) {
    var newData = new Array();
    for (i = 0; i < AttributeTypeAndInfoLst.length; i++) {
        if (AttributeTypeAndInfoLst[i].ATTRIBUTE_TYPE_ID == strDYNAMIC_ATTRIBUTE_ID) {
            newData.push(AttributeTypeAndInfoLst[i]);
        }
    }
    var dataJson = GetAttributeControlJson(newData, intSelectCount);

    $("#divAttr").html("");

    $.ligerui.managers["divAttr"] = [];
    $.ligerui.managers["divAttr"] = managertmp;

    var obj1 = $("#divAttr").ligerForm(dataJson);
    //obj1._render();
}
//根据指定的动态属性集合，生成json，以动态生成控件
function GetAttributeControlJson(data, intSelectCount) {
    var strdata = { inputWidth: 160, labelWidth: 170, space: 20, labelAlign: 'right', fields: [] };

    if (data.length > 0) {
        for (i = 0; i < data.length; i++) {
            var strNewLine = (i % 2 > 0) ? "false" : "true";
            var strdata1 = "";

            if (data[i].CONTROL_NAME == "textbox") {
                strdata1 += "{ display: '" + data[i].ATTRIBUTE_NAME + "', name: '" + data[i].CONTROL_ID + intSelectCount + "', newline: " + strNewLine + ", type: 'text'";
                if (i == 0)
                    strdata1 += ", group: '属性', groupicon: groupicon";
                strdata1 += "}";
            }
            if (data[i].CONTROL_NAME == "dropdownlist") {
                strdata1 += "{ display: '" + data[i].ATTRIBUTE_NAME + "',name: '" + data[i].CONTROL_ID + intSelectCount + "', newline:" + strNewLine + ", type: 'select',";
                strdata1 += "comboboxName: '" + data[i].CONTROL_ID + intSelectCount + "-1',options:";

                if (data[i].DICTIONARY && data[i].DICTIONARY.length > 0) {
                    strdata1 += "{ valueFieldID: '" + data[i].CONTROL_ID + intSelectCount + "', url: '../../../../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|" + data[i].DICTIONARY + "'}";
                }
                else if (data[i].TABLE_NAME && data[i].TABLE_NAME.length > 0) {
                    strdata1 += "{ valueFieldID: '" + data[i].CONTROL_ID + intSelectCount + "', url: '../../../../../Base/MonitorType/Select.ashx?view=" + data[i].TABLE_NAME + "&idfield=" + data[i].VALUE_FIELD + "&textfield=" + data[i].TEXT_FIELD + "'}";
                }
                if (i == 0)
                    strdata1 += ", group: '属性', groupicon: groupicon";
                strdata1 += "}";
            }
            var strJsonData = eval('(' + strdata1 + ')');
            strdata.fields.push(strJsonData);
        }
    }

    return strdata;
}
//给动态属性控件赋值，编辑模式
function initAttributeControlValue(strDYNAMIC_ATTRIBUTE_ID) {
    var newData = new Array();
    for (i = 0; i < AttributeTypeAndInfoLst.length; i++) {
        if (AttributeTypeAndInfoLst[i].ATTRIBUTE_TYPE_ID == strDYNAMIC_ATTRIBUTE_ID) {
            newData.push(AttributeTypeAndInfoLst[i]);
        }
    }

    for (i = 0; i < newData.length; i++) {
        for (j = 0; j < PointValue.length; j++) {
            if (newData[i].ID == PointValue[j].ATTRBUTE_CODE) {
                $("#" + newData[i].CONTROL_ID + intSelectCount).val(PointValue[j].ATTRBUTE_VALUE);
            }
        }
    }
}

//得到保存信息
function getSaveDate() {
    var strData = "{";
    if (strID == "") {
        strData += "'strPointID':'',";
    }
    else {
        strData += "'strPointID':'" + strID + "',";
    }
    strData += "'strAttrID':'" + strAttrID + "',";
    strData += GetAttrStr();
    strData += "}";

    return strData;
}

//得到动态属性保存参数
function GetAttrStr() {
    var strData = "";

    var newData = new Array();
    for (i = 0; i < AttributeTypeAndInfoLst.length; i++) {
        if (AttributeTypeAndInfoLst[i].ATTRIBUTE_TYPE_ID == strAttrID) {
            newData.push(AttributeTypeAndInfoLst[i]);
        }
    }

    if (newData.length > 0) {
        for (i = 0; i < newData.length; i++) {
            var strdata1 = "";
            strdata1 += newData[i].CONTROL_NAME + "|" + newData[i].ID + "|" + $("#" + newData[i].CONTROL_ID + intSelectCount).val();

            strData += (strData.length > 0 ? "=" : "") + strdata1;
        }
    }
    if (strData.length > 0) {
        strData = "'strAttribute':'" + strData + "'";
    }
    else
        strData = "'strAttribute':''";
    
    return strData;
}