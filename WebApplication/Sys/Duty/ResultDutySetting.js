//@ Create By 潘德军 2013-4-19
//@ Company: Comleader(珠海高凌)
//@ 功能：用户分析岗位职责设定
//@ *修改人（时间）:潘德军 2013-4-19
//@ *修改原因：客户要求，专门为分析室制作一个单独的岗位职责

var manager = null, seletedOption = "", moveid = "", moveAllId = "", moveAuid = "", moveAuAllId = "", moveExAllId = "", moveExid = "";
var mdivsamp = null, sampdiv = null;
var usergrid = null;
var strevmonitorItems = "", strAllmonitorItems = "", strDefAuItems = "", strAllDefAuItems = "", strAllDefExItems = "", strDefExItems = "";
var struserId = "", seletComboxValue = "", selectComboxText = "";
var strTypeAnaly = "duty_analyse";
//var strTypeAnaly = "duty_other_analyse_result";
var confrimtext = "", removeUserId = "";
var isAllMonitor = false, saveAllType = false;
var gridMonitSelectId = "", gridSelectUserId = [], isAuDefault = "false", isHave = false;
var detailWinSrh = null;
var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(function () {
    $("#layout1").ligerLayout({ height: '100%' });

    var bodyHeight = $(".l-layout-center:first").height();

    //DIV 设置条件项目监测项目部分

    var vItemData = null, vSelectedData = null, vnoSelectedData = null, vAuItemData = null, vExItemData = null;
    var isAddNew = false;

    //初始化左侧ListBox
    function GetMonitor() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "UserDutySetting.aspx/GetMonitor",
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

        $("#Monitor").ligerComboBox({ data: vItemData, width: 200, valueFieldID: 'MonitorBox', valueField: 'ID', textField: 'MONITOR_TYPE_NAME', isMultiSelect: false, onSelected: function (newvalue, newtext) {
            if (newvalue != "") {
                seletComboxValue = newvalue;
                selectComboxText = newtext;
                GetMonitorItems(newvalue, struserId);
                GetExistMonitorItems(newvalue, struserId);
                GetAuItems(newvalue, struserId);
                GetExItems(newvalue, struserId);
            }

        }
        });
        $("#pageloading").hide();
    }


    //double click to move left
    $("#listLeft").dblclick(function () {
        seletedOption = $("#listLeft option:selected").val();
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

    //right move
    $("#btnRight").click(function () {
        moveright();
    });

    //double click to move right
    $("#listDefaultAu").dblclick(function () {
        moveDefaultAuL();
    });

    //right move
    $("#btnAuLeft").click(function () {
        moveDefaultAuL();
    });

    //right move
    $("#btnAuRight").click(function () {
        moveDefaultAuR();
    });

    //double click to move right
    $("#listDefaultEx").dblclick(function () {
        moveDefaultExL();
    });

    //right move
    $("#btnExLeft").click(function () {
        moveDefaultExL();
    });

    //right move
    $("#btnExRight").click(function () {
        moveDefaultExR();
    });

    function moveDefaultAuR() {
        //数据option选中的数据集合赋值给变量vSelect
        var isExist = false;
        var vSelect = $("#listRight option:selected");
        //克隆数据添加到listRight中
        if ($("#listDefaultAu option").length > 0) {
            $("#listDefaultAu option").each(function () {
                if ($(this).val() == $("#listRight option:selected").val()) {
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
            vSelect.clone().appendTo("#listDefaultAu");
        }
    }

    function moveDefaultAuL() {
        moveAuid = "";
        moveAuAllId = "";
        var vSelect = $("#listDefaultAu option:selected");
        for (var i = 0; i < vSelect.length; i++) {
            if (vSelect[i].text == '所有(' + selectComboxText + '类)') {
                moveAuAllId += vSelect[i].value + ";";
                isAllMonitor = true;
            }
            else {
                moveid += vSelect[i].value + ";";
            }
        }
        vSelect.remove();
    }

    function moveDefaultExR() {
        //数据option选中的数据集合赋值给变量vSelect
        var isExist = false;
        var vSelect = $("#listRight option:selected");
        //克隆数据添加到listRight中
        if ($("#listDefaultEx option").length > 0) {
            $("#listDefaultEx option").each(function () {
                if ($(this).val() == $("#listRight option:selected").val()) {
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
            vSelect.clone().appendTo("#listDefaultEx");
        }
    }

    function moveDefaultExL() {
        moveExid = "";
        moveExAllId = "";
        var vSelect = $("#listDefaultEx option:selected");


        for (var i = 0; i < vSelect.length; i++) {
            if (vSelect[i].text == '所有(' + selectComboxText + '类)') {
                moveExAllId += vSelect[i].value + ";";
                isAllMonitor = true;
            }
            else {
                moveid += vSelect[i].value + ";";
            }
        }
        vSelect.remove();
    }

    function moveright() {
        var isExist = false;

        //数据option选中的数据集合赋值给变量vSelect
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
        moveAllId = "";
        var vSelect = $("#listRight option:selected");

        if (vSelect.length > 0) {
            for (var i = 0; i < vSelect.length; i++) {
                if (vSelect[i].text == '所有(' + selectComboxText + '类)') {
                    moveAllId += vSelect[i].value + ";";
                    isAllMonitor = true;
                }
                else {
                    moveid += vSelect[i].value + ";";
                }
            }
        }

        vSelect.clone().appendTo("#listLeft")
        //调用级联移除方法
        RealtionMonve(vSelect.val());

        vSelect.remove();
    }

    //级联移除，当已选的监测项目移除时，则关联该项目的默认负责人默认协调人一并移除
    function RealtionMonve(moveLeftId) {
        if ($("#listDefaultAu option").length > 0) {
            $("#listDefaultAu option").each(function () {
                if (moveLeftId == $(this).val()) {
                    $(this).select();
                    if ($(this).text() == '所有(' + selectComboxText + '类)') {
                        moveAuAllId += $(this).val() + ";";
                        isAllMonitor = true;
                    }
                    else {
                        moveAuid += $(this).val() + ";";
                    }
                    $(this).remove();
                }

            })
        }

        if ($("#listDefaultEx option").length > 0) {
            $("#listDefaultEx option").each(function () {
                if (moveLeftId == $(this).val()) {
                    $(this).select();
                    if ($(this).text() == '所有(' + selectComboxText + '类)') {
                        moveExAllId += $(this).val() + ";";
                        isAllMonitor = true;
                    }
                    else {
                        moveExid += $(this).val() + ";";
                    }
                    $(this).remove();
                }

            })
        }
    }


    window['g'] =
    manager = $("#maingrid3").ligerGrid({
        columns: [
                { display: 'ID', name: 'ID', align: 'left', width: 100, minWidth: 60, hide: 'true' },
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
        pageSizeOptions: [5, 10, 20, 50],
        url: 'UserDutySetting.aspx?Action=GetUserList',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        //        isScroll: false,
        rownumbers: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'analyse', text: '分析岗位职责设置', click: itemclickOfToolbar, icon: 'bullet_wrench' },
                { line: true },
                { id: 'srh', text: '查询', click: searchData, icon: 'search' }
                ]
        },
        onCheckRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //查询数据
    function searchData() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构
            var srhform = $("#searchForm");
            srhform.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
                fields: [
                      { display: "部门", name: "SrhDept_ID", newline: true, type: "select", comboboxName: "SrhDept_ID1", group: "查询信息", groupicon: groupicon, options: { valueFieldID: "SrhDept_ID", url: "../../Channels/Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|dept&Order=ORDER_ID"} },
                      { display: "用户姓名", name: "SrhREAL_NAME", newline: true, type: "text" }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 350, height: 200, top: 90, title: "查询用户",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SrhDept_ID1 = $("#SrhDept_ID").val();
            var srhUserName1 = $("#SrhREAL_NAME").val();

            manager.set('url', "UserDutySetting.aspx?Action=GetUserList&strSrhDept_ID=" + SrhDept_ID1 + "&srhUserName=" + escape(srhUserName1));
        }
        //查询对话框元素的值 清除
        function clearSearchDialogValue() {
            $("#SrhDept_ID").val("");
            $("#SrhDept_ID1").val("");
            $("#SrhREAL_NAME").val("");
        }
    }
    

    function itemclickOfToolbar(item) {
        switch (item.id) {
            case 'analyse':
                var isSelected = manager.getSelectedRow();
                if (!isSelected) { $.ligerDialog.warn('请先选择要设置的记录！'); return; }
                var rownu = manager.getCheckedRows().length
                if (rownu > 1) { $.ligerDialog.warn('设置操作只能选择一条记录'); return; }
                if (isSelected) {
                    struserId = isSelected.ID;
                    openSettingPage(strTypeAnaly);
                }
                else {
                    return;
                }
                break;
            default:
                break;
        }
    }

    function isSelected(rowid) {
        var selected = manager.getSelectedRow();
        if (!selected) { $.ligerDialog.warn('请先选择要进行配置的记录！'); return false; }
        var rownu = manager.getCheckedRows().length
        if (rownu > 1) { $.ligerDialog.warn('【配置】操作只能选择一条记录'); return false; }
        return true;
    }

    function GetMonitorItems(monitorId, struserid) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "UserDutySetting.aspx/GetMonitorItems",
            data: "{'strMonitor':'" + monitorId + "','strUserId':'" + struserid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d != null) {
                    vnoSelectedData = data.d;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
        var vnoItemlist = "";
        if (seletComboxValue != "") {
            vnoItemlist = "<option value=" + seletComboxValue + ">所有(" + selectComboxText + "类)</option>";
        }
        jQuery.each(vnoSelectedData, function (i, n) {
            vnoItemlist += "<option value=" + vnoSelectedData[i].ID + ">" + vnoSelectedData[i].ITEM_NAME + "</option>";
        });
        $("#listLeft option").remove();
        if (vnoItemlist.length > 0) {
            $("#listLeft").append(vnoItemlist);
        }
    }


    function GetExistMonitorItems(monitorId, struserid) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "UserDutySetting.aspx/GetExistMonitorItems",
            data: "{'strMonitor':'" + monitorId + "','strUserId':'" + struserid + "','strDefaultAu':'','strDefaultEx':''}",
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
            vItemlist += "<option value=" + vSelectedData[i].ITEM_ID + ">" + vSelectedData[i].ITEM_NAME + "</option>";
        });
        $("#listRight option").remove();
        if (vItemlist.length > 0) {
            $("#listRight").append(vItemlist);
        }
    }
    //加载作为默认负责人项目
    function GetAuItems(monitorId, struserid) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "UserDutySetting.aspx/GetExistMonitorItems",
            data: "{'strMonitor':'" + monitorId + "','strUserId':'" + struserid + "','strDefaultAu':'true','strDefaultEx':''}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d != null) {
                    vAuItemData = data.d;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });

        var vAuItemlist = "";
        jQuery.each(vAuItemData, function (i, n) {
            vAuItemlist += "<option value=" + vAuItemData[i].ITEM_ID + ">" + vAuItemData[i].ITEM_NAME + "</option>";
        });
        $("#listDefaultAu option").remove();
        if (vAuItemlist.length > 0) {
            $("#listDefaultAu").append(vAuItemlist);
        }
    }

    //加载作为默认协同人项目
    function GetExItems(monitorId, struserid) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "UserDutySetting.aspx/GetExistMonitorItems",
            data: "{'strMonitor':'" + monitorId + "','strUserId':'" + struserid + "','strDefaultAu':'','strDefaultEx':'true'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d != null) {
                    vExItemData = data.d;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });

        var vExItemlist = "";
        jQuery.each(vExItemData, function (i, n) {
            vExItemlist += "<option value=" + vExItemData[i].ITEM_ID + ">" + vExItemData[i].ITEM_NAME + "</option>";
        });
        $("#listDefaultEx option").remove();
        if (vExItemlist.length > 0) {
            $("#listDefaultEx").append(vExItemlist);
        }
    }

    function openSettingPage(strtype) {
        SetControlValue();
        //打开设置条件项目的DIV 
        mtardiv = $.ligerDialog.open({ target: $("#targerdiv"), height: 556, width: 780, top: -5, title: '监测项目岗位设置-分析', buttons: [{ text: '确定', onclick: function (item, text) {
            if ((moveid != "" || moveAllId != "") && $("#listRight option").length == 0) {
                $.ligerDialog.confirm('确定要删除所有已选监测项目吗？\r\n', function (result) {
                    if (result) {
                        //在清空全部的状态下如果存在分析的 监测类别 选择，则先执行移除类别 函数 再执行移除监测项目
                        if (isAllMonitor) {
                            SaveTypeDivDate();
                            if (saveAllType) {
                                if (moveid != "" || moveAuid != "" || moveExid != "") {
                                    SaveDivData();
                                }
                                else {
                                    $.ligerDialog.success('数据保存成功！');
                                }
                            }
                        }
                        else {
                            SaveDivData();
                        }
                    }
                    else {
                        return;
                    }
                })
            }
            else {
                //如果存在分析的 监测类别 选择，则先执行保存类别 函数 再执行保存监测项目
                if ($("#listRight option").length > 0) {
                    GetItemsValue();
                    if (isAllMonitor) {
                        SaveTypeDivDate();
                        if (saveAllType) {
                            if (strevmonitorItems != "" || strDefAuItems !== "" || strDefExItems != "") {
                                SaveDivData();
                            }
                            else {
                                $.ligerDialog.success('数据保存成功！');
                            }

                        }
                        else {
                            $.ligerDialog.warn('执行增加监测类别岗位职责失败！');
                        }
                    }
                    else {
                        SaveDivData();
                    }
                }
                else {
                    $.ligerDialog.warn('请选择监测项目！');
                }
            }


        }
        }, { text: '返回', onclick: function (item, text) {
            manager.loadData();
            SetControlValue();
            mtardiv.hide();
        }
        }]
        });
    }

    function SetControlValue() {
        $("#listLeft option").remove();
        $("#listRight option").remove();
        $("#listDefaultAu option").remove();
        $("#listDefaultEx option").remove();
        $("#txtSeach").val("");
        GetMonitor();
        $("#Monitor").ligerGetComboBoxManager().setValue("");
    }

    function GetItemsValue() {
        strevmonitorItems = "";
        strAllmonitorItems = "";
        $("#listRight option").each(function () {
            if ($(this).text() == '所有(' + selectComboxText + '类)') {
                strAllmonitorItems += $(this).val() + ";";
                isAllMonitor = true;
            }
            else {
                strevmonitorItems += $(this).val() + ";";
            }
        });
        strDefAuItems = "";
        strAllDefAuItems = "";
        $("#listDefaultAu option").each(function () {
            if ($(this).text() == '所有(' + selectComboxText + '类)') {
                strAllDefAuItems += $(this).val() + ";";
                isAllMonitor = true;
            }
            else {
                strDefAuItems += $(this).val() + ";";
            }
        });

        strDefExItems = "";
        strAllDefExItems = "";
        $("#listDefaultEx option").each(function () {
            if ($(this).text() == '所有(' + selectComboxText + '类)') {
                strAllDefExItems += $(this).val() + ";";
                isAllMonitor = true;
            }
            else {
                strDefExItems += $(this).val() + ";";
            }
        });
    }

    function SaveTypeDivDate() {
        //初始化
        isAllMonitor = false;
        saveAllType = false;

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            //url: "EvaluationTapSetting.aspx" + Methold + "",
            url: "UserDutySetting.aspx/SaveTypeDivDate",
            data: "{'strUserId':'" + struserId + "','strMonitor':'" + seletComboxValue + "','strDutyType':'" + strTypeAnaly + "','strMonitorItems':'" + strAllmonitorItems + "','strMoveId':'" + moveAllId + "','strAuItems':'" + strAllDefAuItems + "','strExItems':'" + strAllDefExItems + "','MoveAuId':'" + moveAuAllId + "','MoveExId':'" + moveExAllId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == true) {
                    saveAllType = true;
                    //ClearTempValue();
                    return saveAllType;
                }
                else {
                    //ClearTempValue();
                    return saveAllType;
                }
            },
            error: function () {
                ClearTempValue();
                return saveAllType;
            }
        });
    }

    function SaveDivData() {
        //        GetItemsValue();
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            //url: "EvaluationTapSetting.aspx" + Methold + "",
            url: "UserDutySetting.aspx/EditData",
            data: "{'strUserId':'" + struserId + "','strMonitor':'" + seletComboxValue + "','strDutyType':'" + strTypeAnaly + "','strMonitorItems':'" + strevmonitorItems + "','strMoveId':'" + moveid + "','strAuItems':'" + strDefAuItems + "','strExItems':'" + strDefExItems + "','MoveAuId':'" + moveAuid + "','MoveExId':'" + moveExid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == true) {
                    $.ligerDialog.success('数据保存成功！');
                    ClearTempValue();
                    GetMonitorItems(seletComboxValue, struserId);
                    GetExistMonitorItems(seletComboxValue, struserId);
                    GetAuItems(seletComboxValue, struserId);
                    GetExItems(seletComboxValue, struserId);
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

function ClearTempValue() {
    moveid = "";
    moveAuid = "";
    moveExid = "";
    moveAllId = "";
    moveAuAllId = "";
    moveExAllId = "";
}

function txtSeachOption() {
    //移除上次查询的底色
    $('#listLeft option').css({ 'background-color': '' });
    $('#listRight option').css({ 'background-color': '' });
    $('#listDefaultAu option').css({ 'background-color': '' });
    $('#listDefaultEx option').css({ 'background-color': '' });
    //获取所有包含查询内容的文本ListBox,并遍历
    $('#listLeft option:contains("' + $('#txtSeach').val() + '")').each(function () {
        //1、查询内容和td完全相同才改变ListBox项背景颜色
        //if($(this).text() == $('#txtSeach').val()){
        //$(this).css({'background-color':'red'});
        //}

        //2、改变所有满足条件的ListBox背景色
        $(this).css({ 'background-color': '#6FC8F5' });
    });

    $('#listRight option:contains("' + $('#txtSeach').val() + '")').each(function () {

        $(this).css({ 'background-color': '#6FC8F5' });
    });
    $('#listDefaultAu option:contains("' + $('#txtSeach').val() + '")').each(function () {
        $(this).css({ 'background-color': '#6FC8F5' });
    });
    $('#listDefaultEx option:contains("' + $('#txtSeach').val() + '")').each(function () {
        $(this).css({ 'background-color': '#6FC8F5' });
    });
}

