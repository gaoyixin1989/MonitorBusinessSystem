// Create by 潘德军 2013.01.07   "工作流管理"功能

var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strID = "";

$(document).ready(function () {
    strID = request('strid');
    if (!strID)
        strID = "";

    //创建表单结构 
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
        fields: [
                { name: "ID", type: "hidden" },
                { display: "工作流名称", name: "WF_CAPTION", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "工作流代号", name: "WF_ID", newline: false, type: "text" },
                { display: "类别归属", name: "WF_CLASS_ID", newline: true, type: "select", comboboxName: "WF_CLASS_ID1", options: { valueFieldID: "WF_CLASS_ID", url: "../../Channels/Base/MonitorType/Select.ashx?view=T_WF_SETTING_BELONGS&idfield=WF_CLASS_ID&textfield=WF_CLASS_NAME"} },
                { display: "首环节转向页面", name: "FSTEP_RETURN_URL", width: 530, newline: true, type: "text" },
                { display: "工作流描述", name: "WF_NOTE",width:530, newline: true, type: "text" }
                ]
    });

    //加载数据
    if (strID != "") {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "WFSettingFlowInputForJS.aspx?type=loadData&strid=" + strID ,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                bindJsonToPage(data);
            }
        });
    }
});

//得到保存信息
function getSaveDate() {
    var strid = request('strid');
    if (!strid)
        strid = "0";
    var strData = "{";
    strData += "'strid':'" + strid + "',";
    strData += "'strWF_CAPTION':'" + $("#WF_CAPTION").val() + "',";
    strData += "'strWF_ID':'" + $("#WF_ID").val() + "',";
    strData += "'strWF_CLASS_ID':'" + $("#WF_CLASS_ID").val() + "',";
    strData += "'strFSTEP_RETURN_URL':'" + $("#FSTEP_RETURN_URL").val() + "',";
    strData += "'strWF_NOTE':'" + $("#WF_NOTE").val() + "'";
    strData += "}";

    return strData;
}

