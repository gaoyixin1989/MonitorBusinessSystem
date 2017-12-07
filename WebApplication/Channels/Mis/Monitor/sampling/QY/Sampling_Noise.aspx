<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_QY_Sampling_Noise" Codebehind="Sampling_Noise.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
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
    <script src="Sampling_Noise.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="SUBTASK_ID" runat="server" type="hidden" />
    <input id="MONITOR_ID" runat="server" type="hidden" />
    <input id="IS_BACK" runat="server" type="hidden" />
    <input id="Link" runat="server" type="hidden" />
    <div id="layout1" style="width: 99%;overflow:hidden; border:1px solid #A3C0E8; ">
        <div position="center" id="framecenter">
            <div tabid="home" title="现状信息" lselected="true">
                <div id="divContract" style="margin:0px; "></div>
                <div id="divLocale" style="margin:0px; "></div>
            </div>
            <div title="监测点位">
                <div id="divPoint" style="margin:0px; "></div>
            </div>
        </div>
    </div>
    <%--新增表单开始--%>
    <div id="addDiv" style="display: none">
        <div id="addForm">
        </div>
    </div>
    <%--新增表单结束--%>
    <div id="detailRemark" style="display:none;">
        <div id="RemarkForm"></div>
    </div>
    <%--发送人表单开始--%>
    <div id="sendDiv" style="display: none">
        <div id="sendForm">
        </div>
    </div>
    <%--发送人表单结束--%>
    <div style="text-align: center; width: 100%">
        <input type="button" value="发送" id="btn_Ok" onclick="showSend();" name="btn_Ok" class="l-button l-button-submit" style="display: inline; margin-top: 8px;" />
        <input type="button" value="退回" id="btn_Back" name="btn_Back" class="l-button l-button-submit" onclick="BackClick();" style="display: inline; margin-top: 8px;" />
        <label id="lbSuggestion" style="color:Red;"></label>
    </div>
    
    </form>
</body>
</html>
