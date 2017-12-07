var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var maingrid = null, DeptComBoxItem = null;
var strSrhDept_ID = "";
$(document).ready(function () {
    $("#layout1").ligerLayout({ topHeight: 30, leftWidth: "100%", rightWidth: "0%", allowLeftCollapse: false, allowRightCollapse: false, height: "100%" });
    maingrid = $("#maingrid4").ligerGrid({
        columns: [
                { display: '用户名称', name: 'REAL_NAME', align: 'left', width: 100, minWidth: 60 },
                { display: '用户职务', name: 'POST_NAME', align: 'left', width: 180, minWidth: 120, render: function (record) {
                    return getPostName(record.ID);
                }
                },
                { display: '所属部门', name: 'DEPT_NAME', align: 'left', width: 180, minWidth: 140, render: function (record) {
                    return getDeptName(record.ID);
                }
                }
                ],
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        url: '../../../../Sys/General/UserList.aspx?type=getData',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true
    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //初始化部门的ComBox
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../../Sys/Duty/DutySettingPage.aspx/GetDeptItems",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                DeptComBoxItem = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！');
        }
    });

    $("#txtDept").ligerComboBox({ data: DeptComBoxItem, width: 220, valueFieldID: 'DeptBox', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false, onSelected: function (newvalue) {
        strSrhDept_ID = newvalue;
        var surl = '../../../../Sys/General/UserList.aspx?type=getData&strSrhDept_ID=' + strSrhDept_ID;
        maingrid.set('url',surl);
    }
    });

    //获取职位信息
    function getPostName(strUserID) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../../../../Sys/General/UserList.aspx/getPostName",
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
            url: "../../../../Sys/General/UserList.aspx/getDeptName",
            data: "{'strValue':'" + strUserID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }

})

function f_select() {
    return maingrid.getSelectedRow();
}