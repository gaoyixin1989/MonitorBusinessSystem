// Create by 潘德军 2012.11.16  "附加费用设置"功能
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strID = "";

$(document).ready(function () {
    strID = request('strid');
    if (!strID)
        strID = "";

    //创建表单结构 
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 90, labelAlign: 'right',
        fields: [
                { name: "ID", type: "hidden" },
                { display: "附加项目", name: "ATT_FEE_ITEM", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "费用单价", name: "PRICE", newline: true, type: "text" },
                { display: "费用描述", name: "INFO", newline: true, type: "text" }
                ]
    });
    //add validate by ssz
    $("#ATT_FEE_ITEM").attr("validate", "[{required:true,msg:'请填写附加项目'},{maxlength:64,msg:'附加项目最大长度为64'}]");
    $("#PRICE").attr("validate", "[{required:true,msg:'请填写费用单价'},{maxlength:64,msg:'费用单价最大长度为64'}]");
    $("#INFO").attr("validate", "[{maxlength:64,msg:'费用描述最大长度为64'}]");

    //加载数据
    if (strID != "") {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "AttachPriceEdit.aspx?type=loadData&strid=" + strID,
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
    //表单验证
    if (!$("#divEdit").validate())
        return false;
    var strData = "{";

    if (!strID)
        strData += "'strID':'',";
    else
        strData += "'strID':'" + strID + "',";

    strData += "'strAttFeeItem':'" + $("#ATT_FEE_ITEM").val() + "',";
    strData += "'strPrice':'" + $("#PRICE").val() + "',";
    strData += "'strInfo':'" + $("#INFO").val() + "'";

    strData += "}";

    return strData;
}