
//@ Create By Castle(胡方扬) 2012-11-12
//@ Company: Comleader(珠海高凌)
//@ 功能：用户采样岗位职责设定
//@ *修改人（时间）:
//@ *修改原因：
var mdivsamp2 = null, sampdiv2 = null;
var usergrid = null;
var struserItems = "";
var struserId = "", seletComboxValue = "";
var strTypeSamp = "duty_sampling";
var confrimtext = "", removeUserId = "";
var strPost_Dept="";
var gridMonitSelectId = "", gridSelectUserId = [], isAuDefault = "false", isHave = false;
var srtDefaultAu = "";
$(document).ready(function () {


    $("#layout1").ligerLayout({  leftWidth: "30%", rightWidth: "70%", allowLeftCollapse: false, allowRightCollapse: false, height: "100%" });
    
    window['g1'] = mdivsamp2 = $("#divsamp2").ligerGrid({
        columns: [
        //                { display: 'ID', name: 'ID', align: 'left', width: 100, minWidth: 60, hide: 'true' },
                {display: '监测类别名称', name: 'MONITOR_TYPE_NAME', align: 'left', width: 100, minWidth: 60 }
                ],
        //        title: '监测类别列表',
        width: '100%', height: '100%',
        pageSizeOptions: [5, 10],
        url: 'DutySettingPage.aspx?Action=GetMonitors',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        //        isScroll: false, //会影响样式
        rownumbers: true,
        sortName: "ID",
        whenRClickToSelect: true,
        onCheckRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            gridMonitSelectId = rowindex.ID;
            SelectUserList();

        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $("#pageloading2").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    //初始化加载
    SelectUserList();
    //填充平行布局
    gridDraggable(g1, g2);

    function SetUserList(item) {
        if (gridMonitSelectId == "") {
            $.ligerDialog.warn('请选择监测类别！');
            return;
        }
        else {
            setUserItem();
        }

    }

    function setUserItem() {


        SetControlValue();
        //打开设置条件项目的DIV 
        mtardiv = $.ligerDialog.open({ target: $("#targerdiv2"), height: 388, width: 436, top: 10, title: '监测项目用户设置', buttons: [{ text: '确定', onclick: function (item, text) {
            GetItemsValue();
            if (struserItems != "" || moveid != "") {
                SaveDivData();
            }
            else {
                $.ligerDialog.warn('请选择要用户！');
                return;
            }
        }
        }, { text: '关闭', onclick: function (item, text) {
            usergrid.loadData();
            mtardiv.hide();
        }
        }]
        });

    }

    function SelectUserList() {
        window['g2'] = usergrid = $("#divuserlist2").ligerGrid({
            columns: [
            //                { display: 'ID', name: 'ID', align: 'left', width: 40, minWidth: 60, hide: 'true' },
                {display: '用户名称', name: 'REAL_NAME', width: 100, minWidth: 60 },
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
            //            title: '用户列表【岗位职责设定】',
            width: '98%',
            height: '100%',
            pageSizeOptions: [5, 10],
            url: 'DutySettingPage.aspx?Action=LoadSetUserList&strMonitor=' + gridMonitSelectId + '&strType=' + strTypeSamp + '',
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 10,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            whenRClickToSelect: true,
            toolbar: { items: [
                { id: 'defaultAu', text: '设置默认负责人', click: SetDefault, icon: 'bullet_wrench' },
                { line: true },
                { id: 'defaultEx', text: '设置默认协同人', click: SetDefault, icon: 'database_wrench' },
                  { line: true },
                { id: 'useSet', text: '用户选择', click: SetUserList, icon: 'user_add' },
                  { line: true },
                { id: 'useDel', text: '用户删除', click: SetDefault, icon: 'user_delete' }
                ]
            },
            isChecked: f_isChecked,
            onCheckRow: f_onCheckRow,
            onCheckAllRow: f_onCheckAllRow,
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }

        });
        $("#pageloading2").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    }

    function f_onCheckAllRow(checked) {
        for (var rowid in this.records) {
            if (checked)
                addCheckedCustomer(this.records[rowid]['ID']);
            else
                removeCheckedCustomer(this.records[rowid]['ID']);
        }
    }

    /*
    该例子实现 表单分页多选
    即利用onCheckRow将选中的行记忆下来，并利用isChecked将记忆下来的行初始化选中
    */
    var checkedCustomer = [];


    function findCheckedCustomer(ID) {
        for (var i = 0; i < checkedCustomer.length; i++) {
            if (checkedCustomer[i] == ID) return i;
        }
        return -1;
    }
    function addCheckedCustomer(ID) {
        if (findCheckedCustomer(ID) == -1)
            checkedCustomer.push(ID);
    }
    function removeCheckedCustomer(ID) {
        var i = findCheckedCustomer(ID);
        removeUserId += ID + ";";
        if (i == -1) return;
        checkedCustomer.splice(i, 1);
    }
    //判断符合条件的进行选中
    function f_isChecked(rowdata) {
        if (rowdata.IF_DEFAULT == "0" || rowdata.IF_DEFAULT_EX == "0") {
            isHave = true;
            return true;
        }
        return false;
    }
    function f_onCheckRow(checked, data) {
        if (checked) addCheckedCustomer(data.ID);
        else removeCheckedCustomer(data.ID);
    }
    function f_getChecked() {
        alert(checkedCustomer.join(','));
    }



    function SetDefault(item) {
        switch (item.id) {
            case 'defaultAu':
                var rows = usergrid.getCheckedRows();

                var strUserID = "";
                $(rows).each(function () {
                    strUserID += (strUserID.length > 0 ? ";" : "") + this.ID;
                });

                if (rows.length == 0 && isHave == false) {
                    $.ligerDialog.warn('请先选择要【默认负责人】！');
                    return;
                }

                if (rows.length > 1) {
                    $.ligerDialog.warn('【默认负责人】必须唯一！');
                    return
                }
                else {
                    if (gridMonitSelectId == "") {
                        $.ligerDialog.warn('请先选择监测类别！');
                        return;
                    }
                    else {
                        isAuDefault = "true";
                        var defAuSum = 0;
                        for (var i = 0; i < rows.length; i++) {
                            if (rows[i].IF_DEFAULT == "0") {
                                defAuSum++;
                            }
                        }
                        if (rows.length == 0 && isHave && defAuSum == 0) {
                            confrimtext = "确认清空已经设置的默认负责人吗？\r";
                            if (removeUserId != "") {
                                //                                removeUserId = removeUserId.substring(0, removeUserId.length - 1);
                                $.ligerDialog.confirm(confrimtext, function (result) {
                                    if (result == true) {
                                        deleteData();
                                    }
                                    else {
                                        return;
                                    }
                                });
                            }
                        }
                        if (rows.length > 0) {
                            SaveSampSetData(strUserID, isAuDefault);
                        }
                    }
                }

                break;
            case 'defaultEx':
                var rows = usergrid.getCheckedRows();

                var strUserID = "";
                $(rows).each(function () {
                    strUserID += (strUserID.length > 0 ? ";" : "") + this.ID;
                });

                if (rows.length == 0 && isHave == false) {
                    $.ligerDialog.warn('请先选择要【默认协同人】！');
                    return;
                }
                else {
                    if (gridMonitSelectId == "") {
                        $.ligerDialog.warn('请先选择监测类别！');
                        return;
                    }
                    else {
                        isAuDefault = "false";
                        var defExSum = 0;
                        for (var i = 0; i < rows.length; i++) {
                            if (rows[i].IF_DEFAULT_EX == "0")
                                defExSum++;
                        }
                        if (rows.length == 0 && isHave && defExSum == 0) {
                            confrimtext = "确认清空已经设置的默认协同人吗？\r";
                            if (removeUserId != "") {
                                //                                removeUserId = removeUserId.substring(0, removeUserId.length - 1);
                                $.ligerDialog.confirm(confrimtext, function (result) {
                                    if (result == true) {
                                        deleteData();
                                    }
                                    else {
                                        return;
                                    }
                                });
                            }
                        }
                        if (rows.length > 0) {
                            SaveSampSetData(strUserID, isAuDefault);
                        }
                    }
                }
                break;
            case 'useDel':
                var rows = usergrid.getCheckedRows();

                var strUserID = "";
                $(rows).each(function () {
                    strUserID += (strUserID.length > 0 ? ";" : "") + this.ID;
                });

                if (rows.length == 0) {
                    $.ligerDialog.warn('请选择要进行删除的用户！');
                    return;
                }
                else {
                    if (gridMonitSelectId == "") {
                        $.ligerDialog.warn('请先选择监测类别！');
                        return;
                    }
                    else {
                        struserItems = "";
                        moveid = "";
                        moveid = strUserID;
                        SaveDivData();
                    }
                }
                break;
            default:
                break;
        }

    }

    function deleteData() {
        $.ajax({
            cache: false,
            type: "POST",
            url: "DutySettingPage.aspx/ClearSampUserDuty",
            data: "{'strUserId':'" + removeUserId + "','strMonitor':'" + gridMonitSelectId + "','strDutyType':'" + strTypeSamp + "','isAuDefault':'" + isAuDefault + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == true) {
                    removeUserId = "";
                    SelectUserList();
                    $.ligerDialog.success('数据删除成功!');
                }
                else {
                    removeUserId = "";
                    SelectUserList();
                    $.ligerDialog.warn('数据删除失败!');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求数据失败!');
                removeUserId = "";
            }
        });

    }

    function SaveSampSetData(strUser, strDefaultAu) {

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "DutySettingPage.aspx/EditSampling",
            data: "{'strMonitor':'" + gridMonitSelectId + "','strUserId':'" + strUser + "','strDutyType':'" + strTypeSamp + "','isAuDefault':'" + strDefaultAu + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == "true") {
                    SelectUserList();
                    $.ligerDialog.success('数据操作成功！');
                }
                else {
                    SelectUserList();
                    $.ligerDialog.warn('数据操作失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });

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


    //DIV 设置条件项目监测项目部分

    var vSelectedData = null, vItemData = null, DeptComBoxItem = null;
    var moveid = "";

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
            $("#listRight option").remove();
            GetSubUserItems(newvalue);
            GetSelectUserItems();
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
            url: "DutySettingPage.aspx/GetSubUserItems",
            data: "{'strPost_Dept':'" + strDept + "','strMonitorId':'" + gridMonitSelectId + "','strDutyType':'" + strTypeSamp + "'}",
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

    //right move
    $("#btnRight").click(function () {
        moveright();
    });
    //double click to move left
    $("#listLeft").dblclick(function () {
        //克隆数据添加到listRight中
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

    function SelectItemsRange() {
        $(this).attr("selected", "selected");
    }

    function moveright() {
        //数据option选中的数据集合赋值给变量vSelect
        var isExist = false;
        var vSelect = $("#listLeft option:selected");
        //克隆数据添加到listRight中
        if ($("#listRight option").length > 0) {
            $("#listRight option").each(function () {
                if ($(this).val() == $("#listLeft option:selected").val()) {
                    $.ligerDialog.warn('所选数据已存在！');
                    return isExist = false;
                }
                else {
                    isExist = true;
                }
            });
        }
        else {
            isExist = true;
        }
        if (isExist) {
            vSelect.clone().appendTo("#listRight");
            vSelect.remove();
        }
    }
    function moveleft() {
        moveid = "";
        var vSelect = $("#listRight option:selected");
        if (vSelect.length > 0) {
            for (var i = 0; i < vSelect.length; i++) {
                moveid += vSelect[i].value + ";";
            }
        }
        vSelect.clone().appendTo("#listLeft");
        vSelect.remove();
    }
    function GetSelectUserItems() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "DutySettingPage.aspx/GetSelectUserItems",
            data: "{'strMonitorId':'" + gridMonitSelectId + "','strDutyType':'" + strTypeSamp + "'}",
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



    function SetControlValue() {
        $("#listLeft option").remove();
        $("#listRight option").remove();
        GeUserDeptItems();
        GetSubUserItems(strPost_Dept);
        GetSelectUserItems();
        $("#Dept").ligerGetComboBoxManager().setValue("");
    }

    function GetItemsValue() {

        struserItems = "";
        $("#listRight option").each(function () {
            struserItems += $(this).val() + ";";
        });
    }
    function SaveDivData() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            //url: "EvaluationTapSetting.aspx" + Methold + "",
            url: "DutySettingPage.aspx/InsertSelectedUser",
            data: "{'strUserId':'" + struserItems + "','strMonitor':'" + gridMonitSelectId + "','strDutyType':'" + strTypeSamp + "','strMoveUserId':'" + moveid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == true) {
                    $.ligerDialog.success('数据保存成功！');
                    SelectUserList();
                    SetControlValue();
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
});