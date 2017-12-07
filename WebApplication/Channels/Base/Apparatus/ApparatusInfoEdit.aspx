<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Base_Apparatus_ApparatusInfoEdit" Codebehind="ApparatusInfoEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        $(document).ready(function () {
            //创建表单结构 
            $("#divEdit").ligerForm({
                inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
                fields: [
                { name: "ID", type: "hidden" },
                { display: "仪器编号", name: "APPARATUS_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "仪器名称", name: "NAME", newline: true, type: "text", width: 530 },
                { display: "规格型号", name: "MODEL", newline: true, type: "text" },
                { display: "出厂编号", name: "SERIAL_NO", newline: false, type: "text" },
                { display: "所属仪器或项目", name: "BELONG_TO", newline: true, type: "text" },
                { display: "购买时间", name: "BUY_TIME", newline: false, type: "text" },
                { display: "报废时间", name: "SCRAP_TIME", newline: true, type: "date" },
                { display: "溯源方式", name: "CERTIFICATE_TYPE_NAME", newline: false, type: "select", comboboxName: "CERTIFICATE_TYPE_NAME", options: { valueFieldID: "CERTIFICATE_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "ApparatusInfoEdit.aspx?type=getDict&dictType=certificate_Type"} },
                { display: "溯源结果", name: "TRACE_RESULT_NAME", newline: true, type: "select", comboboxName: "TRACE_RESULT_NAME", options: { valueFieldID: "TRACE_RESULT", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "ApparatusInfoEdit.aspx?type=getDict&dictType=TRACE_RESULT"} },
                { display: "检定方式", name: "TEST_MODE_NAME", newline: false, type: "select", comboboxName: "TEST_MODE_NAME", options: { valueFieldID: "TEST_MODE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "ApparatusInfoEdit.aspx?type=getDict&dictType=test_mode"} },
                { display: "校正周期", name: "VERIFY_CYCLE", newline: true, type: "text" },
                { display: "使用科室", name: "DEPT_NAME", newline: false, type: "select", comboboxName: "DEPT_NAME", options: { valueFieldID: "DEPT", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "ApparatusInfoEdit.aspx?type=getDict&dictType=dept"} },
                { display: "保管人", name: "KEEPER", newline: true, type: "text" },
                { display: "放置地点", name: "POSITION", newline: false, type: "text" },
                { display: "档案类别", name: "ARCHIVES_TYPE", newline: true, type: "text", group: "类别信息", groupicon: groupicon },
                { display: "类别1", name: "SORT1_NAME", newline: false, type: "select", comboboxName: "SORT1_NAME", options: { valueFieldID: "SORT1", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "ApparatusInfoEdit.aspx?type=getDict&dictType=sort1"} },
                { display: "类别2", name: "SORT2", newline: true, type: "text" },
                { display: "仪器供应商", name: "APPARATUS_PROVIDER", newline: true, type: "text", group: "供应商信息", groupicon: groupicon },
                { display: "配件供应商", name: "FITTINGS_PROVIDER", newline: false, type: "text" },
                { display: "仪器供应商网址", name: "WEB_SITE", newline: true, type: "text", width: 530 },
                { display: "联系人", name: "LINK_MAN", newline: true, type: "text", group: "联系人信息", groupicon: groupicon },
                { display: "联系电话", name: "LINK_PHONE", newline: false, type: "text" },
                { display: "邮编", name: "POST", newline: true, type: "text" },
                { display: "联系地址", name: "ADDRESS", newline: false, type: "text" },
                { display: "最近检定/校准时间", name: "BEGIN_TIME", newline: true, type: "date", group: "检定信息", groupicon: groupicon },
                { display: "到期检定/校准时间", name: "END_TIME", newline: false, type: "date" },
                { display: "期间核查方式", name: "VERIFICATION_WAY_NAME", newline: true, type: "select", comboboxName: "VERIFICATION_WAY_NAME", options: { valueFieldID: "VERIFICATION_WAY", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "ApparatusInfoEdit.aspx?type=getDict&dictType=VERIFICATION_WAY"} },
                { display: "期间核查结果", name: "VERIFICATION_RESULT_NAME", newline: false, type: "select", comboboxName: "VERIFICATION_RESULT_NAME", options: { valueFieldID: "VERIFICATION_RESULT", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "ApparatusInfoEdit.aspx?type=getDict&dictType=VERIFICATION_RESULT"} },
                { display: "最近期间核查时间", name: "VERIFICATION_BEGIN_TIME", newline: true, type: "date" },
                { display: "到期期间核查时间", name: "VERIFICATION_END_TIME", newline: false, type: "date" },
                { display: "扩展不确定度", name: "EXPANDED_UNCETAINTY", newline: true, type: "text" },
                { display: "测量范围", name: "MEASURING_RANGE", newline: false, type: "text" },
                { display: "检定单位", name: "EXAMINE_DEPARTMENT", newline: true, type: "text" },
                { display: "检定单位电话", name: "DEPARTMENT_PHONE", newline: false, type: "text" },
                { display: "检定单位联系人", name: "DEPARTMENT_LINKMAN", newline: true, type: "text" }
                ]
            });
            $("#APPARATUS_CODE").attr("validate", "[{required:true, msg:'请填写仪器编号'},{maxlength:64,msg:'仪器编号最大长度为64'}]");
            $("#NAME").attr("validate", "[{required:true, msg:'请填写仪器名称'},{maxlength:256,msg:'仪器名称最大长度为256'}]");
            $("#MODEL").attr("validate", "[{maxlength:32,msg:'规格型号最大长度为32'}]");
            $("#SERIAL_NO").attr("validate", "[{maxlength:32,msg:'出厂编号最大长度为32'}]");
            $("#BELONG_TO").attr("validate", "[{maxlength:32,msg:'所属仪器或项目最大长度为32'}]");
            $("#VERIFY_CYCLE").attr("validate", "[{maxlength:8,msg:'校正周期最大长度为8'}]");
            $("#KEEPER").attr("validate", "[{maxlength:8,msg:'保管人最大长度为8'}]");
            $("#POSITION").attr("validate", "[{maxlength:512,msg:'放置地点最大长度为512'}]");
            $("#ARCHIVES_TYPE").attr("validate", "[{maxlength:8,msg:'档案类别最大长度为8'}]");
            $("#SORT2").attr("validate", "[{maxlength:8,msg:'类别2最大长度为8'}]");
            $("#APPARATUS_PROVIDER").attr("validate", "[{maxlength:256,msg:'仪器供应商最大长度为256'}]");
            $("#FITTINGS_PROVIDER").attr("validate", "[{maxlength:256,msg:'配件供应商最大长度为256'}]");
            $("#WEB_SITE").attr("validate", "[{maxlength:256,msg:'仪器供应商网址最大长度为256'}]");
            $("#LINK_MAN").attr("validate", "[{maxlength:256,msg:'联系人最大长度为256'}]");
            $("#LINK_PHONE").attr("validate", "[{maxlength:256,msg:'联系电话最大长度为256'}]");
            $("#POST").attr("validate", "[{maxlength:256,msg:'邮编最大长度为256'}]");
            $("#ADDRESS").attr("validate", "[{maxlength:256,msg:'联系地址最大长度为256'}]");
            $("#EXPANDED_UNCETAINTY").attr("validate", "[{maxlength:256,msg:'扩展不确定度最大长度为256'}]");
            $("#MEASURING_RANGE").attr("validate", "[{maxlength:256,msg:'测量范围最大长度为256'}]");
            $("#EXAMINE_DEPARTMENT").attr("validate", "[{maxlength:128,msg:'检定单位最大长度为128'}]");
            $("#DEPARTMENT_PHONE").attr("validate", "[{maxlength:128,msg:'检定单位电话最大长度为128'}]");
            $("#DEPARTMENT_LINKMAN").attr("validate", "[{maxlength:128,msg:'检定单位联系人最大长度为128'}]");
            //加载数据
            if ($("#formId").val() != "") {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "ApparatusInfoEdit.aspx?type=loadData&id=" + $("#formId").val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        bindJsonToPage(data);
                    }
                });
            }

            $("#BUY_TIME").ligerDateEditor({ onChangeDate: function (value) {
                var str = value.split('-');
                var d = parseInt(str[0]) + 10 + '-' + str[1] + '-' + str[2];
                //alert(d);
                $("#SCRAP_TIME").val(d);
            }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divEdit">
    </div>
    <input type="hidden" id="formId" runat="server" />
    <input type="hidden" id="formStatus" runat="server" />
    </form>
</body>
</html>
