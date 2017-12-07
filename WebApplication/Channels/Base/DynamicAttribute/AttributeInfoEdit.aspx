<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Base_DynamicAttribute_AttributeInfoEdit" Codebehind="AttributeInfoEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        $(document).ready(function () {
            //创建表单结构 
            $("#divEdit").ligerForm({
                inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
                fields: [
                { name: "ID", type: "hidden" },
                { display: "属性名称", name: "ATTRIBUTE_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "控件ID", name: "CONTROL_ID", newline: true, type: "text" },
                { display: "控件名称", name: "CONTROL_NAME_NAME", newline: true, type: "select", comboboxName: "CONTROL_NAME_NAME", options: { valueFieldID: "CONTROL_NAME", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "AttributeInfoEdit.aspx?type=getDict&dictType=AttributeControlType"} },
                { display: "控件宽度", name: "WIDTH", newline: true, type: "text" },
                { display: "是否可编辑", name: "ENABLE", newline: true, type: "select", comboboxName: "ENABLE_NAME", options: { valueFieldID: "ENABLE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "AttributeInfoEdit.aspx?type=getDict&dictType=AttributeYesNo"} },
                { display: "可否为空", name: "IS_NULL", newline: true, type: "select", comboboxName: "IS_NULL_NAME", options: { valueFieldID: "IS_NULL", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "AttributeInfoEdit.aspx?type=getDict&dictType=AttributeYesNo"} },
                { display: "最大长度", name: "MAX_LENGTH", newline: true, type: "text" },
                { display: "字典项", name: "DICTIONARY", newline: true, type: "text", group: "数据库信息", groupicon: groupicon },
                { display: "数据库表名", name: "TABLE_NAME", newline: true, type: "text" },
                { display: "文本字段", name: "TEXT_FIELD", newline: true, type: "text" },
                { display: "值字段", name: "VALUE_FIELD", newline: true, type: "text" }
                ]
            });

            $("#ATTRIBUTE_NAME").attr("validate", "[{required:true, msg:'请填写属性名称'},{maxlength:64,msg:'属性名称不能超过64'}]");
            $("#CONTROL_ID").attr("validate", "[{required:true, msg:'请填写控件ID'},{maxlength:64,msg:'控件ID不能超过64'}]");
            $("#WIDTH").attr("validate", "[{maxlength:32,msg:'控件宽度不能超过32'}]");
            $("#MAX_LENGTH").attr("validate", "[{maxlength:32,msg:'最大长度不能超过32'}]");
            $("#DICTIONARY").attr("validate", "[{maxlength:64,msg:'字典项不能超过64'}]");
            $("#TABLE_NAME").attr("validate", "[{maxlength:64,msg:'数据库表名不能超过64'}]");
            $("#TEXT_FIELD").attr("validate", "[{maxlength:64,msg:'文本字段不能超过64'}]");
            $("#VALUE_FIELD").attr("validate", "[{maxlength:64,msg:'值字段不能超过64'}]");
            //加载数据
            if ($("#formId").val() != "") {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "AttributeInfoEdit.aspx?type=loadData&id=" + $("#formId").val(),
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
