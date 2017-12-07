<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_File_FileBorrowEdit" Codebehind="FileBorrowEdit.aspx.cs" %>

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

            //创建表单结构 
            $("#divEdit").ligerForm({
                inputWidth: 160, labelWidth: 120, space: 0, labelAlign: 'right',
                fields: [
                { name: "ID", type: "hidden" },
                { name: "DOCUMENT_ID", type: "hidden" },
                { display: "档案编号", name: "DOCUMENT_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "借阅人", name: "BORROWER_NAME", newline: false, type: "text" },
                { name: "BORROWER", newline: false, type: "hidden" },
                { display: "借阅天数", name: "HOLD_TIME", newline: true, type: "digits" },
                { display: "借出时间", name: "LOAN_TIME", newline: false, type: "date" },
                { display: "备注", name: "REMARK", newline: true, type: "textarea", width: 450 }
                ]
            });
            $("#BORROWER").attr("validate", "[{required:true,msg:'请选择借阅人'}]");
            $("#HOLD_TIME").attr("validate", "[{maxlength:8,msg:'借阅天数录入最大长度为8'}]");
            $("#REMARK").attr("validate", "[{maxlength:512,msg:'备注录入最大长度为512'}]");
            $("#BORROWER_NAME").ligerGetTextBoxManager().setDisabled();
            $("#DOCUMENT_CODE").ligerGetTextBoxManager().setDisabled();
            $("#DOCUMENT_CODE").val($("#document_code").val());
            $("#REMARK").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:430px");
            //数据初始化
            $("#HOLD_TIME").val("1");
            //部门选择
            $("#BORROWER_NAME").click(function () {
                $.ligerDialog.open({ title: '人员选择', top: 0, width: 450, height: 250, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            var fn = dialog.frame.selectRow || dialog.frame.window.selectRow;
            var data = fn();
            if (!data) {
                $.ligerDialog.warn('请选择借出人员!');
                return;
            }
            $.each(data, function (index, row) {
                $("#BORROWER_NAME").val(row.EMPLOYE_NAME);
                $("#BORROWER").val(row.ID);
            });
            dialog.close();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'SelectUser.aspx'
                });
            });

            //加载数据
            if ($("#formId").val() != "") {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "FileBorrowEdit.aspx?type=loadData&id=" + $("#formId").val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        bindJsonToPage(data);
                    }
                });
            }
        });

        //获得人员信息
        function getUserByDept(dept) {
            if (dept != null) {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "FileBorrowEdit.aspx?type=getUserByDept&dept=" + dept,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        userList = data;
                    }
                });
            }
        }

        //保存于更新数据
        function DataSave() {
            var successReturn;
            var options = {
                cache: false,
                async: false,
                success: function (result) {
                    successReturn = result;
                },
                url: "FileBorrowEdit.aspx?document_id=" + $("#document_id").val(),
                type: "post"
            };
            //验证
            if (!$("#form1").validate())
                return false;
            $("#form1").ajaxSubmit(options);
            if (successReturn == "1")
                return true;
            else if (successReturn == "2") {
                $.ligerDialog.warn("已借出！");
                return false;
            }
            else
                return false;
        }
    </script>
</head>
<body>
    <form id="form1" action="FileBorrowEdit.aspx" method="post">
    <div id="divEdit">
    </div>
    <input type="hidden" id="formId" runat="server" />
    <input type="hidden" id="formStatus" runat="server" />
    <input type="hidden" id="document_id" runat="server" />
    <input type="hidden" id="document_code" runat="server" />
    </form>
</body>
</html>
