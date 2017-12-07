<%@ Page Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True" Inherits="Channels_MisII_sampling_SamplePoint" Codebehind="SamplePoint.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>

    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>

    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="SamplePoint.js" type="text/javascript"></script>
    <script src="SamplePoint_Item_Grid.js" type="text/javascript"></script>
    <script src="SamplePointEdit_Select.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            $("#layout3").ligerLayout({ leftWidth: "70%", rightWidth: "29%", height: 500, allowLeftCollapse: false, allowRightCollapse: false });
        });
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
<input id="SUBTASK_ID" runat="server" type="hidden" />
    <div id="layout3" style="width: 99%;overflow:hidden; border:1px solid #A3C0E8; ">
        <div position="left" title="监测点位">
            <div id="PointGrid"></div>
        </div>
        <div position="right"  title="监测项目">
            <div id="ItemGrid"></div>
        </div>
    </div> 
    <div id="detail" style="display:none;"><form id="editItemForm" method="post"></form> </div>
       <div id="detailSrh" style="display:none;">
    <div id="InputForm"></div>
    </div>
           <div id="detailRemark" style="display:none;">
    <div id="RemarkForm"></div> 
    </div>
    <div id="dateDiv" style="display:none;">
        <div id="dateForm"></div> 
    </div>
</asp:Content>
