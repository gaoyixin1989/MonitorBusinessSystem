
var strCompanyInfoId="";
$(document).ready(function () {
    $("#Company_Name").unautocomplete();

    $("#Company_Name").autocomplete("../Contract/MethodHander.ashx?action=GetCompanyInfo",
    {
        max: 12,     // 列表里的条目数 
        minChars: 0,     // 自动完成激活之前填入的最小字符 
        matchContains: true,     // 包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示 
        autoFill: false,     // 自动填充 
        width: 250,
        max: 20,
        dataType: "json",
        scrollHeight: 150,
        parse: function (data) {

            return $.map(eval(data), function (row) {
                return {
                    data: row,

                    value: row.ID + " <" + row.COMPANY_NAME + ">",
                    result: row.COMPANY_NAME

                }
            });
        },
        formatItem: function (item) {
            //return "<font color=green>" + item.ID + "</font>&nbsp;(" + item.COMPANY_NAME + ")";
            return item.COMPANY_NAME;
        }
    }
    );
    $("#Company_Name").result(findValueCallback); //加这个主要是联动显示id


    $("#Company_Name").bind("change", function () {
        if ($("#Company_Name").val() == "") {
            strCompanyInfoId = "";
        }
    })
    function findValueCallback(event, data, formatted) {
        strCompanyInfoId = data["ID"]; //获取选择的ID
    }
});

function GetCompanyIdStr() {
    return strCompanyInfoId;
}