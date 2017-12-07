<%@ Page Language="C#" AutoEventWireup="true" Codebehind="PolluteEidt.aspx.cs" Inherits="Channels_Env_Point_PolluteRule_PolluteEidt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        var url = "PolluteEidt.aspx";
        $(document).ready(function () {
            GlobalMonitorType = "EnvRiver";
            $("#divEdit").ligerForm({
                inputWidth: 150, labelWidth: 160, space: 90, labelAlign: 'right',
                fields: [
                          { display: "年度", name: "YEAR_NAME", newline: true, type: "select", comboboxName: "YEAR_NAME_BOX", options: { valueFieldID: "YEAR", valueField: "value", textField: "value", url: url + "?type=getYearInfo" }, group: "基本信息", groupicon: groupicon },
                          { display: "月度", name: "MONTH_NAME", newline: false, type: "select", comboboxName: "MONTH_NAME_BOX", options: { valueFieldID: "MONTH", valueField: "value", textField: "value", url: url + "?type=getMonthInfo"} },
                          { display: "断面代码", name: "POINT_CODE", newline: true, type: "text" },
                          { display: "断面名称", name: "POINT_NAME", newline: false, type: "text" },
                          { display: "监测月度", name: "SELECT_MONTH", newline: true, type: "select", comboboxName: "SELECT_MONTH_BOX", width: 530, options: { valueFieldID: "SelectMonths", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getDict&dictType=itemmonths", isShowCheckBox: true, isMultiSelect: true, selectBoxHeight: 265} },
                          { display: "受纳水体代码", name: "Water_Code", newline: true, type: "text",group: "废水", groupicon: groupicon },
                          { display: "受纳水体名称", name: "Water_Name", newline: false, type: "text" },
                          { display: "排污设备名称", name: "Sewerage_Name", newline: true, type: "text", group: "废气", groupicon: groupicon },
                          { display: "排放设备类型", name: "Equipment_Name", newline: false, type: "text" }, 
                          { display: "生产产品", name: "MO_Name", newline: true, type: "text" },
                          { display: "生产能力", name: "MO_Capacity", newline: false, type: "text" },
                          { display: "生产能力单位", name: "MO_UOM", newline: true, type: "text" },
                          { display: "投产日期", name: "MO_Date", newline: false, type: "date" }, 
                          { display: "燃料类型", name: "Fuel_Type", newline: true, type: "text" },
                          { display: "燃料年消耗量", name: "Fuel_Qty", newline: false, type: "text" },
                          { display: "锅炉燃烧方式", name: "Fuel_Model", newline: true, type: "text" },
                          { display: "低炭燃烧技术", name: "Fuel_Tech", newline: false, type: "text" },
                          { display: "是否循环流化床锅炉", name: "Is_Fuel", newline: true, type: "text" },
                          { display: "排放规律", name: "Discharge_Way", newline: false, type: "text" },
                          { display: "日生产小时数", name: "MO_Hour_Qty", newline: true, type: "text" },
                          { display: "工况负荷", name: "Load_Mode", newline: false, type: "text" },
                          { display: "测点温度", name: "Point_Temp", newline: true, type: "text" },
                          { display: "治理设施是否正常运行", name: "Is_Run", newline: false, type: "text" },
                          { display: "处理设施前实测浓度", name: "Measured", newline: true, type: "text" },
                          { display: "处理设施前实测废气排放量", name: "Waste_Air_Qty", newline: false, type: "text" },
                          { display: "经度", name: "LONGITUDE_DEGREE", newline: true, type: "text", width: 40, space: 1, group: "扩展信息", groupicon: groupicon },
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
                    }
                });
            }

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div  id="divEdit">
    </div>
      <input type="hidden" id="formId" runat="server" />
    <input type="hidden" id="formStatus" runat="server" /> 
       <input type="hidden" id="TypeId" runat="server" /> 
    </form>
</body>
</html>
