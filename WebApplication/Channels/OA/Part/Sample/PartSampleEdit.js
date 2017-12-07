///标准样品新增功能
///创建人：魏林 2013-09-16
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var url = "PartSampleEdit.aspx";
$(document).ready(function () {

    //创建表单结构 
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
        fields: [
                { display: "编号", name: "SAMPLE_CODE", newline: true, type: "text", group: "样品信息", groupicon: groupicon },
                { display: "项目名称", name: "SAMPLE_NAME", newline: false, type: "text" },
                { display: "类别", name: "SAMPLE_TYPE_NAME", newline: true, type: "select", comboboxName: "SAMPLE_TYPE_BOX", options: { valueFieldID: "SAMPLE_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=sample_type"} },
                { display: "分类", name: "CLASS_TYPE_NAME", newline: false, type: "select", comboboxName: "CLASS_TYPE_BOX", options: { valueFieldID: "CLASS_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=sample_class"} },
                { display: "数量", name: "INVENTORY", newline: true, type: "text" },
                { display: "单位", name: "UNIT", newline: false, type: "text" },
                { display: "购置日期", name: "BUY_DATE", newline: true, type: "date" },
                { display: "有效日期", name: "EFF_DATE", newline: false, type: "date" },
                { display: "样品来源", name: "SAMPLE_SOURCE_NAME", newline: true, type: "select", comboboxName: "SAMPLE_SOURCE_BOX", options: { valueFieldID: "SAMPLE_SOURCE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=sample_sources"} },
                { display: "质量等级", name: "LEVEL_NAME", newline: false, type: "select", comboboxName: "LEVEL_BOX", options: { valueFieldID: "LEVEL", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=sample_level"} },
                { display: "浓度", name: "POTENCY", newline: true, type: "text" },
                { display: "保管人", name: "CARER", newline: false, type: "text" }
                ]
    });

    //添加表单验证
    $("#SAMPLE_CODE").attr("validate", "[{required:true,msg:'编号不能为空'},{maxlength:32,msg:'断面代码录入最大长度为32'}]");
    $("#SAMPLE_NAME").attr("validate", "[{required:true,msg:'项目名称不能为空'},{maxlength:128,msg:'断面名称录入最大长度为128'}]");
    $("#INVENTORY").attr("validate", "[{required:true,msg:'数量不能为空'}]");

    $("#BUY_DATE").val(currentTime());
});

//JS 获取当前时间
function currentTime() {

    var d = new Date(), str = '';
    str += d.getFullYear() + '-';
    str += d.getMonth() + 1 + '-';
    str += d.getDate();
    return str;
}