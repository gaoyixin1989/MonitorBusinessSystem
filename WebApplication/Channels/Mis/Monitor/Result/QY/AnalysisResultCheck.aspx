<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_Result_QY_AnalysisResultCheck" Codebehind="AnalysisResultCheck.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
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
    <script src="AnalysisResultCheck.js" type="text/javascript"></script>
    <script type="text/javascript">
        var topHeight, twoHeight, threeHeight, fourHeight;

        $(function () {
            topHeight = 2 * $(window).height() / 5 - 35;
            twoHeight = 3 * $(window).height() / 5 - 35;
            threeHeight = 3 * $(window).height() / 10 - 35;
            fourHeight = 3 * $(window).height() / 10 - 35;
            
            $("#layout1").ligerLayout({ topHeight: topHeight, leftWidth: 430, allowLeftCollapse: false, height: '100%' });
            $("#layout2").ligerLayout({ leftWidth: '50%', rightWidth: '50%', allowLeftCollapse: false, allowRightCollapse: false, height: '100%' });

            $("#layout3").ligerLayout({ topHeight: topHeight, leftWidth: 430, allowLeftCollapse: false, height: '100%' });
            $("#layout4").ligerLayout({ leftWidth: '50%', rightWidth: '50%', allowLeftCollapse: false, allowRightCollapse: false, height: '100%' });
        });
    </script>
    <style type="text/css">
        #layout1
        {
            width: 100%;
            margin: 0;
            padding: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <div id="navtab1" style="width: 100%; overflow: hidden; border: 1px solid #A3C0E8;">
        <div id="layout1" title="待办任务列表">
            <div position="top">
                <div id="layout2">
                    <div position="left" title="任务单号">
                        <div id="oneGrid">
                        </div>
                    </div>
                    <div position="right" title="监测类别">
                        <div id="twoGrid">
                        </div>
                    </div>
                </div>
            </div>
            <div position="left" title="样品信息">
                <div id="threeGrid">
                </div>
            </div>
            <div position="center" title="监测项目信息">
                <div id="fourGrid">
                </div>
                <div id="fiveGrid">
                </div>
            </div>
        </div>
        <div id="layout3" title="已办任务列表">
            <div position="top">
                <div id="layout4">
                    <div position="left" title="任务单号">
                        <div id="oneGrid2">
                        </div>
                    </div>
                    <div position="right" title="监测类别">
                        <div id="twoGrid2">
                        </div>
                    </div>
                </div>
            </div>
            <div position="left" title="样品信息">
                <div id="threeGrid2">
                </div>
            </div>
            <div position="center" title="监测项目信息">
                <div id="fourGrid2">
                </div>
                <div id="fiveGrid2">
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
    <div id="divSugg" style="display:none;">
        <div id="SuggForm"></div>
    </div>
</asp:Content>