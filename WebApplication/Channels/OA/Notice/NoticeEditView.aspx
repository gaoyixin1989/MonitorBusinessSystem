<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_Notice_NoticeEditView" Codebehind="NoticeEditView.aspx.cs" %>

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
    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        var url = "NoticeEditView.aspx";
        $(document).ready(function () {
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

        function uploadNoticeDownLoad() {
            parent.$.ligerDialog.open({ title: '附件下载', width: 800, height: 350, isHidden: false,
                buttons: [
                 { text: '直接下载', onclick: function (item, dialog) {
                     dialog.frame.aa(); //调用下载按钮
                 }
                 },
                {
                    text:
                     '关闭', onclick: function (item, dialog) { dialog.close(); }
                }], url: '../Channels/OA/ATT/AttMoreFileDownLoad.aspx?filetype=OA_NOTICE&id=' + $("#formId").val()
            });
        }
    </script>
    <style type="text/css">
        .l-minheight div
        {
            height: 90px;
            overflow: hidden;
        }
        .style1
        {
            width: 15%;
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="tablelayout">
        <table border="0" cellpadding="0" cellspacing="0" class="tableedit mlr8" width="100px">
            <tr>
                <th>
                    公告标题
                </th>
                <td colspan="3">
                    <asp:TextBox ID="TITLE" Width="620" runat="server" Text="" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    附件下载
                </th>
                <td colspan="3">
                    <a href="#" onclick="uploadNoticeDownLoad()">附件下载</a> 
                </td>
            </tr>
            <tr>
                <th>
                    公告内容
                </th>
                <td colspan="3">
                    <textarea cols="100" rows="15" class="l-textarea" id="CONTENT" runat="server" style="padding: 2px;
                        margin-left: 0; line-height: 20px;" name="S1"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="formId" runat="server" />
    <input type="hidden" id="formStatus" runat="server" />
    </form>
</body>
</html>
