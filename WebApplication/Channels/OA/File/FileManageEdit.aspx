<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_File_FileManageEdit" Codebehind="FileManageEdit.aspx.cs" %>

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
            var arrSaveType;
            $.ajax({
                type: "POST",
                async: false,
                url: "FileManageEdit.aspx?type=getDictJson&dict_type=save_type",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    arrSaveType = data;
                }
            });

            //创建表单结构 
            $("#divEdit").ligerForm({
                inputWidth: 160, labelWidth: 120, space: 0, labelAlign: 'right',
                fields: [
                { name: "ID", type: "hidden" },
                { display: "档案编号", name: "DOCUMENT_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "版本号/修订数", name: "VERSION", newline: false, type: "text" },
                { display: "档案名称", name: "DOCUMENT_NAME", newline: true, type: "text", width: 440 },
                { display: "主题词/关键字", name: "P_KEY", newline: true, type: "text", width: 440 },
                { display: "页码", name: "PAGE_CODE", newline: true, type: "digits" },
                { display: "总页数", name: "PAGE_SIZE", newline: false, type: "digits" },
                { display: "颁布时间/修订时间", name: "UPDATE_DATE", newline: true, type: "date" },
                { display: "保存类型", name: "SAVE_TYPE_NAME", newline: false, type: "select", comboboxName: "SAVE_TYPE_ID", options: { valueFieldID: "SAVE_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", data: arrSaveType, initValue: selectFirst()} },
                { display: "结束标记/颁布机构", name: "END_UNIT", newline: true, type: "text" },
                { display: "保存年份", name: "SAVE_YEAR", newline: false, type: "text" },
                { display: "条形码", name: "BAR_CODE", newline: true, type: "text", width: 440 },
                { display: "存放位置", name: "DOCUMENT_LOCATION", newline: true, type: "text", width: 440 },
                { display: "文件描述", name: "DOCUMENT_DESCRIPTION", newline: true, type: "text", width: 440 }
                ]
            });
            $("#DOCUMENT_CODE").attr("validate", "[{required:true, msg:'请填写档案编号'},{maxlength:64,msg:'档案编号录入最大长度为64'}]");
            $("#VERSION").attr("validate", "[{maxlength:32,msg:'版本号/修订数录入最大长度为32'}]");
            $("#DOCUMENT_NAME").attr("validate", "[{required:true, msg:'请填写档案名称'},{maxlength:256,msg:'档案名称录入最大长度为256'}]");
            $("#P_KEY").attr("validate", "[{maxlength:256,msg:'主题词/关键字录入最大长度为256'}]");
            $("#PAGE_CODE").attr("validate", "[{maxlength:8,msg:'页码录入最大长度为8'}]");
            $("#PAGE_SIZE").attr("validate", "[{maxlength:8,msg:'总页数录入最大长度为8'}]");
            $("#END_UNIT").attr("validate", "[{maxlength:64,msg:'结束标记/颁布机构录入最大长度为64'}]");
            $("#SAVE_YEAR").attr("validate", "[{maxlength:8,msg:'档案编号录入最大长度为8'}]");
            $("#BAR_CODE").attr("validate", "[{maxlength:64,msg:'条形码录入最大长度为64'}]");
            $("#DOCUMENT_LOCATION").attr("validate", "[{maxlength:512,msg:'存放位置录入最大长度为512'}]");
            $("#DOCUMENT_DESCRIPTION").attr("validate", "[{maxlength:512,msg:'文件描述录入最大长度为512'}]");

            //数据初始化
            $("#PAGE_CODE").val("1");
            $("#PAGE_SIZE").val("1");

            //保存类型选择第一个
            function selectFirst() {
                return arrSaveType[0].DICT_CODE;
            }

            //永久保存选择
            $("#SAVE_TYPE_ID").change(function () {
                if ($("#SAVE_TYPE_ID").val() == "长期保存") {
                    $("#SAVE_YEAR").val("永久");
                }
                else {
                    $("#SAVE_YEAR").val("1");
                }
            });

            //加载数据
            if ($("#formId").val() != "") {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "FileManageEdit.aspx?type=loadData&id=" + $("#formId").val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        bindJsonToPage(data);

                        for (var i = 0; i < arrSaveType.length; i++) {
                            if (arrSaveType[i].DICT_CODE == data.SAVE_TYPE) {
                                $("#SAVE_TYPE_ID").val(arrSaveType[i].DICT_TEXT);
                                if (arrSaveType[i].DICT_TEXT == "长期保存") {
                                    $("#SAVE_YEAR").val("永久");
                                    //$("#SAVE_YEAR").attr("disabled", true);
                                }
                            }
                        }
                    }
                });
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <div id="divEdit">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="color: Red; font-size: 13px; padding: 10px">
                    主题词/关键字录入的格式应为：主题词1|主题词2</div>
            </td>
        </tr>
    </table>
    <input type="hidden" id="formId" runat="server" />
    <input type="hidden" id="formStatus" runat="server" />
    </form>
</body>
</html>
