<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Report_SelectUser" Codebehind="SelectUser.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <script type="text/javascript">
        var fileManage;
        var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        $(document).ready(function () {
            var arrDept;
            $.ajax({
                type: "POST",
                async: false,
                url: "SelectUser.aspx?type=getDept",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    arrDept = data;
                }
            });

            //档案文件列表
            fileManage =
            $("#divUser").ligerGrid({
                dataAction: 'server',
                usePager: true,
                pageSize: 10,
                alternatingRow: false,
                checkbox: true,
                onRClickToSelect: true,
                width: 390,
                pageSizeOptions: [5, 10, 15, 20],
                height: 170,
                url: 'SelectUser.aspx?type=getUserInfo&task_id=' + $("#hdnTaskID").val(),
                columns: [
                { display: '姓名', name: 'REAL_NAME', align: 'left', width: 100, minWidth: 60 },
                { display: '职务', name: 'POST_NAME', align: 'left', width: 150, minWidth: 60
                }//,
                //{ display: '监测类别', name: 'MONITOR_NAME', align: 'left', width: 100, minWidth: 60 }
                ],
                onCheckRow: function (checked, rowdata, rowindex) {
                    for (var rowid in this.records)
                        this.unselect(rowid);
                    this.select(rowindex);
                },
                onBeforeCheckAllRow: function (checked, grid, element) { return false; }
            });
            $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
        });
        //选择
        function selectRow() {
            return fileManage.getSelectedRows();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divUser" position="center" style="margin-left: 5px">
    </div>
    <input type="hidden" id="formId" runat="server" />
    <input type="hidden" id="formStatus" runat="server" />
    <input type="hidden" id="hdnTaskID" runat="server" />
    </form>
</body>
</html>
