<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_PersonnelFile_Administration_ExamInfor_Admin" Codebehind="ExamInfor_Admin.aspx.cs" %>


<%@ Register TagName="UserInforAdmin"  TagPrefix="UC" Src="~/Channels/OA/PersonnelFile/UserInfor_Admin.ascx" %>
<%@ Register TagName="UserContent"  TagPrefix="UC" Src="~/Channels/OA/PersonnelFile/UserInfor_Content.ascx" %>
<%@ Register TagName="UserOptionAdmin"  TagPrefix="UC" Src="~/Channels/OA/PersonnelFile/UserInfor_AdminOption.ascx" %>
<%@ Register TagName="UserOptionAduit"  TagPrefix="UC" Src="~/Channels/OA/PersonnelFile/UserInfor_AduitOption.ascx" %>
<%@ Register TagName="UserOptionDept"  TagPrefix="UC" Src="~/Channels/OA/PersonnelFile/UserInfor_DeptOption.ascx" %>
<%@ Register TagName="UserOptionPerson"  TagPrefix="UC" Src="~/Channels/OA/PersonnelFile/UserInfor_PersonOption.ascx" %>
<%@ Register TagName="UserOptionCheck"  TagPrefix="UC" Src="~/Channels/OA/PersonnelFile/UserInfor_CheckorRemark.ascx" %>
<%@ Register TagPrefix="UC" TagName="WFControl" Src="~/Sys/WF/UCWFControls.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
          <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
     <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../../CSS/welcome.css" rel="stylesheet" type="text/css" />
     <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
     <script src="../../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script> 
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

        <script src="ExamInfor_Admin.js" type="text/javascript"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="">
        <div id="layout1" >
   <div position="top"> 
    <div>
       <UC:UserInforAdmin runat="server" ID="UserInforAdmin" />
    </div>
        <div style="height:600px;">
        <UC:UserContent runat="server" ID="UserContent" />
    </div>
    </div>
    <div position="center" style=" height:400px">
<div id="OptionDiv" style="display:none;">
        <UC:UserOptionAdmin runat="server" ID="UserOptionAdmin" />
         <UC:UserOptionDept runat="server" ID="UserOptionDept" />
          <UC:UserOptionAduit runat="server" ID="UserOptionAduit" />
        <UC:UserOptionPerson runat="server" ID="UserOptionPerson" />
        <UC:UserOptionCheck runat="server" ID="UserOptionCheck" />
    </div>
<div id="divContratSubmit">
<UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />
</div>
</div>
</div>
<div>
<input type="hidden"  id="hidTaskId" runat="server" />
<input type="hidden"  id="hidLength" value="0" runat="server"  />
<input type="hidden"  id="hidTaskProjectName"  runat="server" />
<input type="hidden"  id="hidContent"  runat="server" />
<input type="hidden"  id="hidOption"  runat="server" />
</div>
    </form>
</body>
</html>

