<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master"  AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_QHD_SamplingAllocationSheet" Codebehind="SamplingAllocationSheet.aspx.cs" %>


<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
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
    <script src="SamplingAllocationSheet.js" type="text/javascript"></script>
    <script type="text/javascript">
        var topHeight, bottomHeight;

        $(function () {
            topHeight = 2 * $(window).height() / 5 - 35;
            bottomHeight = 3 * $(window).height() / 5 - 35;

            $("#layout1").ligerLayout({ topHeight: 2 * $(window).height() / 5, leftWidth: 200, allowLeftCollapse: false, height: '100%' });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <form id="form1" runat="server">
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

    <asp:HiddenField ID="strPrintId" runat="server" />
    <asp:HiddenField ID="strPrintId_code" runat="server" />
    <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" Style="display: none;" />
    <asp:Button ID="btnImportCode" runat="server" OnClick="btnImportCode_Click" Style="display: none;" />
    </form>
</asp:Content>