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
    //var standartId = "000000001";
    //获取URL 参数
    var standartId = $.getUrlVar('standartId'); //$.query.get('standartId');
    var strevcode = $.getUrlVar('strevcode');
    var strevname = $.getUrlVar('strevname');
    var strevtype = $.getUrlVar('strevtype');
    var strevmonitor = $.getUrlVar('strevmonitor');
    var strevmonitorId = $.getUrlVar('strevmonitorid');
    var strcondtionId = null;
    //加载数据
    $("#inputform").ligerForm({
        inputWidth: 110, labelWidth: 70,
        fields: [
                { name: "EvaluaInfo_id", type: "hidden" },
                { display: "标准代码", name: "EvaluaInfo_Code", newline: true, type: "text" },
        //                { display: "标准名称", name: "EvaluaInfo_Name", newline: false, type: "text" },
                {display: "标准类型 ", name: "EvaluaInfo_Standand", newline: false, type: "text" },
                { display: "监测类型 ", name: "EvaluaInfo_Monitor", newline: false, type: "text" }
                ]
    });
    //设置只读
    $("#EvaluaInfo_Code").ligerGetTextBoxManager().setDisabled();
    //    $("#EvaluaInfo_Name").ligerGetTextBoxManager().setDisabled();
    $("#EvaluaInfo_Standand").ligerGetTextBoxManager().setDisabled();
    $("#EvaluaInfo_Monitor").ligerGetTextBoxManager().setDisabled();
    //赋值
    $("#EvaluaInfo_id").val(standartId);
    $("#EvaluaInfo_Code").val(strevcode);
    //    $("#EvaluaInfo_Name").val(strevname);
    $("#EvaluaInfo_Standand").val(strevtype);
    $("#EvaluaInfo_Monitor").val(strevmonitor);


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
            url: "EvaluationTapSetting.aspx",
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
    function addDom(treeId, treeNode) {
        //添加用户控件
        //alert(treeNode.id);
        var sObj = $("#" + treeNode.tId + "_a");
        if (treeNode.editNameFlag || $("#addBtn_" + treeNode.id).length > 0) return;
        var addStr = "<span class='button add' id='addBtn_" + treeNode.id
				+ "' title='添加子菜单' onfocus='this.blur();'></span>";
        addStr += "<span class='button edit' id='editBtn_" + treeNode.id
				+ "' title='编辑当前菜单' onfocus='this.blur();'></span>";
        addStr += "<span class='button remove' id='removeBtn_" + treeNode.id
				+ "' title='删除当前菜单' onfocus='this.blur();'></span>";
        //        addStr += "<span class='button ico_open' id='setBtn_" + treeNode.id
        //				+ "' title='监测项目设置' onfocus='this.blur();'></span>";
        sObj.after(addStr);

        //绑定添加按钮的用户控件事件

        var addBtn = $("#addBtn_" + treeNode.id);
        if (addBtn) addBtn.bind("click", function () {
            showDetail({ PARENT_ID: treeNode.id, STANDARD_ID: treeNode.STANDARD_ID }, true);
        });
        var editBtn = $("#editBtn_" + treeNode.id);

        if (editBtn) editBtn.bind("click", function () {
            if (treeNode.pId == null) {
                $.ligerDialog.warn('根节点不允许编辑，请联系管理员！');
                return;
            }
            showDetail({
                NODE_ID: treeNode.id,
                CONDITION_CODE: treeNode.code,
                CONDITION_NAME: treeNode.name,
                STANDARD_ID: treeNode.STANDARD_ID,
                PARENT_ID: treeNode.pId,
                CONDITION_REMARK: treeNode.CONDITION_REMARK
            }, false);
        });
        //绑定删除按钮的用户控件事件
        var deleteBtn = $("#removeBtn_" + treeNode.id);

        if (deleteBtn) deleteBtn.bind("click", function () {
            //            StrParentID = treeNode.pId;
            if (treeNode.pId == null) {
                $.ligerDialog.warn('根节点不允许删除，请联系管理员！');
                return;
            }
            deleteNode();
        });


        //绑定删除按钮的用户控件事件
        //        var setBtn = $("#setBtn_" + treeNode.id);

        //        if (setBtn) setBtn.bind("click", function () {
        //            //            StrParentID = treeNode.pId;
        //            if (treeNode.pId == null) {
        //                $.ligerDialog.warn('根节点不允许设置，请联系管理员！');
        //                return;
        //            }
        //            strcondtionId = treeNode.id;
        //            setMonitorItem();
        //        });
    };
    //记录上次节点id
    var nodeIdTemp = "", strnewstand = "";
    //当鼠标移出节点的时候自动隐藏用户控件
    function removeDom(strNodeIdTemp) {
        $("#addBtn_" + nodeIdTemp).unbind().remove();
        $("#editBtn_" + nodeIdTemp).unbind().remove();
        $("#removeBtn_" + nodeIdTemp).unbind().remove();
        //        $("#setBtn_" + nodeIdTemp).unbind().remove();
    };
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
        //将数据排序信息发送至数据库
        $.ajax({
            cache: false,
            type: "POST",
            url: "EvaluationTapSetting.aspx/SortData",
            data: "{'strValue':'" + strValue + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == "true") {
                    //alert("菜单排序成功");
                    var managerd = $.ligerDialog.waitting('数据保存成功！');
                    setTimeout(function () {
                        managerd.close();
                    }, 1000);
                }
                else {
                    $.ligerDialog.warn('数据保存失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求数据失败！');
            }
        });
    };
    //点击节点事件
    function onTreeClick(e, treeId, treeNode) {
        var zTree = $.fn.zTree.getZTreeObj(treeId);
        //展开节点
        zTree.expandNode(treeNode, null, null, null, true);
        //移除控件
        removeDom(nodeIdTemp);
        //增加新控件
        addDom(treeId, treeNode);
        //记录本次选择的节点ID
        nodeIdTemp = treeNode.id;
        strnewstand = treeNode.STANDARD_ID;
        strcondtionId = nodeIdTemp;
        loadGrid(treeNode.id);
    }

    function setMonitorItem() {
        //        $.ligerDialog.open({ url: 'SetMonitorItems.aspx?standartId=' + standartId + '&strevmonitorid=' + strevmonitorId + '&strcondtionid=' + nodeIdTemp + '', height: 360, width: 400, top: 50, title: '监测项目设置'
        //        });
        //初始化listBox
        $("#listLeft option").remove();
        $("#listRight option").remove();

        //加载左侧和有才ListBox值
        GetMonitorSubItems(standartId, strcondtionId, strevmonitorId);
        getItems(standartId, strcondtionId, strevmonitorId);
        //打开设置条件项目的DIV 
        mtardiv = $.ligerDialog.open({ target: $("#targerdiv"), height: 380, width: 440, top: 10, title: '监测项目设置', buttons: [{ text: '确定', onclick: function (item, text) { SaveDivData(); } }, { text: '返回', onclick: function (item, dialog) {
            manager.loadData();
            mtardiv.hide();
        }
        }]
        });

    }
    function SaveDivData() {

        var strevmonitorItems = "";
        if ($("#listRight option").length > 0) {
            $("#listRight option").each(function () {
                strevmonitorItems += $(this).val() + ";";
            });
        }
        if (strevmonitorItems != "") {
            strevmonitorItems = strevmonitorItems.substring(0, strevmonitorItems.length - 1);
        }

        if (moveid != "") {
            moveid = moveid.substring(0, moveid.length - 1);
        }
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            //url: "EvaluationTapSetting.aspx" + Methold + "",
            url: "EvaluationTapSetting.aspx/EditData",
            data: "{'strStandId':'" + standartId + "','strCondtionId':'" + strcondtionId + "','strMonitor':'" + strevmonitorId + "','strMonitorItems':'" + strevmonitorItems + "','strMoveId':'" + moveid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == "true") {
                    manager.loadData();
                    mtardiv.hide();
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
        //        else {
        //            $.ligerDialog.warn('请选择监测项目！');
        //        }
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
                      { display: "评价标准", name: "STANDARD_INFOR", newline: true, type: "select", comboboxName: "STANDARD_INFOR_Box", options: { valueFieldID: "STANDARD_INFOR_OP", valueField: "ID", textField: "STANDARD_CODE", url: "EvaluationTapSetting.aspx?action=GetStandardConInfor"} }
                //                      { display: "备 注", name: "CONDITION_REMARK", newline: false, type: "textarea" }
                      ]
            });
            //add validate by ssz
            $("#CONDITION_CODE").attr("validate", "[{required:true, msg:'请填写条件项目编码'},{maxlength:64,msg:'项目包最大长度为64'}]");
            $("#CONDITION_NAME").attr("validate", "[{required:true, msg:'请填写条件项目名称'},{maxlength:256,msg:'项目包最大长度为256'}]");
            //打开窗口事件
            detailWin = $.ligerDialog.open({
                //要弹出的DIV
                target: $("#dvtap"),
                title: currentIsAddNew ? '新增数据' : '编辑数据',
                width: 560, height: 220, top: 60,
                //按钮设置
                buttons: [
                  { text: '确定', onclick: function () { save(); } },
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
            });
        }
        // $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setEnabled();
        $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setValue(curentData.STANDARD_ID);
        $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setDisabled();

        //DIV窗口填充数据
        if (curentData) {

            $("#CONDITION_CODE").val(curentData.CONDITION_CODE);
            $("#CONDITION_NAME").val(curentData.CONDITION_NAME);
            //$("#CONDITION_REMARK").val(curentData.CONDITION_REMARK);
            //            if (isAddNew) {
            //                $("#CONDITION_CODE").ligerTextBox({ nullText: '条件项目编码不能为空' });
            //                $("#CONDITION_NAME").ligerTextBox({ nullText: '条件项目名称不能为空' });
            //            }
            //            else {
            //                $("#CONDITION_CODE").attr("class", "l-text-field");
            //                $("#CONDITION_NAME").attr("class", "l-text-field");
            //            }
            $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setValue(curentData.STANDARD_ID);
            // currentIsAddNew ? $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setEnabled() : $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setDisabled();
            // $("#STANDARD_INFOR_OP").val(curentData.STANDARD_ID);
            $("#PARENT_ID").val(curentData.PARENT_ID);
            $("#NODE_ID").val(curentData.NODE_ID);

            $("#CONDITION_CODE").focus();
        }

        function clearDialogValue() {
            $("#CONDITION_CODE").focus();
            $("#CONDITION_CODE").val("");
            $("#CONDITION_NAME").val("");
            // $("#CONDITION_REMARK").val("");
            $("#STANDARD_INFOR_Box").val("");
            $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setEnabled();

        }


        //保存事件
        function save() {
            //表单验证
            if (!$("#editEvaluform").validate())
                return false;
            curentData = curentData || {};
            //            if ($("#CONDITION_CODE").val() == "" || $("#CONDITION_CODE").val() == "条件项目编码不能为空") {
            //                return;
            //            }
            //            if ($("#CONDITION_NAME").val() == "" || $("#CONDITION_NAME").val() == "条件项目名称不能为空") {
            //                return;
            //            }
            curentData.CONDITION_CODE = $("#CONDITION_CODE").val();
            curentData.CONDITION_NAME = $("#CONDITION_NAME").val();
            curentData.CONDITION_REMARK = "";
            curentData.STANDARD_ID = $("#STANDARD_INFOR_Box").val();
            curentData.PARENT_ID = $("#PARENT_ID").val();
            curentData.NODE_ID = $("#NODE_ID").val();
            var strData = "{" + (currentIsAddNew ? "" : "'strID':'" + $("#NODE_ID").val() + "',") + "'strPARENT_ID':'" + $("#PARENT_ID").val() + "','strSTANDARD_ID':'" + $("#STANDARD_INFOR_OP").val() + "','strCONDITION_CODE':'" + $("#CONDITION_CODE").val() + "','strCONDITION_NAME':'" + $("#CONDITION_NAME").val() + "','strCONDITION_REMARK':''}";
            var Methold = currentIsAddNew ? "/CreateData" : "/EditDataCon";
            $.ajax({
                cache: false,
                type: "POST",
                url: "EvaluationTapSetting.aspx" + Methold + "",
                data: strData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $('body').append("<div class='jloading'>正在保存数据中...</div>");
                    $.ligerui.win.mask();
                },
                complete: function () {
                    $('body > div.jloading').remove();
                    $.ligerui.win.unmask({ id: new Date().getTime() });
                },
                success: function (data) {
                    if (data.d != "") {
                        var zTree = $.fn.zTree.getZTreeObj("tree");
                        var treeNode = zTree.getSelectedNodes()[0];
                        if (currentIsAddNew == true) {
                            var newNode = { id: data.d, pId: $("#PARENT_ID").val(), code: $("#CONDITION_CODE").val(), name: $("#CONDITION_NAME").val(), STANDARD_ID: $("#STANDARD_INFOR_OP").val(), CONDITION_REMARK: $("#CONDITION_REMARK").val() };
                            $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setEnabled();
                            newNode = zTree.addNodes(treeNode, newNode);

                            clearDialogValue();
                            $("#STANDARD_INFOR_Box").val(curentData.STANDARD_ID);
                            $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setDisabled();
                        }
                        else if (data.d == "true") {

                            treeNode.name = $("#CONDITION_NAME").val();
                            treeNode.code = $("#CONDITION_CODE").val()
                            treeNode.STANDARD_ID = $("#STANDARD_INFOR_OP").val();
                            //                            treeNode.CONDITION_REMARK = $("#CONDITION_REMARK").val();
                            treeNode.CONDITION_REMARK = "";
                            $("#STANDARD_INFOR_Box").disabled = false;
                            $("#STANDARD_INFOR_Box").ligerGetComboBoxManager().setEnabled();
                            zTree.updateNode(treeNode);
                        }
                        $.ligerDialog.success("数据操作成功！");
                    }
                    else {
                        $.ligerDialog.warn('数据操作失败！');
                    }
                },
                error: function () {
                    $.ligerDialog.warn('Ajax请求数据失败！');
                }
            });
        }
    }

    //删除节点
    function deleteNode() {
        $.ligerDialog.confirm("确认删除选择条件项目吗？\r", function (result) {
            confirmObj = result;
            if (result == true) {
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "EvaluationTapSetting.aspx/DelData",
                    data: "{'strID':'" + nodeIdTemp + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == "true") {
                            var zTree = $.fn.zTree.getZTreeObj("tree");
                            var treeNode = zTree.getSelectedNodes()[0];
                            zTree.removeNode(treeNode);
                            //alert("数据删除成功")
                            $.ligerDialog.success('数据删除成功!');
                        }
                        else {
                            $.ligerDialog.warn('数据删除失败!');
                            //alert("数据删除失败")
                        }
                    },
                    error: function () {
                        $.ligerDialog.warn('Ajax请求数据失败!');
                    }
                });
            }
            else {
                return;
            }
        });
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
        var GetMonitorItem = null, GetMonitorItemJson = null;
        //        $.ajax({
        //            cache: false,
        //            async: false, //设置是否为异步加载,此处必须
        //            type: "POST",
        //            url: "EvaluationTapSetting.aspx/GetMonitorItems",
        //            data: "{'strMonitor':'" + strevmonitorId + "'}",
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            success: function (data) {
        //                if (data != null) {
        //                    GetMonitorItem = data.d;
        //                }
        //            },
        //            error: function () {
        //                $.ligerDialog.warn('Ajax请求监测项目数据失败！');
        //            }
        //        });

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EvaluationTapSetting.aspx?action=GetMonitorItemsJson&strMonitorId=" + strevmonitorId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != "0") {
                    GetMonitorItemJson = data.Rows;
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
            url: "EvaluationInfor.aspx/GetMonitor",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    GetMonitorData = data.d;
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
            url: "EvaluationTapSetting.aspx/GetItemUnit",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    GetItemUnitData = data.d;
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
            url: "EvaluationTapSetting.aspx/GetMonitorValue",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    GetMonitorValue = data.d;
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
            url: "EvaluationTapSetting.aspx/GetOpreator",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    GetOpreatorData = data.d;
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
                { display: '监测项目', name: 'ITEM_NAME', align: 'left', width: 100 },
                { display: '监测值类型', name: 'MONITOR_VALUE_ID', width: 100, isSort: false, editor: { type: 'select', data: GetMonitorValue, valueColumnName: 'DICT_CODE', displayColumnName: 'DICT_TEXT' }, render: function (item) {
                    if (GetMonitorValue != null) {
                        for (var i = 0; i < GetMonitorValue.length; i++) {
                            if (GetMonitorValue[i]['DICT_CODE'] == item.MONITOR_VALUE_ID)
                                return GetMonitorValue[i]['DICT_TEXT']
                        }
                    }
                    return item.MONITOR_VALUE_ID;
                }
                },
                { display: '上限运算符', name: 'UPPER_OPERATOR', width: 80, isSort: false,
                    editor: { type: 'select', data: GetOpreatorData, valueColumnName: 'DICT_CODE', displayColumnName: 'DICT_TEXT' }, render: function (item) {
                        if (GetOpreatorData != null) {
                            for (var i = 0; i < GetOpreatorData.length; i++) {
                                if (GetOpreatorData[i]['DICT_CODE'] == item.UPPER_OPERATOR)
                                    return GetOpreatorData[i]['DICT_TEXT']
                            }
                        }
                        return item.UPPER_OPERATOR;
                    }
                },
                { display: '排放上限', name: 'DISCHARGE_UPPER', width: 80, editor: { type: 'text'} },
                { display: '下限运算符', name: 'LOWER_OPERATOR', width: 80, isSort: false, editor: { type: 'select', data: GetOpreatorData, valueColumnName: 'DICT_CODE', displayColumnName: 'DICT_TEXT' }, render: function (item) {
                    if (GetOpreatorData != null) {
                        for (var i = 0; i < GetOpreatorData.length; i++) {
                            if (GetOpreatorData[i]['DICT_CODE'] == item.LOWER_OPERATOR)
                                return GetOpreatorData[i]['DICT_TEXT']
                        }
                    }
                    return item.LOWER_OPERATOR;
                }
                },
                { display: '排放下限', name: 'DISCHARGE_LOWER', width: 80, editor: { type: 'text'} },
                { display: '排放单位', name: 'UNIT', width: 80, isSort: false,
                    editor: { type: 'select', data: GetItemUnitData, valueColumnName: 'DICT_CODE', displayColumnName: 'DICT_TEXT' }, render: function (item) {
                        if (GetItemUnitData != null) {
                            for (var i = 0; i < GetItemUnitData.length; i++) {
                                if (GetItemUnitData[i]['DICT_CODE'] == item.UNIT)
                                    return GetItemUnitData[i]['DICT_TEXT']
                            }
                        }
                        return item.UNIT;
                    }
                }
                ],
        title: '阀值设置列表',
        width: '60%',
        height: '96%',
        pageSizeOptions: [10, 20, 50],
        url: 'EvaluationTapSetting.aspx?action=GetEvaluItemData&standid=' + strConinfoId + '',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        //        whenRClickToSelect: true,
        enabledEdit: true,
        //        isScroll: false,
        checkbox: true,
        rownumbers: true,
        onAfterEdit: f_onAfterEdit,
        toolbar: { items: [
                { id: 'setting', text: '监测项目设置', click: itemclickOfToolbar, icon: 'database_wrench' },
                { line: true },
                { id: 'copy', text: '复制', click: itemclickOfToolbar, icon: 'page_copy' },
                { line: true },
                { id: 'paste', text: '粘贴', click: itemclickOfToolbar, icon: 'page_paste' },
                { line: true },
                { id: 'save', text: '保存', click: itemclickOfToolbar, icon: 'page_save' }
                ]
        }
    }
    );
        $("#pageloading").hide();
        //$(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
        //修改行数据后，更新列的数据，保存数据库需点击“保存”按钮
        function f_onAfterEdit(e) {


            manager.updateCell('MONITOR_VALUE_ID', e.record.MONITOR_VALUE_ID, e.record);
            manager.updateCell('UPPER_OPERATOR', e.record.UPPER_OPERATOR, e.record);
            manager.updateCell('DISCHARGE_LOWER', e.record.DISCHARGE_LOWER, e.record);
            manager.updateCell('LOWER_OPERATOR', e.record.LOWER_OPERATOR, e.record);
            manager.updateCell('DISCHARGE_UPPER', e.record.DISCHARGE_UPPER, e.record);
            manager.updateCell('UNIT', e.record.UNIT, e.record);

        }

        function itemclickOfToolbar(item) {
            switch (item.id) {

                case 'setting':
                    if (strcondtionId == null) {
                        $.ligerDialog.warn('请选择条件项目！');
                        return;
                    } else {
                        setMonitorItem();
                    }
                    break;
                case 'paste':
                    var succ = 0;
                    var hav = true;
                    if (Copy.length < 1) {
                        $.ligerDialog.warn('请选择要复制的监测项目！');
                        return;
                    }
                    if (Copy.length > 0) {
                        for (var i = 0; i < Copy.length; i++) {
                            var flag = isExistItem(Copy[i].MONITOR_ID, Copy[i].ITEM_ID, strnewstand, nodeIdTemp);
                            if (flag) {
                                hav = true;
                                Copy.splice(i, 1);
                            }
                        }
                    }

                    if (Copy.length > 0) {
                        for (var i = 0; i < Copy.length; i++) {
                            $.ajax({
                                cache: false,
                                type: "POST",
                                url: "EvaluationTapSetting.aspx/InsertCopyData",
                                data: "{'strStandardId':'" + strnewstand + "','strConditionId':'" + nodeIdTemp + "','strMonitorId':'" + Copy[i].MONITOR_ID + "','strMonitorValue':'" + Copy[i].MONITOR_VALUE_ID + "','strItemId':'" + Copy[i].ITEM_ID + "','strUpperOperator':'" + Copy[i].UPPER_OPERATOR + "','strLowerOperator':'" + Copy[i].LOWER_OPERATOR + "','strUpperChar':'" + Copy[i].DISCHARGE_UPPER + "','strLowerChar':'" + Copy[i].DISCHARGE_LOWER + "','strUnit':'" + Copy[i].UNIT + "'}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                beforeSend: function () {
                                    $('body').append("<div class='jloading'>正在保存数据中...</div>");
                                    $.ligerui.win.mask();
                                },
                                complete: function () {
                                    $('body > div.jloading').remove();
                                    $.ligerui.win.unmask({ id: new Date().getTime() });
                                },
                                success: function (data) {
                                    if (data.d == "true") {
                                        succ++;
                                        if (succ == Copy.length) {
                                            manager.loadData();
                                            $.ligerDialog.success('粘贴数据操作成功！');
                                        }
                                    }
                                    else {
                                        $.ligerDialog.warn('粘贴数据操作失败！');
                                    }
                                },
                                error: function () {
                                    $.ligerDialog.warn('Ajax请求数据失败！');
                                }
                            });
                        }
                    }
                    else {
                        if (hav) {
                            $.ligerDialog.success("已忽略复制中已存在于目标中的监测项目！");
                        }
                    }
                    break;
                case 'copy':
                    var selected = manager.getCheckedRows();
                    if (selected == null) { $.ligerDialog.warn('请先选择要复制的记录！'); return; }
                    var rownu = manager.getCheckedRows().length;
                    if (rownu > 0)
                        var succnu = 0;
                    for (var i = 0; i < rownu; i++) {
                        Copy.push(selected[i]);
                        succnu++;
                    }
                    if (succnu == rownu) {
                        $.ligerDialog.success('所选数据复制成功！');
                    }
                    break;
                case 'save':
                    var flag = 0;
                    var rowcount = manager.data.Total;
                    var rows = manager.data.Rows;
                    var row = manager.data.Rows.length;
                    if (row < 1) {//rowcount
                        $.ligerDialog.warn('没有要修改的数据！');
                        return;
                    }
                    for (var i = 0; i < row; i++) {//原来：rowcount
                        $.ajax({
                            cache: false,
                            type: "POST",
                            url: "EvaluationTapSetting.aspx/EditGridColumnData",
                            //data: "{'strId':'" + e.record.ID + "','strItemId':'" + e.record.ITEM_ID + "','strUpperOperator':'" + e.record.UPPER_OPERATOR + "','strLowerOperator':'" + e.record.LOWER_OPERATOR + "','strUpperChar':'" + e.record.DISCHARGE_UPPER + "','strLowerChar':'" + e.record.DISCHARGE_LOWER + "','strUnit':'" + e.record.UNIT + "'}",
                            data: "{'strId':'" + rows[i].ID + "','strMonitorValue':'" + rows[i].MONITOR_VALUE_ID + "','strItemId':'" + rows[i].ITEM_ID + "','strUpperOperator':'" + rows[i].UPPER_OPERATOR + "','strLowerOperator':'" + rows[i].LOWER_OPERATOR + "','strUpperChar':'" + rows[i].DISCHARGE_UPPER + "','strLowerChar':'" + rows[i].DISCHARGE_LOWER + "','strUnit':'" + rows[i].UNIT + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == "true") {
                                    flag++;
                                    if (flag == rowcount) {
                                        manager.loadData();
                                        $.ligerDialog.success('更新数据操作成功！');
                                    }
                                    // $.ligerDialog.waitting('更新数据操作已成功！'); setTimeout(function () { $.ligerDialog.closeWaitting(); }, 500);
                                }
                                else {
                                    $.ligerDialog.warn('更新数据操作失败！');
                                }
                            },
                            error: function () {
                                $.ligerDialog.warn('Ajax请求数据失败！');
                            }
                        });
                    }
                    break;
                default:
                    break;

            }
        };
    }


    function isExistItem(strMonitor, strItem, strstanid, strconid) {
        var flag = false;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EvaluationTapSetting.aspx?action=isExistItem&strMonitorId=" + strMonitor + "&standartId=" + strstanid + "&strConditionId=" + strconid + "&strItemId=" + strItem,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != "0") {
                    flag = true;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求监测项目数据失败！');
            }
        });
        return flag
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
            url: "EvaluationTapSetting.aspx/GetMonitorSubItems",
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

    //    $("#btnSeach").bind("click", function () {
    //        //移除上次查询的底色
    //        $('#listLeft option').css({ 'background-color': '' })
    //        //获取所有包含查询内容的文本ListBox,并遍历
    //        $('#listLeft option:contains("' + $('#txtSeach').val() + '")').each(function () {
    //            //1、查询内容和td完全相同才改变ListBox项背景颜色
    //            //if($(this).text() == $('#txtSeach').val()){
    //            //$(this).css({'background-color':'red'});
    //            //}

    //            //2、改变所有满足条件的ListBox背景色
    //            $(this).css({ 'background-color': 'red' });
    //        });

    //    })
    function getItems(standId, condtionId, monitorId) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "EvaluationTapSetting.aspx/GetSelectedMonitorItems",
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