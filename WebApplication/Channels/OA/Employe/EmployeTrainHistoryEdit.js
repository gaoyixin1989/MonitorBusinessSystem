/// 站务管理--人员档案明细证书编辑
/// 创建时间：2013-01-07
/// 创建人：胡方扬

///-------------------------------------------------------------------------------------
///定义变量
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strEmployeID = "", strEmployeTrainID = "";
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
    strEmployeTrainID = $.getUrlVar('strEmployeTrainID');
    if (!strEmployeTrainID)
        strEmployeTrainID = "";

    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 410, labelWidth: 120, space: 40, labelAlign: 'right', modal: true,
        fields: [
                { display: "培训项目", name: "ATT_NAME", newline: true, type: "text",width:160, group: "基本信息", groupicon: groupicon },
                { display: "培训日期", name: "ATT_URL", newline: false, width: 160, type: "date" },
                { display: "培训结果", name: "TRAIN_RESULT", newline: true, width: 160, type: "text" },
                { display: "证书号", name: "BOOK_NUM", newline: false, width: 160, type: "text" },
                { display: "培训内容", name: "ATT_INFO", newline: true, type: "textarea" }
                ]
    });

    $("#ATT_NAME").attr("validate", "[{required:true, msg:'请输入所在培训项目名称'},{minlength:2,maxlength:40,msg:'证书名称最小长度为2，最大长度为40'}]");
    $("#ATT_INFO").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style","width:400px");;
    if (strEmployeTrainID != "") {
        var InitDataList = [];
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "EmployeHander.ashx?action=GetEmployeTrainHistoryDetail&strFileType=EmployeTrain&strEmployeID=" + strEmployeID + "&strEmployeTrainID=" + strEmployeTrainID + "",
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
        $("#ATT_NAME").val(InitDataList[0].ATT_NAME);

        $("#ATT_URL").val(InitDate(InitDataList[0].ATT_URL));
        $("#TRAIN_RESULT").val(InitDataList[0].TRAIN_RESULT);
        $("#BOOK_NUM").val(InitDataList[0].BOOK_NUM);
        $("#ATT_INFO").val(InitDataList[0].ATT_INFO);
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
        strData += "&strEmployeTrainID=" + strEmployeTrainID;
        strData += "&strEmployeTrainAttName=" + encodeURI($("#ATT_NAME").val());
        strData += "&strEmployeTrainAttUrl=" + encodeURI($("#ATT_URL").val());
        strData += "&strEmployeTrainAttResultl=" + encodeURI($("#TRAIN_RESULT").val());
        strData += "&strEmployeTrainAttBookNum=" + encodeURI($("#BOOK_NUM").val());
        strData += "&strEmployeTrainAttInfor=" + encodeURI($("#ATT_INFO").val());
    }
    return strData;
}