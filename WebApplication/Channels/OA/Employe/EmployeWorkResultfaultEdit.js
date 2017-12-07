/// 站务管理--人员档案明细业绩成果编辑
/// 创建时间：2013-01-07
/// 创建人：胡方扬

///-------------------------------------------------------------------------------------
///定义变量
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strEmployeID = "", strEmployeWorkResultID = "";
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
    strEmployeWorkResultID = $.getUrlVar('strEmployeWorkResultID');
    if (!strEmployeWorkResultID)
        strEmployeWorkResultID = "";

    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 410, labelWidth: 100, space: 40, labelAlign: 'right', modal: true,
        fields: [
                { display: "发生时间", name: "ACCIDENTHAPPENDATE", newline: true, width:160, type: "date",group: "基本信息", groupicon: groupicon },
                { display: "工作事故", name: "ACCIDENTS", newline: true, type: "textarea"}
                ]
    });

    $("#ACCIDENTS").attr("validate", "[{required:true, msg:'请输入所在事故内容'},{minlength:2,maxlength:200,msg:'工作成果最小长度为2，最大长度为200'}]");
    $("#ACCIDENTS").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:400px") ;
    if (strEmployeWorkResultID != "") {
        var InitDataList = [];
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "EmployeHander.ashx?action=GetEmployeWorkResultDetail&strEmployeID=" + strEmployeID + "&strEmployeWorkResultID=" + strEmployeWorkResultID + "&strEmployeWorkResultType=2",
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
        $("#ACCIDENTHAPPENDATE").val(InitDate( InitDataList[0].ACCIDENTHAPPENDATE));
        $("#ACCIDENTS").val(InitDataList[0].ACCIDENTS);
    }

})


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

    var strData = "";
    var isTrue = $("#form1").validate();
    if (isTrue) {
        strData += "&strEmployeID=" + strEmployeID;
        strData += "&strEmployeWorkResultID=" + strEmployeWorkResultID;
        strData += "&strEmployeWorkResultAccidents=" + encodeURI($("#ACCIDENTS").val());
        strData += "&strEmployeWorkResultAccidentDate=" + encodeURI($("#ACCIDENTHAPPENDATE").val());
        strData += "&strEmployeWorkResultType=2";
    }
    return strData;
}