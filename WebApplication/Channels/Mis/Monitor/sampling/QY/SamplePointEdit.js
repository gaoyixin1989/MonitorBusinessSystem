var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strSubtaskID = "";
var strID = "";
var strMonitorID = "";
var objDivAttr;
var intSelectCount = 0;
var AttributeTypeAndInfoLst = [];
var PointValue = [];
var managertmp;
var obj;

$(document).ready(function () {
    strSubtaskID = request('strSubtaskID');
    strID = request('strid');
    if (!strID)
        strID = "";

    strMonitorID = request('strMonitorID');

    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
        fields: [
                { name: "ID", type: "hidden" },
                { display: "监测点", name: "POINT_NAME", newline: true, type: "text", width: 530, group: "基本信息", groupicon: groupicon },
                { display: "监测类型", name: "MONITOR_ID", newline: true, type: "select", comboboxName: "MONITOR_ID1", options: { valueFieldID: "MONITOR_ID", url: "../../../../Base/MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} },
        //{ display: "委托类型", name: "POINT_TYPE", newline: false, type: "select", comboboxName: "POINT_TYPE1", options: { valueFieldID: "POINT_TYPE", url: "../../../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|Contract_Type"} },
                {display: "点位属性类别", name: "DYNAMIC_ATTRIBUTE_ID", newline: false, type: "select", comboboxName: "DYNAMIC_ATTRIBUTE_ID1", options: { valueFieldID: "DYNAMIC_ATTRIBUTE_ID", valueField: "ID", textField: "SORT_NAME"} },
        //{ display: "监测频次", name: "FREQ", newline: false, type: "select", comboboxName: "FREQ1", options: { valueFieldID: "FREQ", url: "../../../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|FREQ"} },
                {display: "采样频次", name: "SAMPLE_FREQ", newline: true, type: "select", comboboxName: "SAMPLEFREQ_Box", options: { valueFieldID: "SAMPLEFREQ_OP", url: "../../../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|POINT_SAMPLEFREQ"} },
                { display: "建成时间", name: "CREATE_DATE", newline: false, type: "date" },
                { display: "监测点位置", name: "ADDRESS", newline: true, type: "text" },
                { display: "经度", name: "LONGITUDE", newline: false, type: "text" },
                { display: "纬度", name: "LATITUDE", newline: true, type: "text" },
                { display: "排列序号", name: "NUM", newline: false, type: "text" }
                ]
    });

    //    $("#POINT_TYPE1").ligerGetComboBoxManager().setDisabled();
    //$("#FREQ1").ligerGetComboBoxManager().setDisabled();
    $("#SAMPLEFREQ_Box").ligerGetComboBoxManager().setValue("1");
    $("#divStandard").ligerForm({
        inputWidth: 320, labelWidth: 120, space: 90, labelAlign: 'right',
        fields: [
        { display: "国标", name: "NATIONAL_ST_CONDITION_ID", newline: true, type: "select", group: "执行标准", groupicon: groupicon },
        { display: "地标", name: "LOCAL_ST_CONDITION_ID", newline: true, type: "select" },
        { display: "行标", name: "INDUSTRY_ST_CONDITION_ID", newline: true, type: "select" }
        ]
    });

    $("#NATIONAL_ST_CONDITION_ID").ligerComboBox({
        onBeforeOpen: NATIONAL_select, valueFieldID: 'hidNATIONAL_ST_CON_ID'
    });
    $("#LOCAL_ST_CONDITION_ID").ligerComboBox({
        onBeforeOpen: LOCAL_select, valueFieldID: 'hidLOCAL_ST_CON_ID'
    });
    $("#INDUSTRY_ST_CONDITION_ID").ligerComboBox({
        onBeforeOpen: INDUSTRY_select, valueFieldID: 'hidINDUSTRY_ST_CON_ID'
    });

    //获取动态属性数据，以在监测类别下拉列表改变实现联动效果时，提供数据集合
    var AttributeTypeList = [];
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../../../Base/DynamicAttribute/AttributeConfigEdit.aspx?type=getAttributeTypeInfo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            AttributeTypeList = data;
        }
    });

    obj = $("#divAttr").ligerForm({ inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right', fields: [] });
    //监测类别下拉列表改变时候实现联动效果，根据提供数据集合，过滤出对应属性类别的属性，方便进行生成动态控件
    $.ligerui.get("MONITOR_ID1").bind('Selected', function (value, text) {
        var newData = new Array();
        for (i = 0; i < AttributeTypeList.length; i++) {
            if (AttributeTypeList[i].MONITOR_ID == value) {
                newData.push(AttributeTypeList[i]);
            }
        }
        $.ligerui.get("DYNAMIC_ATTRIBUTE_ID1").setData(newData);
    });

    if (strMonitorID != '')
        $("#MONITOR_ID").val(strMonitorID);

    //获取所有属性类别及属性信息关联数据
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePointEdit.aspx?type=GetAttrrbute",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            AttributeTypeAndInfoLst = data;
        }
    });

    //获取点位对应的动态属性值
    if (strID != "") {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "SamplePointEdit.aspx?type=GetAttrValue&strid=" + strID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                PointValue = data;
            }
        });
    }

    managertmp = $.ligerui.managers.divAttr;

    //点位属性类别下拉列表改变时候实现联动效果
    $.ligerui.get("DYNAMIC_ATTRIBUTE_ID1").bind('Selected', function (value, text) {
        intSelectCount += 1;
        initAttributeControl(value);
        initAttributeControlValue(value);
    });

    //加载数据
    if (strID != "") {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "SamplePointEdit.aspx?type=loadData&strid=" + strID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                bindJsonToPage(data);
                $("#SAMPLEFREQ_Box").ligerGetComboBoxManager().setValue(data.SAMPLE_FREQ);
                initAttributeControl(data.DYNAMIC_ATTRIBUTE_ID);
                initAttributeControlValue(data.DYNAMIC_ATTRIBUTE_ID);
            }
        });
    }
});

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
    obj1._render();
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
                    strdata1 += ", group: '点位属性', groupicon: groupicon";
                strdata1 += "}";
            }
            if (data[i].CONTROL_NAME == "dropdownlist") {
                strdata1 += "{ display: '" + data[i].ATTRIBUTE_NAME + "',name: '" + data[i].CONTROL_ID + intSelectCount + "', newline:" + strNewLine + ", type: 'select',";
                strdata1 += "comboboxName: '" + data[i].CONTROL_ID + intSelectCount + "-1',options:";

                if (data[i].DICTIONARY && data[i].DICTIONARY.length > 0) {
                    strdata1 += "{ valueFieldID: '" + data[i].CONTROL_ID + intSelectCount + "', url: '../../../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|" + data[i].DICTIONARY + "'}";
                }
                else if (data[i].TABLE_NAME && data[i].TABLE_NAME.length > 0) {
                    strdata1 += "{ valueFieldID: '" + data[i].CONTROL_ID + intSelectCount + "', url: '../../../../Base/MonitorType/Select.ashx?view=" + data[i].TABLE_NAME + "&idfield=" + data[i].VALUE_FIELD + "&textfield=" + data[i].TEXT_FIELD + "'}";
                }
                if (i == 0)
                    strdata1 += ", group: '点位属性', groupicon: groupicon";
                strdata1 += "}";
            }
            var strJsonData = eval('(' + strdata1 + ')');
            strdata.fields.push(strJsonData);
        }
    }

    return strdata;
}

