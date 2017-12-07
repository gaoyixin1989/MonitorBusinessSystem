﻿<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_QHD_SamplingBeginQc" Codebehind="SamplingBeginQc.aspx.cs" %>

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
    <script src="../../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="SamplingBeginQc.js" type="text/javascript"></script>
    <script type="text/javascript">
        var topHeight, bottomHeight;

        $(function () {
            topHeight = 2 * $(window).height() / 5 - 35;
            bottomHeight = 3 * $(window).height() / 5 - 35;

            $("#layout1").ligerLayout({ topHeight: 2 * $(window).height() / 5, leftWidth: 500, allowLeftCollapse: false, height: '100%' });
            $("#layout2").ligerLayout({ leftWidth: '50%', rightWidth: '50%', allowLeftCollapse: false, allowRightCollapse: false, height: '100%' });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <form runat="server" id="form1" name="form1">
    <div id="layout1">
        <div position="top">
            <div id="layout2">
                <div position="left" title="任务单号">
                    <div id="oneGrid">
                    </div>
                </div>
                <div position="right" title="监测类别">
                    <div id="twoGrid">
                    </div>
                </div>
            </div>
        </div>
        <div position="left" title="样品号">
            <div id="threeGrid">
            </div>
        </div>
        <div position="center" title="监测项目">
            <div id="fourGrid">
            </div>
        </div>
    </div>
    
    <div>
        <asp:Button ID="btnExport" runat="server" Text="" OnClick="btnExport_Click" Style="display:none;" />
        <input type="hidden"  id="hidPlanId" runat="server"  />
        <input type="hidden"  id="hidASK_DATE" runat="server" />
        <input type="hidden"  id="hidFINISH_DATE" runat="server" />
        <input type="hidden"  id="hidMonitorId" runat="server" />
    </div>
    </form>
</asp:Content>
