<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Base_Company_CompanyInfoEdit" Codebehind="CompanyInfoEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                inputWidth: 160, labelWidth: 180, space: 20, labelAlign: 'right',
                fields: [
                { name: "ID", type: "hidden" },
                { display: "企业名称", name: "COMPANY_NAME", newline: true, type: "text", width: 520, group: "基本信息", groupicon: groupicon },
                { display: "企业法人代码", name: "COMPANY_CODE", newline: true, type: "text" },
                { display: "法人代表", name: "COMPANY_MAN", newline: false, type: "text" },
                { display: "企业级别", name: "COMPANY_LEVEL", newline: true, type: "select", comboboxName: "COMPANY_LEVEL_ID", options: { valueFieldID: "COMPANY_LEVEL", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "CompanyInfoEdit.aspx?type=getDict&dictType=company_level"} },
                { display: "拼音编码", name: "PINYIN", newline: false, type: "text" },
                { display: "主管部门", name: "DIRECTOR_DEPT", newline: true, type: "text" },
                { display: "经济类型", name: "ECONOMY_TYPE_NAME", newline: false, type: "select", comboboxName: "ECONOMY_TYPE_NAME", options: { valueFieldID: "ECONOMY_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "CompanyInfoEdit.aspx?type=getDict&dictType=company_economic_type"} },
                { display: "所在区域", name: "AREA_NAME", newline: true, type: "select", comboboxName: "AREA_NAME", options: { valueFieldID: "AREA", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "CompanyInfoEdit.aspx?type=getDict&dictType=administrative_area"} },
                { display: "废水最终排放去向", name: "WATER_FOLLOW", newline: false, type: "select",  comboboxName: "WATER_FOLLOW_ID", options: { valueFieldID: "WATER_FOLLOW", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "CompanyInfoEdit.aspx?type=getDict&dictType=water_follow"} },
                { display: "企业规模", name: "SIZE_NAME", newline: true, type: "select", comboboxName: "SIZE_NAME", options: { valueFieldID: "SIZE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "CompanyInfoEdit.aspx?type=getDict&dictType=company_size"} },
                { display: "企业网址", name: "WEB_SITE", newline: false, type: "text" },
                { display: "行业类别", name: "INDUSTRY_NAME", newline: true, type: "select", comboboxName: "INDUSTRY_NAME", options: { valueFieldID: "INDUSTRY", valueField: "ID", textField: "INDUSTRY_NAME", url: "CompanyInfoEdit.aspx?type=getIndustry"} },
                { display: "开业时间", name: "PRACTICE_DATE", newline: false, type: "date" },
                { display: "废气控制级别", name: "GAS_LEAVEL_NAME", newline: true, type: "select", comboboxName: "GAS_LEAVEL_NAME", options: { valueFieldID: "GAS_LEAVEL", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "CompanyInfoEdit.aspx?type=getDict&dictType=control_level"} },
                { display: "废水控制级别", name: "WATER_LEAVEL_NAME", newline: false, type: "select", comboboxName: "WATER_LEAVEL_NAME", options: { valueFieldID: "WATER_LEAVEL", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "CompanyInfoEdit.aspx?type=getDict&dictType=control_level"} },
                { display: "联系人", name: "CONTACT_NAME", newline: true, type: "text", group: "联系方式", groupicon: groupicon },
                { display: "联系部门", name: "LINK_DEPT", newline: false, type: "text" },
                { display: "电子邮件", name: "EMAIL", newline: true, type: "text" },
                { display: "联系电话", name: "LINK_PHONE", newline: false, type: "text" },
                { display: "委托代理人", name: "FACTOR", newline: true, type: "text" },
                { display: "办公电话", name: "PHONE", newline: false, type: "text" },
                { display: "移动电话", name: "MOBAIL_PHONE", newline: true, type: "text" },
                { display: "传真号码", name: "FAX", newline: false, type: "text" },
                { display: "邮政编码", name: "POST", newline: true, type: "text" },
                { display: "监测地址", name: "MONITOR_ADDRESS", newline: true, type: "text", width: 520 },
                { display: "通讯地址", name: "CONTACT_ADDRESS", newline: true, type: "text", width: 520 },
                { display: "经度", name: "LONGITUDE", newline: true, type: "text" },
                { display: "纬度", name: "LATITUDE", newline: false, type: "text" },

                { display: "现有工程环评批复时间及文号", name: "CHECK_TIME", newline: true, type: "text",group: "其他信息", groupicon: groupicon,},
                { display: "现有工程竣工环境保护验收时间", name: "ACCEPTANCE_TIME", newline: false, type: "text" },
                { display: "执行标准", name: "STANDARD", newline: true, type: "text",width:520 },
                { display: "主要环保设施名称、数量", name: "MAIN_APPARATUS", newline: true, type: "text",width:520},
                { display: "环保设施运行情况", name: "APPARATUS_STATUS", newline: true, type: "text" },
                { display: "主要产品名称", name: "MAIN_PROJECT", newline: true, type: "text" },
                { display: "主要生产原料", name: "MAIN_GOOD", newline: false, type: "text" },
                { display: "设计生产能力", name: "DESIGN_ANBILITY", newline: true, type: "text" },
                { display: "实际生产能力", name: "ANBILITY", newline: false, type: "text" },
                { display: "监测期间生产负荷（%）", name: "CONTRACT_PER", newline: true, type: "text" },
                { display: "全年平均生产负荷（%）", name: "AVG_PER", newline: false, type: "text" },
                { display: "废水排放量", name: "WATER_COUNT", newline: true, type: "text" },
                { display: "年运行时间", name: "YEAR_TIME", newline: false, type: "text" }
                ]
            });
            $("#COMPANY_NAME").attr("validate", "[{required:true, msg:'请填写企业名称'},{maxlength:256,msg:'企业名称最大长度为256'}]");
            $("#COMPANY_CODE").attr("validate", "[{maxlength:16,msg:'企业法人代码最大长度为16'}]");
            $("#PINYIN").attr("validate", "[{maxlength:64,msg:'拼音编码最大长度为64'}]");
            $("#DIRECTOR_DEPT").attr("validate", "[{maxlength:128,msg:'主管部门最大长度为128'}]");
            $("#WEB_SITE").attr("validate", "[{maxlength:256,msg:'企业网址最大长度为256'}]");
            $("#CONTACT_NAME").attr("validate", "[{maxlength:32,msg:'联系人最大长度为32'}]");
            $("#LINK_DEPT").attr("validate", "[{maxlength:64,msg:'联系部门最大长度为64'}]");
            $("#EMAIL").attr("validate", "[{maxlength:128,msg:'电子邮件最大长度为128'}]");
            $("#LINK_PHONE").attr("validate", "[{maxlength:32,msg:'联系电话最大长度为32'}]");
            $("#FACTOR").attr("validate", "[{maxlength:32,msg:'委托代理人最大长度为32'}]");
            $("#PHONE").attr("validate", "[{maxlength:32,msg:'办公电话最大长度为32'}]");
            $("#MOBAIL_PHONE").attr("validate", "[{maxlength:32,msg:'移动电话最大长度为32'}]");
            $("#FAX").attr("validate", "[{maxlength:32,msg:'传真号码最大长度为32'}]");
            $("#POST").attr("validate", "[{maxlength:8,msg:'邮政编码最大长度为8'}]");
            $("#MONITOR_ADDRESS").attr("validate", "[{maxlength:256,msg:'监测地址最大长度为256'}]");
            $("#CONTACT_ADDRESS").attr("validate", "[{maxlength:256,msg:'通讯地址最大长度为256'}]");
            $("#LONGITUDE").attr("validate", "[{maxlength:64,msg:'经度最大长度为64'}]");
            $("#LATITUDE").attr("validate", "[{maxlength:64,msg:'纬度最大长度为64'}]");

            $("#COMPANY_MAN").attr("validate", "[{maxlength:64,msg:'法人代表最大长度为64'}]");
            $("#CHECK_TIME").attr("validate", "[{maxlength:64,msg:'现有工程环评批复时间及文号最大长度为64'}]");
            $("#ACCEPTANCE_TIME").attr("validate", "[{maxlength:64,msg:'现有工程竣工环境保护验收时间最大长度为64'}]");
            $("#STANDARD").attr("validate", "[{maxlength:1024,msg:'执行标准最大长度为1024'}]");
            $("#MAIN_APPARATUS").attr("validate", "[{maxlength:1024,msg:'主要环保设施名称、数量最大长度为1024'}]");
            $("#APPARATUS_STATUS").attr("validate", "[{maxlength:64,msg:'环保设施运行情况最大长度为64'}]");
            $("#MAIN_PROJECT").attr("validate", "[{maxlength:1024,msg:'主要产品名称最大长度为1024'}]");
            $("#MAIN_GOOD").attr("validate", "[{maxlength:1024,msg:'主要生产原料最大长度为1024'}]");
            $("#DESIGN_ANBILITY").attr("validate", "[{maxlength:64,msg:'设计生产能力最大长度为64'}]");
            $("#ANBILITY").attr("validate", "[{maxlength:64,msg:'实际生产能力最大长度为64'}]");
            $("#CONTRACT_PER").attr("validate", "[{maxlength:8,msg:'监测期间生产负荷（%）最大长度为8'}]");
            $("#AVG_PER").attr("validate", "[{maxlength:8,msg:'全年平均生产负荷（%）最大长度为8'}]");
            $("#WATER_COUNT").attr("validate", "[{maxlength:64,msg:'废水排放量最大长度为64'}]");
            $("#YEAR_TIME").attr("validate", "[{maxlength:64,msg:'年运行时间最大长度为64'}]");

            //加载数据
            if ($("#formId").val() != "") {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "CompanyInfoEdit.aspx?type=loadData&id=" + $("#formId").val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        bindJsonToPage(data);
                    }
                });
            }
        });
        //保存于更新数据
        function DataSave() {
            var isSuccess = false;
            ajaxSubmit("CompanyInfoEdit.aspx", function () {
                return true;
            }, function (responseText, statusText) {
                if (responseText == "1")
                    isSuccess = true;
            });
            return isSuccess;
        }
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
