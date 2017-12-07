<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_QY_QcInfo" Codebehind="QcInfo.aspx.cs" %>

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
        var strUrl = "QcInfo.aspx";
        var objOneGrid = null;
        var strSubTaskID = "", strResultID = "", strItemID = "", strMark = "";

        //监测任务管理
        $(document).ready(function () {
            strSubTaskID = getQueryString("strSubTaskID");
            strResultID = getQueryString("strResultID");
            strItemID = getQueryString("strItemID");
            strMark = getQueryString("strMark");
            
            objOneGrid = $("#oneGrid").ligerGrid({
                dataAction: 'server',
                usePager: false,
                pageSize: 5,
                alternatingRow: false,
                checkbox: false,
                enabledEdit: false,
                onRClickToSelect: false,
                sortName: "ID",
                width: 760,
                pageSizeOptions: [5, 10, 15, 20],
                height: 450,
                url: strUrl + '?type=getOneGridInfo&strSubTaskID=' + strSubTaskID + '&strResultID=' + strResultID + '&strItemID=' + strItemID + '&strMark=' + strMark,
                columns: [
                    { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 100, minWidth: 60, render: function (record) {
                        var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                        return strItemName;
                    }
                    },
                    { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 120, minWidth: 60 },
                    { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
                        return getQcName(record.QC_TYPE);
                    }
                    },
                    { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 50, minWidth: 60 },
                    { display: '质控结果', name: 'REMARK', align: 'left', width: 320, minWidth: 60 },
                    { display: '是否合格', name: 'IS_OK', align: 'left', width: 50, minWidth: 60, render: function (record) {
                        if (record.IS_OK == "0")
                            return "<span style='color:red'>否</span>";
                        else if (record.IS_OK == "1")
                            return "是";
                        else
                            return "";
                    }
                    }
                ]
            });

        });

        //获取质控手段名称
        function getQcName(strQcId) {
            var strValue = "";
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/getQcName",
                data: "{'strQcId':'" + strQcId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    strValue = data.d;
                }
            });
            return strValue;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="oneGrid">
        </div>
    </form>
</body>
</html>
