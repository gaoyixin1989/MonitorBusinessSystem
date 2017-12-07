<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_MisII_EnvPlan_EnvPlanAdd"
    CodeBehind="EnvPlanAdd.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <!--Jquery 基础文件-->
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <!--自动完成-->
    <script src="../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <!--LigerUI-->
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
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
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
    <script src="EnvPlanAdd.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            padding: 5px;
            margin: 0;
            padding-bottom: 15px;
        }
        #layout1
        {
            width: 100%;
            margin: 0;
            padding: 0;
        }
        h4
        {
            margin: 20px;
        }
        .float2013 .l-text
        {
            float: left;
        }
    </style>

    <title></title>
</head>
<body>
    <div style="display: block" id="pageloading">
    </div>
    <div id="layout1">
        <div position="top">
            <table>
                <tr>
                    <th style="padding-top: 5px; padding-left: 5px;">
                        环境类别:
                    </th>
                    <td align="left" class="l-table-edit-td" style="padding-top: 5px">
                        <input type="text" id="EnvTypes" name="EnvTypes" class="l-text l-text-editing" style="width: 160px" />
                    </td>
                    <th style="padding-top: 5px; padding-left: 5px;">
                        月份:
                    </th>
                    <td align="left" class="l-table-edit-td" style="padding-top: 5px">
                        <input type="text" id="EnvMonth" name="EnvMonth" class="l-text l-text-editing" style="width: 160px" />
                    </td>
                    <td>
                        <div style="display: none" id="iDBody1">
                            <table>
                                <tr>
                                    <th>
                                        行政区:
                                    </th>
                                    <td style="padding-top: 0px; padding-left: 5px;">
                                        <input type="text" id="RegionCode" name="RegionCode" class="l-text l-text-editing"
                                            style="width: 80px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td>
                        <div style="display: none" id="iDBody2">
                            <table>
                                <tr>
                                    <th >
                                        功能区:
                                    </th>
                                    <td align="left" class="l-table-edit-td" style="padding-top: 10px">
                                        <input type="text" id="Functional" name="Functional" class="l-text l-text-editing"
                                            style="width: 80px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th style="padding-top: 5px; padding-left: 5px;">
                        项目名称:
                    </th>
                    <td align="left" class="l-table-edit-td" style="padding-top: 5px" colspan="3">
                        <input type="text" id="txtProjectName" name="txtProjectName" class="l-text l-text-editing"
                            style="width: 200px" />
                    </td>
                </tr>
                <%--<tr>
                    <th style="padding-top: 5px; padding-left: 5px;">
                        任务下达日期:
                    </th>
                    <td align="left" class="l-table-edit-td" style="padding-top: 5px">
                        <input type="text" id="txtDate" name="txtDate" class="l-text l-text-editing" />
                    </td>
                    <th style="padding-top: 5px">
                        要求完成日期:
                    </th>
                    <td align="left" class="l-table-edit-td float2013" style="padding-top: 5px">
                        <input type="text" id="txtAskingDate" name="txtAskingDate" class="l-text l-text-editing"
                            style="float: left;" />
                    </td>
                </tr>--%>
                <tr>
                    <th style="padding-top: 5px">
                        任务单号:
                    </th>
                    <td align="left" class="l-table-edit-td float2013" style="padding-top: 5px">
                        <%--//潘德军 2013-12-23 任务单号可改，且不自动生成--%>
                        <input type="text" id="txtTASK_CODE" name="txtTASK_CODE" class="l-text l-text-editing"
                            style="width: 200px" />
                    </td>
                    <%--<td><input id="BtnSearch" type="button" value="单号查询" style="width:80px;" /></td>--%>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" id="chkBC" />是否补测
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" id="chkFXS" />ph值、电导率、溶解氧是否设为分析室项目
                    </td>
                </tr>
                <tr>
                    <th style="padding-top: 5px; padding-left: 5px;">
                        是否送样：
                    </th>
                    <td align="left" class="l-table-edit-td" style="padding-top: 5px">
                        <input type="text" id="EnvSY" name="EnvSY" class="l-text l-text-editing" style="width: 160px" />
                    </td>
                    <td>
                        <input type="button" id="btnGet" name="btnGet" value="生 成" class="l-button l-button-submit"
                            style="display: inline-block; float: left; margin-left: 15px;" />
                    </td>
                </tr>
                <tr>
                    <th style="padding-top: 5px; padding-left: 5px;">
                        备注:
                    </th>
                    <td align="left" class="l-table-edit-td" colspan='3' style="padding-top: 5px">
                        <textarea id="txtRemarks" name="txtRemarks" class="l-text l-text-editing" cols="100"
                            style="width: 512px; height: 55px;" rows="2"></textarea>
                    </td>
                </tr>
            </table>
        </div>
        <div position="left" title="任务详细信息">
            <div id="createDiv" class="l-messagebox-btn-inner">
            </div>
        </div>
    </div>
    <div>
        <input type="hidden" id="hidDmId" />
    </div>
</body>
</html>
