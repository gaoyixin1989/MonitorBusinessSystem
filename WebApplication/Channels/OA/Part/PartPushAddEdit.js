
///-------------------------------------------------------------------------------------
///定义变量
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strtaskID = "",strPartPlanId="",strPartName="",strPartId="",strBugMoney="";
///-------------------------------------------------------------------------------------
/// 站务管理--采购计划编辑
/// 创建时间：2013-01-31
/// 创建人：胡方扬
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
    strtaskID = $.getUrlVar('strtaskID');
    strPartPlanId=$.getUrlVar('strPartPlanId');
    if (!strtaskID)
        strtaskID = "";

$("#editform").ligerForm({
    inputWidth: 410, labelWidth: 90, space: 40, labelAlign: 'right', modal: true,
    fields: [
                { display: "物料名称", name: "PARTNAME", newline: true, type: "text",width:160, group: "基本信息", groupicon: groupicon },
                { display: "需求数量", name: "NEEDQUITAY", newline: false, type: "text",width:160, },
                { display: "交货期限", name: "DELIVERY_DATE", newline: true, type: "date",width:160, },
                { display: "计划资金", name: "BUDGET_MONEY", newline: false, type: "text",width:160, },
                { display: "备注", name: "USERDO", newline: true, type: "textarea" }
                ]
});
$("#USERDO").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:400px"); ;
$("#PARTNAME").attr("validate", "[{required:true, msg:'请选择要采购的物料'},{minlength:2,msg:'物料名称最小长度为2!'}]");
//$("#DELIVERY_DATE").attr("validate", "[{required:true, msg:'请选择交货期限'}]");
$("#NEEDQUITAY").attr("validate", "[{required:true, msg:'请输入需求数量'}]");
$("#NEEDQUITAY").val('1');
$("#BUDGET_MONEY").attr("style", "background-image:url(../../../Images/Icons/money_yen.png);background-position:left bottom;background-repeat: no-repeat; padding-left:15px");
$("#BUDGET_MONEY").val('0');
$("#BUDGET_MONEY").formatCurrency();
$("#PARTNAME").bind("focus",function(){
    f_selectPart();
})
$("#BUDGET_MONEY").bind("blur",function(){
strBugMoney=$(this).val();
$(this).formatCurrency();
})
    function f_selectPart() {
        $.ligerDialog.open({ title: '物料选择', name: 'winselector', width: 700, height: 300, url: 'GetSelectPartList.aspx?keshi='+$.getUrlVar('keshi'), buttons: [
                { text: '确定', onclick: f_selectPartOK },
                { text: '返回', onclick: f_selectPartCancel }
            ]
        });
        return false;
    }

    function f_selectPartOK(item, dialog) {
        var fn = dialog.frame.f_select || dialog.frame.window.f_select;
        var data = fn();
        if (!data||data.length==0) {
            $.ligerDialog.warn('请选择行!');
            return;
        }
        strPartName=data[0].PART_NAME;
        strPartId=data[0].ID;
        $("#PARTNAME").val(strPartName);
        $("#hidPartID").val(strPartId);
        dialog.close();
    }

    function f_selectPartCancel(item, dialog) {
        dialog.close();
    }

if (strPartPlanId != "") {
    var InitDataList = [];
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "PartHandler.ashx?action=GetPartPlanList&strtaskID="+strtaskID+"&strPartPlanId=" + strPartPlanId,
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
    
    $("#PARTNAME").val(getPartName(InitDataList[0].PART_ID));

    $("#NEEDQUITAY").val(InitDataList[0].NEED_QUANTITY);
    $("#USERDO").val(InitDataList[0].USERDO);
    $("#DELIVERY_DATE").val(InitDate(InitDataList[0].DELIVERY_DATE));
    $("#BUDGET_MONEY").val(InitDataList[0].BUDGET_MONEY);
        $("#USERDO").val(InitDataList[0].USERDO);
    $("#BUDGET_MONEY").formatCurrency();
}

})

function getPartName(PartId){
$.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "PartHandler.ashx?action=GetPartList&strPartId=" + PartId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
        strPartName=data.Rows[0].PART_NAME;
        }
    });
    return strPartName;
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

    var strData = "";
    var isTrue = $("#form1").validate();
    if (isTrue) {
        strData += "&strtaskID=" + strtaskID;
        strData += "&strPartPlanId=" + strPartPlanId;
        strData += "&strPartId=" +$("#hidPartID").val();
        strData += "&strNeedQuatity=" + encodeURI($("#NEEDQUITAY").val());
        strData += "&strDevDate=" + encodeURI($("#DELIVERY_DATE").val());
        strData += "&strBudgetMoney=" + encodeURI(strBugMoney);
        strData += "&strUserDo=" + encodeURI($("#USERDO").val());
    }
    return strData;
}