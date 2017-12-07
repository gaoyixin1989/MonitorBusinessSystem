<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_SW_SWList" Codebehind="SWList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <!--Jquery 基础文件-->
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <!--自动完成-->
    <script src="../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <!--LigerUI-->
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
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
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="SWList.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $("#navtab1").ligerTab({ contextmenu: false, onBeforeSelectTabItem: function (tabid) {
            },
                //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
                onAfterSelectTabItem: function (tabid) {
                    navtab = $("#navtab1").ligerGetTabManager();
                    if (tabid == "home") {
                        gridName = "0";
                    }
                    if (tabid == "tabitem1") {
                        gridName = "1";
                    }
                    if (tabid == "tabitem2") {
                        gridName = "2";
                    }
                    navtab.reload(navtab.getSelectedTabItemID());
                    if (tabid != 'home') {
                        isFisrt = false;
                        //GetIngDate();
                        //GetFinishedDate();
                    }
                    else {
                        isFisrt = true;
                    }
                }
            });
        });
    </script>
    <title></title>
</head>
<body style="padding: 0px; overflow: hidden;">
    <div id="navtab1" style="width: 100%; overflow: hidden; border: 1px solid #A3C0E8;">
        <div tabid="home" title="未提交列表" lselected="true" contextmenu="false">
            <div id="oneGrid" style="margin: 0px;">
            </div>
        </div>
        <div title="流转中列表">
            <div id="twoGrid" style="margin: 0px;">
            </div>
        </div>
        <div title="已办结列表">
            <div id="threeGrid" style="margin: 0px;">
            </div>
        </div>
    </div>
</body>
</html>