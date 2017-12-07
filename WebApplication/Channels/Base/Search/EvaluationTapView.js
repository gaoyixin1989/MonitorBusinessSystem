//Create by Castle(胡方扬 2012-10-31)

//获取上一页面传过来的URL的参数
//方法:    //获取传入页面的参数值
// Get object of URL parameters
//var allVars = $.getUrlVars();

// Getting URL var by its nam
//var byName = $.getUrlVar('name');

var firstNodeId = null, mtardiv = null;
var manager = null;
var Copy = [];
$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});

$(document).ready(function () {
    //构造标准信息
    var standartId = $.getUrlVar('standartId'); //$.query.get('standartId');
    var strevcode = "";
    var strevname = "";
    var strevtype = "";
    var strevmonitor = "";
    var strevmonitorId = "";
    var strcondtionId = null;

    $.get("EvaluationTapView.aspx?type=getStandardInfo&standartId=" + standartId, function (data) {
        if (data != null) {
            data = $.parseJSON(data);
            strevcode = data.STANDARD_CODE;
            strevname = data.STANDARD_NAME;
            strevtype = data.REMARK1;
            strevmonitor = data.REMARK2;
            strevmonitorId = data.MONITOR_ID;
        }
        SetStandardInfo(data);
    });
    //加载数据
    function SetStandardInfo(data) {
        $("#inputform").ligerForm({
            inputWidth: 110, labelWidth: 70,
            fields: [
                { name: "EvaluaInfo_id", type: "hidden" },
                { display: "标准代码", name: "EvaluaInfo_Code", newline: true, type: "text" },
                { display: "标准类型 ", name: "EvaluaInfo_Standand", newline: false, type: "text" },
                { display: "监测类型 ", name: "EvaluaInfo_Monitor", newline: false, type: "text" }
                ]
        });
        //设置只读
        $("#EvaluaInfo_Code").ligerGetTextBoxManager().setDisabled();
        $("#EvaluaInfo_Standand").ligerGetTextBoxManager().setDisabled();
        $("#EvaluaInfo_Monitor").ligerGetTextBoxManager().setDisabled();
        //赋值
        $("#EvaluaInfo_id").val(standartId);
        $("#EvaluaInfo_Code").val(data.STANDARD_CODE);
        $("#EvaluaInfo_Standand").val(data.REMARK1);
        $("#EvaluaInfo_Monitor").val(data.REMARK2);
    }

    var isfirst = false; //是否选中第一节点
    var setting = {
        check: {
            enable: false
        },
        data: {
            simpleData: {
                enable: true
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
            url: "../Evaluation/EvaluationTapSetting.aspx",
            otherParam: { "action": "GetEvaluConChild", "standartId": "" + standartId + "" }
        },
        callback: {
            onAsyncSuccess: zTreeOnAsyncSuccess,
            onClick: onTreeClick,
            onDrop: zTreeOnDrop
        }
    };
    //设置按钮不可用
    setting.edit.showRemoveBtn = false;
    //******************************************添加用户控件*******************************************************
    var SltID, SltNodeName, StrParentID, StrParentName, strConinfoCode, StrConinfoName, strStandardId, strReamrk;

    //记录上次节点id
    var nodeIdTemp = "", strnewstand = "";
    //异步加载 选中根节点先第一个节点
    function zTreeOnAsyncSuccess(event, treeId, msg) {
        if (isfirst) {
            var nodes = zTreeObj.getNodes();
            if (nodes.length > 0 && nodes[0].children != null) {
                //选中节点
                zTreeObj.selectNode(nodes[0].children[0]);

                firstNodeId = nodes[0].children[0].id;
                //首次加载第一个节点数据
                loadGrid(firstNodeId);
                return firstNodeId;
            }
        }
    }

    function zTreeOnDrop(event, treeId, treeNodes, targetNode, moveType) {
        var zTree = $.fn.zTree.getZTreeObj(treeId);
        var node = treeNodes[0].getParentNode();
        treeNodes[0].pId = node.id;
        zTree.updateNode(treeNodes[0]);
        var strValue = "";
        var spit = "";
        var Treenodes = zTree.transformToArray(zTree.getNodes());
        //        alert(Treenodes.length);
        for (var i = 0; i < Treenodes.length; i++) {
            //遍历所有节点，重新进行排序
            if (Treenodes[i].pId != null && Treenodes[i].pId != '-1') {
                strValue = strValue + spit + i + "|" + Treenodes[i].id + "|" + Treenodes[i].pId;
                //alert(strValue);
                spit = ",";
            }
        }
    };
    //点击节点事件
    function onTreeClick(e, treeId, treeNode) {
        var zTree = $.fn.zTree.getZTreeObj(treeId);
        //展开节点
        zTree.expandNode(treeNode, null, null, null, true);
        //记录本次选择的节点ID
        nodeIdTemp = treeNode.id;
        strnewstand = treeNode.STANDARD_ID;
        strcondtionId = nodeIdTemp;
        loadGrid(treeNode.id);
    }

    function setMonitorItem() {
        //初始化listBox
        $("#listLeft option").remove();
        $("#listRight option").remove();

        //加载左侧和有才ListBox值
        GetMonitorSubItems(standartId, strcondtionId, strevmonitorId);
        getItems(standartId, strcondtionId, strevmonitorId);
        //打开设置条件项目的DIV 
        mtardiv = $.ligerDialog.open({ target: $("#targerdiv"), height: 380, width: 440, top: 10, title: '监测项目设置', buttons: [{ text: '确定', onclick: function (item, text) { SaveDivData(); } }, { text: '返回', onclick: function (item, text) {
            manager.loadData();
            mtardiv.hide();
        }
        }]
        });

    }
    var detailWin = null, curentData = null, currentIsAddNew;
    function showDetail(data, isAddNew) {
        //传值，当为新增时,data为null，isAddNew为true
        curentData = data;
        currentIsAddNew = isAddNew;
        if (detailWin) {
            detailWin.show();
        }
        else {
            //创建表单结构
            var mainform = $("#editEvaluform");
            mainform.ligerForm({
                inputWidth: 140, labelWidth: 90, space: 40,
                fields: [
                      { name: "PARENT_ID", type: "hidden" },
                      { name: "NODE_ID", type: "hidden" },
                      { display: "条件项目编码", name: "CONDITION_CODE", newline: true, type: "text" },
                      { display: "条件项目名称", name: "CONDITION_NAME", newline: false, type: "text" },
                      { display: "评价标准", name: "STANDARD_INFOR", newline: true, type: "select", comboboxName: "STANDARD_INFOR_Box", options: { valueFieldID: "STANDARD_INFOR_OP", valueField: "ID", textField: "STANDARD_CODE", url: "../Evaluation/EvaluationTapSetting.aspx?action=GetStandardConInfor"} }
                      ]
            });
        }
        $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setValue(curentData.STANDARD_ID);
        $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setDisabled();

        //DIV窗口填充数据
        if (curentData) {
            $("#CONDITION_CODE").val(curentData.CONDITION_CODE);
            $("#CONDITION_NAME").val(curentData.CONDITION_NAME);
            $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setValue(curentData.STANDARD_ID);
            $("#PARENT_ID").val(curentData.PARENT_ID);
            $("#NODE_ID").val(curentData.NODE_ID);

            $("#CONDITION_CODE").focus();
        }

        function clearDialogValue() {
            $("#CONDITION_CODE").focus();
            $("#CONDITION_CODE").val("");
            $("#CONDITION_NAME").val("");
            $("#STANDARD_INFOR_Box").val("");
            $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setEnabled();
        }
    }

    //加载菜单
    isfirst = true;
    var zTreeObj = $.fn.zTree.init($("#tree"), setting);

    function loadGrid(strConinfoId) {
        //加载下拉列表数据
        var GetItemUnitData = null;
        var GetMonitorValue = null;
        var GetOpreatorData = null;
        var GetMonitorData = null;
        var GetMonitorItem = null;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../Evaluation/EvaluationTapSetting.aspx/GetMonitorItems",
            data: "{'strMonitor':'" + strevmonitorId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    GetMonitorItem = data;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求监测项目数据失败！');
            }
        });

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../Evaluation/EvaluationInfor.aspx/GetMonitor",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    GetMonitorData = data;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求监测项目单位数据失败！');
            }
        });
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../Evaluation/EvaluationTapSetting.aspx/GetItemUnit",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    GetItemUnitData = data;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求监测项目单位数据失败！');
            }
        });


        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../Evaluation/EvaluationTapSetting.aspx/GetMonitorValue",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    GetMonitorValue = data;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求监测类型值数据失败！');
            }
        });

        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../Evaluation/EvaluationTapSetting.aspx/GetOpreator",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    GetOpreatorData = data;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求运算符号数据失败！');
            }
        });

        //加载数据表数据
        window['g'] =
    manager = $("#maingrid").ligerGrid({
        columns: [
                { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 150, data: GetMonitorItem.d, render: function (item) {
                    for (var i = 0; i < GetMonitorItem.d.length; i++) {
                        if (GetMonitorItem.d[i]['ID'] == item.ITEM_ID)
                            return GetMonitorItem.d[i]['ITEM_NAME']
                    }
                    return item.ITEM_ID;
                }
                },
                { display: '监测值类型', name: 'MONITOR_VALUE_ID', width: 150, isSort: false, render: function (item) {
                    for (var i = 0; i < GetMonitorValue.d.length; i++) {
                        if (GetMonitorValue.d[i]['DICT_CODE'] == item.MONITOR_VALUE_ID)
                            return GetMonitorValue.d[i]['DICT_TEXT']
                    }
                    return item.MONITOR_VALUE_ID;
                }
                },
                { display: '上限运算符', name: 'UPPER_OPERATOR', width: 80, isSort: false,
                    render: function (item) {
                        for (var i = 0; i < GetOpreatorData.d.length; i++) {
                            if (GetOpreatorData.d[i]['DICT_CODE'] == item.UPPER_OPERATOR)
                                return GetOpreatorData.d[i]['DICT_TEXT']
                        }
                        return item.UPPER_OPERATOR;
                    }
                },
                { display: '排放上限', name: 'DISCHARGE_UPPER', width: 80 },
                { display: '下限运算符', name: 'LOWER_OPERATOR', width: 80, isSort: false, render: function (item) {
                    for (var i = 0; i < GetOpreatorData.d.length; i++) {
                        if (GetOpreatorData.d[i]['DICT_CODE'] == item.LOWER_OPERATOR)
                            return GetOpreatorData.d[i]['DICT_TEXT']
                    }
                    return item.LOWER_OPERATOR;
                }
                },
                { display: '排放下限', name: 'DISCHARGE_LOWER', width: 80 },
                { display: '排放单位', name: 'UNIT', width: 80, isSort: false,
                    render: function (item) {
                        for (var i = 0; i < GetItemUnitData.d.length; i++) {
                            if (GetItemUnitData.d[i]['DICT_CODE'] == item.UNIT)
                                return GetItemUnitData.d[i]['DICT_TEXT']
                        }
                        return item.UNIT;
                    }
                }
                ],
        title: '阀值列表',
        width: '60%',
        height: '96%',
        pageSizeOptions: [10, 20, 50],
        url: '../Evaluation/EvaluationTapSetting.aspx?action=GetEvaluItemData&standid=' + strConinfoId + '',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        enabledEdit: true,
        rownumbers: true
    }
    );
        $("#pageloading").hide();
    }

    //DIV 设置条件项目监测项目部分

    var vSelectedData = null, vItemData = null;
    var moveid = "";

    //初始化左侧ListBox
    function GetMonitorSubItems(standId, condtionId, monitorId) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../Evaluation/EvaluationTapSetting.aspx/GetMonitorSubItems",
            data: "{'strStandId':'" + standId + "','strCondtionId':'" + condtionId + "','strMonitor':'" + monitorId + "'}",
            //            data: "{'strMonitor':'" + monitorId + "'}",
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
            vlist += "<option value=" + vItemData[i].ID + ">" + vItemData[i].ITEM_NAME + "</option>";
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

    function getItems(standId, condtionId, monitorId) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../Evaluation/EvaluationTapSetting.aspx/GetSelectedMonitorItems",
            data: "{'strStandId':'" + standId + "','strCondtionId':'" + condtionId + "','strMonitor':'" + monitorId + "'}",
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
            vItemlist += "<option value=" + vSelectedData[i].ID + ">" + vSelectedData[i].ITEM_NAME + "</option>";
        });
        if (vItemlist.length > 0) {
            $("#listRight").append(vItemlist);
        }
    }
});

function txtSeachOption() {
    //移除上次查询的底色
    $('#listLeft option').css({ 'background-color': '' })
    //获取所有包含查询内容的文本ListBox,并遍历
    $('#listLeft option:contains("' + $('#txtSeach').val() + '")').each(function () {
        //1、查询内容和td完全相同才改变ListBox项背景颜色
        //if($(this).text() == $('#txtSeach').val()){
        //$(this).css({'background-color':'red'});
        //}

        //2、改变所有满足条件的ListBox背景色
        $(this).css({ 'background-color': '#6FC8F5' });
    });
}