//得到保存信息
function getSaveDate() {
    var strid = request('id');
    if (!strid)
        strid = "0";
    var strCompanyID = request('CompanyID');
    var strData = "{";
    strData += GetBaseInfoStr();
    strData += ",";
    strData += GetAttrStr();
    strData += ",";
    strData += GetStStr();
    strData += "}";

    return strData;
}

//得到基本信息保存参数
function GetBaseInfoStr() {
    var strData = "";
    if (strID == "") {
        strData += "'strPointID':'',";
        strData += "'strSubtaskID':'" + strSubtaskID + "',";
    }
    else {
        strData += "'strPointID':'" + strID + "',";
        strData += "'strSubtaskID':'',";
    }
    strData += "'strPOINT_NAME':'" + $("#POINT_NAME").val() + "',";
    strData += "'strMONITOR_ID':'" + $("#MONITOR_ID").val() + "',";
    strData += "'strPOINT_TYPE':'" + $("#POINT_TYPE").val() + "',";
    strData += "'strDYNAMIC_ATTRIBUTE_ID':'" + $("#DYNAMIC_ATTRIBUTE_ID").val() + "',";
    //strData += "'strFREQ':'" + $("#FREQ").val() + "',";
    strData += "'strSAMPLEFREQ':'" + $("#SAMPLEFREQ_OP").val() + "',";
    strData += "'strCREATE_DATE':'" + $("#CREATE_DATE").val() + "',";
    strData += "'strADDRESS':'" + $("#ADDRESS").val() + "',";
    strData += "'strLONGITUDE':'" + $("#LONGITUDE").val() + "',";
    strData += "'strLATITUDE':'" + $("#LATITUDE").val() + "',";
    strData += "'strNUM':'" + $("#NUM").val() + "'";

    return strData;
}

//得到动态属性保存参数
function GetAttrStr() {
    var strData = "";

    var newData = new Array();
    for (i = 0; i < AttributeTypeAndInfoLst.length; i++) {
        if (AttributeTypeAndInfoLst[i].ATTRIBUTE_TYPE_ID == $("#DYNAMIC_ATTRIBUTE_ID").val()) {
            newData.push(AttributeTypeAndInfoLst[i]);
        }
    }

    if (newData.length > 0) {
        for (i = 0; i < newData.length; i++) {
            var strdata1 = "";
            strdata1 += newData[i].CONTROL_NAME + "|" + newData[i].ID + "|" + $("#" + newData[i].CONTROL_ID + intSelectCount).val();

            strData += (strData.length > 0 ? "-" : "") + strdata1;
        }
    }
    if (strData.length > 0) {
        strData = "'strAttribute':'" + strData + "'";
    }
    else
        strData = "'strAttribute':''";

    return strData;
}

//得到标准保存参数
function GetStStr() {
    var strData = "";

    strData += "'strNATIONAL_ST_CONDITION_ID':'" + $("#hidNATIONAL_ST_CON").val() + "',";
    strData += "'strLOCAL_ST_CONDITION_ID':'" + $("#hidLOCAL_ST_CON").val() + "',";
    strData += "'strINDUSTRY_ST_CONDITION_ID':'" + $("#hidINDUSTRY_ST_CON").val() + "'";

    return strData;
}

