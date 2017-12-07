var EmployeStatusItems = null, DeptItems = null, PostDutyItems = null;
var strEmployeID = "";
$(document).ready(function () {

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=EmployeStatus",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                EmployeStatusItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    //岗位
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=PostDuty",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                PostDutyItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=dept",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                DeptItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false });
    $("#dropDept").ligerComboBox({ data: DeptItems, width: 200, valueFieldID: 'dropDept_ID', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false, onSelected: function (value, text) {
        maingrid.set('url', '../Employe/EmployeHander.ashx?action=GetEmployeInfor&srhDepart=' + value)
    }
    });

    window['g1'] = maingrid = $("#divEmployeInfo").ligerGrid({
        columns: [
            { display: '员工编号', name: 'EMPLOYE_CODE', align: 'left', width: 160, minWidth: 60 },
            { display: '员工姓名', name: 'EMPLOYE_NAME', width: 100, minWidth: 60 },
            { display: '所在部门', name: 'DEPART', width: 100, minWidth: 60, render: function (items) {
                for (var i = 0; i < DeptItems.length; i++) {
                    if (DeptItems[i].DICT_CODE == items.DEPART) {
                        return DeptItems[i].DICT_TEXT;
                    }
                }
                return items.DEPART;
            }
            },
            { display: '所在岗位', name: 'POSITION', width: 100, minWidth: 60, render: function (items) {
                for (var i = 0; i < PostDutyItems.length; i++) {
                    if (PostDutyItems[i].DICT_CODE == items.POSITION) {
                        return PostDutyItems[i].DICT_TEXT;
                    }
                }
                return items.POSITION;
            }
            },
            { display: '在职状态', name: 'POST_STATUS', width: 100, minWidth: 60, render: function (items) {
                for (var i = 0; i < EmployeStatusItems.length; i++) {
                    if (EmployeStatusItems[i].DICT_CODE == items.POST_STATUS) {
                        return EmployeStatusItems[i].DICT_TEXT;
                    }
                }
                return items.POST_STATUS;
            }
            }
            ],
        title: '人员档案列表',
        width: '100%', height: '100%',
        pageSizeOptions: [5, 10, 20],
        url: "../Employe/EmployeHander.ashx?action=GetEmployeInfor",
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        rownumbers: true,
        whenRClickToSelect: true,

        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
})

function f_select() {
    return maingrid.getSelectedRows();
}