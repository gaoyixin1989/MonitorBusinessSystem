//拖拽节点部分-------------------------------
var autoExpandNode;
var curDragNodes;

//用于捕获拖拽节点移动到折叠状态的父节点后，即将自动展开该父节点之前的事件回调函数，并且根据返回值确定是否允许自动展开操作
//treeIdString：需要被展开的父节点 treeNode 所在 zTree 的 treeId，便于用户操控
//treeNodeJSON：要被自动展开的父节点 JSON 数据对象
//返回值是 true / false
function beforeDragOpen(treeId, treeNode) {
    autoExpandNode = treeNode;
    return true;
}

//用于捕获节点被展开的事件回调函数
//event：js event 对象
//treeIdString：对应 zTree 的 treeId，便于用户操控
//treeNodeJSON：被展开的节点 JSON 数据对象
function onExpand(event, treeId, treeNode) {
    if (treeNode === autoExpandNode) {
        
    }
}

//用于捕获节点被拖拽之前的事件回调函数，并且根据返回值确定是否允许开启拖拽操作
//treeIdString：被拖拽的节点 treeNodes 所在 zTree 的 treeId，便于用户操控
//treeNodesArray(JSON)：要被拖拽的节点 JSON 数据集合
//返回值是 true / false
function beforeDrag(treeId, treeNodes) {
    for (var i = 0, l = treeNodes.length; i < l; i++) {
        if (treeNodes[i].drag === false) {
            curDragNodes = null;
            return false;
        } else if (treeNodes[i].parentTId && treeNodes[i].getParentNode().childDrag === false) {
            curDragNodes = null;
            return false;
        }
    }
    curDragNodes = treeNodes;
    return true;
}

//用于捕获节点被拖拽的事件回调函数
//如果设置了 setting.callback.beforeDrag 方法，且返回 false，将无法触发 onDrag 事件回调函数。
//eventjs event 对象
//treeIdString：被拖拽的节点 treeNodes 所在 zTree 的 treeId，便于用户操控
//treeNodesArray(JSON)：要被拖拽的节点 JSON 数据集合
function onDrag(event, treeId, treeNodes) {}

//用于捕获节点拖拽操作结束之前的事件回调函数，并且根据返回值确定是否允许此拖拽操作
//如未拖拽到有效位置，则不触发此回调函数，直接将节点恢复原位置
//treeIdString：目标节点 targetNode 所在 zTree 的 treeId，便于用户操控
//treeNodesArray(JSON)：被拖拽的节点 JSON 数据集合
//targetNodeJSON：treeNodes 被拖拽放开的目标节点 JSON 数据对象。如果拖拽成为根节点，则 targetNode = null
//moveTypeString：指定移动到目标节点的相对位置。"inner"：成为子节点，"prev"：成为同级前一个节点，"next"：成为同级后一个节点
//isCopyBoolean：拖拽节点操作是 复制 或 移动。true：复制；false：移动
//返回值是 true / false
function beforeDrop(treeId, treeNodes, targetNode, moveType, isCopy) 
{
    return true;
}

//用于捕获节点拖拽操作结束的事件回调函数
//如果设置了 setting.callback.beforeDrop 方法，且返回 false，将无法触发 onDrop 事件回调函数。
//eventjs event 对象
//treeIdString：目标节点 targetNode 所在 zTree 的 treeId，便于用户操控
//treeNodesArray(JSON)：被拖拽的节点 JSON 数据集合。
//targetNodeJSON：成为 treeNodes 拖拽结束的目标节点 JSON 数据对象。如果拖拽成为根节点，则 targetNode = null
//moveTypeString：指定移动到目标节点的相对位置。"inner"：成为子节点，"prev"：成为同级前一个节点，"next"：成为同级后一个节点。如果 moveType = null，表明拖拽无效
//isCopyBoolean：拖拽节点操作是 复制 或 移动。true：复制；false：移动
function onDrop(event, treeId, treeNodes, targetNode, moveType, isCopy) {
    for (var i=0,l=curDragNodes.length; i<l; i++) 
    {
	    var curPNode = treeNodes[i];
        var dragID = curPNode.id;
        var targetID = "";

        if (moveType == "inner") {
            targetID = targetNode.id;
        }
        else if (moveType == "prev") {
            targetID = targetNode.getParentNode().id;
        }
        else if (moveType == "next") {
            targetID = targetNode.getParentNode().id;
        }

        if (targetID != "") {
            $.post("PostTree.aspx?type=DragTreeNode&strDragNodeID=" + dragID + "&strTargetNodeID=" + targetID, function (data) {});
        }
    }

    return true;
}

