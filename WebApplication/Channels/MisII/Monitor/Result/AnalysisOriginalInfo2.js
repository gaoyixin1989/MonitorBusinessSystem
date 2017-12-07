// Create by 魏林 2014.01.21  "数据原始记录信息"功能
var selectTabId = "0";

$(document).ready(function () {
    $("#navtab1").ligerTab({ onAfterSelectTabItem: function (tabid) {
        var tab = $("#navtab1").ligerGetTabManager();
        var selecttabindex = tab.getSelectedTabItemID();
        changeTab(selecttabindex);
    }
    });
    //Tab标签切换事件
    function changeTab(tabid) {
        switch (tabid) {
            case "tabitem1":
                selectTabId = "0";
                break;
            case "tabitem2":
                selectTabId = "1";
                break;
        }
    }


});
