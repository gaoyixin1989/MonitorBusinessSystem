//职位树的beforeClick事件
function beforeClick(treeId, treeNode, clickFlag) {
    return (treeNode.click != false);
}

//职位树的onClick事件
function onClick(event, treeId, treeNode, clickFlag) {
    if (!treeNode) {
        return;
    }
    CheckMenuNodeOfRight(treeNode.id, "2");
}

//展示职位树第一个节点的权限（打开页面时使用）
function CheckMenuRootNodeOfRight() {
    var zTree = $.fn.zTree.getZTreeObj("treePost");

    var nodes = zTree.getNodes();
    if (!nodes) {
        var zTree = $.fn.zTree.getZTreeObj("treeDemoMenu");
        zTree.checkAllNodes(false);
        return;
    }
    var firstNode = nodes[0];
    if (firstNode) {
        zTree.selectNode(firstNode, false);
        CheckMenuNodeOfRight(firstNode.id, "2");
    }
}

//根据指定用户或者职位，展示权限
function CheckMenuNodeOfRight(strPostID, strRightType) {
    $("#hidPostID").get(0).value = strPostID;
    $("#hidRightType").get(0).value = strRightType;

    var zTree = $.fn.zTree.getZTreeObj("treeDemoMenu");
    zTree.checkAllNodes(false);

    $.post("PostMenu.aspx?type=ClickPostNode&strRightType=" + strRightType + "&ClickPostID=" + strPostID, function (data) {
        if (data != null && data != "") {
            data = data.split("|");
            for (var i = 0; i < data.length; i++) {
                strs = data[i];

                function filter(node) {
                    return (node.id == strs);
                }

                var treeNode = zTree.getNodesByFilter(filter, true); // 仅查找一个节点

                if (treeNode) {
                    zTree.checkNode(treeNode, true, false, false);
                }
            }
        }
    });
}

//设置选择子节点会关联父节点，设置取消节点会关联父节点和子节点
function setCheck() {
    var zTree = $.fn.zTree.getZTreeObj("treeDemoMenu"),
			py = "p",
			sy = "",
			pn = "p",
			sn = "s",
			type = { "Y": py + sy, "N": pn + sn };
    zTree.setting.check.chkboxType = type;
}

//权限树的beforeCheck事件
function beforeCheck(treeId, treeNode) {
    return (treeNode.doCheck !== false);
}

//权限树的onCheck事件
function onCheck(e, treeId, treeNode) {
    var strRightType = $("#hidRightType").get(0).value;

    var zTree = $.fn.zTree.getZTreeObj("treeDemoMenu");
    var checkNodes = zTree.getCheckedNodes(true);

    if (checkNodes) {
        var strCheckNodeIDs = "";
        for (var i = 0, l = checkNodes.length; i < l; i++) {
            if (strCheckNodeIDs.length > 0)
                strCheckNodeIDs = strCheckNodeIDs + "|" + checkNodes[i].id;
            else
                strCheckNodeIDs = strCheckNodeIDs + checkNodes[i].id;
        }

        $.post("PostMenu.aspx?type=CheckMenuNode&CheckPostID=" + $("#hidPostID").get(0).value + "&strChkMenuNodes=" + strCheckNodeIDs + "&strRightType=" + strRightType, function (dicts) {
            return;
        });
    }
}


//my data dict
//获取所属部门下拉框内容
function sendData_callback() {
    if ($("#ddl_DEPT").get(0).childNodes.length < 1) {
        $.post("PostMenu.aspx?type=getDictDept", function (dicts) {
            if (dicts != null && dicts != "") {
                dictss = dicts.split("|");
                for (var i = 0; i < dictss.length; i++) {
                    strs = dictss[i].split(",");
                    $("#ddl_DEPT").append("<option value=\"" + strs[0] + "\">" + strs[1] + "</option>");
                }

                ShowUserLstUnderDept(true);
            }
        });
    }
}

//部门下拉框onchange事件
function ddl_DEPT_onchange() {
    ShowUserLstUnderDept(false);
}

//根据部门下拉框选中部门，展示对应的用户
//增加标志，true是打开页面时调用，不显示权限树，避免和职位的权限展示冲突
function ShowUserLstUnderDept(isLoadPage) {
    if ($("#ddl_DEPT").get(0).childNodes.length < 1)
        return;
    var strSelValue = $("#ddl_DEPT").get(0).options[$("#ddl_DEPT").get(0).options.selectedIndex].value;

    $.post("PostMenu.aspx?type=changeDdl_DictDept&strSelDept=" + strSelValue, function (dicts) {
        $("#lb_User").empty();
        if (dicts != null && dicts != "") {
            dictss = dicts.split("|");
            for (var i = 0; i < dictss.length; i++) {
                strs = dictss[i].split(",");
                $("#lb_User").append("<option value=\"" + strs[0] + "\">" + strs[1] + "</option>");
            }

            if (!isLoadPage) {
                $("#lb_User").get(0).options[0].selected = "selected ";
                ShowSelectUserRight();
            }
        }
        else {
            if (!isLoadPage) {
                var zTree = $.fn.zTree.getZTreeObj("treeDemoMenu");
                zTree.checkAllNodes(false);
            }
        }
    });
}

//用户列表onchange事件
function lb_User_onchange() {
    ShowSelectUserRight();
}

//根据选中用户展示权限
function ShowSelectUserRight() {
    if ($("#lb_User").get(0).childNodes.length < 1)
        return;

    var strUserID = $("#lb_User").get(0).options[$("#lb_User").get(0).options.selectedIndex].value;
    CheckMenuNodeOfRight(strUserID, "1");
}