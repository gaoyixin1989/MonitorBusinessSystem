<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Env_Point_RiverCity_RiverCityEdit" Codebehind="RiverCityEdit.aspx.cs" %>

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
        var url = "RiverCityEdit.aspx";
        $(document).ready(function () {
            GlobalMonitorType = "EnvRiverCity";
            //创建表单结构 
            $("#divEdit").ligerForm({
                inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
                fields: [
                { display: "年度", name: "YEAR_NAME", newline: true, type: "select", comboboxName: "YEAR_NAME_BOX", options: { valueFieldID: "YEAR", valueField: "value", textField: "value", url: url + "?type=getYearInfo" }, group: "基本信息", groupicon: groupicon },
                { display: "月度", name: "MONTH_NAME", newline: false, type: "select", comboboxName: "MONTH_NAME_BOX", options: { valueFieldID: "MONTH", valueField: "value", textField: "value", url: url + "?type=getMonthInfo"} },
                { display: "测站名称", name: "SATAIONS_ID_NAME", newline: true, type: "select", comboboxName: "SATAIONS_ID_NAME", options: { valueFieldID: "SATAIONS_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=SATAIONS"} },
                { display: "断面代码", name: "SECTION_CODE", newline: true, type: "text" },
                { display: "断面名称", name: "SECTION_NAME", newline: false, type: "text" },
                { display: "所在地区", name: "AREA_ID_NAME", newline: true, type: "select", comboboxName: "AREA_ID_NAME", options: { valueFieldID: "AREA_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=administrative_area"} },
                { display: "评价标准", name: "CONDITION_ID_NAME", newline: false, type: "select" },
                { display: "监测月度", name: "SELECT_MONTH", newline: true, type: "select", comboboxName: "SELECT_MONTH_BOX", width: 530, options: { valueFieldID: "SelectMonths", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=itemmonths", isShowCheckBox: true, isMultiSelect: true, selectBoxHeight: 265} },


                { display: "所属省份", name: "PROVINCE_ID_NAME", newline: true, type: "select", comboboxName: "PROVINCE_ID_NAME", options: { valueFieldID: "PROVINCE_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=province" }, group: "扩展信息", groupicon: groupicon },
                { display: "控制级别", name: "CONTRAL_LEVEL_NAME", newline: false, type: "select", comboboxName: "CONTRAL_LEVEL_NAME", options: { valueFieldID: "CONTRAL_LEVEL", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=control_level"} },
                { display: "河流", name: "RIVER_ID_NAME", newline: true, type: "select" },
                { display: "流域", name: "VALLEY_ID_NAME", newline: false, type: "text" },
                { display: "水质目标", name: "WATER_QUALITY_GOALS_ID_NAME", newline: true, type: "select", comboboxName: "WATER_QUALITY_GOALS_ID_NAME", options: { valueFieldID: "WATER_QUALITY_GOALS_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=water_quality"} },
                { display: "监测时段", name: "MONITOR_TIMES_NAME", newline: false, type: "select", comboboxName: "MONITOR_TIMES_NAME", options: { valueFieldID: "MONITOR_TIMES", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=monitor_times"} },
                { display: "类别", name: "CATEGORY_ID_NAME", newline: true, type: "select", comboboxName: "CATEGORY_ID_NAME", options: { valueFieldID: "CATEGORY_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=river_category"} },
                { display: "是否交接", name: "IS_HANDOVER_NAME", newline: false, type: "select", comboboxName: "IS_HANDOVER_NAME", options: { valueFieldID: "IS_HANDOVER", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=river_ishandover"} },
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

            //添加表单验证
            $("#YEAR_NAME_BOX").attr("validate", "[{required:true,msg:'请选择年度'}]");
            $("#SECTION_CODE").attr("validate", "[{required:true,msg:'请填写断面代码'},{maxlength:32,msg:'断面代码录入最大长度为32'}]");
            $("#SECTION_NAME").attr("validate", "[{required:true,msg:'请填写断面名称'},{maxlength:128,msg:'断面名称录入最大长度为128'}]");
            $("#LONGITUDE_DEGREE").attr("validate", "[{maxlength:32,msg:'经度录入最大长度为32'}]");
            $("#LONGITUDE_MINUTE").attr("validate", "[{maxlength:32,msg:'经度-度录入最大长度为32'}]");
            $("#LONGITUDE_SECOND").attr("validate", "[{maxlength:32,msg:'经度-分录入最大长度为32'}]");
            $("#LATITUDE_DEGREE").attr("validate", "[{maxlength:32,msg:'纬度录入最大长度为32'}]");
            $("#LATITUDE_MINUTE").attr("validate", "[{maxlength:32,msg:'纬度-度录入最大长度为32'}]");
            $("#LATITUDE_SECOND").attr("validate", "[{maxlength:32,msg:'纬度-分录入最大长度为32'}]");
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
                        $("#VALLEY_ID_NAME").val(data.REMARK2);
                        $("#RIVER_ID_NAME").val(data.REMARK3);

                        $("#hidYear").val(data.YEAR);
                        $("#hidMonth").val(data.MONTH);
                        $("#hidCode").val(data.SECTION_CODE);
                        $("#hidValue").val(data.SECTION_NAME);
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
                }], url: '../EvaluationStandardSelected.aspx?strMonitorId=000000001&strValue=' + $("#CONDITION_ID").val()
                });
                return false;
            }
            //流域河流选择
            $("#RIVER_ID_NAME").ligerComboBox({
                onBeforeOpen: selectRiverValue, valueFieldID: 'RIVER_ID'
            });
            function selectRiverValue() {
                $.ligerDialog.open({ title: "流域河流选择", width: 700, height: 300, isHidden: false, buttons:
                [
                { text:
                '确定', onclick: function (item, dialog) {
                    var obj = $("iframe")[0].contentWindow.getRiverInfo();
                    $("#RIVER_ID").val(obj.RiverValue);
                    $("#RIVER_ID_NAME").val(obj.RiverText);
                    $("#VALLEY_ID_NAME").val(obj.WatershedText);
                    $("#VALLEY_ID").val(obj.WatershedValue);
                    dialog.close();
                }
                }, { text:
                '关闭', onclick: function (item, dialog) { dialog.close(); }
                }], url: '../RiverSelected.aspx?strValue=' + $("#RIVER_ID").val()
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
    <input type="hidden" id="RIVER_ID" runat="server" />
    <input type="hidden" id="VALLEY_ID" runat="server" />
   
    <input type="hidden" id="hidYear" runat="server" />
    <input type="hidden" id="hidMonth" runat="server" />
    <input type="hidden" id="hidCode" runat="server" />
    <input type="hidden" id="hidValue" runat="server" />
    </form>
</body>
</html>
