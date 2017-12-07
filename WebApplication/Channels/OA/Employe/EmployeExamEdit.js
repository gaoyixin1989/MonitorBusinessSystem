/// 站务管理--人员年度考核编辑
/// 创建时间：2013-08-20
/// 创建人：潘德军

///-------------------------------------------------------------------------------------
///定义变量
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strEmployeID = "", strEmployeExamID = "";
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
    strEmployeID = $.getUrlVar('strEmployeID');
    strEmployeExamID = $.getUrlVar('strEmployeExamID');
    if (!strEmployeExamID)
        strEmployeExamID = "";

    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 120, space: 40, labelAlign: 'right', modal: true,
        fields: [
                { display: "考核年度", name: "EX_YEAR", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "考核评价", name: "EX_INFO", newline: true, type: "text" }
                ]
    });

    $("#EX_YEAR").attr("validate", "[{required:true, msg:'请输入考核年度'},{minlength:4,maxlength:8,msg:'考核年度最小长度为4，最大长度为8'}]");

    if (strEmployeExamID != "") {
        var InitDataList = [];
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "EmployeHander.ashx?action=GetEmployeExHis&strFileType=EmployeExam&strEmployeID=" + strEmployeID + "&strEmployeExamID=" + strEmployeExamID + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InitDataList = data.Rows;
            }
        });
        SetInitValue();
    }


    //编辑模式下 初始化基本信息
    function SetInitValue() {
        $("#EX_YEAR").val(InitDataList[0].EX_YEAR);

        $("#EX_INFO").val(InitDataList[0].EX_INFO);

    }

})


//得到基本信息保存参数
function GetBaseInfoStr() {

    var strData = "";
    var isTrue = $("#form1").validate();
    if (isTrue) {
        strData += "&strEmployeID=" + strEmployeID;
        strData += "&strEmployeExamID=" + strEmployeExamID;
        strData += "&strEX_YEAR=" + encodeURI($("#EX_YEAR").val());
        strData += "&strEX_INFO=" + encodeURI($("#EX_INFO").val());
    }
    return strData;
}