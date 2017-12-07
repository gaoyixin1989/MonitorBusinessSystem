/// 站务管理--采购验收页面
/// 创建时间：2013-01-07
/// 创建人：胡方扬
/// 修改人：魏林　2013-09-12

///-------------------------------------------------------------------------------------
///定义变量
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var NationItems = null, UserListItems = null, strUserList = null;
var strPartPlanId = "", strPartId = "", strPartCode = "", strPartName = "", strNeedQuanity = "0", strPrice = "0", strAmount = "0", strRealName = "", strUserID = "", strUserDo = "";
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

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "PartHandler.ashx?action=GetLoginUserInfor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strUserList = data.Rows;
                strRealName = strUserList[0].REAL_NAME;
                strUserID = strUserList[0].ID;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    strPartPlanId = $.getUrlVar('strPartPlanId');
    strPartName = decodeURI($.getUrlVar('strPartName'));
    strPartCode = decodeURI($.getUrlVar('strPartCode'));
    strNeedQuanity = decodeURI($.getUrlVar('strNeedQuanity'));
    strUserDo = decodeURI($.getUrlVar('strUserDo'));
    strPartId = decodeURI($.getUrlVar('strPartId'));
    strId = $.getUrlVar('strId');

    if (!strPartPlanId)
        strPartPlanId = "";

    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 300, labelWidth: 120, space: 40, labelAlign: 'right', modal: true,
        fields: [
                { display: "物料名称", name: "PARTNAME", newline: true, type: "text", width: 160, group: "基本信息", groupicon: groupicon },
                { display: "物料编码", name: "PARTCODE", newline: false, type: "text", width: 160 },
                { display: "数量", name: "NEED_QUANTITY", newline: true, type: "text", width: 160 },
                { display: "实际单价", name: "PRICE", newline: false, type: "text", width: 160 },
                { display: "实际金额", name: "AMOUNT", newline: true, type: "text", width: 160 },
                { display: "供应商", name: "ENTERPRISE_NAME", newline: false, type: "text", width: 160 },
                { display: "收货日期", name: "RECIVEPART_DATE", newline: true, type: "date", width: 160 },
                { display: "检验日期", name: "CHECK_DATE", newline: false, type: "date", width: 160 },
                { display: "验收情况", name: "CHECK_RESULT", newline: true, width: 160, type: "select", comboboxName: "CHECK_RESULT_BOX", options: { valueFieldID: "CHECK_RESULT_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|CheckResult"} },
                { display: "验收人", name: "CHECK_USERID", newline: false, type: "text", width: 160 },
                { display: "备注", name: "REMARK1", newline: true, type: "textarea" }
                ]
    });
    $("#REMARK1").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:280px");
    $("#NEED_QUANTITY").attr("validate", "[{required:true, msg:'请输入数量'}]");
    $("#PRICE").attr("validate", "[{required:true, msg:'请输入单价'}]");
    $("#AMOUNT").attr("validate", "[{required:true, msg:'请输入实际金额'}]");
    $("#ENTERPRISE_NAME").attr("validate", "[{required:true, msg:'请输入供应商名称'}]");
    $("#RECIVEPART_DATE").attr("validate", "[{required:true, msg:'请输入收货日期'}]");
    $("#CHECK_DATE").attr("validate", "[{required:true, msg:'请输入检验日期'}]");
    $("#CHECK_RESULT_OP").attr("validate", "[{required:true, msg:'请选择验收情况'}]");

    $("#NEED_QUANTITY").bind("blur", function () {
        strNum = $(this).val();
        strPrice = $("#PRICE").val();
        if (strPrice != "" && strNum != "") {
            strAmount = strPrice * strNum;
        }
        $("#AMOUNT").val(strAmount);
        $("#AMOUNT").formatCurrency();
    })

    $("#PRICE").bind("blur", function () {
        strPrice = $(this).val();
        strNum = $("#NEED_QUANTITY").val();
        if (strPrice != "" && strNum != "") {
            strAmount = strPrice * strNum;
        }
        $("#AMOUNT").val(strAmount);
        $("#AMOUNT").formatCurrency();
        $(this).formatCurrency();
    })

    $("#AMOUNT").bind("blur", function () {
        strAmount = $(this).val();
        $(this).formatCurrency();
    })

    SetInitValue();
    if (strPartId != "") {
        return;
    }
    else {//编辑模式
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "PartAcceptEdit.aspx?type=loadData&strid=" + strId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                bindJsonToPage(data);
                $("#CHECK_USERID").val(data.REMARK2);
                $("#CHECK_RESULT").val(data.REMARK3);
                $("#CHECK_RESULT_OP").val(data.CHECK_RESULT);
            }
        });
    }
    $("#PARTNAME").val(strPartName);

    //编辑模式下 初始化基本信息
    function SetInitValue() {

        $("#PARTNAME").val(strPartName);
        $("#PARTNAME").ligerTextBox({ disabled: true });

        $("#PARTCODE").val(strPartCode);
        $("#PARTCODE").ligerTextBox({ disabled: true });

        $("#CHECK_USERID").val(strRealName);
        $("#CHECK_USERID").ligerTextBox({ disabled: true });

        $("#NEED_QUANTITY").val(strNeedQuanity);

        $("#RECIVEPART_DATE").val(currentTime());
        $("#CHECK_DATE").val(currentTime());

        $("#PRICE").attr("style", "background-image:url(../../../Images/Icons/money_yen.png);background-position:left bottom;background-repeat: no-repeat; padding-left:15px");
        $("#PRICE").val('0');
        $("#PRICE").formatCurrency();

        $("#AMOUNT").attr("style", "background-image:url(../../../Images/Icons/money_yen.png);background-position:left bottom;background-repeat: no-repeat; padding-left:15px");
        $("#AMOUNT").val('0');
        $("#AMOUNT").formatCurrency();
    }

})
//JS 获取当前时间
function currentTime() {

    var d = new Date(), str = '';
    str += d.getFullYear() + '-';
    str += d.getMonth() + 1 + '-';
    str += d.getDate();
    return str;
}
//处理 日期函数
function InitDate(vDate) {
    if (vDate == "") return;
    var createDate = new Date(Date.parse(vDate.replace(/-/g, '/')))
    var strData = createDate.getFullYear() + "-";
    strData += (createDate.getMonth() + 1) + "-";
    strData += createDate.getDate();
    return strData;
}
//得到基本信息保存参数
function GetBaseInfoStr() {
    if (strPartPlanId != "") {
        var strData = "";
        strData += "&strPartPlanId=" + strPartPlanId;
        strData += "&strPartId=" + strPartId;
        strData += "&strNeedQuatity=" + encodeURI($("#NEED_QUANTITY").val());
        strData += "&strPrice=" + strPrice;
        strData += "&strAmount=" + strAmount;
        strData += "&strEnterpriseName=" + encodeURI($("#ENTERPRISE_NAME").val());
        strData += "&strReciveDate=" + $("#RECIVEPART_DATE").val();
        strData += "&strCheckDate=" + $("#CHECK_DATE").val()
        strData += "&strCheckResult=" +encodeURI( $("#CHECK_RESULT_OP").val());
        strData += "&strCheckUserID=" + strUserID;
        strData += "&strUserDo=" + encodeURI(strUserDo);
        strData += "&strRemark=" + encodeURI($("#REMARK").val());
    }
    return strData;
}

