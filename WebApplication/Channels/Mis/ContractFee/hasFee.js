// Create by 潘德军 2012.12.19  "委托书未缴费"功能

var hasgrid;
var menu;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    $("#layout1").ligerLayout({ leftWidth: 170, height: '100%' });

    //设置菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menupay', text: '未缴费', click: setNotPay, icon: 'modify' }
            ]
    });

    //grid
    window['g'] =
    hasgrid = $("#maingrid").ligerGrid({
        columns: [
        { display: '项目名称', name: 'PROJECT_NAME', width: 300, align: 'left', isSort: false, render: function (record) {
            return getProjectName(record.CONTRACT_ID);
        }
        },
        { display: '委托单号', name: 'CONTRACT_CODE', width: 150, align: 'left', isSort: false, render: function (record) {
            return getContractCode(record.CONTRACT_ID);
        }
        },
        { display: '委托单位', name: 'CLIENT_COMPANY_ID', width: 300, align: 'left', isSort: false, render: function (record) {
            return getCompanyNameForFee(record.CONTRACT_ID);
        }
        },
        { display: '委托年度', name: 'CONTRACT_YEAR', width: 80, align: 'left', isSort: false, render: function (record) {
            return getContractYear(record.CONTRACT_ID);
        }
        },
        { display: '委托类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', isSort: false, render: function (record) {
            return getContractType(record.CONTRACT_ID);
        }
        },
        { display: '监测类型', name: 'TEST_TYPES', width: 150, align: 'left', isSort: false, render: function (record) {
            return getTestTypes(record.CONTRACT_ID);
        }
        },
        { display: '费用（元）', name: 'INCOME', width: 100, align: 'left', isSort: false },
        { display: '缴费时间', name: 'REMARK5', width: 100, align: 'left', isSort: false}
        ], width: '100%', height: '100%', pageSizeOptions: [10, 15, 20, 50],
        url: 'hasFee.aspx?type=GetFee',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'pay', text: '未缴费', click: setNotPay, icon: 'modify' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

//未缴费
function setNotPay() {
    if (hasgrid.getSelectedRow() == null) {
        $.ligerDialog.warn('请选择一条记录进行未缴费操作！');
        return;
    }
    var strValue = hasgrid.getSelectedRow().ID;
    $.ajax({
        cache: false,
        type: "POST",
        url: "hasFee.aspx/setNotPay",
        data: "{'strValue':'" + strValue + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                hasgrid.loadData();
                $.ligerDialog.success('未缴费成功！')
            }
            else {
                $.ligerDialog.warn('未缴费失败！');
            }
        }
    });
}

//获取项目名称
function getProjectName(strContractID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "hasFee.aspx/getProjectName",
        data: "{'strValue':'" + strContractID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取委托单号
function getContractCode(strContractID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "hasFee.aspx/getContractCode",
        data: "{'strValue':'" + strContractID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取委托年度
function getContractYear(strContractID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "hasFee.aspx/getContractYear",
        data: "{'strValue':'" + strContractID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取委托单位
function getCompanyNameForFee(strContractID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "hasFee.aspx/getCompanyName",
        data: "{'strValue':'" + strContractID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取委托类型
function getContractType(strContractID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "hasFee.aspx/getContractType",
        data: "{'strValue':'" + strContractID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取监测类型
function getTestTypes(strContractID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "hasFee.aspx/getTestTypes",
        data: "{'strValue':'" + strContractID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
