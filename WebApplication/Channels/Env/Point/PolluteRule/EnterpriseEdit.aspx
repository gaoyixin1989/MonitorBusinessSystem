<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EnterpriseEdit.aspx.cs" Inherits="Channels_Env_Point_PolluteRule_EnterpriseEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <title></title>
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../../Scripts/jquery.form.js" type="text/javascript"></script>
     <script type="text/javascript">

//var monthJSON = [
//    { "VALUE": "污水厂企业", "TYPE": "污水厂企业" },
//    { "VALUE": "国控企业(废水)", "TYPE": "国控企业(废水)" },
//    { "VALUE": "国控企业(废气)", "TYPE": "国控企业(废气)" },
//    { "VALUE": "重金属企业", "TYPE": "重金属企业" },
//    { "VALUE": "省控企业", "TYPE": "省控企业" },
//    { "VALUE": "敏感地下水企业", "TYPE": "敏感地下水企业" }
//];

         var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
         var url = "EnterpriseEdit.aspx";
         $(document).ready(function () {
             GlobalMonitorType = "EnvPollute";
             //创建表单结构 
             $("#divEdit").ligerForm({
                 inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
                 fields: [
               { display: "企业编码", name: "ENTER_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "企业名称", name: "ENTER_NAME", newline: false, type: "text" },
                { display: "所属省份", name: "PROVINCE_ID_NAME", newline: true, type: "select", comboboxName: "PROVINCE_ID_NAME", options: { valueFieldID: "PROVINCE_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=province"} },
                { display: "所在地区", name: "AREA_ID_NAME", newline: false, type: "select", comboboxName: "AREA_ID_NAME", options: { valueFieldID: "AREA_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=administrative_area"} },
                { display: "一级行业", name: "LEVEL1_NAME", newline: true, type: "select", comboboxName: "LEVEL1_NAME", options: { valueFieldID: "LEVEL1", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=LEVEL1"} },
                { display: "二级行业", name: "LEVEL2_NAME", newline: false, type: "select", comboboxName: "LEVEL2_NAME", options: { valueFieldID: "LEVEL2", valueField: "DICT_CODE", textField: "DICT_TEXT"} },
                { display: "三级行业", name: "LEVEL3_NAME", newline: false, type: "select", comboboxName: "LEVEL3_NAME", options: { valueFieldID: "LEVEL3", valueField: "DICT_CODE", textField: "DICT_TEXT"} },
                { display: "企业类型", name: "REMARK1_NAME", newline: false, type: "select", comboboxName: "REMARK1_NAME", options: { valueFieldID: "REMARK1", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=EnterpriseType"} }
              ]
             });
             $("#ENTER_CODE").attr("validate", "[{required:true,msg:'请填写企业代码'},{maxlength:32,msg:'企业代码录入最大长度为32'}]");
             $("#ENTER_NAME").attr("validate", "[{required:true,msg:'请填写企业名称'},{maxlength:128,msg:'企业名称录入最大长度为128'}]");
             $.ligerui.get("LEVEL1_NAME").bind("selected", function () {
                 //根据一级行业找二级行业
                 if ($("#LEVEL1").val() != "") {
                     $.ajax({
                         cache: false,
                         async: false,
                         type: "POST",
                         url: url + "?type=LEVEL&level=" + $("#LEVEL1").val(),
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         success: function (data, textStatus) {
                             if (data.length > 0) {
                                 $.ligerui.get("LEVEL2_NAME").setData(eval(data));
                                 $.ligerui.get("LEVEL2_NAME").selectValue(data[0].DICT_CODE)
                             }
                         }
                     });
                 }
             });
             $.ligerui.get("LEVEL2_NAME").bind("selected", function () {
             //根据二级行业找三级行业
               if ($("#LEVEL2").val() != "") {
                     $.ajax({
                         cache: false,
                         async: false,
                         type: "POST",
                         url: url + "?type=LEVEL&level=" + $("#LEVEL2").val(),
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         success: function (data, textStatus) {
                             if (data.length > 0) {
                                 $.ligerui.get("LEVEL3_NAME").setData(eval(data));
                                 $.ligerui.get("LEVEL3_NAME").selectValue(data[0].DICT_CODE)
                             }
                         }
                     });
                 }
             });
             //加载数据
             if ($("#formId").val() != "") {
                 $.ajax({
                     cache: false,
                     async: false,
                     type: "POST",
                     url: url + "?type=loadData&id=" + $("#formId").val(),
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (data, textStatus) {
                         bindJsonToPage(data);
                     }
                 });
             }

         });
     </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div id="divEdit">
    </div>
    <input type="hidden" id="formId" runat="server" />
      <input type="hidden" id="formStatus" runat="server" />
    </form>
</body>
</html>
