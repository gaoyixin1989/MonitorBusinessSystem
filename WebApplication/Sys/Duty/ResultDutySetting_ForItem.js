// Create by 潘德军 2013.6.28  "分析岗位职责--按项目"功能
var manager,itemUserGrid;
var menu;
var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var gridItemSelectId = "", gridSelectUserId = [], isAuDefault = "false", isHave = false;
var strDutytype = "duty_analyse";
var strPost_Dept = "";
var struserItems = "";
var vSelectedData = null, vItemData = null, DeptComBoxItem = null;

$(document).ready(function () {
    $("#layout1").ligerLayout({ leftWidth: "30%", rightWidth: "70%", allowLeftCollapse: false, allowRightCollapse: false, height: "100%" });

    //right move
    $("#btnRight").click(function () {
        moveright();
    });
    //double click to move left
    $("#listLeft").dblclick(function () {
        moveright();
    });

    //left move 
    $("#btnLeft").click(function () {
        moveleft();
    });

    //double click to move right
    $("#listRight").dblclick(function () {
        moveleft();
    });

    //监测项目grid
    window['g'] =
    manager = $("#maingrid").ligerGrid({
        columns: [
        { display: '监测类型', name: 'MONITOR_NAME', width: 100, align: 'left', isSort: false },
        { display: '监测项目', name: 'ITEM_NAME', width: 200, align: 'left', isSort: false }
        ], width: '100%',
        pageSizeOptions: [10, 15, 20, 50], height: '100%',
        url: 'ResultDutySetting_ForItem.aspx?Action=GetItems',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        title: '',
        toolbar: { items: [
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            var selectedItem = manager.getSelectedRow();
            gridItemSelectId = rowdata.ID;
            SelectUserList();
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    function SelectUserList() {
        itemUserGrid = $("#divItemUserLst").ligerGrid({
            columns: [
                { display: '用户名称', name: 'REAL_NAME', width: 100, minWidth: 60 },
                { display: '用户职务', name: 'POST_NAME', align: 'left', width: 180, minWidth: 60, render: function (record) {
                    return getPostName(record.ID);
                }
                },
                { display: '所属部门', name: 'DEPT_NAME', align: 'left', width: 180, minWidth: 60, render: function (record) {
                    return getDeptName(record.ID);
                }
                },
                { display: '默认负责人', name: 'IF_DEFAULT', width: 100, minWidth: 60, render: function (item) {
                    if (item.IF_DEFAULT == '0') return '<a style=" color:Red">是</a>';
                    return '否';
                }
                },
                { display: '默认协同人', name: 'IF_DEFAULT_EX', width: 100, minWidth: 60, render: function (item) {
                    if (item.IF_DEFAULT_EX == '0') return '<a style=" color:Red">是</a>';
                    return '否';
                }
                }
                ],
            width: '98%', height: '100%',
            url: 'ResultDutySetting_ForItem.aspx?Action=LoadSetUserList&strItemID=' + gridItemSelectId + '&strDutyType=' + strDutytype,
            dataAction: 'server', //服务器排序
            usePager: false,       //服务器分页
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            whenRClickToSelect: true,
            toolbar: { items: [
                                { id: 'defaultAu', text: '设置默认负责人', click: SetDefaultAu, icon: 'bullet_wrench' },
                                { line: true },
                                { id: 'defaultEx', text: '设置默认协同人', click: SetDefaultEx, icon: 'database_wrench' },
                                { line: true },
                                { id: 'useSet', text: '用户选择', click: SetUserList, icon: 'user_add' },
                                { line: true },
                                { id: 'useDel', text: '用户删除', click: DelUser, icon: 'user_delete' }
                ]
            }
        });
    }

    function SetUserList(item) {
        if (gridItemSelectId == "") {
            $.ligerDialog.warn('请选择监测类别！');
            return;
        }
        else {
            SetControlValue();
            //打开设置条件项目的DIV 
            mtardiv = $.ligerDialog.open({ target: $("#targerdiv2"), height: 388, width: 436, top: 10, title: '监测项目用户设置', buttons: [{ text: '确定', onclick: function (item, text) {
                struserItems = "";
                $("#listRight option").each(function () {
                    struserItems += $(this).val() + ";";
                });

                if (struserItems != "") {
                    SaveDivData();

                }
                else {
                    $.ligerDialog.warn('请选择要用户！');
                    return;
                }
            }
            }, { text: '关闭', onclick: function (item, text) {
                itemUserGrid.loadData();
                mtardiv.hide();
            }
            }]
            });
        }
    }

    function SetDefaultAu(item) {
        var rows = itemUserGrid.getCheckedRows();

        if (gridItemSelectId == "") {
            $.ligerDialog.warn('请先选择监测项目！');
            return;
        }

        if (rows.length == 0) {
            $.ligerDialog.warn('请选择用户！');
            return;
        }

        if (rows.length > 1) {
            $.ligerDialog.warn('【默认负责人】必须唯一！');
            return
        }

        var strUSERDUTYIDs = "";
        $(rows).each(function () {
            strUSERDUTYIDs += (strUSERDUTYIDs.length > 0 ? ";" : "") + this.USERDUTYID;
        });

        $.ajax({
            cache: false,
            type: "POST",
            url: "ResultDutySetting_ForItem.aspx/SetDefaultAu",
            data: "{'strUSERDUTYIDs':'" + strUSERDUTYIDs + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    itemUserGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('操作失败');
                }
            }
        });
    }
    function SetDefaultEx(item) {
        var rows = itemUserGrid.getCheckedRows();

        if (gridItemSelectId == "") {
            $.ligerDialog.warn('请先选择监测项目！');
            return;
        }

        if (rows.length == 0) {
            $.ligerDialog.warn('请选择用户！');
            return;
        }

        var strUSERDUTYIDs = "";
        $(rows).each(function () {
            strUSERDUTYIDs += (strUSERDUTYIDs.length > 0 ? ";" : "") + this.USERDUTYID;
        });

        $.ajax({
            cache: false,
            type: "POST",
            url: "ResultDutySetting_ForItem.aspx/SetDefaultEx",
            data: "{'strUSERDUTYIDs':'" + strUSERDUTYIDs + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    itemUserGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('操作失败');
                }
            }
        });
    }

    function DelUser(item) {
        var rows = itemUserGrid.getCheckedRows();

        var strUSERDUTYIDs = "";
        $(rows).each(function () {
            strUSERDUTYIDs += (strUSERDUTYIDs.length > 0 ? ";" : "") + this.USERDUTYID;
        });

        if (rows.length == 0) {
            $.ligerDialog.warn('请选择要进行删除的用户！');
            return;
        }
        else {
            if (gridItemSelectId == "") {
                $.ligerDialog.warn('请先选择监测项目！');
                return;
            }
            else {
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "ResultDutySetting_ForItem.aspx/DelUser",
                    data: "{'strUSERDUTYIDs':'" + strUSERDUTYIDs + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            itemUserGrid.loadData();
                        }
                        else {
                            $.ligerDialog.warn('删除失败');
                        }
                    }
                });
            }
        }
    }
});

