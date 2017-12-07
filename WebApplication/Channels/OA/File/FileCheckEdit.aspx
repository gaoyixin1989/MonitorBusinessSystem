<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_File_FileCheckEdit" Codebehind="FileCheckEdit.aspx.cs" %>

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
        var userList;
        var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        $(document).ready(function () {

            var update_type;
            $.ajax({
                type: "POST",
                async: false,
                url: "FileManageEdit.aspx?type=getDictJson&dict_type=update_type",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    update_type = data;
                }
            });
            //创建表单结构 
            $("#divEdit").ligerForm({
                inputWidth: 160, labelWidth: 120, space: 0, labelAlign: 'right',
                fields: [
                { name: "ID", type: "hidden" },
                { name: "DOCUMENT_ID", type: "hidden" },
                { display: "档案编号", name: "DOCUMENT_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "修订类别", name: "UPDATE_TYPE", newline: false, type: "select", comboboxName: "UPDATE_TYPE_ID", options: { valueFieldID: "UPDATE_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", data: update_type, initValue: selectFirst()} },
                { display: "档案名称", name: "DOCUMENT_NAME", newline: true, type: "text" },
                { display: "修订后名称", name: "OLD_FILE_NAME", newline: false, type: "text" },
                { display: "版本", name: "VERSION", newline: true, type: "text" },
                { display: "文件修订页", name: "PAGE_NUM", newline: false, type: "text" },
                { display: "修订人", name: "UPDATE_ID", newline: true, type: "text" },
                { display: "修订日期", name: "UPDATE_DATE", newline: false, type: "date", format: "yyyy-MM-dd" },
                { display: "备注", name: "REMARK", newline: true, type: "textarea", width: 450 }
                ]
            });

            //表单验证信息
            $("#OLD_FILE_NAME").attr("validate", "[{required:true,msg:'请录入修订后名称'},{maxlength:256,msg:'修改后名称录入最大长度为256'}]");
            $("#VERSION").attr("validate", "[{maxlength:32,msg:'版本录入最大长度为32'}]");
            $("#PAGE_NUM").attr("validate", "[{maxlength:8,msg:'文件修订页录入最大长度为8'}]");
            $("#UPDATE_ID").attr("validate", "[{required:true},{maxlength:64,msg:'修订人录入最大长度为64'}]");
            //表单填充&置灰处理
            $("#DOCUMENT_CODE").ligerGetTextBoxManager().setDisabled(); //档案编号
            $("#DOCUMENT_CODE").val($("#document_code").val());
            $("#DOCUMENT_NAME").ligerGetTextBoxManager().setDisabled(); //档案名称
            $("#DOCUMENT_NAME").val($("#document_name").val());

            $("#REMARK").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:430px");
            //保存类型选择第一个
            function selectFirst() {
                return update_type[0].DICT_CODE;
            }

            //加载数据
            if ($("#formId").val() != "") {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "FileCheckEdit.aspx?type=loadData&id=" + $("#formId").val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        bindJsonToPage(data);
                        for (var i = 0; i < update_type.length; i++) {
                            if (update_type[i].DICT_CODE == data.UPDATE_TYPE) {
                                $("#UPDATE_TYPE_ID").val(update_type[i].DICT_TEXT);
                            }
                        }
                    }
                });
            }
        });

        //保存于更新数据
        function DataSave() {
            var successReturn;
            var options = {
                cache: false,
                async: false,
                success: function (result) {
                    successReturn = result;
                },
                url: "FileCheckEdit.aspx?document_id=" + $("#document_id").val(),
                type: "post"
            };
            $("#form1").ajaxSubmit(options);
            if (successReturn == "1")
                return true;
            else
                return false;
        }
    </script>
</head>
<body>
    <form id="form1" action="FileCheckEdit.aspx" method="post">
    <div id="divEdit">
    </div>
    <input type="hidden" id="formId" runat="server" />
    <input type="hidden" id="formStatus" runat="server" />
    <input type="hidden" id="document_id" runat="server" />
    <input type="hidden" id="document_code" runat="server" />
    <input type="hidden" id="document_name" runat="server" />
    </form>
</body>
</html>
