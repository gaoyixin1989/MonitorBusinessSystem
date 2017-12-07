<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_ZZ_SamplingView" Codebehind="SamplingView.aspx.cs" %>

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
    <script src="SamplingView.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#layout1").ligerLayout({ height: 510, width: '100%' });
        });
        $(document).ready(function () {
            tab = $("#framecenter").ligerTab({ height: 530, contextmenu: true });
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 99%">
    <input id="SUBTASK_ID" runat="server" type="hidden" />
    <div id="layout1" style="width: 100%; overflow: hidden; border: 1px solid #A3C0E8;">
        <div position="center" id="framecenter">
            <div tabid="home" title="现状信息" lselected="true">
                <iframe frameborder="0" id="SampleLicaleID" name="showmessage" src='SampleLocaleView.aspx?strSubtaskID=<%=this.Request["strSubtaskID"].ToString() %>&IS_SEND=<%=this.Request["IS_SEND"].ToString() %>'>
                </iframe>
            </div>
            <div title="监测点位">
                <iframe frameborder="0" id="SamplePointID" name="showmessage" src='SamplePointView.aspx?strSubtaskID=<%=this.Request["strSubtaskID"].ToString() %>&IS_SEND=<%=this.Request["IS_SEND"].ToString() %>'>
                </iframe>
            </div>
            <div title="现场监测项目">
                <iframe frameborder="0" id="SampleResultID" name="showmessage" src='SampleResultView.aspx?strSubtaskID=<%=this.Request["strSubtaskID"].ToString() %>&IS_SEND=<%=this.Request["IS_SEND"].ToString() %>'>
                </iframe>
            </div>
        </div>
    </div>
    <div style="text-align: center; width: 90%">
        <input type="button" value="发送" id="btn_Ok" name="btn_Ok" class="l-button l-button-submit"
            onclick="SendClick();" style="display: inline; margin-top: 8px;" />
    </div>
    </form>
</body>
</html>