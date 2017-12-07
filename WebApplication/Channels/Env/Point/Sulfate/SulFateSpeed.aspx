<%@ Page Language="C#" AutoEventWireup="True"  MasterPageFile="~/Masters/SearchEx.master" Inherits="Channels_Env_Point_Sulfate_SulFateSpeed" Codebehind="SulFateSpeed.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
<link href ="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel ="Stylesheet" type ="text/css" />
<link href ="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel ="Stylesheet" type="text/css" />
<script src ="../../../../Scripts/jquery-1.8.2.min.js" type ="text/ecmascript" ></script>
<script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type ="text/javascript" ></script>
<script src ="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type ="text/javascript" ></script>
<script src ="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript" ></script>
<script src ="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type ="text/javascript" ></script>
<script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type ="text/javascript" ></script>
<script  src ="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type ="text/javascript" ></script>
<script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type ="text/javascript" ></script>
<script src ="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type ="text/javascript" ></script>
<script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type ="text/javascript" ></script>
<script src="SulFateSpeed.js" type="text/javascript" ></script>
<script type ="text/javascript">
    $(function () {
        var topHeight = $(window).height() / 2;
        //        $("#layout1").ligerLayout({ topHeight: topHeight, height: "100%" });
        $("#layout1").ligerLayout({ leftWidth: "70%", rightWidth: "30%" });
        GlobalMonitorType = "EnvSpeed";
    });
</script> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <div id="layout1">
        <div position="left">
            <div id="oneGrid">
            </div>
        </div>
        <div position="right">
            <div id="twoGrid">
            </div>
        </div>
    </div>
    <input type="hidden" id="VerticalID" />
</asp:Content>