//拖拽成为同级前一个节点
function dropPrev(treeId, nodes, targetNode) {
    var pNode = targetNode.getParentNode();
    if (pNode && pNode.dropInner === false) {
        return false;
    } else {
        for (var i = 0, l = curDragNodes.length; i < l; i++) {
            var curPNode = curDragNodes[i].getParentNode();
            if (curPNode && curPNode !== targetNode.getParentNode() && curPNode.childOuter === false) {
                return false;
            }
        }
    }
    return true;
}

//拖拽成为子节点
function dropInner(treeId, nodes, targetNode) {
    if (targetNode && targetNode.dropInner === false) {
        return false;
    } else {
        for (var i = 0, l = curDragNodes.length; i < l; i++) {
            if (!targetNode && curDragNodes[i].dropRoot === false) {
                return false;
            } else if (curDragNodes[i].parentTId && curDragNodes[i].getParentNode() !== targetNode && curDragNodes[i].getParentNode().childOuter === false) {
                return false;
            }
        }
    }
    return true;
}

//拖拽成为同级后一个节点
function dropNext(treeId, nodes, targetNode) {
    var pNode = targetNode.getParentNode();
    if (pNode && pNode.dropInner === false) {
        return false;
    } else {
        for (var i = 0, l = curDragNodes.length; i < l; i++) {
            var curPNode = curDragNodes[i].getParentNode();
            if (curPNode && curPNode !== targetNode.getParentNode() && curPNode.childOuter === false) {
                return false;
            }
        }
    }
    return true;
}

//编辑
var newCount = 1;

//用于当鼠标移动到节点上时，显示用户自定义控件，显示隐藏状态同 zTree 内部的编辑、删除按钮
//treeIdString：对应 zTree 的 treeId，便于用户操控
//treeNodeJSON：需要显示自定义控件的节点 JSON 数据对象
//这里主要自定义了添加和编辑按钮
function addHoverDom(treeId, treeNode) {
    var sObj = $("#" + treeNode.tId + "_span");
    if (treeNode.editNameFlag || $("#addBtn_" + treeNode.id).length > 0) return;

    var addStr = "<span class='button add' id='addBtn_" + treeNode.id
				+ "' title='添加' onfocus='this.blur();'></span>";
    addStr += "<span class='button edit' id='myEditBtn_" + treeNode.id
				+ "' title='编辑' onfocus='this.blur();'></span>";
    sObj.after(addStr);

    var btn = $("#addBtn_" + treeNode.id);
    if (btn) btn.bind("click", function () {
        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
        zTree.selectNode(treeNode);

        $("#hidAddEditID").get(0).value = treeNode.id;
        $("#POST_NAME").get(0).value = "";
        $("#PARENT_POST").get(0).value = treeNode.name;
        $("#ROLE_NOTE").get(0).value = "";

        $("#btnAdd").get(0).style.display = '';
        $("#btnEdit").get(0).style.display = 'none';
        $("#btnCancel").get(0).style.display = '';

        return false;
    });

    var btn = $("#myEditBtn_" + treeNode.id);
    if (btn) btn.bind("click", function () {
        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
        zTree.selectNode(treeNode);

        $("#hidAddEditID").get(0).value = treeNode.id;
        $("#POST_NAME").get(0).value = treeNode.name;
        if (treeNode.getParentNode() != null)
            $("#PARENT_POST").get(0).value = treeNode.getParentNode().name;
        else
            $("#PARENT_POST").get(0).value = "";
        for (var i = 0; i < $("#selPOST_DEPT").get(0).options.length; i++) {
            if ($("#selPOST_DEPT").get(0).options[i].value == treeNode.POST_DEPT_ID) {
                $("#selPOST_DEPT").get(0).options[i].selected = 'selected';
            }
        }
        for (var i = 0; i < $("#selPOST_LEVEL").get(0).options.length; i++) {
            if ($("#selPOST_LEVEL").get(0).options[i].value == treeNode.POST_LEVEL_ID) {
                $("#selPOST_LEVEL").get(0).options[i].selected = 'selected';
            }
        }
        $("#ROLE_NOTE").get(0).value = treeNode.ROLE_NOTE;

        $("#btnAdd").get(0).style.display = 'none';
        $("#btnEdit").get(0).style.display = '';
        $("#btnCancel").get(0).style.display = '';

        return false;
    });
};

