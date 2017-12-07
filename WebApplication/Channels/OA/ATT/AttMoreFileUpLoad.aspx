<%@ Page Language="C#" AutoEventWireup="True" Inherits="n16.Channels_OA_ATT_AttMoreFileUpLoad" Codebehind="AttMoreFileUpLoad.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
         <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"    type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
   <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
   <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"  type="text/javascript"></script>
    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
   <script src="./../../Scripts/jquery.form.js" type="text/javascript"></script> 
    <script src="AttMoreFileUpLoad.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         <asp:Button ID="btnExcelOut" runat="server" OnClick="btnExcelOut_Click" Style="display: none" />
       <div id="Grid" style="width:100%; height:100%;">
    </div>
    <input type="hidden"  id="hidSave" runat="server" />
<input type="hidden"  id="hidType"  runat="server" /> 
    </form>
</body>
</html>
