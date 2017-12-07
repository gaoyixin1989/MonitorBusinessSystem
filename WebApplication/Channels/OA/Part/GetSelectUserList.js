//获取选择用户列表
//创建人：胡方扬
//创建时间：2013-02-05
var DeptItems = null, maingrid=null
//用户选择列表
$(document).ready(function () {

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
    //获取职位信息
    function getPostName(strUserID) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../../../Sys/General/UserList.aspx/getPostName",
            data: "{'strValue':'" + strUserID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }

    //获取部门信息
    function getDeptName(strUserID) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../../../Sys/General/UserList.aspx/getDeptName",
            data: "{'strValue':'" + strUserID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false });
    $("#dropDept").ligerComboBox({ data: DeptItems, width: 200, valueFieldID: 'dropDept_ID', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false, onSelected: function (value, text) {
        maingrid.set('url', 'PartHandler.ashx?action=GetUnionUserInfor&strDept='+value)
    } });

    window['g1'] = maingrid = $("#divEmployeInfo").ligerGrid({
        columns: [
            { display: '姓名', name: 'REAL_NAME', width: 100, minWidth: 60 },
            { display: '所在部门', name: 'POST_NAME', width: 100, minWidth: 60, render: function (items) {
                return getDeptName(items.ID);
            }
            },
            { display: '职务', name: 'DEPT_NAME', width: 100, minWidth: 60, render: function (items) {
                return getPostName(items.ID);
            }
            }
            ],
        title: '用户列表',
        width: '100%', height: '100%',
        pageSizeOptions: [5, 10, 20],
        url: "PartHandler.ashx?action=GetUnionUserInfor",
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        rownumbers: true,
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
})

function f_select() {
    return maingrid.getSelectedRows();
}