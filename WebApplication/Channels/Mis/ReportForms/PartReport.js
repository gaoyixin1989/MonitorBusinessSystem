var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    $("#topmenu").ligerMenuBar({ items: [
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
            ]
    });
//设置grid 的弹出查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构

        var mainform = $("#SrhForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40,
            fields: [
                     { display: "物料编码", name: "SEA_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "物料名称", name: "SEA_NAME", newline: false, type: "text" },
                    { display: "领用开始日期", name: "SEA_BEGINDATE", newline: true, type: "date" },
                    { display: "领用截止日期", name: "SEA_ENDDATE", newline: false, type: "date" },
                     { display: "领用人", name: "SEA_USERNAME", newline: true, type: "text" }
                    ]
        });
        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 660, height: 200, top: 90, title: "物料统计查询",
            buttons: [
                  { text: '确定', onclick: function () { search(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SEA_CODE = encodeURI($("#SEA_CODE").val());
        var SEA_NAME = encodeURI($("#SEA_NAME").val());
        var SEA_ENDDATE = encodeURI($("#SEA_ENDDATE").val());
        var SEA_BEGINDATE = encodeURI($("#SEA_BEGINDATE").val());
        var SEA_USERNAME = encodeURI($("#SEA_USERNAME").val());
        if (SEA_BEGINDATE != "" && SEA_ENDDATE == "") {
            $.ligerDialog.warn('请选择截止日期！'); return;
        }
        if (SEA_ENDDATE != "" && SEA_BEGINDATE == "") {
            $.ligerDialog.warn('请选择开始日期！'); return;
        }

        self.location.href = "PartReport.aspx?action=PartCollarInfor&strPartCode=" + SEA_CODE + "&strPartName=" + SEA_NAME + "&strBeginDate=" + SEA_BEGINDATE + "&strEndDate=" + SEA_ENDDATE+"&strReal_Name="+SEA_USERNAME;
        clearSearchDialogValue();
        detailWinSrh.hide();
    }
}

function clearSearchDialogValue() {
    $("#SEA_CODE").val("");
    $("#SEA_NAME").val("");
    $("#SEA_ENDDATE").val("");
    $("#SEA_BEGINDATE").val("");
     $("#SEA_USERNAME").val("");
}
})