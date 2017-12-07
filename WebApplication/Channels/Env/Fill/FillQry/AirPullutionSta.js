
/// 全市空气首要污染物统计报表
/// 创建人：魏林
/// 创建时间：2013-09-03

var objGrid = null;
var gridJSON = null;
var url = "AirPullutionSta.aspx";
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

Date.prototype.dateAdd = function (strInterval, Number) {
    var dtTmp = this;
    switch (strInterval) {
        case 's': return new Date(Date.parse(dtTmp) + (1000 * Number));
        case 'n': return new Date(Date.parse(dtTmp) + (60000 * Number));
        case 'h': return new Date(Date.parse(dtTmp) + (3600000 * Number));
        case 'd': return new Date(Date.parse(dtTmp) + (86400000 * Number));
        case 'w': return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));
        case 'q': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number * 3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'm': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'y': return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
    }
}
//取日期所在月的最大天数
Date.prototype.MaxDayOfDate = function () {
    var result = this.dateAdd('m', 1).dateAdd('d', -this.getDate()).getDate();
    return result;
}

$(document).ready(function () {
    //创建查询表单结构
    $("#searchForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "时间范围", name: "dateStart", newline: false, type: "date", format: "yyyy-MM-dd", showTime: "false", group: "查询条件", groupicon: groupicon },
                      { display: "至", name: "dateEnd", newline: false, type: "date", format: "yyyy-MM-dd", showTime: "false" }
                    ]
    });

    //默认值 
    $.ligerui.get("dateStart").setValue(new Date().getFullYear() + "-" + (new Date().getMonth() + 1) + "-01");
    $.ligerui.get("dateEnd").setValue(new Date().getFullYear() + "-" + (new Date().getMonth() + 1) + "-" + new Date().MaxDayOfDate());

    //构建统计表格
    objGrid = $("#grid").ligerGrid({
        title: '全市首要污染物统计',
        dataAction: 'server',
        usePager: true,
        pageSize: 31,
        pageSizeOptions: [11, 21, 31, 41],
        alternatingRow: true,
        checkbox: false,
        enabledEdit: false,
        width: '100%',
        height: '100%',
        toolbar: { items: [
                { text: '查询', click: showSearch, icon: 'search' }
                ]
        }
    });

    getData();
});

//弹出查询窗口
var searchDialog = null;
function showSearch() {
    if (searchDialog) {
        searchDialog.show();
    } else {
        
        //弹出窗口
        searchDialog = $.ligerDialog.open({
            target: $("#searchDiv"),
            width: 650, height: 220, top: 90, title: "查询",
            buttons: [
                  { text: '确定', onclick: function () { getData(); searchDialog.hide(); } },
                  { text: '取消', onclick: function () { searchDialog.hide(); } }
                  ]
        });
    }
}

//获取数据
function getData() {
    var startdate = $("#dateStart").val();
    var enddate = $("#dateEnd").val();
    $.ajax({
        url: url,
        data: "action=GetData&startdate=" + startdate + "&enddate=" + enddate,
        type: "post",
        dataType: "json",
        async: true,
        cache: false,
        beforeSend: function () {
            $.ligerDialog.waitting('数据加载中,请稍候...');
        },
        complete: function () {
            $.ligerDialog.closeWaitting();
        },
        success: function (json) {
            if (parseInt(json.Total) > 0) {
                gridJSON = json;

                //构建表格列
                //固定的列
                var columnsArr = [];

                //添加所有动态的列
                $.each(json.UnSureColumns, function (i, n) {
                    columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth)+50, minWidth: 60, align: "center" });
                });

                objGrid.set("columns", columnsArr);
                objGrid.set("data", json);

                //隐藏不需要显示的列
                //objGrid.toggleCol("ID");
                //objGrid.toggleCol("SECTION_ID");
                //objGrid.toggleCol("POINT_ID");

                $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
            }
            else {
                objGrid.set("data", json);
            }
        }
    });

}