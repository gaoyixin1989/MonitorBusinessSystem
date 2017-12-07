/// 站务管理--人员档案明细证书编辑
/// 创建时间：2013-01-07
/// 创建人：胡方扬

///-------------------------------------------------------------------------------------
///定义变量
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strEmployeID = "", strEmployeQualID = "";
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
    strEmployeQualID = $.getUrlVar('strEmployeQualID');
    if (!strEmployeQualID)
        strEmployeQualID = "";

    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 120, space: 40, labelAlign: 'right', modal: true,
        fields: [
                { display: "证书名称", name: "CERTITICATENAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "证书编号", name: "CERTITICATECODE", newline: false, type: "text" },
                { display: "发证日期", name: "ISSUINDATE", newline: true, type: "date" },
                { display: "有效期限", name: "ACTIVEDATE", newline: false, type: "date" },
                { display: "发证单位", name: "ISSUINGAUTHO", newline: true, type: "text" }
                ]
    });

    $("#CERTITICATENAME").attr("validate", "[{required:true, msg:'请输入证书名称'},{minlength:2,maxlength:15,msg:'证书名称最小长度为2，最大长度为15'}]");

    if (strEmployeQualID != "") {
        var InitDataList = [];
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "EmployeHander.ashx?action=GetEmployeQualDetail&strFileType=EmployeQual&strEmployeID=" + strEmployeID + "&strEmployeQualID=" + strEmployeQualID + "",
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
        $("#CERTITICATENAME").val(InitDataList[0].CERTITICATENAME);

        $("#CERTITICATECODE").val(InitDataList[0].CERTITICATECODE);
        $("#ISSUINDATE").val(InitDate(InitDataList[0].ISSUINDATE));
        $("#ACTIVEDATE").val(InitDate(InitDataList[0].ACTIVEDATE));
        $("#ISSUINGAUTHO").val(InitDataList[0].ISSUINGAUTHO);
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
        strData += "&strEmployeQualID=" + strEmployeQualID;
        strData += "&strEmployeQualName=" + encodeURI($("#CERTITICATENAME").val());
        strData += "&strEmployeQualCode=" + encodeURI($("#CERTITICATECODE").val());
        strData += "&strEmployeQualIsDepart=" + encodeURI($("#ISSUINGAUTHO").val());
        strData += "&strEmployeQualIsDate=" + encodeURI($("#ISSUINDATE").val());
        strData += "&strEmployeQualActiveDate=" + $("#ACTIVEDATE").val();
    }
    return strData;
}