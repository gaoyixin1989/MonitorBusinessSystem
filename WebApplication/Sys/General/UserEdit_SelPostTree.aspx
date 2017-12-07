<%@ Page Language="C#" MasterPageFile="~/Masters/InputEx.master" AutoEventWireup="True" Inherits="Sys_General_UserEdit_SelPostTree" Codebehind="UserEdit_SelPostTree.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../../Controls/zTree3.4/css/demo.css" type="text/css">
	<link rel="stylesheet" href="../../Controls/zTree3.4/css/zTreeStyle/zTreeStyle.css" type="text/css">
	<script type="text/javascript" src="../../Controls/zTree3.4/js/jquery-1.4.4.min.js"></script>
	<script type="text/javascript" src="../../Controls/zTree3.4/js/jquery.ztree.core-3.4.js"></script>
	<script type="text/javascript" src="../../Controls/zTree3.4/js/jquery.ztree.excheck-3.4.js"></script>
    <script src="../../Scripts/comm.js" type="text/javascript"></script>
    <script type="text/javascript">
    var strPostSelIDs = request('strPostSelIDs');
    if (!strPostSelIDs)
        strPostSelIDs = "";

    var setting = {
		view: {
			selectedMulti: false
		},
        check: {
			enable: true
		},
		data: {
			simpleData: {
				enable: true
			}
		},
        view: {
		    showIcon: false
	    }
	};

    var zNodes = <%= postData.ToString() %>;

    $(document).ready(function () {
        $.fn.zTree.init($("#treePost"), setting, zNodes);

		setCheck();
        checkNode();
    });

    //设置选择子节点不会关联父节点，设置取消节点不会关联父节点和子节点
    function setCheck() {
        var zTree = $.fn.zTree.getZTreeObj("treePost"),
			    type = { "Y": "", "N": "" };
        zTree.setting.check.chkboxType = type;
    }

    function checkNode(){
        var zTree = $.fn.zTree.getZTreeObj("treePost");
        zTree.checkAllNodes(false);

        var data = strPostSelIDs.split("，");
        for (var i = 0; i < data.length; i++) {
            var strs = data[i];

            function filter(node) {
                return (node.id == strs);
            }

            var treeNode = zTree.getNodesByFilter(filter, true); // 仅查找一个节点

            if (treeNode) {
                zTree.checkNode(treeNode, true, false, false);
            }
        }
    }

    function f_select() {
        var strSelID = "";
        var strSelName = "";

        var zTree = $.fn.zTree.getZTreeObj("treePost");
        var checkNodes = zTree.getCheckedNodes(true);
        
        if (checkNodes) {
            for (var i = 0, l = checkNodes.length; i < l; i++) {
                strSelID += (strSelID.length > 0 ? "，" : "") + checkNodes[i].id;
                strSelName += (strSelName.length > 0 ? "，" : "") + checkNodes[i].name;
            }
        }
        
        return strSelID + "|" + strSelName;
    }
    </script>

    <style type="text/css">
        html
        {
            overflow-x: hidden; /*隐藏水平滚动条*/
            overflow-y: hidden; /*隐藏垂直滚动条*/
        }
        body
        {
            padding: 0;
            margin: 0;
            font-size: 12px;
            list-style: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphInput" runat="Server">
    <div style="width: 100%;height:100%; float: left; overflow: scroll; overflow-x: hidden; left: 100px; overflow-y: auto; background-color: White;" id="div_Tree" >
        <ul id="treePost" class="ztree" style="height:100%"></ul>
    </div>
    <input type="hidden" id="hidPostID" name="hidPostID"  />
    <input type="hidden" id="Hidden1" name="hidPostName"  />
</asp:Content>