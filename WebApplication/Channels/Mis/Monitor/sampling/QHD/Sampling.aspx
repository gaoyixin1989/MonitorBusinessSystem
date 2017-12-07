<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_QHD_Sampling" Codebehind="Sampling.aspx.cs" %>

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
    <script src="Sampling.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#layout1").ligerLayout({ height: 530 });
        });
        $(document).ready(function () {
            //tab = $("#framecenter").ligerTab({ height: 550, contextmenu: true });

            tab = $("#framecenter").ligerTab({ height: 550, contextmenu: true, onAfterSelectTabItem: function (tabid) {
                if (tabid == "home") {
                }
                if (tabid == "tabitem1") {
                }
                if (tabid == "tabitem2") {
                    navtab = $("#framecenter").ligerGetTabManager();
                    navtab.reload(navtab.getSelectedTabItemID());
                }

            }
            });
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 99%">
    <input id="SUBTASK_ID" runat="server" type="hidden" />
    <div id="layout1" style="width: 99%;overflow:hidden; border:1px solid #A3C0E8; ">
        <div position="center" id="framecenter">
            <div tabid="home" title="现状信息" lselected="true">
                <iframe frameborder="0" id="SampleLicaleID" name="showmessage" src='SampleLocale.aspx?strSubtaskID=<%=this.Request["strSubtaskID"].ToString() %>'>
                </iframe>
            </div>
            <div title="监测点位">
                <iframe frameborder="0" id="SamplePointID" name="showmessage" src='SamplePoint.aspx?strTaskID=<%=this.Request["strTaskID"].ToString() %>&strSubtaskID=<%=this.Request["strSubtaskID"].ToString() %>&strMonitorID=<%=this.Request["strMonitorID"].ToString() %>'>
                </iframe>
            </div>
            <div title="现场监测项目">
                <iframe frameborder="0" id="SampleResultID" name="showmessage" src='SampleResult.aspx?strSubtaskID=<%=this.Request["strSubtaskID"].ToString() %>'>
                </iframe>
            </div>
            <%--<div  title="点位属性">
    <iframe frameborder="0" id="SampleAttributeID   " name="showmessage" src='PointAttribute.aspx?strSubtaskID=<%=this.Request["strSubtaskID"].ToString() %>'></iframe>
    </div>--%>
        </div>
    </div>
    <div style="text-align: center; width: 100%">
        <input type="button" value="发送" id="btn_Ok" name="btn_Ok" class="l-button l-button-submit"
            onclick="SendClick();" style="display: inline; margin-top: 8px;" />
    </div>
    </form>
</body>
</html>