function GetInStoreInfo() {
    if ($("#form1").validate()) {
        var strData = "";
        strData += "&strPartId=" + strPartId;
        strData += "&strNeedQuatity=" + encodeURI($("#NEED_QUANTITY").val());
        strData += "&strPrice=" + strPrice;
        strData += "&strAmount=" + strAmount;
        strData += "&strEnterpriseName=" + encodeURI($("#ENTERPRISE_NAME").val());
        strData += "&strReciveDate=" + $("#RECIVEPART_DATE").val();
        strData += "&strCheckDate=" + $("#CHECK_DATE").val()
        strData += "&strCheckResult=" + encodeURI($("#CHECK_RESULT_OP").val());
        strData += "&strCheckUserID=" + strUserID;
        strData += "&strRemark=" + encodeURI($("#REMARK").val());

        return strData;
    } else {
        return "";
    }
}

function GetInStoreInfoEx() {
    if ($("#form1").validate()) {
        var strData = "";
        strData += "&strMyPartAcceptedID=" + strId;
        strData += "&strPartId=" + strPartId;
        strData += "&strNeedQuatity=" + encodeURI($("#NEED_QUANTITY").val());
        strData += "&strPrice=" + strPrice;
        strData += "&strAmount=" + strAmount;
        strData += "&strEnterpriseName=" + encodeURI($("#ENTERPRISE_NAME").val());
        strData += "&strReciveDate=" + $("#RECIVEPART_DATE").val();
        strData += "&strCheckDate=" + $("#CHECK_DATE").val()
        strData += "&strCheckResult=" + encodeURI($("#CHECK_RESULT_OP").val());
        strData += "&strCheckUserID=" + strUserID;
        strData += "&strRemark=" + encodeURI($("#REMARK").val());

        return strData;
    } else {
        return "";
    }
}