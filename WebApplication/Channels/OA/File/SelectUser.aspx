﻿<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_File_SelectUser" Codebehind="SelectUser.aspx.cs" %>

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
            $("#dropDept").ligerComboBox({ textField: 'DICT_TEXT', valueField: 'DICT_CODE', valueFieldID: 'DEPT', data: arrDept });

            //档案文件列表
            fileManage =
            $("#divUser").ligerGrid({
                dataAction: 'server',
                usePager: true,
                pageSize: 10,
                alternatingRow: false,
                checkbox: true,
                onRClickToSelect: true,
                width: 420,
                pageSizeOptions: [5, 10, 15, 20],
                height: 140,
                url: 'SelectUser.aspx?type=getUserInfo',
                columns: [
                { display: '员工编号', name: 'EMPLOYE_CODE', align: 'left', width: 100, minWidth: 60 },
                { display: '员工姓名', name: 'EMPLOYE_NAME', align: 'left', width: 100, minWidth: 60 },
                { display: '职务', name: 'POST', align: 'left', width: 60, minWidth: 60, render: function (data) {
                    return getDict("duty_code", data.POST);
                }
                },
                { display: '在职状态', name: 'POST_STATUS', align: 'left', width: 60, minWidth: 60, render: function (data) {
                    getDict("EmployeStatus", data.POST_STATUS);
                }
                }
                ],
                onCheckRow: function (checked, rowdata, rowindex) {
                    for (var rowid in this.records)
                        this.unselect(rowid);
                    this.select(rowindex);
                },
                onBeforeCheckAllRow: function (checked, grid, element) { return false; }
            });
            $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
            //部门选择
            $("#dropDept").change(function () {
                fileManage.set('url', "SelectUser.aspx?type=getUserInfo&dept=" + $("#DEPT").val());
            });
        });

        //获得在职状态
        function getDict(dict_type, dict_code) {
            var text;
            $.ajax({
                type: "POST",
                async: false,
                url: "SelectUser.aspx?type=getDict&dict_type=" + dict_type + "&dict_code=" + dict_code,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    text = data;
                }
            });
            return text;
        }
        //选择
        function selectRow() {
            return fileManage.getSelectedRows();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divDept" position="top">
        <table style="margin: 5px">
            <tr>
                <td>
                    部门：
                </td>
                <td>
                    <input id="dropDept" type="text" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divUser" position="center" style="margin-left: 5px">
    </div>
    <input type="hidden" id="formId" runat="server" />
    <input type="hidden" id="formStatus" runat="server" />
    </form>
</body>
</html>
