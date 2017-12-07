<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Base_Industry_IndustryEdit" Codebehind="IndustryEdit.aspx.cs" %>

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
    <script src="../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script type="text/javascript">
        var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        var strID = "";
        var editValue = [];

        $(document).ready(function () {
            strID = request('strid');
            if (!strID)
                strID = "";

            //创建表单结构 --点位基本信息
            $("#divEdit").ligerForm({
                inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
                fields: [
                { name: "ID", type: "hidden" },
                { display: "行业代码", name: "INDUSTRY_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "行业名称", name: "INDUSTRY_NAME", newline: true, type: "text" }
                ]
            });
            //表单验证
            //add validate by ssz
            $("#INDUSTRY_CODE").attr("validate", "[{required:true, msg:'请填写行业代码'},{maxlength:16,msg:'行业代码最大长度为16'}]");
            $("#INDUSTRY_NAME").attr("validate", "[{required:true, msg:'请填写行业名称'},{maxlength:256,msg:'行业名称最大长度为256'}]");


            //加载数据
            if (strID != "") {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "IndustryEdit.aspx?type=loadData&strid=" + strID,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        bindJsonToPage(data);
                    }
                });
            }
        });

        //得到保存信息
        function getSaveDate() {
            //表单验证
            if (!$("#divEdit").validate())
                return false;
            var strData = "{";
            if (strID == "")
                strData += "'strID':'0',";
            else
                strData += "'strID':'" + strID + "',";
            strData += "'strINDUSTRY_CODE':'" + $("#INDUSTRY_CODE").val() + "',";
            strData += "'strINDUSTRY_NAME':'" + $("#INDUSTRY_NAME").val() + "'";
            strData += "}";

            return strData;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divEdit">
    </div>
    </form>
</body>
</html>
