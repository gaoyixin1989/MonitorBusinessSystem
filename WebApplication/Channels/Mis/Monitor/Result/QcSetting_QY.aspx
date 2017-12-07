<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_Result_QcSetting_QY" Codebehind="QcSetting_QY.aspx.cs" %>

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
        var url = "QcSetting_QY.aspx";
        var objResult = null;
        $(document).ready(function () {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: "QY/AnalysisResult.aspx?type=getItemInfo&resultids=" + $("#resultjson").val(),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    objResult = data;
                }
            });

            //创建表单结构 
            $("#divQcEmpty").ligerForm({
                inputWidth: 100, labelWidth: 110, space: 20, labelAlign: 'right',
                fields: [
                  { display: "空白值1", name: "QC_EMPTY_IN_VALUE1", width: 40, newline: true, type: "text" },
                  { display: "空白值2", name: "QC_EMPTY_IN_VALUE2", newline: false, labelWidth: 60, width: 40, type: "text" },
                  { display: "空白值3", name: "QC_EMPTY_IN_VALUE3", newline: false, labelWidth: 60, width: 40, type: "text" },
                  { display: "空白平均值", name: "QC_EMPTY_IN_RESULT", newline: true, type: "text" }
                ]
            });
            $("#divQcSt").ligerForm({
                inputWidth: 100, labelWidth: 110, space: 20, labelAlign: 'right',
                fields: [
                  { display: "标准值1", name: "SRC_IN_VALUE1", width: 40, newline: true, type: "text" },
                  { display: "标准值2", name: "SRC_IN_VALUE2", newline: false, labelWidth: 60, width: 40, type: "text" },
                  { display: "标准值3", name: "SRC_IN_VALUE3", newline: false, labelWidth: 60, width: 40, type: "text"}//,
                //{ display: "标准平均值(ml/L)", name: "SRC_RESULT", newline: true, isHidden: true, type: "text" }
                ]
            });
            $("#divQcAdd").ligerForm({
                inputWidth: 100, labelWidth: 110, space: 20, labelAlign: 'right',
                fields: [
                  { display: "样品", name: "ddlAddSampleCode", width: 180, newline: true, type: "select", comboboxName: "ddlAddSampleCodeBox", options: { valueFieldID: "hidAddSampleCode", valueField: "ResultID", textField: "SampleCode", resize: false} },
                  { display: "原测定值", name: "ADD_RESULT", newline: true, type: "text" },
                  { display: "测定值", name: "ADD_RESULT_EX", newline: false, type: "text" },
                  { display: "加标量", name: "QC_ADD", newline: true, type: "text" },
                  { display: "回收率", name: "ADD_BACK", newline: false, type: "text" }
                ]
            });
            $("#divQcTwin").ligerForm({
                inputWidth: 100, labelWidth: 110, space: 20, labelAlign: 'right',
                fields: [
                  { display: "样品", name: "ddlTwinSampleCode", width: 180, newline: true, type: "select", comboboxName: "ddlTwinSampleCodeBox", options: { valueFieldID: "hidTwinSampleCode", valueField: "ResultID", textField: "SampleCode", resize: false} },
                  { display: "测定值1(mg/L)", name: "TWIN_RESULT1", newline: true, type: "text" },
                  { display: "测定值2(mg/L)", name: "TWIN_RESULT2", newline: false, type: "text" },
                  { display: "测定均值(mg/L)", name: "TWIN_AVG", newline: true, type: "text" },
                  { display: "相对偏差", name: "TWIN_OFFSET", newline: false, type: "text" }
                ]
            });
            if ($("#result").val() != "") {
                //$("#QC_EMPTY_IN_COUNT").val($("#emptycount").val());
            }

            $.ligerui.get("ddlAddSampleCodeBox").setData(objResult);
            $.ligerui.get("ddlTwinSampleCodeBox").setData(objResult);

            //下拉框联动
            $.ligerui.get("ddlAddSampleCodeBox").bind("selected", function (value, text) {
                var obj = objResult;
                for (var i = 0; i < obj.length; i++) {
                    if (obj[i].ResultID == value) {
                        $("#ADD_RESULT").val(obj[i].ItemResult);
                        return;
                    }
                }
            });
        });
        function calculate() {
            $("#status").val("calculate");
            ajaxSubmit(url, function () { return true; }, function (responseText, statusText) {
                var arr = jQuery.parseJSON(responseText);
                $("#ADD_BACK").val(arr["strAddBack"]);
                $("#TWIN_AVG").val(arr["strAvgValue"]);
                $("#TWIN_OFFSET").val(arr["strOffSet"]);
                $("#QC_EMPTY_IN_RESULT").val(arr["strEmptyValue"]);
                //$("#SRC_RESULT").val(arr["strSrcValue"]);
                $("#dEmptyCount").val(arr["strEmptyCount"]);
                $("#dSrcCount").val(arr["strSrcCount"]);
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
    <input type="hidden" id="resultjson" runat="server" />
    <input type="hidden" id="resultids" runat="server" />
    <input type="hidden" id="Addresult" runat="server" />
    <input type="hidden" id="Twinresult" runat="server" />
    <input type="hidden" id="emptyBatId" runat="server" />
    <input type="hidden" id="emptycount" runat="server" />
    <input type="hidden" id="status" runat="server" />
    <input type="hidden" id="dEmptyCount" runat="server" />  <%--空白个数--%>
    <input type="hidden" id="dSrcCount" runat="server" />  <%--标准样个数--%>
    </form>
</body>
</html>
