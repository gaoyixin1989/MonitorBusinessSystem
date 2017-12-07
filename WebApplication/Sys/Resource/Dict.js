// Create by 熊卫华 2012.10.25  "字典项管理"功能

$(document).ready(function () {
    var setting = {
        check: {
            enable: false
        },
        data: {
            key: {
                name: "DICT_TEXT"
            },
            simpleData: {
                enable: true,
                idKey: "ID",
                pIdKey: "PARENT_CODE",
                rootPid: "0"
            }
        },
        view: {
            selectedMulti: false,
            showIcon: false
        },
        edit: {
            enable: true,
            showRenameBtn: false
        },
        async: {
            enable: true,
            url: "DictDataEdit.aspx",
            otherParam: { "type": "getDictInfo" }
        },
        callback: {
            onClick: onTreeClick,
            onDrop: zTreeOnDrop
        }
    };
    //设置按钮不可用
    setting.edit.showRemoveBtn = false;
    $("#btnAdd").get(0).style.display = 'none';
    $("#btnEdit").get(0).style.display = 'none';
    //******************************************添加用户控件*******************************************************
    function addDom(treeId, treeNode) {
        //添加用户控件
        var sObj = $("#" + treeNode.tId + "_a");

        var addStr = "<span class='button add' id='addBtn_" + treeNode.ID
				+ "' title='添加' onfocus='this.blur();'></span>";
        addStr += "<span class='button remove' id='removeBtn_" + treeNode.ID
				+ "' title='删除' onfocus='this.blur();'></span>";
        sObj.after(addStr);
        //绑定添加按钮的用户控件事件
        var addBtn = $("#addBtn_" + treeNode.ID);
        if (addBtn) addBtn.bind("click", clearInput);
        //绑定删除按钮的用户控件事件
        var deleteBtn = $("#removeBtn_" + treeNode.ID);
        if (deleteBtn) deleteBtn.bind("click", function () {
            deleteNode(treeId);
        });
    };
    //记录上次节点id
    var nodeIdTemp = "";
    //当鼠标移出节点的时候自动隐藏用户控件
    function removeDom(strNodeIdTemp) {
        $("#addBtn_" + nodeIdTemp).unbind().remove();
        $("#removeBtn_" + nodeIdTemp).unbind().remove();
    };
    function zTreeOnDrop(event, treeId, treeNodes, targetNode, moveType) {
        var zTree = $.fn.zTree.getZTreeObj(treeId);
        var node = treeNodes[0].getParentNode();
        treeNodes[0].PARENT_CODE = node.ID;
        zTree.updateNode(treeNodes[0]);
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
        nodeIdTemp = treeNode.ID;
        //获取节点数据，将节点数据填充到输入框中
        loadNodeData(treeNode);
    }
    //将节点数据填充到输入框中
    function loadNodeData(treeNode) {
        $("#txtStatus").val("edit");
        $("#txtHidden").val(treeNode.ID);
        $("#txtDictType").val(treeNode.DICT_TYPE);
        $("#txtDictCode").val(treeNode.DICT_CODE);
        $("#txtDictName").val(treeNode.DICT_TEXT);
        $("#txtRemark").val(treeNode.REMARK);

        //设置按钮可见性
        $("#btnAdd").get(0).style.display = 'none';
        $("#btnEdit").get(0).style.display = '';
    }
    //清空表单数据
    function clearInput() {

        var zTree = $.fn.zTree.getZTreeObj("tree");
        var treeNode = zTree.getSelectedNodes()[0];

        $("#txtStatus").val("new");
        $("#txtDictType").val(treeNode.DICT_CODE);
        $("#txtDictCode").val("");
        $("#txtDictName").val("");
        $("#txtRemark").val("");

        //设置按钮可见性
        $("#btnAdd").get(0).style.display = '';
        $("#btnEdit").get(0).style.display = 'none';
    }
    //添加节点
    $("#btnAdd").bind("click", function () {
        if ($("#txtStatus").val() == "new") {
            var strParentId = $("#txtHidden").val();
            var strDictType = encodeURIComponent($("#txtDictType").val());
            var strDictCode = encodeURIComponent($("#txtDictCode").val());
            var strDictName = encodeURIComponent($("#txtDictName").val());
            //var strRemark = encodeURIComponent($("#txtRemark").val());
            var strRemark = $("#txtRemark").val();
            //在数据库中添加数据
            $.ajax({
                cache: false,
                type: "POST",
                url: "DictDataEdit.aspx/createData",
                data: "{'strParentId':'" + strParentId + "','strDictType':'" + strDictType + "','strDictCode':'" + strDictCode + "','strDictName':'" + strDictName + "','strRemark':'" + strRemark + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d != "0") {
                        //将创建好的字典项加入节点中
                        var zTree = $.fn.zTree.getZTreeObj("tree");
                        var treeNode = zTree.getSelectedNodes()[0];
                        var newNode = { 'ID': data.d, 'DICT_TYPE': decodeURIComponent(strDictType), 'DICT_CODE': decodeURIComponent(strDictCode), 'DICT_TEXT': decodeURIComponent(strDictName), 'PARENT_CODE': strParentId, 'REMARK': decodeURIComponent(strRemark) };
                        newNode = zTree.addNodes(treeNode, newNode);
                        alert("创建字典项成功")
                    }
                    else {
                        alert("创建字典项失败")
                    }
                }
            });
        }
    });
    //修改节点
    $("#btnEdit").bind("click", function () {
        if ($("#txtStatus").val() == "edit") {
            var strId = $("#txtHidden").val();
            var strDictType = encodeURIComponent($("#txtDictType").val());
            var strDictCode = encodeURIComponent($("#txtDictCode").val());
            var strDictName = encodeURIComponent($("#txtDictName").val());
            //var strRemark = encodeURIComponent($("#txtRemark").val());
            var strRemark = $("#txtRemark").val();
            $.ajax({
                cache: false,
                type: "POST",
                url: "DictDataEdit.aspx/editData",
                data: "{'id':'" + strId + "','strDictType':'" + strDictType + "','strDictCode':'" + strDictCode + "','strDictName':'" + strDictName + "','strRemark':'" + strRemark + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        //将修改好的字典项更新到节点中
                        var zTree = $.fn.zTree.getZTreeObj("tree");
                        var treeNode = zTree.getSelectedNodes()[0];
                        treeNode.DICT_TYPE = $("#txtDictType").val();
                        treeNode.DICT_CODE = $("#txtDictCode").val();
                        treeNode.DICT_TEXT = $("#txtDictName").val();
                        treeNode.REMARK = $("#txtRemark").val();
                        zTree.updateNode(treeNode);
                        alert("修改字典项成功")
                    }
                    else {
                        alert("修改字典项失败")
                    }
                }
            });
        }
    });
    //删除节点
    function deleteNode(treeId) {
        var zTree = $.fn.zTree.getZTreeObj(treeId);
        var treeNode = zTree.getSelectedNodes()[0];
        if (treeNode.ID == "0") {
            alert("字典项根节点不允许删除");
            return;
        }
        var nodes = zTree.transformToArray(treeNode);
        var confirmObj = confirm("确认删除字典项“" + treeNode.DICT_TEXT + "”吗？\r注意：删除后可能导致系统不能正常运行，请谨慎操作!");
        if (confirmObj == false) return;
        var strValue = "";
        var spit = ""; ;
        for (var i = 0; i < nodes.length; i++) {
            var strId = nodes[i].ID;
            strValue = strValue + spit + strId;
            spit = ",";
        }
        $.ajax({
            cache: false,
            type: "POST",
            url: "DictDataEdit.aspx/deleteData",
            data: "{'strValue':'" + strValue + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    zTree.removeNode(treeNode);
                    alert("删除字典项数据成功")
                }
                else {
                    alert("删除字典项数据失败")
                }
            }
        });
    }
    //节点排序
    $("#btnSort").bind("click", function () {
        //收集节点数据
        var ztree = $.fn.zTree.getZTreeObj("tree");
        var strValue = "";
        var spit = "";
        var nodes = ztree.transformToArray(ztree.getNodes());
        for (var i = 0; i < nodes.length; i++) {
            var strId = nodes[i].ID;
            var strParentId = nodes[i].PARENT_CODE == null ? "0" : nodes[i].PARENT_CODE;
            strValue = strValue + spit + i + "|" + strId + "|" + strParentId;
            spit = ",";
        }
        //将数据排序信息发送至数据库
        $.ajax({
            cache: false,
            type: "POST",
            url: "DictDataEdit.aspx/sortData",
            data: "{'strValue':'" + strValue + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    alert("字典项排序成功");
                }
                else {
                    alert("字典项排序失败！");
                }
            }
        });
    });
    //加载菜单
    $.fn.zTree.init($("#tree"), setting);
});