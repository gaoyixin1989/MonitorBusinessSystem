<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_Notice_NoticeEdit" Codebehind="NoticeEdit.aspx.cs" %>

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
        var url = "NoticeEdit.aspx";
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
        //上传附件
        function uploadNoticeFile() {
            
            $.ligerDialog.open({ title: '上传附件', width: 500, height: 270,
                buttons: [
                                {
                                    text:
                                        '上传', onclick: function (item, dialog) {
                                            $("iframe")[0].contentWindow.upLoadFile();
                                        }
                                },
                                {
                                    text:
                                        '关闭', onclick: function (item, dialog) {
                                            var strStatus = $("iframe")[0].contentWindow.getUpLoadStatus();
                                            dialog.close();
                                        }
                                }
                         ],
                url: '../ATT/AttFileUpload.aspx?filetype=OA_NOTICE&id=' + $("#formId").val()
            });
        }
        //下载附件
        function uploadNoticeDownLoad() {
            $.ligerDialog.open({ title: '图片预览', width: 500, height: 270,
                buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../ATT/AttFileDownLoad.aspx?filetype=OA_NOTICE&id=' + $("#formId").val()
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
                    <asp:TextBox ID="TITLE" runat="server" Text="" class="tableedit_title"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    发布时间
                </th>
                <td>
                    <asp:TextBox ID="RELEASE_TIME" runat="server" Text="" Style="width: 160px;"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 55px;">
                    发布人
                </td>
                <td>
                    <asp:TextBox ID="RELIEASER" runat="server" Text="" Style="width: 160px;"></asp:TextBox>
                </td>
                
            </tr>
            <%--<tr>
                <th>
                    附件信息
                </th>
                <td  >
                    <div id="divDownLoad">
                        <asp:LinkButton ID="btnFileUp" runat="server" OnClientClick="javascript:uploadNoticeFile();return false;">上传附件</asp:LinkButton>
                        <asp:LinkButton ID="btnDownLoad" runat="server" OnClientClick="javascript:uploadNoticeDownLoad();return false;">附件预览</asp:LinkButton>
                    </div>
                </td>
                <td style="text-align: right; width: 70px;">
                    显示序号
                </td>
                <td>
                    <asp:TextBox ID="REMARK1" runat="server" Text="" Style="width: 160px;"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <th>
                    公告内容
                </th>
                <td colspan="3">
                    <textarea cols="100" rows="12" class="l-textarea" id="CONTENT" runat="server" style="padding: 2px;
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
