<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Base_Search_TrackInfo" Codebehind="TrackInfo.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js"
        type="text/javascript"></script>
    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery.form.js" type="text/javascript"></script>

    <script type="text/javascript">
        var strUrl = "TotalSearch.aspx";
        var objOneGrid = null;
        var strContractID = "";
        var strTaskID = "";

        //监测任务管理
        $(document).ready(function () {
            strContractID = getQueryString("ContractID");
            strTaskID = getQueryString("TaskID");

            objOneGrid = $("#oneGrid").ligerGrid({
                dataAction: 'server',
                usePager: false,
                alternatingRow: false,
                checkbox: false,
                enabledEdit: false,
                onRClickToSelect: false,
                sortName: "ID",
                width: 720,
                height: 450,
                url: strUrl + '?type=getTotalFlowList&ContractID=' + strContractID + '&TaskID=' + strTaskID,
                columns: [
                    { display: '环节名称', name: 'WF_LINK', align: 'left', width: 140, minWidth: 60 },
                    { display: '环节状态', name: 'WF_STATUS', align: 'left', width: 100, minWidth: 60, render: function (record) {
                        if (record.WF_STATUS == "2B")
                            return "已办";
                        else
                            return "<a style='color:red;'>待办</a>";
                    }
                    },
                    { display: '办理人', name: 'WF_USER', align: 'left', width: 200, minWidth: 60 },
                    { display: '办理时间', name: 'WF_TIME', align: 'left', width: 100, minWidth: 60 }
                ]
            });

        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="oneGrid">
        </div>
    </form>
</body>
</html>
