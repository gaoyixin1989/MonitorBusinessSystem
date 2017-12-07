<%@ Page Language="C#" MasterPageFile="~/Masters/InputEx.master" AutoEventWireup="True" Inherits="Channels_Base_Company_SelectST" Codebehind="SelectST.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../../../Controls/zTree3.4/css/demo.css" type="text/css">
	<link rel="stylesheet" href="../../../Controls/zTree3.4/css/zTreeStyle/zTreeStyle.css" type="text/css">
	<script type="text/javascript" src="../../../Controls/zTree3.4/js/jquery-1.4.4.min.js"></script>
	<script type="text/javascript" src="../../../Controls/zTree3.4/js/jquery.ztree.core-3.4.js"></script>
	<script type="text/javascript" src="../../../Controls/zTree3.4/js/jquery.ztree.excheck-3.4.js"></script>
    
    <script type="text/javascript">
        var setting = {
            data: {
				key: {
					title:"t"
				},
				simpleData: {
					enable: true
				}
			},
			callback: {
				beforeClick: beforeClick,
				onClick: onClick
			}
        };
        
        var zNodes = <%= stData.ToString() %>;
        

        $(document).ready(function () {
            $("#div_Tree").css("height", 304);

            var zTreeObj =$.fn.zTree.init($("#treeST"), setting, zNodes);
            var strSelNode ="<%= strSelNodeId.ToString() %>"; 
            if(strSelNode!="00")
            {
                var nodes = zTreeObj.getNodesByParam("id", strSelNode, null);
                zTreeObj.selectNode(nodes[0]);
            }
        });

        //职位树的beforeClick事件
        function beforeClick(treeId, treeNode, clickFlag) {
            return (treeNode.click != false);
        };

        //职位树的onClick事件
        function onClick(event, treeId, treeNode, clickFlag) {
            if (!treeNode) {
                return;
            }
            if (treeNode.IsSt == "0"){
                $("#hidSelNode").val(treeNode.id);
                $("#hidpNode").val(treeNode.pCode);
            }
        }

        function f_select() {
            return $("#hidSelNode").val() + "|" + $("#hidpNode").val();
        }

        function request(strParame) {
            var args = new Object();
            var query = location.search.substring(1);

            var pairs = query.split("&"); // Break at ampersand 
            for (var i = 0; i < pairs.length; i++) {
                var pos = pairs[i].indexOf('=');
                if (pos == -1) continue;
                var argname = pairs[i].substring(0, pos);
                var value = pairs[i].substring(pos + 1);
                value = decodeURIComponent(value);
                args[argname] = value;
            }
            return args[strParame];
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
    <div style="width: 100%;height:100%; float: left; overflow: scroll; overflow-x: hidden;
        left: 100px; overflow-y: hidden; background-color: White;" id="div_Tree" >
        <ul id="treeST" class="ztree" style="height:100%"></ul>
    </div>
    <input type="hidden" id="hidSelNode" name="hidSelNode"  />
    <input type="hidden" id="hidpNode" name="hidpNode"  />
</asp:Content>