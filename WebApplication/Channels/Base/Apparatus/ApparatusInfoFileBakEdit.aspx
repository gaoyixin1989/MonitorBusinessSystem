<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Base_Apparatus_ApparatusInfoFileBakEdit" Codebehind="ApparatusInfoFileBakEdit.aspx.cs" %>

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
                inputWidth: 160, labelWidth: 120, space: 40, labelAlign: 'right',
                fields: [
                { name: "ID", type: "hidden" },
                //                { display: "资料编号", name: "APPARATUS_FILE_CODE", newline: true, type: "text"},
                {display: "资料名称", name: "APPARATUS_ATT_NAME", newline: true, type: "text", width: 300, group: "基本信息", groupicon: groupicon }
                ]
            });
            $("#APPARATUS_ATT_NAME").attr("validate", "[{required:true, msg:'请填写资料名称'},{maxlenght:256,msg:'资料名称最大长度为256'}]");

            //加载数据
            if ($("#formId").val() != "") {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "ApparatusInfoFileBakEdit.aspx?type=loadData&id=" + $("#formId").val(),
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
    <div id="divEdit">
    </div>
    <input type="hidden" id="formId" runat="server" />
    <input type="hidden" id="formObjId" runat="server" />
    <input type="hidden" id="formStatus" runat="server" />
    </form>
</body>
</html>
