<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_ZZ_SamplingEndQcSettingList" Codebehind="SamplingEndQcSettingList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/jquery.form.js" type="text/javascript"></script>
     <script src="../../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="SamplingEndQcSettingList.js" type="text/javascript"></script>
    <script type="text/javascript">
        var topHeight, bottomHeight;
        $(function () {
            topHeight = 2 * $(window).height() / 5 - 35;
            bottomHeight = 3 * $(window).height() / 5 - 35;

            $("#layout1").ligerLayout({ topHeight: 2 * $(window).height() / 5, leftWidth: 200, allowLeftCollapse: false, height: '100%' });
        });
    </script>
</head>
<body>
    <form id="form2" runat="server">
        <div id="layout1">
            <div position="top">
                <div id="oneGrid">
                </div>
            </div>
            <div position="left" title="监测类别">
                <div id="twoGrid">
                </div>
            </div>
            <div position="center" title="监测样品">
                <div id="threeGrid">
                </div>
            </div>
        </div>
        <input type="hidden" id="hidTaskId" runat="server" />
        <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" Style="display: none;" />
    </form>
</body>



