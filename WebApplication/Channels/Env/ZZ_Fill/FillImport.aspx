<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Env_ZZ_Fill_FillImport" Codebehind="FillImport.aspx.cs" %>

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
        var url = "FillImportDemo.aspx";
        //Excel导入方法
        function Import(Type) {
            $("#actions").val(Type); //填报类型
            document.getElementById("btnImport").click();
        }
    </script>
</head>
<body>
<div id="dataImportDiv">
        <form id="dataImportForm" method="post" enctype="multipart/form-data" runat="server">
        <div style="margin: 10px">
            <div style="height: 35px; margin-left: 50px; padding-top: 40px">
                <div style="float: left">
                    导入数据文件：</div>
                <div style="float: left">
                    <asp:FileUpload ID="importFiles" Width="300px" runat="server" />
                </div>
            </div>
        </div>
        <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" Style="display:none " />
         <asp:TextBox ID="actions" runat="server" Style="display:none"></asp:TextBox>
         <asp:Label ID="lable" runat ="server" type="hidden" Style="color:Red; margin-left: 50px; "></asp:Label>
        </form>
    </div>
</html>
