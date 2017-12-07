<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_Result_QY_AnalysisMasterQcCheck" Codebehind="AnalysisMasterQcCheck.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <script src="AnalysisMasterQcCheck.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#layout1").ligerLayout({ topHeight: 200, leftWidth: 360, allowLeftCollapse: false, height: 760 });
            $("#layout2").ligerLayout({ topHeight: 180, leftWidth: '55%', rightWidth: '45%', allowLeftCollapse: false, allowRightCollapse: false, height: 510 });

            $("#layout3").ligerLayout({ topHeight: 200, leftWidth: 360, allowLeftCollapse: false, height: 760 });
            $("#layout4").ligerLayout({ topHeight: 180, leftWidth: '75%', rightWidth: '25%', allowLeftCollapse: false, allowRightCollapse: false, height: 510 });
        });
    </script>
</head>
<body  style="padding: 0px; overflow: hidden; width: 99%; height: 100%;">
    <form id="form1" runat="server">
    <div id="navtab1" style="width: 100%; overflow: hidden; border: 1px solid #A3C0E8;">
        <div id="layout1"  title="待办任务列表">
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
        <div id="layout3"  title="已办任务列表">
            <div position="top">
                <div id="oneGrid2">
                </div>
            </div>
            <div position="left" title="样品信息">
                <div id="twoGrid2">
                </div>
            </div>
            <div position="center" title="监测项目信息">
                <div id="layout4">
                    <div position="top">
                        <div id="threeGrid2">
                        </div>
                    </div>
                    <div position="left" title="现场质控">
                        <div id="fourGrid2">
                        </div>
                    </div>
                    <div position="right" title="实验质控">
                        <div id="fiveGrid2">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
                               <div id="detailRemark" style="display:none;">
    <div id="RemarkForm"></div>
    </div>
<div id="detailSampleRemark" style="display:none;">
    <div id="SampleRemarkForm"></div>
    </div>
    </form>
    <div id="divSugg" style="display:none;">
        <div id="SuggForm"></div>
    </div>
</body>
</html>