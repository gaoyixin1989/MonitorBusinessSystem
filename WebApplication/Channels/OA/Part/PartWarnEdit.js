/// 站务管理--物料报警阀值设定
/// 创建时间：2013-02-01
/// 创建人：胡方扬

///-------------------------------------------------------------------------------------
///定义变量
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strPartId = "", strWarnValue = "", strUserName = "", strUserId = "";
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
    strWarnValue = $.getUrlVar('strWarnValue');

    if (!strPartId)
        strPartId = "";

    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 400, labelWidth: 120, space: 40, labelAlign: 'right', modal: true,
        fields: [
                { display: "阀值", name: "ALARM", newline: true, type: "text", width: 160, group: "基本信息", groupicon: groupicon }
                ]
    });
    $("#ALARM").attr("validate", "[{required:true, msg:'请输入领用数量'}]");
    $("#ALARM").val(strWarnValue);
})

//得到基本信息保存参数
function GetWarnInfoStr() {
    if (strPartId != "") {
        var strData = "";
        strData += "&strPartId=" + strPartId;
        strData += "&strWarnValue=" +$("#ALARM").val() ;
    }
    return strData;
}