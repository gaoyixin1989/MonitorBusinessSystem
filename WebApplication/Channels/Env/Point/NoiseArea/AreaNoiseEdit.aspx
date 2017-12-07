﻿<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Env_Point_NoiseArea_AreaNoiseEdit" Codebehind="AreaNoiseEdit.aspx.cs" %>

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
        var url = "AreaNoiseEdit.aspx";
        $(document).ready(function () {
            //创建表单结构 
            $("#divEdit").ligerForm({
                labelAlign: 'right',
                fields: [
                { display: "年度", name: "YEAR_NAME", newline: true, type: "select", comboboxName: "YEAR_NAME_BOX", group: "基本信息", groupicon: groupicon, options: { valueFieldID: "YEAR", valueField: "value", textField: "value", url: url + "?type=getYearInfo"} },
                { display: "月度", name: "MONTH_NAME", newline: false, type: "select", comboboxName: "MONTH_NAME_BOX", options: { valueFieldID: "MONTH", valueField: "value", textField: "value", url: url + "?type=getMonthInfo"} },
                { display: "测点编号", name: "POINT_CODE", newline: true, type: "text" },
                { display: "测点名称", name: "POINT_NAME", newline: false, type: "text" },
                { display: "功能区", name: "FUNCTION_AREA_ID_NAME", newline: true, type: "select", comboboxName: "FUNCTION_AREA_ID_NAME", options: { valueFieldID: "FUNCTION_AREA_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=Function_Area"} },
                { display: "测站名称", name: "SATAIONS_ID_NAME", newline: false, type: "select", comboboxName: "SATAIONS_ID_NAME", options: { valueFieldID: "SATAIONS_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=SATAIONS"} },
                { display: "行政区", name: "AREA_ID_NAME", newline: true, type: "select", comboboxName: "AREA_ID_NAME", options: { valueFieldID: "AREA_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=administrative_area"} },
                { display: "声源类型", name: "SOUND_SOURCE_ID_NAME", newline: false, type: "select", comboboxName: "SOUND_SOURCE_ID_NAME", options: { valueFieldID: "SOUND_SOURCE_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=NoiseSource_Type"} },
                { display: "监测月度", name: "SELECT_MONTH", newline: true, type: "select", comboboxName: "SELECT_MONTH_BOX", width: 490, options: { valueFieldID: "SelectMonths", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=itemmonths", isShowCheckBox: true, isMultiSelect: false, selectBoxHeight: 265} },

                { display: "覆盖面积", name: "COVER_AREA", newline: true, type: "text", group: "扩展信息", groupicon: groupicon },
                { display: "覆盖人口", name: "COVER_PUPILATION", newline: false, type: "text" },
                { display: "经度", name: "LONGITUDE_DEGREE", newline: true, type: "text", width: 40, space: 1 },
                { display: "度", name: "LONGITUDE_MINUTE", newline: false, type: "text", width: 40, space: 1, labelWidth: 15, align: "left" },
                { display: "分", name: "LONGITUDE_SECOND", newline: false, type: "text", width: 40, space: 1, labelWidth: 15, align: "left" },
                { display: "秒", name: "hidLONGITUDE", newline: false, type: "text", width: 0.01, space: 52, labelWidth: 15, align: "left" },
                { display: "纬度", name: "LATITUDE_DEGREE", newline: false, type: "text", width: 40, space: 1 },
                { display: "度", name: "LATITUDE_MINUTE", newline: false, type: "text", width: 40, space: 1, labelWidth: 15, align: "left" },
                { display: "分", name: "LATITUDE_SECOND", newline: false, type: "text", width: 40, space: 1, labelWidth: 15, align: "left" },
                { display: "秒", name: "hidLATITUDE", newline: false, type: "text", width: 0.01, space: 1, labelWidth: 15, align: "left" },
                { display: "网格大小", name: "GRID_SIZE_X", newline: true, type: "text" },
                { name: "GRID_SIZE_Y", newline: false, type: "text", align: "left" },
                { display: "测点位置", name: "LOCATION", newline: true, type: "text", width: 490 }
                ]
            });

            //添加表单验证
            $("#YEAR_NAME_BOX").attr("validate", "[{required:true,msg:'请选择年度'}]");
            $("#POINT_CODE").attr("validate", "[{required:true,msg:'请填写测点编号'},{maxlength:32,msg:'测点编号录入最大长度为32'}]");
            $("#POINT_NAME").attr("validate", "[{required:true,msg:'请填写测点名称'},{maxlength:128,msg:'测点名称录入最大长度为128'}]");
            $("#COVER_AREA").attr("validate", "[{maxlength:32,msg:'覆盖面积录入最大长度为32'}]");
            $("#COVER_PUPILATION").attr("validate", "[{maxlength:32,msg:'覆盖人口录入最大长度为32'}]");
            $("#LONGITUDE_DEGREE").attr("validate", "[{maxlength:32,msg:'经度录入最大长度为32'}]");
            $("#LONGITUDE_MINUTE").attr("validate", "[{maxlength:32,msg:'经度-度录入最大长度为32'}]");
            $("#LONGITUDE_SECOND").attr("validate", "[{maxlength:32,msg:'经度-分录入最大长度为32'}]");
            $("#LATITUDE_DEGREE").attr("validate", "[{maxlength:32,msg:'纬度录入最大长度为32'}]");
            $("#LATITUDE_MINUTE").attr("validate", "[{maxlength:32,msg:'纬度-度录入最大长度为32'}]");
            $("#LATITUDE_SECOND").attr("validate", "[{maxlength:32,msg:'纬度-分录入最大长度为32'}]");
            $("#GRID_SIZE_X").attr("validate", "[{maxlength:32,msg:'网格大小X录入最大长度为32'}]");
            $("#GRID_SIZE_Y").attr("validate", "[{maxlength:32,msg:'网格大小Y录入最大长度为32'}]");
            $("#LOCATION").attr("validate", "[{maxlength:32,msg:'测点位置录入最大长度为32'}]");
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

                        $("#hidYear").val(data.YEAR);
                        $("#hidMonth").val(data.MONTH);
                        $("#hidCode").val(data.POINT_CODE);
                        $("#hidValue").val(data.POINT_NAME);
                    }
                });
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

    <input type="hidden" id="hidYear" runat="server" />
    <input type="hidden" id="hidMonth" runat="server" />
    <input type="hidden" id="hidCode" runat="server" />
    <input type="hidden" id="hidValue" runat="server" />
    </form>
</body>
</html>
