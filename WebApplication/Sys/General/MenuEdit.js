 //Create by Castle(胡方扬 2012-10-31)
$(document).ready(function () {
//    $("#layout1").ligerLayout({ height: '100%', leftWidth: 250,  allowLeftCollapse: false, allowRightCollapse: false });

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
            showIcon: true,
            selectedMulti: false
        },
        edit: {
            enable: true,
            showRenameBtn: false,
            showRemoveBtn: false
        },
        async: {
            enable: true,
            url: "MenuEdit.aspx",
            otherParam: { "type": "GetMenuChild" }
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
    var SltID, SltNodeName, StrParentID, StrParentName, IsShutCut, ImgValue;
    var ImgPath = "../../Images/Menu/";
    function addDom(treeId, treeNode) {
        //添加用户控件
        //alert(treeNode.id);
        var sObj = $("#" + treeNode.tId + "_a");
        if (treeNode.editNameFlag || $("#addBtn_" + treeNode.id).length > 0) return;
        var addStr = "<span class='button add' id='addBtn_" + treeNode.id
				+ "' title='添加子菜单' onfocus='this.blur();'></span>";
        addStr += "<span class='button remove' id='removeBtn_" + treeNode.id
				+ "' title='删除当前菜单' onfocus='this.blur();'></span>";
        sObj.after(addStr);
        //绑定添加按钮的用户控件事件
        var addBtn = $("#addBtn_" + treeNode.id);
        if (addBtn) addBtn.bind("click", clearInput);
        //绑定删除按钮的用户控件事件
        var deleteBtn = $("#removeBtn_" + treeNode.id);

        if (deleteBtn) deleteBtn.bind("click", function () {
            deleteNode();
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
        treeNodes[0].pId = node.id;
        zTree.updateNode(treeNodes[0]);
        var strValue = "";
        var spit = "";
        var Treenodes = zTree.transformToArray(zTree.getNodes());
        //        alert(Treenodes.length);
        for (var i = 0; i < Treenodes.length; i++) {
            //遍历所有节点，重新进行排序
            if (Treenodes[i].pId != null) {
                strValue = strValue + spit + i + "|" + Treenodes[i].id + "|" + Treenodes[i].pId;
                //                alert(strValue);
                spit = ",";
            }
        }
        //将数据排序信息发送至数据库
        $.ajax({
            cache: false,
            type: "POST",
            url: "MenuEdit.aspx/SortData",
            data: "{'strValue':'" + strValue + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == "true") {
                    //alert("菜单排序成功");
                    var manager = $.ligerDialog.waitting('数据保存成功！');
                    setTimeout(function () {
                        manager.close();
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
        //获取节点数据，将节点数据填充到输入框中
        loadNodeData(treeNode);
    }
    //将节点数据填充到输入框中
    function loadNodeData(treeNode) {
        //获取父亲节点
        var Parentnode = treeNode.getParentNode();
        //        alert(Parentnode);
        if (Parentnode != null) {
            StrParentID = Parentnode.id;
            StrParentName = Parentnode.name;
            $("#Parent_Text").val(StrParentName);
        }
        else {
            StrParentID = "-1";
            StrParentName = "";
            $("#Parent_Text").val("根节点无父节点");
        }
        SltID = treeNode.id;
        SltNodeName = treeNode.name;
        $("#MENU_TEXT").val(treeNode.name);
        if (treeNode.url == null && StrParentID != "-1") {
            //$("#MENU_URL").attr("disabled", "disabled");
            $("#MENU_URL").get(0).style.color = 'Gray';
            $("#MENU_URL").val("当前节点无超链接(点击编辑)");
            $("#MENU_URL").focus(function () { $("#MENU_URL").val(""); })
            $("#MENU_URL").blur(function () { $("#MENU_URL").val("当前节点无超链接(点击编辑)"); })
        }
        if (StrParentID == "-1") {
            $("#MENU_URL").val("根节点无超链接");
            $('#MENU_URL').unbind("focus");
            $('#MENU_URL').unbind("blur");
            $("#MENU_URL").attr("disabled", "disabled");
        }

        else {
            //$("#MENU_URL").removeAttr("disabled");
            //解除绑定事件
            $('#MENU_URL').unbind("focus");
            $('#MENU_URL').unbind("blur");
            $("#MENU_URL").get(0).style.color = 'Black';
            $("#MENU_URL").removeAttr("disabled");
            //填充数据 
            $("#MENU_URL").val(treeNode.url);

        }

        var iconsrc = treeNode.icon;
        if (iconsrc.length > 0) {
            var arryList = new Array();
            arryList = iconsrc.split("/");
            checkMenuIcon(arryList[4].toString())
        }

        var quickvalue = treeNode.IS_SHORTCUT;
        checkMenueQuick(quickvalue);
        //设置按钮可见性
        $("#btnAdd").get(0).style.display = 'none';
        $("#btnEdit").get(0).style.display = '';
    }
    //菜单图片显示 选择事件 focus 获取焦点时触发，blur失去焦点时触发 trIMGURL 是包含radio控件的上层控件的ID
    $("#trIMGURL input:radio").focus(function () {
        var radioname = $(this).attr("name");
        var existradio = $("#trIMGURL input:radio:checked").attr("name");
        $("#trIMGURL input:radio[name=" + existradio + "]").removeAttr("checked");
        $("#trIMGURL input:radio[name=" + radioname + "]").attr("checked", "checked");
    })
    //是否快捷菜单 选择事件
    $("#trRabIsShortcutMenu input:radio").focus(function () {
        //获取当前控件name或ID
        var radioname = $(this).attr("name");
        //获取已选中控件的name或ID
        var existradio = $("#trRabIsShortcutMenu input:radio:checked").attr("name");
        //清理已经选中的控件
        $("#trRabIsShortcutMenu input:radio[name=" + existradio + "]").removeAttr("checked");
        //选中控件
        $("#trRabIsShortcutMenu input:radio[name=" + radioname + "]").attr("checked", "checked");
    })

    //根据选定的项目，加载定位图片
    function checkMenuIcon(IconName) {
        $('#trIMGURL input:radio').removeAttr("checked");
        //        var RadioItem = $("#trIMGURL input:radio");
        //        if (RadioItem.length > 0) {
        //         
        //        }
        if (IconName == $("input[name='radiodata']").val()) {
            $('#trIMGURL input:radio').removeAttr("checked");
            $('input[name="radiodata"]').attr("checked", "true");
        }
        if (IconName == $("input[name='radiohelp']").val()) {
            $('#trIMGURL input:radio').removeAttr("checked");
            $("input[name='radiohelp']").attr("checked", "true");
        }
        if (IconName == $("input[name='radiomanage']").val()) {
            $('#trIMGURL input:radio').removeAttr("checked");
            $("input[name='radiomanage']").attr("checked", "true");
        }
        if (IconName == $("input[name='radiooset']").val()) {
            $('#trIMGURL input:radio').removeAttr("checked");
            $("input[name='radiooset']").attr("checked", "true");
        }
        if (IconName == $("input[name='radioset']").val()) {
            $('#trIMGURL input:radio').removeAttr("checked");
            $("input[name='radioset']").attr("checked", "true");
        }
        if (IconName == $("input[name='radiosysset']").val()) {

            $("input[name='radiosysset']").attr("checked", "true");
        }
    }

    //根据选定的项目，加载是否为快捷菜单值
    function checkMenueQuick(quickvalue) {
        $("#trRabIsShortcutMenu input:radio").removeAttr("checked");

        if (quickvalue == $("input[name='radiofalse']").val()) {
            $("input[name='radiofalse']").attr("checked", "true");
        }
        if (quickvalue == $("input[name='radiotrue']").val()) {
            $("input[name='radiotrue']").attr("checked", "true");
        }
    }
    //radio赋值
    function GetCheckedValue() {
        if ($('input[name="radiofalse"]:checked').val() != null) {
            IsShutCut = $("input[name='radiofalse']").val();
        }
        if ($('input[name="radiotrue"]:checked').val() != null) {
            IsShutCut = $("input[name='radiotrue']").val();
        }
        if ($('input[name="radiodata"]:checked').val() != null) {
            ImgValue = $("input[name='radiodata']").val();
        }
        if ($('input[name="radiomanage"]:checked').val() != null) {
            ImgValue = $("input[name='radiomanage']").val();
        }
        if ($('input[name="radiohelp"]:checked').val() != null) {
            ImgValue = $("input[name='radiohelp']").val();
        }
        if ($('input[name="radioset"]:checked').val() != null) {
            ImgValue = $("input[name='radioset']").val();
        }
        if ($('input[name="radiooset"]:checked').val() != null) {
            ImgValue = $("input[name='radiooset']").val();
        }
        if ($('input[name="radiosysset"]:checked').val() != null) {
            ImgValue = $("input[name='radiosysset']").val();
        }
    }

    //清空表单数据
    function clearInput() {
        $("#txtStatus").val("new");
        $("#Parent_Text").val(SltNodeName);
        $("#MENU_TEXT").val("");
        $('#MENU_URL').unbind("focus");
        $('#MENU_URL').unbind("blur");
        $("#MENU_URL").get(0).style.color = 'Black';

        $("#MENU_URL").val("");
        $("#MENU_URL").attr("disabled");
        $("input:radio").removeAttr("checked");
        //初始化radio的值
        $('input[name="radiodata"]').attr("checked", "true");
        $("input[name='radiofalse']").attr("checked", "true");

        //设置按钮可见性
        $("#btnAdd").get(0).style.display = '';
        $("#btnEdit").get(0).style.display = 'none';
    }

    //添加节点
    $("#btnAdd").bind("click", function () {
        var strMENU_TEXT = $("#MENU_TEXT").val();
        var strMENU_URL = $("#MENU_URL").val();
        //获取radio选项的值
        GetCheckedValue();
        //            alert(ImgValue + "|" + StrParentID + "|" + StrParentName);
        //在数据库中添加数据
        $.ajax({
            cache: false,
            type: "POST",
            url: "MenuEdit.aspx/CreateMenuNode",
            data: "{'ParentId':'" + SltID + "','MenuText':'" + strMENU_TEXT + "','StrUrl':'" + strMENU_URL + "','Icon':'" + ImgValue + "','IsShutCut':'" + IsShutCut + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d != "false") {
                    //将创建好的字典项加入节点中
                    var zTree = $.fn.zTree.getZTreeObj("tree");
                    var treeNode = zTree.getSelectedNodes()[0];
                    var newNode = { id: data.d, pID: StrParentID, name: strMENU_TEXT, url: strMENU_URL, icon: ImgPath + ImgValue, IS_SHORTCUT: IsShutCut };
                    newNode = zTree.addNodes(treeNode, newNode);
                    $.ligerDialog.success('创建菜单成功!');
                }
                else {
                    $.ligerDialog.warn('创建菜单失败!');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求数据失败');
            }
        });
    });
    //修改节点
    $("#btnEdit").bind("click", function () {
        var strMENU_TEXT = $("#MENU_TEXT").val();
        var strMENU_URL = $("#MENU_URL").val();
        GetCheckedValue();
        $.ajax({
            cache: false,
            type: "POST",
            url: "MenuEdit.aspx/EditMenuNode",
            data: "{'Menu_id':'" + SltID + "','ParentId':'" + StrParentID + "','MenuText':'" + strMENU_TEXT + "','StrUrl':'" + strMENU_URL + "','Icon':'" + ImgValue + "','IsShutCut':'" + IsShutCut + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == "true") {
                    //将修改好的字典项更新到节点中
                    var zTree = $.fn.zTree.getZTreeObj("tree");
                    var treeNode = zTree.getSelectedNodes()[0];
                    treeNode.name = strMENU_TEXT;
                    treeNode.url = strMENU_URL;
                    $("#MENU_TEXT").val(strMENU_TEXT);
                    $("#MENU_URL").val(strMENU_URL);
                    checkMenuIcon(ImgValue);
                    checkMenueQuick(IsShutCut);
                    zTree.updateNode(treeNode);
                    $.ligerDialog.success('修改菜单项成功!');
                    return;
                }
                else {
                    $.ligerDialog.warn('修改菜单项失败!');
                    return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求数据失败!');
                return;
            }
        });
    });

    //删除节点
    function deleteNode() {
        if (StrParentID == "-1") {
            $.ligerDialog.warn('根节点不允许删除，请联系数据库管理员！');
            return;
        }

        $.ligerDialog.confirm("确认删除选择的菜单项“" + SltNodeName + "”吗？\r", function (result) {
            confirmObj = result;
            $.ajax({
                cache: false,
                type: "POST",
                url: "MenuEdit.aspx/DelMenuNode",
                data: "{'Menu_id':'" + SltID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "true") {
                        var zTree = $.fn.zTree.getZTreeObj("tree");
                        var treeNode = zTree.getSelectedNodes()[0];
                        zTree.removeNode(treeNode);
                        $.ligerDialog.success('数据删除成功!');
                    }
                    else {
                        $.ligerDialog.warn('数据删除失败!');
                    }
                },
                error: function () {
                    $.ligerDialog.warn('Ajax请求数据失败!');
                }
            });
        });
        //if (confirmObj == false) return;
    }

    //加载菜单
    $.fn.zTree.init($("#tree"), setting);
});