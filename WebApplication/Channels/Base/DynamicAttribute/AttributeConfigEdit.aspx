<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Base_DynamicAttribute_AttributeConfigEdit" Codebehind="AttributeConfigEdit.aspx.cs" %>

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
    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        $(document).ready(function () {
            //创建表单结构 
            $("#divEdit").ligerForm({
                inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
                fields: [
                { name: "ID", type: "hidden" },
                { display: "监测类别", name: "ITEM_TYPE_NAME", newline: true, type: "select", comboboxName: "ITEM_TYPE_NAME", group: "基本信息", groupicon: groupicon, options: { valueFieldID: "ITEM_TYPE", valueField: "ID", textField: "MONITOR_TYPE_NAME", url: "AttributeConfigEdit.aspx?type=getMonitorTypeInfo"} },
                { display: "属性类别", name: "ATTRIBUTE_TYPE_ID_NAME", newline: true, type: "select", comboboxName: "ATTRIBUTE_TYPE_ID_NAME", options: { valueFieldID: "ATTRIBUTE_TYPE_ID", valueField: "ID", textField: "SORT_NAME", url: "AttributeConfigEdit.aspx?type=getAttributeTypeInfo"} },
                { display: "属性", name: "ATTRIBUTE_ID_NAME", newline: true, type: "select", comboboxName: "ATTRIBUTE_ID_NAME", options: { valueFieldID: "ATTRIBUTE_ID", valueField: "ID", textField: "ATTRIBUTE_NAME", url: "AttributeConfigEdit.aspx?type=getAttributeInfo"} },
                { display: "排序", name: "SN", newline: true, type: "text" }
                ]
            });
            $("#SN").attr("validate", "[{maxlength:8,msg:'排序最大长度为8'}]");

            //获取属性类别数据
            var AttributeTypeList = [];
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: "AttributeConfigEdit.aspx?type=getAttributeTypeInfo",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    AttributeTypeList = data;
                }
            });
            //监测类别下拉列表改变时候实现联动效果
            $.ligerui.get("ITEM_TYPE_NAME").bind('Selected', function (value, text) {
                var newData = new Array();
                for (i = 0; i < AttributeTypeList.length; i++) {
                    if (AttributeTypeList[i].MONITOR_ID == value) {
                        newData.push(AttributeTypeList[i]);
                    }
                }
                $.ligerui.get("ATTRIBUTE_TYPE_ID_NAME").setData(newData);
            });
            //加载数据
            if ($("#formId").val() != "") {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "AttributeConfigEdit.aspx?type=loadData&id=" + $("#formId").val(),
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
