/// 站务管理--库存出库操作
/// 创建时间：2013-01-31
/// 创建人：胡方扬

///-------------------------------------------------------------------------------------
///定义变量
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strPartId = "", strNeedQuanity = "", strUserName = "", strUserId = "";
var strId = "";
///-------------------------------------------------------------------------------------

///-------------------------------------------------------------------------------------
///获取URL参数
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
///-------------------------------------------------------------------------------------

$(document).ready(function () {
    strPartId = $.getUrlVar('strPartId');
    strNeedQuanity = $.getUrlVar('strNeedQuanity');
    strId = $.getUrlVar('strId');

    if (!strPartId)
        strPartId = "";

    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 400, labelWidth: 120, space: 40, labelAlign: 'right', modal: true,
        fields: [
                { display: "出库数量", name: "INVENTORY", newline: true, type: "text", width: 160, group: "基本信息", groupicon: groupicon },
                { display: "领用人", name: "USERNAME", newline: false, type: "select", width: 160 },
                { display: "价格", name: "REMARK1", newline: true, type: "text", width: 160 },
                { display: "领用原因", name: "REASON", newline: true, type: "textarea" }
                ]
    });
    $("#USERNAME").attr("validate", "[{required:true, msg:'请选择领用人'}]");
    $("#INVENTORY").attr("validate", "[{required:true, msg:'请输入领用数量'}]");
    //$("#INVENTORY").val(strNeedQuanity);
    $("#REASON").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:380px");
    $("#USERNAME").ligerComboBox({ onBeforeOpen: f_selectUser, valueFieldID: 'hidEmployeID' });
    $("#INVENTORY").blur(function () {
        if ($(this).val().length == 0) {
            $.ligerDialog.warn('请填写出库量');
            $(this).val("");
            return;
        }

        if ((parseInt($(this).val()) <= 0) || (parseInt($(this).val()) > parseInt(strNeedQuanity))) {
            $.ligerDialog.warn('出库量应<a style="color:Red"><b>大于0小于等于' + strNeedQuanity + '<b></a>');
            $(this).val("");
            return;
        }
    });

    if (!strPartId) {
        return;
    }
    else {//编辑模式
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "PartOutEdit.aspx?type=loadData&strid=" + strId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                bindJsonToPage(data);
                $("#INVENTORY").val(data.USED_QUANTITY);
                $("#USERNAME").val(data.REMARK2);
                $("#hidUserID").val(data.USER_ID);
            }
        });
    }

    function f_selectUser() {
        $.ligerDialog.open({ title: '用户选择', name: 'winselector', width: 500, height: 320, url: 'GetSelectUserList.aspx', buttons: [
                { text: '确定', onclick: f_selectUserOK },
                { text: '返回', onclick: f_selectUserCancel }
            ]
        });
        return false;
    }

    function f_selectUserOK(item, dialog) {
        var fn = dialog.frame.f_select || dialog.frame.window.f_select;
        var data = fn();
        if (!data) {
            $.ligerDialog.warn('请选择行!');
            return;
        }
        var tempUserId = $("#hidUserID").val();
        var tempUserIdArr = tempUserId.split(";");

        for (var i = 0; i < data.length; i++) {
            if ($.inArray(data[i].ID, tempUserIdArr) < 0) {
                strUserName += data[i].REAL_NAME + ";";
                strUserId += data[i].ID + ";";
            }
        }

        $("#USERNAME").val(strUserName.substring(0, strUserName.length - 1));
        $("#hidUserID").val(strUserId.substring(0, strUserId.length - 1));
        dialog.close();
    }

    function f_selectUserCancel(item, dialog) {
        dialog.close();
    }
});

//得到基本信息保存参数
function GetBaseInfoStr() {
    if (strPartId != "") {
        var strData = "";
        strData += "&strPartId=" + strPartId;
        strData += "&strNeedQuatity=" +$("#INVENTORY").val() ;
        strData += "&strUserID=" +$("#hidUserID").val();
        strData += "&strREMARK1=" +$("#REMARK1").val();
        strData+="&strRemark="+encodeURI($("#REASON").val());
    }
    return strData;
}

//得到基本信息保存参数--领用明细的修改用
function GetBaseInfoStrEx() {
    if (strPartId != "") {
        var strData = "";
        strData += "&strPartCollarId=" + strId;
        strData += "&strPartId=" + strPartId;
        strData += "&strNeedQuatity=" + $("#INVENTORY").val();
        strData += "&strUserID=" + $("#hidUserID").val();
        strData += "&strREMARK1=" + $("#REMARK1").val();
        strData += "&strRemark=" + encodeURI($("#REASON").val());
    }
    return strData;
}