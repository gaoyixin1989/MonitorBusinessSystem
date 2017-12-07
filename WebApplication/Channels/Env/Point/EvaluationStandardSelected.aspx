<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Env_Point_EvaluationStandardSelected" Codebehind="EvaluationStandardSelected.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../../../Controls/zTree3.4/css/demo.css" type="text/css" />
    <link rel="stylesheet" href="../../../Controls/zTree3.4/css/zTreeStyle/zTreeStyle.css"
        type="text/css" />
    <script type="text/javascript" src="../../../Controls/zTree3.4/js/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../../../Controls/zTree3.4/js/jquery.ztree.core-3.4.js"></script>
    <script type="text/javascript" src="../../../Controls/zTree3.4/js/jquery.ztree.excheck-3.4.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var setting = {
                data: {
                    key: {
                        name: "NAME"
                    },
                    simpleData: {
                        enable: true,
                        idKey: "ID",
                        pIdKey: "PARENT_ID",
                        rootPid: "0"
                    }
                },
                async: {
                    enable: true,
                    url: "EvaluationStandardSelected.aspx",
                    otherParam: { "type": "getTreeInfo", "strMonitorId": $("#hiddenMonitorId").val() }
                },
                callback: {
                    beforeClick: beforeClick,
                    onClick: onClick,
                    onAsyncSuccess: onNodeCreated
                }
            };
            function beforeClick(treeId, treeNode, clickFlag) {
                return (treeNode.click != false);
            };
            function onClick(event, treeId, treeNode, clickFlag) {

            }
            function onNodeCreated() {
                nodeSelected($("#hiddenSelectedValue").val());
            }
            //加载菜单
            $.fn.zTree.init($("#tree"), setting);
        });
        //选中条件项节点
        function nodeSelected(strConditionId) {
            var treeObj = $.fn.zTree.getZTreeObj("tree");
            var nodes = treeObj.transformToArray(treeObj.getNodes());
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].ID == strConditionId && nodes[i].TYPE == '0') {
                    treeObj.selectNode(nodes[i]); break;
                }
            }
        }
        //获取已经选择的条件项名称
        function getConditionText() {
            var strValue = "";
            var treeObj = $.fn.zTree.getZTreeObj("tree");
            var nodes = treeObj.getSelectedNodes();
            if (nodes.length > 0)
                strValue = nodes[0].NAME;
            return strValue;
        }
        //获取已经选择的条件项值
        function getConditionValue() {
            var strValue = "";
            var treeObj = $.fn.zTree.getZTreeObj("tree");
            var nodes = treeObj.getSelectedNodes();
            if (nodes.length > 0)
                strValue = nodes[0].ID;
            return strValue;
        }
        //获取评价标准名称
        function getEvaluationStandardName() {
            var strValue = "";
            var treeObj = $.fn.zTree.getZTreeObj("tree");
            var nodes = treeObj.getSelectedNodes();
            var nodesAll = treeObj.transformToArray(treeObj.getNodes());
            if (nodes.length > 0) {
                var strEvaluationStandard = nodes[0].STANDARD_ID;
                for (var i = 0; i < nodesAll.length; i++) {
                    if (nodesAll[i].ID == strEvaluationStandard && nodesAll[i].TYPE == "0") {
                        strValue = nodesAll[i].NAME; break;
                    }
                }
            }
            return strValue;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; height: 100%; float: left; overflow: scroll; overflow-x: hidden;
        left: 100px; overflow-y: auto; background-color: White;" id="div_Tree">
        <ul id="tree" class="ztree" style="height: 95%">
        </ul>
    </div>
    <input type="hidden" id="hiddenMonitorId" runat="server" />
    <input type="hidden" id="hiddenSelectedValue" runat="server" />
    </form>
</body>
</html>
