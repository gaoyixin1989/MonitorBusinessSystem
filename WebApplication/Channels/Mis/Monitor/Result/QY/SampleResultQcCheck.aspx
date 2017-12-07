<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_Result_QY_SampleResultQcCheck" Codebehind="SampleResultQcCheck.aspx.cs" %>

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
    <script src="SampleResultQcCheck.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            $("#layout1").ligerLayout({ topHeight: 280, leftWidth: 430, allowLeftCollapse: false, height: 760 });
            $("#layout2").ligerLayout({ topHeight: 260, leftWidth: '100%', rightWidth: '0%', allowLeftCollapse: false, allowRightCollapse: false });
            $("#layout3").ligerLayout({ leftWidth: '30%', rightWidth: '70%', allowLeftCollapse: false, allowRightCollapse: false, height: '100%' });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <form id="form1">
    <div id="layout1">
        <div position="top">
            <div id="layout2">
                <div position="left">
                    <div id="oneGrid">
                    </div>
                </div>
            </div>
        </div>
        <div position="center">
        <div id="layout3">
        <div position="left" title="样品信息">
            <div id="twoGrid">
            </div>
        </div>
        <div position="right" title="现场监测项目">
            
                <div id="threeGrid">
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
</asp:Content>
