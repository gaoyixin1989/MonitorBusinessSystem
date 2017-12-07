<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Statistice_QCAccount_Bak" Codebehind="QCAccount_Bak.aspx.cs" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/style.css" rel="stylesheet" type="text/css" />
     <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
     
    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
        <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $("#navtab1").ligerTab({ contextmenu: false });
        });
    </script>
    <title></title>
            <style type="text/css">
    #menu1,.l-menu-shadow{top:10px; left:20px;}
    #menu1{  width:200px;}
    </style>
</head>
<body >
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <table>
            <tr>
                <td>
                    年份：
                </td>
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    季度：
                </td>
                <td>
                    <asp:DropDownList ID="ddlQuarter" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    监测类别：
                </td>
                <td>
                    <asp:DropDownList ID="dllMonitorID" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnStatistical" runat="server" Text="查询"
                        onclick="btnStatistical_Click" />
                </td>
            </tr>
        </table>

        <div id="navtab1" style="width: 98%;overflow:hidden; border:1px solid #A3C0E8; " >
            <div tabid="home" title="质量控制结果统计表"  lselected="true"  style="height:90%" >
                <rsweb:ReportViewer ID="ReportViewer1" runat="server">
                </rsweb:ReportViewer>
            </div>
            <div  title="现场空白" >


            </div>
        </div>
    </form>
</body>
</html>
