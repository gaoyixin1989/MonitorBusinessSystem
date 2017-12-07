<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Env_Point_Sea_SeaEdit" Codebehind="SeaEdit.aspx.cs" %>

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
    <script src="../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        var url = "SeaEdit.aspx";
        GlobalMonitorType = "EnvSear";
        $(document).ready(function () {
            //创建表单结构 
            $("#divEdit").ligerForm({
                inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
                fields: [
                { display: "年度", name: "YEAR_NAME", newline: true, type: "select", comboboxName: "YEAR_NAME_BOX", options: { valueFieldID: "YEAR", valueField: "value", textField: "value", url: url + "?type=getYearInfo" }, group: "基本信息", groupicon: groupicon },
                { display: "月度", name: "MONTH_NAME", newline: false, type: "select", comboboxName: "MONTH_NAME_BOX", options: { valueFieldID: "MONTH", valueField: "value", textField: "value", url: url + "?type=getMonthInfo"} },
                { display: '点位名称', name: 'POINT_NAME', minWidth: 140, newline: true },
                { display: '点位编码', name: 'POINT_CODE', minWidth: 140, newline: false },
               { display: "具体位置", name: "LOCATION", newline: true, minWidth: 140},
                { display: "功能区代码", name: "FUNCTION_CODE", newline: false, type: "text" },
                { display: '国家编号', name: 'COUNTRY_CODE', minWidth: 140, newline: true, type: "text" },
                { display: '省份编号', name: 'PROVINCE_CODE', minWidth: 140, newline: false, type: "text" },
                { display: "点位类别", name: "POINT_TYPE_ID", newline: true, type: "select", comboboxName: "POINT_TYPE_ID", options: { valueFieldID: "POINT_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=SeaPoint_Type"} },
                { display: "评价标准", name: "CONDITION_ID_NAME", type: "select", newline: false },
                { display: "监测月度", name: "SELECT_MONTH", newline: true, type: "select", comboboxName: "SELECT_MONTH_BOX", width: 530, options: { valueFieldID: "SelectMonths", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=itemmonths", isShowCheckBox: true, isMultiSelect: true} },
                { display: "经度", name: "LONGITUDE_DEGREE", newline: true, type: "text", width: 40, space: 1 },
                { display: "度", name: "LONGITUDE_MINUTE", newline: false, type: "text", width: 40, space: 1, labelWidth: 15, align: "left" },
                { display: "分", name: "LONGITUDE_SECOND", newline: false, type: "text", width: 40, space: 1, labelWidth: 15, align: "left" },
                { display: "秒", name: "hidLONGITUDE", newline: false, type: "text", width: 0.01, space: 82, labelWidth: 15, align: "left" },
                { display: "纬度", name: "LATITUDE_DEGREE", newline: false, type: "text", width: 40, space: 1 },
                { display: "度", name: "LATITUDE_MINUTE", newline: false, type: "text", width: 40, space: 1, labelWidth: 15, align: "left" },
                { display: "分", name: "LATITUDE_SECOND", newline: false, type: "text", width: 40, space: 1, labelWidth: 15, align: "left" },
                { display: "秒", name: "hidLATITUDE", newline: false, type: "text", width: 0.01, space: 1, labelWidth: 15, align: "left" }
                ]
            });
            //如果是新增状态把月度置灰,否则把监测月度置灰
            if (getQueryString("id") == null) {
                $("#MONTH_NAME_BOX").ligerGetComboBoxManager().setDisabled();
                $("#SELECT_MONTH_BOX").attr("validate", "[{required:true,msg:'请选择监测月度'}]");
            }
            else {
                $("#SELECT_MONTH_BOX").ligerGetComboBoxManager().setDisabled();
            }
            //添加表单验证  
            $("#YEAR_NAME_BOX").attr("validate", "[{required:true,msg:'请选择年度'}]");
            $("#POINT_NAME").attr("validate", "[{required:true,msg:'请填写测点名称'},{maxlength:256,msg:'测点名称录入最大长度为256'}]");
            $("#POINT_CODE").attr("validate", "[{required:true,msg:'请填写测点代码'},{maxlength:128,msg:'功能区代码录入最大长度为128'}]");
            //加载数据
            if ($("#formId").val() != "") {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: url + "?type=loadData&id=" + $("#formId").val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        bindJsonToPage(data);
                        $("#CONDITION_ID_NAME").val(data.REMARK1);
                        $("#hidYear").val(data.YEAR);
                        $("#hidMonth").val(data.MONTH);
                        $("#hidValue").val(data.POINT_NAME);
                        $("#hidValues").val(data.POINT_CODE);
                    }
                });
            }
            //评价标准选择
            $("#CONDITION_ID_NAME").ligerComboBox({
                onBeforeOpen: selectConditionValue, valueFieldID: 'CONDITION_ID'
            });
            function selectConditionValue() {
                $.ligerDialog.open({ title: "评价标准条件项选择", width: 500, height: 300, isHidden: false, buttons: 
                [
                { text:
                '确定', onclick: function (item, dialog) {
                    $("#CONDITION_ID_NAME").val($("iframe")[0].contentWindow.getEvaluationStandardName());
                    $("#CONDITION_ID").val($("iframe")[0].contentWindow.getConditionValue());
                    dialog.close();
                }
                }, { text:
                '关闭', onclick: function (item, dialog) { dialog.close(); }
                }], url: '../EvaluationStandardSelected.aspx?strMonitorId=' + GlobalMonitorType + '&strValue=' + $("#CONDITION_ID").val()
                });
                return false;
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divEdit">
    </div>
    <input type="hidden" id="formId" runat="server" />
    <input type="hidden" id="formStatus" runat="server" />
    <input type="hidden" id="CONDITION_ID" runat="server" />

        <input type="hidden" id="hidYear" runat="server" />
    <input type="hidden" id="hidMonth" runat="server" />
    <input type="hidden" id="hidValue" runat="server" />
      <input type="hidden" id="hidValues" runat="server" />
    </form>
</body>
</html>
