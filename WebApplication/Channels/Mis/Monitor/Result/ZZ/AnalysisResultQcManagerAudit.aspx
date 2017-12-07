<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_Result_ZZ_AnalysisResultQcManagerAudit" Codebehind="AnalysisResultQcManagerAudit.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="AnalysisResultQcManagerAudit.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            $("#layout1").ligerLayout({ topHeight: 250, leftWidth: 250, allowLeftCollapse: false, height: 760 });
            $("#layout2").ligerLayout({ topHeight: 260, leftWidth: '50%', rightWidth: '50%', allowLeftCollapse: false, allowRightCollapse: false, height: 510 });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <form id="form1">
    <div id="layout1">
        <div position="top">
            <div id="oneGrid">
            </div>
        </div>
        <div position="left" title="样品信息">
            <div id="twoGrid">
            </div>
        </div>
        <div position="center" title="监测项目信息">
            <div id="layout2">
                <div position="top">
                    <div id="threeGrid">
                    </div>
                </div>
                <div position="left" title="一级质控">
                    <div id="fourGrid">
                    </div>
                </div>
                <div position="right" title="二级质控">
                    <div id="fiveGrid">
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>