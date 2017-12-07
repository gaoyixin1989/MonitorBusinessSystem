<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Channels_Base_Search_SearchZZ_ContractSrh" Codebehind="ContractSrh.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="ContractSrh.js" type="text/javascript"></script>
    <script src="../SearchFunction/SearchFunction.js" type="text/javascript"></script>
    <script src="../../../../Scripts/comm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    
    <div id="navtab1" style="width: 100%;overflow:hidden; border:1px solid #A3C0E8; ">
        <div tabid="home" title="未办结列表"  lselected="true" contextmenu="false" >
            <div id="layout1" style="text-align: left">
                <div position="top">
                    <div id="contractGrid">
                    </div>
                </div>
                <div position="left" title="监测报告">
                    <div id="taskGrid">
                    </div>
                </div>
                <div position="right" title="监测结果">
                    <div id="subTaskGrid">
                    </div>
                </div>
            </div>
        </div>
        <div  title="已办结列表">
            <div id="layout2" style="text-align: left">
                <div position="top">
                    <div id="contractGrid1">
                    </div>
                </div>
                <div position="left" title="监测报告">
                    <div id="taskGrid1">
                    </div>
                </div>
                <div position="right" title="监测结果">
                    <div id="subTaskGrid1">
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="detailContract" style="display: none;">
        <form id="divContract" method="post">
        </form>
    </div>
    <div id="detailSrh" style="display: none;">
        <form id="searchForm" method="post"> </form>
    </div>
   <%-- huangjinjun 20140509--%>
    <form id="form1" runat="server" style="display:none">    
        <input type="hidden" id="hiddid" runat="server" />
        <asp:Button runat="server" ID="btnExport" Text="" OnClick="btnExport_Click"/>
    </form>
   <%-- end--%>
</asp:Content>
