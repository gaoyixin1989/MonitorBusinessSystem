<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_Result_QcSetting" Codebehind="QcSetting.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        var url = "QcSetting.aspx";
        $(document).ready(function () {
            //创建表单结构 
            $("#divQcEmpty").ligerForm({
                inputWidth: 100, labelWidth: 90, space: 20, labelAlign: 'right',
                fields: [
                  { display: "空白值", name: "QC_EMPTY_IN_RESULT", newline: true, type: "text" },
                  { display: "空白个数", name: "QC_EMPTY_IN_COUNT", newline: false, type: "text" }
                ]
            });
            $("#divQcSt").ligerForm({
                inputWidth: 100, labelWidth: 90, space: 20, labelAlign: 'right',
                fields: [
                  { display: "标准值(ml/L)", name: "SRC_RESULT", newline: true, type: "text" },
                  { display: "测定值(ml/L)", name: "ST_RESULT", newline: false, type: "text" }
                ]
            });
            $("#divQcAdd").ligerForm({
                inputWidth: 100, labelWidth: 90, space: 20, labelAlign: 'right',
                fields: [
                  { display: "测定值", name: "ADD_RESULT_EX", newline: true, type: "text" },
                  { display: "加标量", name: "QC_ADD", newline: false, type: "text" },
                  { display: "回收率", name: "ADD_BACK", newline: true, type: "text" }
                ]
            });
            $("#divQcTwin").ligerForm({
                inputWidth: 100, labelWidth: 100, space: 20, labelAlign: 'right',
                fields: [
                  { display: "测定值1(mg/L)", name: "TWIN_RESULT1", newline: true, type: "text" },
                  { display: "测定值2(mg/L)", name: "TWIN_RESULT2", newline: false, type: "text" },
                  { display: "测定均值(mg/L)", name: "TWIN_AVG", newline: true, type: "text" },
                  { display: "相对偏差", name: "TWIN_OFFSET", newline: false, type: "text" }
                ]
            });
            if ($("#result").val() != "") {
                $("#QC_EMPTY_IN_COUNT").val($("#emptycount").val());
            }
        });
        function calculate() {
            $("#status").val("calculate");
            ajaxSubmit(url, function () { return true; }, function (responseText, statusText) {
                var arr = jQuery.parseJSON(responseText);
                $("#ADD_BACK").val(arr["strAddBack"]);
                $("#TWIN_AVG").val(arr["strAvgValue"]);
                $("#TWIN_OFFSET").val(arr["strOffSet"]);
            });
        }
        function saveQcValue() {
            $("#status").val("save");
            var isuccess = "0";
            ajaxSubmit(url, function () { return true; }, function (responseText, statusText) {
                isuccess = responseText;
            });
            return isuccess;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="l-form">
        <div class="l-group l-group-hasicon">
            <img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
            <input type="checkbox" name="chkQcEmpty" />实验室空白
        </div>
        <div id="divQcEmpty">
        </div>
    </div>
    <div class="l-form">
        <div class="l-group l-group-hasicon">
            <img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
            <input type="checkbox" name="chkQcSt" />标准样
        </div>
        <div id="divQcSt">
        </div>
    </div>
    <div class="l-form">
        <div class="l-group l-group-hasicon">
            <img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
            <input type="checkbox" name="chkQcAdd" />实验室加标
        </div>
        <div id="divQcAdd">
        </div>
    </div>
    <div class="l-form">
        <div class="l-group l-group-hasicon">
            <img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
            <input type="checkbox" name="chkQcTwin" />实验室明码平行
        </div>
        <div id="divQcTwin">
        </div>
    </div>
    <input type="hidden" id="resultid" runat="server" />
    <input type="hidden" id="result" runat="server" />
    <input type="hidden" id="emptyBatId" runat="server" />
    <input type="hidden" id="emptycount" runat="server" />
    <input type="hidden" id="status" runat="server" />
    </form>
</body>
</html>
