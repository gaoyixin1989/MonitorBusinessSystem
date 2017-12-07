<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_Result_QY_AnalysisResult_Lims" Codebehind="AnalysisResult_Lims.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/comm.js" type="text/javascript"></script>
    <script type="text/javascript">
        var managertmp;
        var obj;
        var strUrl = "AnalysisResult_Lims.aspx";
        var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

        $(document).ready(function () {
            obj = $("#divAttr").ligerForm({ inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right', fields: [] });
            managertmp = $.ligerui.managers.divAttr;

            //获取所有属性类别及属性信息关联数据
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "?type=GetInfo&ResultID=" + request('ResultID'),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.length > 0) {
                        GetControlJson(data);
                        initControlValue(data);
                    }
                }
            });
        });

        function GetControlJson(data) {
            var strdata = { inputWidth: 160, labelWidth: 160, space: 60, labelAlign: 'right', fields: [] };

            if (data.length > 0) {
                if (data[0].TableName == "AA240Z") {
                    strdata.fields.push(GetJsonFieldData("样品标识","SAMPLE_ID","true",true));
                    strdata.fields.push(GetJsonFieldData("仪器", "Apparatus", "false", false));
                    strdata.fields.push(GetJsonFieldData("监测因子", "ANALYSE", "true", false));
                    strdata.fields.push(GetJsonFieldData("浓度", "CONC", "false", false));
                    strdata.fields.push(GetJsonFieldData("单位", "UNIT", "true", false));
                    strdata.fields.push(GetJsonFieldData("RSD", "RSD", "false", false));
                    strdata.fields.push(GetJsonFieldData("平均吸光度", "AVG_ABS", "true", false));
                    strdata.fields.push(GetJsonFieldData("背景吸光度", "BACK_ABS", "false", false));
                    strdata.fields.push(GetJsonFieldData("第1次读取值", "VALUE_1", "true", false));
                    strdata.fields.push(GetJsonFieldData("第2次读取值", "VALUE_2", "false", false));
                }
                if (data[0].TableName == "AFS3100") {
                    strdata.fields.push(GetJsonFieldData("样品标识", "SAMPLE_ID", "true", true));
                    strdata.fields.push(GetJsonFieldData("仪器", "Apparatus", "false", false));
                    strdata.fields.push(GetJsonFieldData("监测因子", "ANALYSE", "true", false));
                    strdata.fields.push(GetJsonFieldData("浓度", "CONC", "false", false));
                    strdata.fields.push(GetJsonFieldData("单位", "UNIT", "true", false));
                    strdata.fields.push(GetJsonFieldData("荧光强度", "IF_VALUE", "false", false));
                    strdata.fields.push(GetJsonFieldData("样品形态", "SHAPE", "true", false));
                    strdata.fields.push(GetJsonFieldData("质量体积", "COFFICIENT", "false", false));
                }

                if (data[0].TableName == "TOCVCPH") {
                    strdata.fields.push(GetJsonFieldData("样品标识", "SAMPLE_ID", "true", true));
                    strdata.fields.push(GetJsonFieldData("仪器", "Apparatus", "false", false));
                    strdata.fields.push(GetJsonFieldData("监测因子", "ANALYSE", "true", false));
                    strdata.fields.push(GetJsonFieldData("浓度", "CONC", "false", false));
                    strdata.fields.push(GetJsonFieldData("单位", "UNIT", "true", false));
                    strdata.fields.push(GetJsonFieldData("氧化方法", "OXIDATION_METHOD", "false", false));
                    strdata.fields.push(GetJsonFieldData("催化剂", "CATALYZER", "true", false));
                    strdata.fields.push(GetJsonFieldData("ASI样品盘", "ASI", "false", false));
                }
            }

            $("#divAttr").html("");

            $.ligerui.managers["divAttr"] = [];
            $.ligerui.managers["divAttr"] = managertmp;

            var obj1 = $("#divAttr").ligerForm(strdata);
            obj1._render();
        }

        function initControlValue(data) {
            if (data.length > 0) {
                if (data[0].TableName == "AA240Z") {
                    $("#SAMPLE_ID").val(data[0].SAMPLE_ID);
                    $("#Apparatus").val("AA-240Z");
                    $("#ANALYSE").val(data[0].METHOD);
                    $("#CONC").val(data[0].CONC);
                    $("#UNIT").val(data[0].UNIT);
                    $("#RSD").val(data[0].RSD);
                    $("#AVG_ABS").val(data[0].AVG_ABS);
                    $("#BACK_ABS").val(data[0].BACK_ABS);
                    $("#VALUE_1").val(data[0].VALUE_1);
                    $("#VALUE_2").val(data[0].VALUE_2);
                }
                if (data[0].TableName == "AFS3100") {
                    $("#SAMPLE_ID").val(data[0].SAMPLE_ID);
                    $("#Apparatus").val("AFS-3100");
                    $("#ANALYSE").val(data[0].ANALYSE);
                    $("#CONC").val(data[0].CONC);
                    $("#UNIT").val(data[0].UNIT);
                    $("#IF_VALUE").val(data[0].IF_VALUE);
                    $("#SHAPE").val(data[0].SHAPE);
                    $("#COFFICIENT").val(data[0].COFFICIENT);
                }

                if (data[0].TableName == "TOCVCPH") {
                    $("#SAMPLE_ID").val(data[0].SAMPLE_ID);
                    $("#Apparatus").val("TOC-VCPH");
                    $("#ANALYSE").val(data[0].ANALYSE);
                    $("#CONC").val(data[0].CONC);
                    $("#UNIT").val(data[0].UNIT);
                    $("#OXIDATION_METHOD").val(data[0].OXIDATION_METHOD);
                    $("#CATALYZER").val(data[0].CATALYZER);
                    $("#ASI").val(data[0].ASI);
                }
            }
        }

        function GetJsonFieldData(displayName, conName, newLine, ifGroup) {
            var strdata1 = "";
            strdata1 += "{ display: '" + displayName + "',";
            strdata1 += "name:'" + conName + "',";
            strdata1 += "newline:" + newLine + ",";
            strdata1 += "type:'text'";
            if (ifGroup) {
                strdata1 += ",group: '仪器数据', groupicon: groupicon";
            }
            strdata1 += "}";

            var strJsonData = eval('(' + strdata1 + ')');

            return strJsonData;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divAttr"></div>
    </form>
</body>
</html>
