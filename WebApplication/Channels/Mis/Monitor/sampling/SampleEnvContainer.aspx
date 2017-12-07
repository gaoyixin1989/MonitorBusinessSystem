<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Mis_Monitor_sampling_SampleEnvContainer" Codebehind="SampleEnvContainer.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="SampleEnvContainer.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            padding: 2px;
            margin: 0;
        }
        #divLayout
        {
            width: 100%;
            margin: 40px;
            height: 400px;
            margin: 0;
            padding: 0;
        }
        #accordion1
        {
            height: 270px;
        }
        h4
        {
            margin: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divLayout" style="text-align: left">
        <div position="top" title="操作">
            <input type="button" value="完成" id="btn_Ok" name="btn_Ok" class="l-button l-button-submit"
                onclick="btnFinish()" style="display: inline; margin-top: 8px;" />
        </div>
        <div position="center">
            <iframe id="iFrame" name="iFrame" frameborder="0" width="100%" height="100%"></iframe>
        </div>
    </div>
    <asp:HiddenField ID="hiddenSubTaskId" runat="server" />
    <asp:HiddenField ID="hiddenMonitorType" runat="server" />
    <asp:HiddenField ID="hiddenYear" runat="server" />
    <asp:HiddenField ID="hiddenMonth" runat="server" />
    </form>
</body>
</html>