//监测项目grid 的查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchItemForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "监测类型", name: "SrhMONITOR_ID", newline: true, type: "select", comboboxName: "SrhMONITOR_TYPE_ID", resize: false, group: "查询信息", groupicon: groupicon, options: { valueFieldID: "SrhMONITOR_ID", url: "../../channels/base/MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} },
                      { display: "监测项目", name: "SrhITEM_NAME", newline: true, type: "text", validate: { required: true, minlength: 3} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 350, height: 200, top: 90, title: "查询监测项目",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhMONITOR_ID = $("#SrhMONITOR_ID").val();
        var SrhITEM_NAME = escape($("#SrhITEM_NAME").val());

        manager.set('url', "ResultDutySetting_ForItem.aspx?Action=GetItems&SrhMONITOR_ID=" + SrhMONITOR_ID + "&SrhITEM_NAME=" + SrhITEM_NAME);
    }

    //监测项目grid 的查询对话框元素的值 清除
    function clearSearchDialogValue() {
        $("#SrhMONITOR_ID").val("");
        $("#SrhMONITOR_TYPE_ID").val("");
        $("#SrhITEM_NAME").val("");
    }
}

//获取职位信息
function getPostName(strUserID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../General/UserList.aspx/getPostName",
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
        url: "../General/UserList.aspx/getDeptName",
        data: "{'strValue':'" + strUserID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

function SetControlValue() {
    $("#listLeft option").remove();
    $("#listRight option").remove();
    GeUserDeptItems();
    GetSubUserItems(strPost_Dept);
    GetSelectUserItems();
    $("#Dept").ligerGetComboBoxManager().setValue("");
}

function moveright() {
    //数据option选中的数据集合赋值给变量vSelect
    var vSelect = $("#listLeft option:selected");
    vSelect.clone().appendTo("#listRight");
    vSelect.remove();
}
function moveleft() {
    var vSelect = $("#listRight option:selected");
    vSelect.clone().appendTo("#listLeft");
    vSelect.remove();
}

//初始化部门的ComBox
function GeUserDeptItems() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "DutySettingPage.aspx/GetDeptItems",
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

    $("#Dept").ligerComboBox({ data: DeptComBoxItem, width: 120, valueFieldID: 'DeptBox', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false, onSelected: function (newvalue) {
        seletComboxValue = newvalue;
        //初始化listBox
        $("#listLeft option").remove();
        //$("#listRight option").remove();
        GetSubUserItems(newvalue);
        //GetSelectUserItems();
    }
    });
    $("#pageloading2").hide();
}

//初始化左侧ListBox
function GetSubUserItems(strDept) {
    vItemData = null;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "ResultDutySetting_ForItem.aspx/GetSubUserItems",
        data: "{'strPost_Dept':'" + strDept + "','strItemID':'" + gridItemSelectId + "','strDutyType':'" + strDutytype + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                vItemData = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！');
        }
    });

    //bind data
    var vlist = "";
    //遍历json数据,获取监测项目列表
    jQuery.each(vItemData, function (i, n) {
        vlist += "<option value=" + vItemData[i].ID + ">" + vItemData[i].REAL_NAME + "</option>";
    });
    //绑定数据到listLeft
    $("#listLeft").append(vlist);

    $("#pageloading2").hide();
}

function GetSelectUserItems() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "ResultDutySetting_ForItem.aspx/GetSelectUserItems",
        data: "{'strItemID':'" + gridItemSelectId + "','strDutyType':'" + strDutytype + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                vSelectedData = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！');
        }
    });

    var vItemlist = "";
    jQuery.each(vSelectedData, function (i, n) {
        vItemlist += "<option value=" + vSelectedData[i].ID + ">" + vSelectedData[i].REAL_NAME + "</option>";
    });
    if (vItemlist.length > 0) {
        $("#listRight").append(vItemlist);
    }
}

function SaveDivData() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "ResultDutySetting_ForItem.aspx/InsertSelectedUser",
        data: "{'strUserId':'" + struserItems + "','strItemID':'" + gridItemSelectId + "','strDutyType':'" + strDutytype + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == true) {
                $.ligerDialog.success('数据保存成功！');
//                SelectUserList();
                //                SetControlValue();
                itemUserGrid.loadData();
                mtardiv.hide();
            }
            else {
                $.ligerDialog.warn('数据操作失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！');
        }
    });
}