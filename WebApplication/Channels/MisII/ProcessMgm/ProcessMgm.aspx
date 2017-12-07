<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessMgm.aspx.cs" Inherits="WebApplication.Channels.Base.ProcessMgm.ProcessMgm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="ProcessMgm.js" type="text/javascript"></script>

</head>
<body>
    <table>
        <tr>
            <th style=" padding-top:5px;padding-left:5px;">任务下达日期:</th>
            <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                <input type="text" id="taskDateStart" name="taskDateStart"  class="l-text l-text-editing" runat="server"  /></td>
            <th style=" padding-top:5px;padding-left:20px;">任务要求完成日期:</th>
            <td align="left"  class="l-table-edit-td float2013" style=" padding-top:5px">
                <input type="text" id="taskDateFinish" name="taskDateFinish"  class="l-text l-text-editing"  style=" float:left;"  runat="server" />
            </td>
        </tr>

        <tr>
            <th style=" padding-top:5px;padding-left:5px;">采样开始日期:</th>
            <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                <input type="text" id="sampleDateStart" name="sampleDateStart"  class="l-text l-text-editing"   runat="server" /></td>
            <th style=" padding-top:5px;padding-left:20px;">采样要求完成日期:</th>
            <td align="left"  class="l-table-edit-td float2013" style=" padding-top:5px">
                <input type="text" id="sampleDateFinish" name="sampleDateFinish"  class="l-text l-text-editing"  style=" float:left;"  runat="server" />
            </td>
        </tr>

        <tr>
            <th style=" padding-top:5px;padding-left:5px;">分析开始日期:</th>
            <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                <input type="text" id="analyseDateStart" name="analyseDateStart"  class="l-text l-text-editing"   runat="server" /></td>
            <th style=" padding-top:5px;padding-left:20px;">分析要求完成日期:</th>
            <td align="left"  class="l-table-edit-td float2013" style=" padding-top:5px">
                    <input type="text" id="analyseDateFinish" name="analyseDateFinish"  class="l-text l-text-editing"  style=" float:left;"  runat="server" />
            </td>
        </tr>

        <tr>
            <th style=" padding-top:5px;padding-left:5px;">分析室主任审核日期:</th>
            <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                <input type="text" id="auditLabLeaderFinish" name="auditLabLeaderFinish"  class="l-text l-text-editing"   runat="server" /></td>
            <th style=" padding-top:5px;padding-left:20px;">质控室审核日期:</th>
            <td align="left"  class="l-table-edit-td float2013" style=" padding-top:5px">
                <input type="text" id="auditQCFinish" name="auditQCFinish"  class="l-text l-text-editing"  style=" float:left;"  runat="server" />
            </td>
        </tr>

        <tr>
            <th style=" padding-top:5px;padding-left:5px;">主管副站长审核日期:</th>
            <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                <input type="text" id="auditCaptionFinish" name="auditCaptionFinish"  class="l-text l-text-editing"   runat="server" /></td>
            <th style=" padding-top:5px;padding-left:20px;">质量负责人审核日期:</th>
            <td align="left"  class="l-table-edit-td float2013" style=" padding-top:5px">
                <input type="text" id="auditQCLeaderFinish" name="auditQCLeaderFinish"  class="l-text l-text-editing"  style=" float:left;"  runat="server" />
            </td>
        </tr>

        <tr>
            <th style=" padding-top:5px;padding-left:5px;">技术负责人审核日期:</th>
            <td align="left"  class="l-table-edit-td float2013" style=" padding-top:5px">
                    <input type="text" id="AuditTechLeaderFinish" name="AuditTechLeaderFinish"  class="l-text l-text-editing"  style=" float:left;" runat="server"/>
            </td>
        </tr>

    </table>
</body>

<div>
    <input type="hidden"  id="strPlanId"  runat="server"/>
</div>
</html>