//用于当鼠标移出节点时，隐藏用户自定义控件，显示隐藏状态同 zTree 内部的编辑、删除按钮
//treeIdString：对应 zTree 的 treeId，便于用户操控
//treeNodeJSON：需要隐藏自定义控件的节点 JSON 数据对象
function removeHoverDom(treeId, treeNode) {
    $("#addBtn_" + treeNode.id).unbind().remove();
    $("#myEditBtn_" + treeNode.id).unbind().remove();
};

//用于捕获节点被删除之前的事件回调函数，并且根据返回值确定是否允许删除操作
//treeIdString：对应 zTree 的 treeId，便于用户操控
//treeNodeJSON：将要删除的节点 JSON 数据对象
//返回值是 true / false
function beforeRemove(treeId, treeNode) {
    var zTree = $.fn.zTree.getZTreeObj("treeDemo");
    zTree.selectNode(treeNode);
    return confirm("确认删除职位： " + treeNode.name + " 吗？");
}

//用于捕获删除节点之后的事件回调函数。如果用户设置了 beforeRemove 回调函数，并返回 false，将无法触发 onRemove 事件回调函数。
//eventjs event 对象
//treeIdString：对应 zTree 的 treeId，便于用户操控
//treeNodeJSON：将要删除的节点 JSON 数据对象
function onRemove(e, treeId, treeNode) {
    var delID = treeNode.id;

    if (delID != "") {
        $.post("PostTree.aspx?type=DelTreeNode&strDelNodeID=" + delID , function (data) { });
    }

    return true;
}

//用于捕获节点编辑名称结束（Input 失去焦点 或 按下 Enter 键）之后，更新节点名称数据之前的事件回调函数，并且根据返回值确定是否允许更改名称的操作
//节点进入编辑名称状态后，按 ESC 键可以放弃当前修改，恢复原名称，取消编辑名称状态
//如果返回 false，zTree 将保持名称编辑状态，无法触发 onRename 事件回调函数，并且会导致屏蔽其它事件，直到修改名称使得 beforeRename 返回 true
//treeIdString：对应 zTree 的 treeId，便于用户操控
//treeNodeJSON：将要更改名称的节点 JSON 数据对象
//newNameString：修改后的新名称
//返回值是 true / false
function beforeRename(treeId, treeNode, newName) {
    return true;
}

//用于捕获节点编辑名称结束之后的事件回调函数。
//1、节点进入编辑名称状态，并且修改节点名称后触发此回调函数。如果用户设置了 beforeRename 回调函数，并返回 false，将无法触发 onRename 事件回调函数。
//2、如果通过直接修改 treeNode 的数据，并且利用 updateNode 方法更新，是不会触发此回调函数的。
//eventjs event 对象
//treeIdString；对应 zTree 的 treeId，便于用户操控
//treeNodeJSON：修改名称的节点 JSON 数据对象
function onRename(e, treeId, treeNode) {
}

//用于捕获节点编辑按钮的 click 事件，并且根据返回值确定是否允许进入名称编辑状态
//此事件回调函数最主要是用于捕获编辑按钮的点击事件，然后触发自定义的编辑界面操作。
//如果返回 false，节点将无法进入 zTree 默认的编辑名称状态
//treeIdString：对应 zTree 的 treeId，便于用户操控
//treeNodeJSON：将要进入编辑名称状态的节点 JSON 数据对象
//返回值是 true / false
function beforeEditName(treeId, treeNode) {
    return true;
}

//my data dict
//获取所属部门、行政级别下拉框内容
function sendData_callback() {
    if ($("#selPOST_DEPT").get(0).childNodes.length < 1) {
        $.post("PostTree.aspx?type=getDictDept", function (dicts) {
            if (dicts != null && dicts != "") {
                dictss = dicts.split("|");
                for (var i = 0; i < dictss.length; i++) {
                    strs = dictss[i].split(",");
                    $("#selPOST_DEPT").append("<option value=\"" + strs[0] + "\">" + strs[1] + "</option>");
                }
            }
        });
    }

    if ($("#selPOST_LEVEL").get(0).childNodes.length < 1) {
        $.post("PostTree.aspx?type=getDictLevel", function (dicts) {
            if (dicts != null && dicts != "") {
                dictss = dicts.split("|");
                for (var i = 0; i < dictss.length; i++) {
                    strs = dictss[i].split(",");
                    $("#selPOST_LEVEL").append("<option value=\"" + strs[0] + "\">" + strs[1] + "</option>");
                }
            }
        });
    }
}

