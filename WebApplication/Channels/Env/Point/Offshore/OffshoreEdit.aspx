<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Env_Point_Offshore_OffshoreEdit" Codebehind="OffshoreEdit.aspx.cs" %>

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
        var url = "OffshoreEdit.aspx";
        $(document).ready(function () {
            //创建表单结构 
            $("#divEdit").ligerForm({
                inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
                fields: [
                { display: "年度", name: "YEAR_NAME", newline: true, type: "select", comboboxName: "YEAR_NAME_BOX", options: { valueFieldID: "YEAR", valueField: "value", textField: "value", url: url + "?type=getYearInfo" }, group: "基本信息", groupicon: groupicon },
                { display: "月度", name: "MONTH_NAME", newline: false, type: "select", comboboxName: "MONTH_NAME_BOX", options: { valueFieldID: "MONTH", valueField: "value", textField: "value", url: url + "?type=getMonthInfo"} },
                { display: "排污口代码", name: "POINT_CODE", newline: true, type: "text" },
                { display: "排污口名称", name: "POINT_NAME", newline: false, type: "text"},
                { display: "企业名称", name: "COMPANY_NAME", newline: true, type: "text"},
                { display: "功能属性", name: "FUNCTION_ATTRIBUTE", newline: false, type: "text" },
                { display: "监测月度", name: "SELECT_MONTH", newline: true, type: "select", comboboxName: "SELECT_MONTH_BOX", width: 530, options: { valueFieldID: "SelectMonths", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=itemmonths", isShowCheckBox: true, isMultiSelect: true} },
//              { display: "评价标准", name: "CONDITION_ID_NAME", newline: true, type: "select" },
                { display: "经度", name: "LONGITUDE_DEGREE", newline: true, type: "text", width: 40, space: 1 },
                { display: "度", name: "LONGITUDE_MINUTE", newline: false, type: "text", width: 40, space: 1, labelWidth: 15, align: "left" },
                { display: "分", name: "LONGITUDE_SECOND", newline: false, type: "text", width: 40, space: 1, labelWidth: 15, align: "left" },
                { display: "秒", name: "hidLONGITUDE", newline: false, type: "text", width: 0.01, space: 82, labelWidth: 15, align: "left" },
                { display: "纬度", name: "LATITUDE_DEGREE", newline: false, type: "text", width: 40, space: 1 },
                { display: "度", name: "LATITUDE_MINUTE", newline: false, type: "text", width: 40, space: 1, labelWidth: 15, align: "left" },
                { display: "分", name: "LATITUDE_SECOND", newline: false, type: "text", width: 40, space: 1, labelWidth: 15, align: "left" },
                { display: "秒", name: "hidLATITUDE", newline: false, type: "text", width: 0.01, space: 1, labelWidth: 15, align: "left" },
                { display: "具体位置", name: "LOCATION", newline: true, type: "text", width: 530 }
                ]
            });
            //添加表单验证
            $("#YEAR_NAME_BOX").attr("validate", "[{required:true,msg:'请选择年度'}]");
            $("#POINT_CODE").attr("validate", "[{required:true,msg:'请填写排污口代码'},{maxlength:256,msg:'企业名称录入最大长度为256'}]");
            $("#POINT_NAME").attr("validate", "[{required:true,msg:'请填写排污口名称'},{maxlength:256,msg:'企业名称录入最大长度为256'}]");
            $("#LONGITUDE_DEGREE").attr("validate", "[{maxlength:32,msg:'经度录入最大长度为32'}]");
            $("#LONGITUDE_MINUTE").attr("validate", "[{maxlength:32,msg:'经度-度录入最大长度为32'}]");
            $("#LONGITUDE_SECOND").attr("validate", "[{maxlength:32,msg:'经度-分录入最大长度为32'}]");
            $("#LATITUDE_DEGREE").attr("validate", "[{maxlength:32,msg:'纬度录入最大长度为32'}]");
            $("#LATITUDE_MINUTE").attr("validate", "[{maxlength:32,msg:'纬度-度录入最大长度为32'}]");
            $("#LATITUDE_SECOND").attr("validate", "[{maxlength:32,msg:'纬度-分录入最大长度为32'}]");
            $("#LOCATION").attr("validate", "[{maxlength:32,msg:'具体位置录入最大长度为32'}]");
            //如果是新增状态把月度置灰,否则把监测月度置灰
            if (getQueryString("id") == null) {
                $("#MONTH_NAME_BOX").ligerGetComboBoxManager().setDisabled();
                $("#SELECT_MONTH_BOX").attr("validate", "[{required:true,msg:'请选择监测月度'}]");
            }
            else {
                $("#SELECT_MONTH_BOX").ligerGetComboBoxManager().setDisabled();
            }
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
                $.ligerDialog.open({ title: "评价标准条件项选择", width: 500, height: 300, buttons:
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
