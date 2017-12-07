<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_File_FileManage" Codebehind="FileManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/style.css" rel="stylesheet"
        type="text/css" />
    <!--加载zTree菜单树控件必须文件-->
    <link href="../../../Controls/zTree3.4/css/zTreeStyle/zTreeStyle.css" rel="stylesheet"
        type="text/css" />
    <link rel="stylesheet" href="../../../Controls/zTree3.4/css/divuniontable.css" type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/zTree3.4/js/jquery.ztree.core-3.4.min.js" type="text/javascript"></script>
    <script src="../../../Controls/zTree3.4/js/jquery.ztree.exedit-3.4.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="FileManage.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 99%">
    <div id="layout1">
        <div position="left" title="档案目录">
            <div id="edit" style="display: none; margin-top: 6px;">
                重命名为：<input id="txt_rename" type="text" style="width: 80px; border: solid 1px #a0d5ef" />
                <input id="btnRename" type="button" onclick="btnRename_Click();" value="确定" class="button2013"
                    style="cursor: pointer" />
                <input id="btnCancel" type="button" onclick="btnCancel_Click();" value="取消" class="button2013"
                    style="cursor: pointer" />
            </div>
            <div id="tree">
            </div>
        </div>
        <div id="layout2" position="center">
            <div position="top" title="档案文件">
                <div id="file">
                </div>
            </div>
            <div position="center">
                <div id="navtab1" style="width: 100%; overflow: hidden; border: 1px solid #A3C0E8;">
                    <div title="借阅管理">
                        <div id="maingrid1">
                        </div>
                    </div>
                    <div id="send" title="分发管理">
                        <div id="maingrid2">
                        </div>
                    </div>
                    <div id="check" title="修订管理">
                        <div id="maingrid3">
                        </div>
                    </div>
                    <div id="update" title="查新管理">
                        <div id="maingrid4">
                        </div>
                    </div>
                    <%--                    <div title="废止历史">
                        <div id="maingrid5">
                        </div>
                    </div>
                    <div title="销毁记录">
                        <div id="maingrid6">
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
    <div id="searchDetail" style="display: none;">
        <table>
            <tr>
                <td>
                    <div id="searchDiv">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="color: Red; font-size: 13px; padding: 10px">
                        主题词/关键字录入的格式应为：主题词1|主题词2
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
