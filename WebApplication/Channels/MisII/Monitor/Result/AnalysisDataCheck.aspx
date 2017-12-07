<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_MisII_Monitor_Result_AnalysisDataCheck" Codebehind="AnalysisDataCheck.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="AnalysisDataCheck.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //            $("#layout1").ligerLayout({ leftWidth: 400, allowLeftCollapse: false, height: '100%' });
            $("#layout1").ligerLayout({ topHeight: 200, leftWidth: 360, allowLeftCollapse: false, height: 760 });
            $("#layout2").ligerLayout({ topHeight: 180, leftWidth: '55%', rightWidth: '45%', allowLeftCollapse: false, allowRightCollapse: false, height: 510 });
        });
    </script>
</head>
<body  style="padding: 0px; overflow: hidden; width: 99%; height: 100%;">

        <input id="TASK_ID" runat="server" type="hidden" />
        <input id="SUBTASK_ID" runat="server" type="hidden" />
        <input id="PLAN_ID" runat="server" type="hidden" />

    <form id="form1" runat="server">
    
    <div id="layout1">
            <div position="top">
                <div id="oneGrid">
                </div>
            </div>
            <div position="left" title="样品信息">
                <div id="twoGrid">
                </div>
            </div>
            <div position="center" title="监测项目信息">
                <div id="layout2">
                    <div position="top">
                        <div id="threeGrid">
                        </div>
                    </div>
                    <div position="left" title="现场质控">
                        <div id="fourGrid">
                        </div>
                    </div>
                    <div position="right" title="实验质控">
                        <div id="fiveGrid">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--<div id="layout1">
            <div position="left" title="样品信息列表">
                <div id="SampleGrid">
                </div>
            </div>
            <div position="center" title="监测项目信息">
                <div id="ItemGrid">
                </div>
                <div id="QCGrid1">
                </div>
                <div id="QCGrid2">
                </div>
                
            </div>
        </div>--%>
    <div id="detailRemark" style="display:none;">
        <div id="RemarkForm"></div>
    </div>
    <div id="detailSampleRemark" style="display:none;">
        <div id="SampleRemarkForm"></div>
    </div>
    </form>
</body>
</html>