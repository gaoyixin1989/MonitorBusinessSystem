
//@ Create By Castle(胡方扬) 2012-11-12
//@ Company: Comleader(珠海高凌)
//@ 功能：用户采样岗位职责设定
//@ *修改人（时间）:
//@ *修改原因：
var nodeIdTemp = "", nodepIdTemp = "";
var mdivsampOther = null, sampdivOther = null;
var usergridOther = null;
var struserItemsOther = "";
var struserIdOther = "", seletComboxValueOther = "";
var strTypeOther = "duty_other";
var confrimtext = "", removeUserIdOther = "";
var strPost_DeptOther = "";
var gridMonitSelectId = "", gridGetDutyId = "",  gridSelectUserId = [], isAuDefault = "false", isHave = false;
var srtDefaultAu = "";

$(document).ready(function () {

    $("#layout1").ligerLayout({ leftWidth: "30%", rightWidth: "70%", allowLeftCollapse: false, allowRightCollapse: false, height: "100%" });

    var setting = {
        check: {
            enable: false
        },
        data: {
            key: {
                name: "Name"
            },
            simpleData: {
                enable: true,
                idKey: "Id",
                pIdKey: "pId"
                //                rootPid: "0"
            }
        },
        view: {
            selectedMulti: false
        },
        edit: {
            enable: true,
            showRenameBtn: false,
            showRemoveBtn: false
        },
        async: {
            enable: true,
            url: "DutySettingOther.aspx",
            otherParam: { "action": "GetMonitorsDutys" }
        },
        callback: {
            onClick: onTreeClick
        }
    };
    //alert(window.screen.height - document.body.clientHeight);
    $("#tree")[0].style.height = window.screen.height - 240;
    var zTreeObj = $.fn.zTree.init($("#tree"), setting);
    $("#pageloading").hide();
    //点击节点事件
    function onTreeClick(e, treeId, treeNode) {
        var zTree = $.fn.zTree.getZTreeObj(treeId);
        //展开节点
        zTree.expandNode(treeNode, null, null, null, true);
        //记录本次选择的节点ID
        nodeIdTemp = treeNode.Id;
        gridGetDutyId = nodeIdTemp;
        nodepIdTemp = treeNode.pId;
        gridMonitSelectId = nodepIdTemp;
        strTypeOther = treeNode.Code;
        SelectUserListOther();
        // alert(nodeIdTemp);
    }


    SelectUserListOther();

    function SetUserList(item) {

        if (gridGetDutyId == "" || nodepIdTemp == "0" || nodepIdTemp == null) {
            $.ligerDialog.warn('请选择岗位职责！');
            return;
        }
        else {
            setUserItem();
        }

    }

    function setUserItem() {
        SetControlValue();
        //打开设置条件项目的DIV 
        mtardiv = $.ligerDialog.open({ target: $("#targerdiv"), height: 388, width: 436, top: 10, title: '监测项目用户设置', buttons: [{ text: '确定', onclick: function (item, text) {
            GetItemsValueOther();
            if (struserItemsOther != "" || moveidOther != "") {
                SaveDivDataOther();
            }
            else {
                $.ligerDialog.warn('请选择要用户！');
                return;
            }
        }
        }, { text: '关闭', onclick: function (item, text) {
            usergridOther.loadData();
            mtardiv.hide();
        }
        }]
        });
    }

    function SelectUserListOther() {
        window['g2'] = usergridOther = $("#divuserlistOther").ligerGrid({
            columns: [
                { display: '用户名称', name: 'REAL_NAME', width: 100, minWidth: 60 },
                { display: '用户职务', name: 'POST_NAME', align: 'left', width: 180, minWidth: 60, render: function (record) {
                    return getPostName(record.ID);
                }
                },
                { display: '所属部门', name: 'DEPT_NAME', align: 'left', width: 180, minWidth: 60, render: function (record) {
                    return getDeptName(record.ID);
                }
                }
                ],
            width: '100%',
            height: '100%',
            pageSizeOptions: [10, 15, 20, 50],
            url: 'DutySettingPage.aspx?Action=LoadSetUserList&strMonitor=' + gridMonitSelectId + '&strType=' + strTypeOther + '',
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 15,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            sortName: "ID",
            whenRClickToSelect: true,
            toolbar: { items: [
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
        $("#pageloading").hide();
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
        removeUserIdOther += ID + ";";
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
            case 'useDel':
                var rows = usergridOther.getCheckedRows();

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
                        struserItemsOther = "";
                        moveidOther = "";
                        moveidOther = strUserID;

                        //                        //获取移除项目的同时 删除ListBox项目
                        //                        
                        //                            var delOption = moveidOther.split(';');
                        //                            if (delOption.length > 0) {
                        //                                for (var i = 0; i < delOption.length; i < 0) {
                        //                                $("#listRight option").each(function () {
                        //                                    if ($(this).val() == delOption[i]) {
                        //                                        $(this).remove();
                        //                                    } 
                        //                                })
                        //                                }
                        //                            }
                        SaveDivDataOther();
                    }
                }
                break;
            default:
                break;
        }

    }

    function deleteDataOther() {
        $.ajax({
            cache: false,
            type: "POST",
            url: "DutySettingPage.aspx/ClearSampUserDuty",
            data: "{'strUserId':'" + removeUserIdOther + "','strMonitor':'" + gridMonitSelectId + "','strDutyType':'" + strTypeOther + "','isAuDefault':'" + isAuDefault + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == true) {
                    removeUserIdOther = "";
                    SelectUserListOther();
                    $.ligerDialog.success('数据删除成功!');
                }
                else {
                    removeUserIdOther = "";
                    SelectUserListOther();
                    $.ligerDialog.warn('数据删除失败!');

                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求数据失败!');
                removeUserIdOther = "";
            }
        });

    }

    function SaveSampSetData(strUser, strDefaultAu) {

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "DutySettingPage.aspx/EditSampling",
            data: "{'strMonitor':'" + gridMonitSelectId + "','strUserId':'" + strUser + "','strDutyType':'" + strTypeOther + "','isAuDefault':'" + strDefaultAu + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == "true") {
                    SelectUserListOther();
                    $.ligerDialog.success('数据操作成功！');
                }
                else {
                    SelectUserListOther();
                    $.ligerDialog.warn('数据操作失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });

    }


    //DIV 设置条件项目监测项目部分

    var vSelectedData = null, vItemData = null, DeptComBoxItem = null;
    var moveidOther = "";

    //初始化部门的ComBox
    function GeUserDeptItemsOther() {
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
            seletComboxValueOther = newvalue;
            //初始化listBox
            $("#listLeft option").remove();
            $("#listRight option").remove();
            GetSubUserItemsOther(newvalue);
            GetSelectUserItemsOther();
        }
        });
        $("#pageloading").hide();
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

    //初始化左侧ListBox
    function GetSubUserItemsOther(strDept) {
        vItemData = null;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "DutySettingPage.aspx/GetSubUserItems",
            data: "{'strPost_Dept':'" + strDept + "','strMonitorId':'" + gridMonitSelectId + "','strDutyType':'" + strTypeOther + "'}",
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

        $("#pageloading").hide();
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
        moveidOther = "";
        var vSelect = $("#listRight option:selected");
        if (vSelect.length > 0) {
            for (var i = 0; i < vSelect.length; i++) {
                moveidOther += vSelect[i].value + ";";
            }
        }
        vSelect.clone().appendTo("#listLeft");
        vSelect.remove();
    }
    function GetSelectUserItemsOther() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "DutySettingPage.aspx/GetSelectUserItems",
            data: "{'strMonitorId':'" + gridMonitSelectId + "','strDutyType':'" + strTypeOther + "'}",
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
        GeUserDeptItemsOther();
        GetSubUserItemsOther(strPost_DeptOther);
        GetSelectUserItemsOther();
        $("#Dept").ligerGetComboBoxManager().setValue("");
    }

    function GetItemsValueOther() {

        struserItemsOther = "";
        $("#listRight option").each(function () {
            struserItemsOther += $(this).val() + ";";
        });
    }
    function SaveDivDataOther() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            //url: "EvaluationTapSetting.aspx" + Methold + "",
            url: "DutySettingPage.aspx/InsertSelectedUser",
            data: "{'strUserId':'" + struserItemsOther + "','strMonitor':'" + gridMonitSelectId + "','strDutyType':'" + strTypeOther + "','strMoveUserId':'" + moveidOther + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == true) {
                    SelectUserListOther();
                    SetControlValue();
                    $.ligerDialog.success('数据保存成功！');
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

