<%@ Page Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True"
    Inherits="Channels_Mis_Monitor_sampling_ZZ_SamplePointView" Codebehind="SamplePointView.aspx.cs" %>

<asp:content id="head" runat="server" contentplaceholderid="head">
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="SamplePointView.js" type="text/javascript"></script>
    <script src="SamplePoint_Item_GridView.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            $("#layout3").ligerLayout({ leftWidth: "50%", rightWidth: "48%", height: 480, allowLeftCollapse: false, allowRightCollapse: false });
        });
    </script>
</asp:content>
<asp:content id="Content1" contentplaceholderid="cphData" runat="Server">
    <input id="SUBTASK_ID" runat="server" type="hidden" />
    <div id="layout3" style="text-align: left">
        <div position="left" title="监测点位">
            <div id="PointGrid">
            </div>
        </div>
        <div position="right" title="监测项目">
            <div id="ItemGrid">
            </div>
        </div>
    </div>
    <div id="detail" style="display: none;">
        <form id="editItemForm" method="post">
        </form>
    </div>
    <div id="detailSrh" style="display: none;">
        <div id="InputForm">
        </div>
    </div>
    <div id="detailRemark" style="display: none;">
        <div id="RemarkForm">
        </div>
    </div>
</asp:content>
