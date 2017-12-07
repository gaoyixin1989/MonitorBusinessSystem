<%@ Page Language="C#" MasterPageFile="~/Masters/InputEx.master" AutoEventWireup="True" Inherits="Sys_General_PostMenu" Codebehind="PostMenu.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    
   
	<link rel="stylesheet" href="../../Controls/zTree3.4/css/zTreeStyle/zTreeStyle.css" type="text/css"/>
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../Controls/zTree3.4/js/jquery-1.4.4.min.js"></script>
	<script type="text/javascript" src="../../Controls/zTree3.4/js/jquery.ztree.core-3.4.js"></script>
	<script type="text/javascript" src="../../Controls/zTree3.4/js/jquery.ztree.excheck-3.4.js"></script>
    
    
    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" ></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>

    <script src="PostMenu.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        var setting = {
            view: {
                showIcon:false
			},
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

        var settingMenu = {
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
            callback: {
				beforeCheck: beforeCheck,
				onCheck: onCheck
			}
		};


        var zNodes = <%= postData.ToString() %>;
        var zNodesMenu = <%= MenuData.ToString() %>;

        var tab;

        //窗口改变时的处理函数
        function f_heightChanged(options) {
            if (tab)
                tab.addHeight(options.diff);
        }

        $(document).ready(function () {
            
            $("#layout1").ligerLayout({ height: '100%',  leftWidth: 250, allowLeftCollapse: false });

            var bodyHeight = $(".l-layout-center:first").height();

            tab=$("#navtab1").ligerTab({ height: bodyHeight}); 

            sendData_callback();

            $.fn.zTree.init($("#treePost"), setting, zNodes);
            $.fn.zTree.init($("#treeDemoMenu"), settingMenu, zNodesMenu);

			setCheck();
			$("#py").bind("change", setCheck);
			$("#sy").bind("change", setCheck);
			$("#pn").bind("change", setCheck);
			$("#sn").bind("change", setCheck);

            CheckMenuRootNodeOfRight();//职位权限
        });
    </script>

    
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphInput" runat="Server">
    <div id="layout1" style="text-align:left;overflow:hidden;" >
        <div position="left" title="职位或用户" style="overflow:hidden;">
            <div id="navtab1" style="overflow:hidden;">
                <div tabid="home" title="职位" lselected="true" >
                        <ul id="treePost" class="ztree" style="height:100%;overflow-y:auto;overflow-x:auto;"></ul>
                </div>
                <div  title="用户">
                        <p >&nbsp;&nbsp;&nbsp;&nbsp;部门：<select name="ddl_DEPT" id="ddl_DEPT" style="width: 100px;" onchange="return ddl_DEPT_onchange()"></select></p>
                        <select size="15" name="lb_User" id="lb_User" style="height:100%;width:100%;" onchange="return lb_User_onchange()"></select>
                </div>
            </div>
        </div>
        <div position="center"  title="权限设置" style="height:100%;">
           <ul id="treeDemoMenu" class="ztree"  style="height:100%;overflow-y:auto;overflow-x:auto;"></ul>
        </div>
    </div> 
    
    <input type="hidden" id="hidPostID" name="hidPostID"  />
    <input type="hidden" id="hidRightType" name="hidRightType"  />
</asp:Content>