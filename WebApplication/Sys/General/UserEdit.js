// Create by 潘德军 2012.11.20  "用户管理"功能

var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strID = "";
var obj;
var objValue = [];


$(document).ready(function () {
    strID = request('strid');
    if (!strID)
        strID = "";

    //创建表单结构 --用户信息
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 120, space: 50, labelAlign: 'right',
        fields: [
                { name: "ID", type: "hidden" },
                { display: "用户登录名", name: "USER_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "密码", name: "USER_PWD", newline: false, type: "text" },
                { display: "用户姓名", name: "REAL_NAME", newline: true, type: "text" },
                { display: "名次排序", name: "ORDER_ID", newline: false, type: "text", type: "spinner", options: { type: "int"} },
                { display: "职位", name: "REMARK1", newline: true, type: "select", width: 490, group: "职位信息", groupicon: groupicon },
                { display: "出生日期", name: "BIRTHDAY", newline: true, type: "date", group: "用户资料", groupicon: groupicon },
                { display: "性别", name: "SEX", newline: false, type: "select", comboboxName: "SEX1", options: { valueFieldID: "SEX", url: "../../Channels/Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|user_sex&Order=ORDER_ID"} },
                { display: "办公电话", name: "PHONE_OFFICE", newline: true, type: "text" },
                { display: "手机号码", name: "PHONE_MOBILE", newline: false, type: "text" },
                { display: "家庭电话", name: "PHONE_HOME", newline: true, type: "text" },
                { display: "电子邮件", name: "EMAIL", newline: false, type: "text" },
                { display: "家庭地址", name: "ADDRESS", newline: true, width: 490, type: "text" },
                { display: "邮编", name: "POSTCODE", newline: true, type: "text" },
                { display: "启用标记", name: "IS_USE", newline: false, type: "select", comboboxName: "IS_USE1", options: { valueFieldID: "IS_USE", url: "../../Channels/Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|IS_USE&Order=ORDER_ID"} },
                { display: "苹果手机MAC地址", name: "IOS_MAC", newline: true, type: "text", group: "手机登录信息", groupicon: groupicon },
                { display: "是否启用", name: "IF_IOS", newline: false, type: "select", comboboxName: "IF_IOS1", options: { valueFieldID: "IF_IOS", url: "../../Channels/Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|IS_USE&Order=ORDER_ID"} },
                { display: "安卓手机MAC地址", name: "ANDROID_MAC", newline: true, type: "text" },
                { display: "是否启用", name: "IF_ANDROID", newline: false, type: "select", comboboxName: "IF_ANDROID1", options: { valueFieldID: "IF_ANDROID", url: "../../Channels/Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|IS_USE&Order=ORDER_ID"} }
                ]
    });
    $("#USER_NAME").attr("validate", "[{required:true,msg:'请填写用户登录名'},{maxlength:32,msg:'用户登录名的最大长度为32'}]");
    $("#REAL_NAME").attr("validate", "[{maxlength:32,msg:'用户姓名的最大长度为32'}]");
    $("#PHONE_OFFICE").attr("validate", "[{maxlength:32,msg:'办公电话的最大长度为32'}]");
    $("#PHONE_MOBILE").attr("validate", "[{maxlength:32,msg:'手机号码的最大长度为32'}]");
    $("#PHONE_HOME").attr("validate", "[{maxlength:32,msg:'家庭电话的最大长度为32'}]");
    $("#EMAIL").attr("validate", "[{maxlength:32,msg:'电子邮件的最大长度为32'}]");
    $("#ADDRESS").attr("validate", "[{maxlength:128,msg:'家庭地址的最大长度为128'}]");
    $("#POSTCODE").attr("validate", "[{maxlength:6,msg:'邮编的最大长度为6'}]");

    $("#REMARK1").ligerComboBox({
        onBeforeOpen: Post_select, valueFieldID: 'REMARK2'
    });
    if (strID == "") {
        $("#USER_PWD").ligerGetTextBoxManager().setDisabled();
    }
    //加载数据
    if (strID != "") {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "UserEdit.aspx?type=loadData&strid=" + strID,
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
    var strData = "{";
    strData += GetBaseInfoStr();
    strData += "}";

    return strData;
}

//得到基本信息保存参数
function GetBaseInfoStr() {
    if (!$("#form1").validate())
        return false;
    var strData = "";
    if (strID == "") {
        strData += "'strID':'',";
    }
    else {
        strData += "'strID':'" + strID + "',";
    }
    strData += "'strUSER_NAME':'" + $("#USER_NAME").val() + "',";
    strData += "'strREAL_NAME':'" + $("#REAL_NAME").val() + "',";
    strData += "'strORDER_ID':'" + $("#ORDER_ID").val() + "',";
    strData += "'strBIRTHDAY':'" + $("#BIRTHDAY").val() + "',";
    strData += "'strSEX':'" + $("#SEX").val() + "',";
    strData += "'strPHONE_OFFICE':'" + $("#PHONE_OFFICE").val() + "',";
    strData += "'strPHONE_MOBILE':'" + $("#PHONE_MOBILE").val() + "',";
    strData += "'strPHONE_HOME':'" + $("#PHONE_HOME").val() + "',";
    strData += "'strEMAIL':'" + $("#EMAIL").val() + "',";
    strData += "'strADDRESS':'" + $("#ADDRESS").val() + "',";
    strData += "'strPOSTCODE':'" + $("#POSTCODE").val() + "',";
    strData += "'strIS_USE':'" + $("#IS_USE").val() + "',";

    strData += "'strIOS_MAC':'" + $("#IOS_MAC").val() + "',";
    strData += "'strIF_IOS':'" + $("#IF_IOS").val() + "',";
    strData += "'strANDROID_MAC':'" + $("#ANDROID_MAC").val() + "',";
    strData += "'strIF_ANDROID':'" + $("#IF_ANDROID").val() + "',";
    strData += "'strUSER_PWD':'" + $("#USER_PWD").val() + "',";
    strData += "'strREMARK1':'" + $("#REMARK2").val() + "'";

    return strData;
}


//cancel按钮
function selectCancel(item, dialog) {
    dialog.close();
}

//弹出职位选择
function Post_select() {
    $.ligerDialog.open({ title: '选择职位', name: 'winselector', width: 500, height: 380, url: 'UserEdit_SelPostTree.aspx?strPostSelIDs=' + $("#REMARK2").val(),
        buttons: [
                { text: '确定', onclick: Post_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
    });
    return false;
}

//职位选择 ok按钮
function Post_selectOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        $.ligerDialog.warn('请选择职位!');
        return;
    }
    var strSelID = data.split("|")[0];
    var strSelName = data.split("|")[1];

    $("#REMARK1").val(strSelName);
    $("#REMARK2").val(strSelID);

    dialog.close();
}
