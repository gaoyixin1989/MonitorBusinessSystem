<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Env_Point_PointInfoCopy" Codebehind="PointInfoCopy.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
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
    <script src="../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        var url = "PointInfoCopy.aspx";

        $(document).ready(function () {
            //创建表单结构 
            $("#divMain").ligerForm({
                inputWidth: 100, labelWidth: 80, space: 1, labelAlign: 'right',
                fields: [
                { display: "从 年份", name: "StartYear", newline: true, type: "select", comboboxName: "StartYear_Box", group: "复制信息", groupicon: groupicon, options: { valueFieldID: "StartYear", valueField: "value", textField: "value", url: url + "?type=getYearInfo"} },
                { display: "月份", name: "StartMonth", labelWidth: 50, newline: false, type: "select", comboboxName: "StartMonth_Box", options: { valueFieldID: "StartMonth", valueField: "value", textField: "value", url: url + "?type=getMonthInfo"} },
                { display: "  复制到 年份", name: "EndYear", labelWidth: 90, newline: false, type: "select", comboboxName: "EndYear_Box", options: { valueFieldID: "EndYear", valueField: "value", textField: "value", url: url + "?type=getYearInfo"} },
                { display: "月份", name: "EndMonth", labelWidth: 50, newline: false, type: "select", comboboxName: "EndMonth_Box", options: { valueFieldID: "EndMonth", valueField: "value", textField: "value", url: url + "?type=getMonthInfo"} }
                ]
            });
            //添加表单验证
            $("#StartYear_Box").attr("validate", "[{required:true,msg:'请选择复制年份'}]");
            $("#StartMonth_Box").attr("validate", "[{required:true,msg:'请选择复制月份'}]");
            $("#EndYear_Box").attr("validate", "[{required:true,msg:'请选择复制到的年份'}]");
            $("#EndMonth_Box").attr("validate", "[{required:true,msg:'请选择复制到的月份'}]");
        });
        //表单验证
        function formValidate() {
            if ($("#form1").validate()) {
                if ($("#StartYear").val() == $("#EndYear").val() && $("#StartMonth").val() == $("#EndMonth").val()) {
                    $.ligerDialog.warn("复制的年月与复制到的年月相同");
                    return false;
                }
                else
                    return true;
            } else {
            return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divMain">
    </div>
    </form>
</body>
</html>
