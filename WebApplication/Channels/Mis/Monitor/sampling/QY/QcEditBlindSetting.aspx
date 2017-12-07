<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_QY_QcEditBlindSetting" Codebehind="QcEditBlindSetting.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js"
        type="text/javascript"></script>
    <script src="../../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        var strUrl = "QcEditBlindSetting.aspx";
        var strQcType = "";
        var objOneGrid = null;
        //监测任务管理
        $(document).ready(function () {
            objOneGrid = $("#oneGrid").ligerGrid({
                dataAction: 'server',
                usePager: false,
                pageSize: 5,
                alternatingRow: false,
                checkbox: false,
                enabledEdit: true,
                sortName: "ID",
                width: 430,
                pageSizeOptions: [5, 10, 15, 20],
                height: 330,
                url: strUrl + '?type=getOneGridInfo&strSampleId=' + $("#txtSampleId").val() + "&strQcType=" + $("#txtQcType").val(),
                columns: [
                { display: '监测项目', name: 'ITEM_NAME', align: 'left', width: 180, minWidth: 60 },
                { display: '标准值', name: 'STANDARD_VALUE', align: 'left', width: 100,
                    editor: {
                        type: 'text'
                    }, render: function (record) {
                        return record.STANDARD_VALUE;
                    }
                },
                { display: '不确定度', name: 'UNCETAINTY', align: 'left', width: 100,
                    editor: {
                        type: 'text'
                    }, render: function (record) {
                        return record.UNCETAINTY;
                    }
                }
                ],
                onAfterEdit: f_onAfterEdit
            });
        });
        
        function f_onAfterEdit(e) {
            //保存数据
            var columnname = "";
            columnname = e.column.columnname;
            var fill_id = e.record["ID"];
            var value = e.value;

            if (e.record["__status"] != "nochanged") {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "/QcUpdate",
                    data: "{'strID':'" + fill_id + "','strUpdateCell':'" + columnname + "','strUpdateCellValue':'" + value + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        
                    }
                });
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--质控监测项目设置-->
    <div class="l-form" style="text-align: center">
        <div style="float: left;" class="l-group l-group-hasicon">
            <img src="../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" /><span>现场密码设置
        </div>
        <ul>
            <li>
                <div id="oneGrid">
                </div>
            </li>
        </ul>
    </div>
    <asp:HiddenField ID="txtQcType" runat="server" />
    <asp:HiddenField ID="txtSampleId" runat="server" />
    </form>
</body>
</html>
