<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="true" Codebehind="EnterpriseInfo.aspx.cs" Inherits="Channels_Env_Point_PolluteRule_EnterpriseInfo" %>

<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
     <script src="PolluteRule.js" type="text/javascript"></script>
     <script type ="text/javascript">
         $(function () {
             GlobalMonitorType = "EnvPollute";
             topHeight = 2 * $(window).height() / 5 - 35;
             bottomHeight = 3 * $(window).height() / 5 - 35;
             $("#layout").ligerLayout({ topHeight: 2 * $(window).height() / 5 + 30, leftWidth: 780, rightWidth:720, allowLeftCollapse: false, height: '100%' });
             $("#layout1").ligerLayout({ leftWidth: '66%', rightWidth: '33.5%', allowLeftCollapse: false, allowRightCollapse: false, height: '100%' });
         });
</script> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" Runat="Server">
    <div id="Seachdetail" style="display: none;">
        <div id="Seachdiv">
        </div>
    </div>
        <div id="layout">
        <div position="top">
          <div id="layout1">
        <div position="left" title="企业信息">
            <div id="oneGrid">
            </div>
              </div>
        <div position="right" title="类别">
            <div id="twoGrid">
            </div>
        </div>
        </div>
        </div>
        <div position="left" title="监测点信息">
            <div id="threeGrid">
            </div>
        </div>
        <div position="right" title="监测项目信息">
            <div id="FourGrid">
            </div>
        </div>
        </div>
             <input type="hidden" id="VerticalID" />
       
  
</asp:Content>

