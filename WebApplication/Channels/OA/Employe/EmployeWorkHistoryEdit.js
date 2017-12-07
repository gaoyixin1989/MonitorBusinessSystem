/// 站务管理--人员档案明细证书编辑
/// 创建时间：2013-01-07
/// 创建人：胡方扬

///-------------------------------------------------------------------------------------
///定义变量
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strEmployeID = "", strEmployeWorkHistoryID = "";
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
    strEmployeWorkHistoryID = $.getUrlVar('strEmployeWorkHistoryID');
    if (!strEmployeWorkHistoryID)
        strEmployeWorkHistoryID = "";

    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 120, space: 40, labelAlign: 'right', modal: true,
        fields: [
                { display: "所在单位", name: "WORKCOMPANY", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "所在岗位", name: "POSITION", newline: false, type: "text" },
                { display: "开始时间", name: "WORKBEGINDATE", newline: true, type: "date" },
                { display: "截止时间", name: "WORKENDDATE", newline: false, type: "date" }
                ]
    });

    $("#WORKCOMPANY").attr("validate", "[{required:true, msg:'请输入所在单位名称'},{minlength:2,maxlength:25,msg:'证书名称最小长度为2，最大长度为25'}]");

    if (strEmployeWorkHistoryID != "") {
        var InitDataList = [];
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "EmployeHander.ashx?action=GetEmployeWorkHistoryDetail&strEmployeID=" + strEmployeID + "&strEmployeWorkHistoryID=" + strEmployeWorkHistoryID + "",
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
        $("#WORKCOMPANY").val(InitDataList[0].WORKCOMPANY);

        $("#POSITION").val(InitDataList[0].POSITION);
        $("#WORKBEGINDATE").val(InitDate(InitDataList[0].WORKBEGINDATE));
        $("#WORKENDDATE").val(InitDate(InitDataList[0].WORKENDDATE));
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
        strData += "&strEmployeWorkHistoryID=" + strEmployeWorkHistoryID;
        strData += "&strEmployeWorkHistoryCompany=" + encodeURI($("#WORKCOMPANY").val());
        strData += "&strEmployeWorkHistoryPostion=" + encodeURI($("#POSITION").val());
        strData += "&strEmployeWorkHistoryBeginDate=" + encodeURI($("#WORKBEGINDATE").val());
        strData += "&strEmployeWorkHistoryEndDate=" + encodeURI($("#WORKENDDATE").val());
    }
    return strData;
}