//添加按钮
//addHoverDom自定义了添加和编辑按钮
//添加数据库、添加树节点
function btnAdd_onclick() {
    var strhidAddEditID = $("#hidAddEditID").get(0).value;
    var strAddParentID = $("#hidAddEditID").get(0).value;
    var strAddName = $("#POST_NAME").get(0).value;
    var strAddPOST_LEVEL_ID = $("#selPOST_LEVEL").get(0).value;
    var strAddPOST_DEPT_ID = $("#selPOST_DEPT").get(0).value;
    var strAddROLE_NOTE = $("#ROLE_NOTE").get(0).value;

    var strRequst = "&strAddParentID=" + strAddParentID + "&strPOST_NAME=" + escape(strAddName) + "&strPOST_LEVEL_ID=" + strAddPOST_LEVEL_ID + "&strPOST_DEPT_ID=" + strAddPOST_DEPT_ID + "&strROLE_NOTE=" + escape(strAddROLE_NOTE);

    $.post("PostTree.aspx?type=AddTreeNode" + strRequst, function (data) {
        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
        function filter(node) {
            return (node.id == strAddParentID);
        }
        var treeNode = zTree.getNodesByFilter(filter, true); // 仅查找一个节点

        if (treeNode) {
            var newNode = { open: true, dropRoot: false,isParent:true, icon: "../../Controls/zTree3.4/css/zTreeStyle/img/diy/2.png", id: data, pId: strhidAddEditID, name: strAddName, POST_DEPT_ID: strAddPOST_DEPT_ID, POST_LEVEL_ID: strAddPOST_LEVEL_ID, ROLE_NOTE: strAddROLE_NOTE };
            newNode = zTree.addNodes(treeNode, newNode);
        }

        if (!treeNode) {
            alert("职位节点被锁定，无法增加子节点");
        }
    });

    ClearEditInput();
}

//编辑按钮
//addHoverDom自定义了添加和编辑按钮
//编辑数据库、编辑树节点
function btnEdit_onclick() {
    var strhidAddEditID = $("#hidAddEditID").get(0).value;
    var strAddName = $("#POST_NAME").get(0).value;
    var strAddPOST_LEVEL_ID = $("#selPOST_LEVEL").get(0).value;
    var strAddPOST_DEPT_ID = $("#selPOST_DEPT").get(0).value;
    var strAddROLE_NOTE = $("#ROLE_NOTE").get(0).value;

    var strRequst = "&strEditPostID=" + strhidAddEditID + "&strPOST_NAME=" + escape(strAddName) + "&strPOST_LEVEL_ID=" + strAddPOST_LEVEL_ID + "&strPOST_DEPT_ID=" + strAddPOST_DEPT_ID + "&strROLE_NOTE=" + escape(strAddROLE_NOTE);

    $.post("PostTree.aspx?type=EditTreeNode" + strRequst, function (data) {
        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
        function filter(node) {
            return (node.id == strhidAddEditID);
        }
        var treeNode = zTree.getNodesByFilter(filter, true); // 仅查找一个节点

        if (treeNode) {
            treeNode.name = strAddName;
            treeNode.POST_DEPT_ID = strAddPOST_DEPT_ID;
            treeNode.POST_LEVEL_ID = strAddPOST_LEVEL_ID;
            treeNode.ROLE_NOTE = strAddROLE_NOTE;

            zTree.updateNode(treeNode);
        }
    });

    ClearEditInput();
}

//取消添加、编辑
function btnCancel_onclick() {
    ClearEditInput();
}

//清除添加、编辑是添加的编辑框内容
function ClearEditInput() {
    $("#hidAddEditID").get(0).value = "";
    $("#POST_NAME").get(0).value = "";
    $("#PARENT_POST").get(0).value = "";
    $("#selPOST_DEPT").get(0).options[0].selected = 'selected';
    $("#selPOST_LEVEL").get(0).options[0].selected = 'selected';
    $("#ROLE_NOTE").get(0).value = "";

    $("#btnAdd").get(0).style.display = 'none';
    $("#btnEdit").get(0).style.display = 'none';
    $("#btnCancel").get(0).style.display = 'none';
}