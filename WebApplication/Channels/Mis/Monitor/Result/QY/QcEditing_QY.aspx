<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_Result_QcEditing_QY" Codebehind="QcEditing_QY.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        var url = "QcEditing_QY.aspx";
        var strID = "", strQC_TYPE = "";
        $(document).ready(function () {
            strID = getQueryString("ID");
            strQC_TYPE = getQueryString("QC_TYPE");

            //创建表单结构 
            switch (strQC_TYPE) {
                case "5":
                    document.getElementById("lbName").innerText = "实验室空白";
                    $("#divQcInfo").ligerForm({
                        inputWidth: 100, labelWidth: 110, space: 20, labelAlign: 'right',
                        fields: [
                            { display: "空白值1", name: "QC_EMPTY_IN_VALUE1", width: 40, newline: true, type: "text" },
                            { display: "空白值2", name: "QC_EMPTY_IN_VALUE2", newline: false, labelWidth: 60, width: 40, type: "text" },
                            { display: "空白值3", name: "QC_EMPTY_IN_VALUE3", newline: false, labelWidth: 60, width: 40, type: "text" },
                            { display: "空白平均值", name: "QC_EMPTY_IN_RESULT", newline: true, type: "text" }
                        ]
                    });
                    break;
                case "8":
                    document.getElementById("lbName").innerText = "标准样";
                    $("#divQcInfo").ligerForm({
                        inputWidth: 100, labelWidth: 110, space: 20, labelAlign: 'right',
                        fields: [
                          { display: "标准值1", name: "SRC_IN_VALUE1", width: 40, newline: true, type: "text" },
                          { display: "标准值2", name: "SRC_IN_VALUE2", newline: false, labelWidth: 60, width: 40, type: "text" },
                          { display: "标准值3", name: "SRC_IN_VALUE3", newline: false, labelWidth: 60, width: 40, type: "text" }
                        ]
                    });
                    break;
                case "6":
                    document.getElementById("lbName").innerText = "实验室加标";
                    $("#divQcInfo").ligerForm({
                        inputWidth: 100, labelWidth: 110, space: 20, labelAlign: 'right',
                        fields: [
                          { display: "样品", name: "ddlAddSampleCode", width: 180, newline: true, type: "text" },
                          { display: "原测定值", name: "ADD_RESULT", newline: true, type: "text" },
                          { display: "测定值", name: "ADD_RESULT_EX", newline: false, type: "text" },
                          { display: "加标量", name: "QC_ADD", newline: true, type: "text" },
                          { display: "回收率", name: "ADD_BACK", newline: false, type: "text" }
                        ]
                    });
                    break;
                case "7":
                    document.getElementById("lbName").innerText = "实验室明码平行";
                    $("#divQcInfo").ligerForm({
                        inputWidth: 100, labelWidth: 110, space: 20, labelAlign: 'right',
                        fields: [
                          { display: "样品", name: "ddlTwinSampleCode", width: 180, newline: true, type: "text" },
                          { display: "测定值1(mg/L)", name: "TWIN_RESULT1", newline: true, type: "text" },
                          { display: "测定值2(mg/L)", name: "TWIN_RESULT2", newline: false, type: "text" },
                          { display: "测定均值(mg/L)", name: "TWIN_AVG", newline: true, type: "text" },
                          { display: "相对偏差", name: "TWIN_OFFSET", newline: false, type: "text" }
                        ]
                    });
                    break;
            }
            $("#status").val("getdata");
            ajaxSubmit(url, function () { return true; }, function (responseText, statusText) {
                var arr = jQuery.parseJSON(responseText);
                switch (strQC_TYPE) {
                    case "5":
                        $("#QC_EMPTY_IN_VALUE1").val(arr["strValue1"]);
                        $("#QC_EMPTY_IN_VALUE2").val(arr["strValue2"]);
                        $("#QC_EMPTY_IN_VALUE3").val(arr["strValue3"]);
                        $("#QC_EMPTY_IN_RESULT").val(arr["strQcEmptyResult"]);
                        $("#dEmptyCount").val(arr["strEmptyCount"]);
                        break;
                    case "8":
                        $("#SRC_IN_VALUE1").val(arr["strValue1"]);
                        $("#SRC_IN_VALUE2").val(arr["strValue2"]);
                        $("#SRC_IN_VALUE3").val(arr["strValue3"]);
                        $("#dSrcCount").val(arr["strStCount"]);
                        break;
                    case "6":
                        $("#ddlAddSampleCode").val(arr["strSampleCode"]);
                        $("#ADD_RESULT").val(arr["strItemResult"]);
                        $("#ADD_RESULT_EX").val(arr["strAddResult"]);
                        $("#QC_ADD").val(arr["strQcAdd"]);
                        $("#ADD_BACK").val(arr["strAddBack"]);
                        break;
                    case "7":
                        $("#hidSrcResultID").val(arr["strSrcResultID"]);
                        $("#ddlTwinSampleCode").val(arr["strSampleCode"]);
                        $("#TWIN_RESULT1").val(arr["strResult1"]);
                        $("#TWIN_RESULT2").val(arr["strResult2"]);
                        $("#TWIN_AVG").val(arr["strAvg"]);
                        $("#TWIN_OFFSET").val(arr["strOffset"]);
                        break;
                }
            });
        });
        function calculate() {
            $("#status").val("calculate");
            ajaxSubmit(url, function () { return true; }, function (responseText, statusText) {
                var arr = jQuery.parseJSON(responseText);
                switch (strQC_TYPE) {
                    case "5":
                        $("#QC_EMPTY_IN_RESULT").val(arr["strEmptyValue"]);
                        $("#dEmptyCount").val(arr["strEmptyCount"]);
                        break;
                    case "8":
                        $("#dSrcCount").val(arr["strSrcCount"]);
                        break;
                    case "6":
                        $("#ADD_BACK").val(arr["strAddBack"]);
                        break;
                    case "7":
                        $("#TWIN_AVG").val(arr["strAvgValue"]);
                        $("#TWIN_OFFSET").val(arr["strOffSet"]);
                        break;
                }
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
            <img src="../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
            <label id="lbName"></label>
        </div>
        <div id="divQcInfo">
        </div>
    </div>
    <input type="hidden" id="status" runat="server" />
    <input type="hidden" id="hidID" runat="server" />
    <input type="hidden" id="hidQC_TYPE" runat="server" />
    <input type="hidden" id="hidSrcResultID" runat="server" />
    <input type="hidden" id="dEmptyCount" runat="server" />  <%--空白个数--%>
    <input type="hidden" id="dSrcCount" runat="server" />  <%--标准样个数--%>
    </form>
</body>
</html>
