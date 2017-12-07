<%@ Page Language="C#" AutoEventWireup="True" Inherits="Portal_Index" Codebehind="Index.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <style type="text/css">
        html
        {
            overflow: hidden; /* 隐藏右侧滚动条，不可修改或改动位置 */
        }
        #iFrameLeftMenu
        {
            height: -7px;
        }
       body{margin:0; padding:0;}
    </style>
    <!--CSS样式加载-->
    <link href="" rel="stylesheet" type="text/css" id="switchSkinControl" />
    <link href="" rel="stylesheet" type="text/css" id="switchSkin" />
    <link href="../App_Themes/default/styles/main.css" rel="stylesheet" type="text/css" />
    <link href="../Controls/zTree3.4/css/zTreeStyle/zTreeStyle.css" rel="stylesheet"  type="text/css" />

    <link href="../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" />  
    <!--Script样式加载-->
    <script src="../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Controls/zTree3.4/js/jquery.ztree.core-3.4.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-cookie/jquery.cookie.js" type="text/javascript"></script>
    <script src="../Scripts/setState.js" type="text/javascript"></script>

    <script src="../Controls/ligerui/lib/ligerUI/js/ligerui.min.js" type="text/javascript"></script>  
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#username").focus();
            if ($.cookie("skin") == null) {
                $.cookie("skin", "default", { path: '/' });
            }
            $("#switchSkin").attr("href", "../App_Themes/" + $.cookie("skin") + "/styles/main.css");

            $("#div_logout").click(function () {
                if (confirm("是否要退出系统？")) {
                    $.get("../portal/login.aspx?logout=1", function () {
                        window.location = "../portal/login.aspx";
                    });
                }
            });

            
        })

        $(document).ready(function () {
            
        })

       
    </script>
</head>
<body>
    <script type="text/javascript">
        var setting = {
             view: {
				    showIcon: false  //是否显示图标
			},
	        data: {
	                simpleData: {
	                enable: true
	                }
	        },
            callback: {
                    //单击事件
				    onClick: onClick
			}
	    };
        //动态一次加载多个ztree的值，无限极
        <%=LoadNodesData %>
        

        function onClick(e,treeId, treeNode) {
                var zTree = $.fn.zTree.getZTreeObj(treeId);
	            zTree.expandNode(treeNode);
                MenuTreeClick(treeNode);
		}

        var layout,tab;
        //tabid计数器，保证tabid不会重复
        var tabidcounter = 0;

        function MenuTreeClick(treeNode) {
            var tabid = "tabid" + treeNode.id;
            var url = treeNode.menuurl;
            
            f_addTab(tabid, treeNode.name, url);
            if (tab.isTabItemExist(tabid)){
                tab.reload(tabid);
            }
        }

        //增加tab项的函数
        function f_addTab(tabid, text, url) {
            if (!tab) return;
            if (!url || url.length==0) return;
            if (!tabid) {
                tabidcounter++;
                tabid = "tabid" + tabidcounter;
            }
            
            tab.addTabItem({ tabid: tabid, text: text, url: url });
        }

        //重载tab项的函数
        function f_overTab( text, url) {
            if (!tab) return;
            if (!url || url.length==0) return;
            tab.overrideSelectedTabItem({ text: text, url: url });
        }

         //重载tab项的函数
        function f_overTabovId( tabid,text, url) {
            if (!tab) return;
            if (!url || url.length==0) return;
            tab.overrideSelectedTabItem({tabid:tabid, text: text, url: url });
        }
        //窗口改变时的处理函数
        function f_heightChanged(options) {
            if (tab)
                tab.addHeight(options.diff);
        }

	    $(document).ready(function () {
            //动态加载多个ztree
            <%=TreeLoad %>
            
            layout = $("#mainbody").ligerLayout({ height: '100%', heightDiff: -3, leftWidth: 140, onHeightChanged: f_heightChanged, minLeftWidth: 120 });
            tab = $("#framecenter").ligerTab({ height: 600, contextmenu: true });
            $("#framecenter").css("height", $(window).height() - 80);
	    });
    </script>
    <div class="header">
        <!-- header start -->
        <div class="headerInner">
            <span>
                <p>
                    欢迎您，<%=LogInfo.UserInfo.REAL_NAME %><a href="#">设置</a>|<a id="div_logout" href="#">退出系统</a></p>
            </span>
        </div>
    </div>
    <!-- header stop -->
    <div class="wrapper">
        <div class="menu">
            <span class="mcleft"></span>
            <ul>
                <li><a href="#" onfocus="this.blur()"><em class="ic01"></em>系统首页</a></li>
                <li><a href="#" onfocus="this.blur()"><em class="ic02"></em>待办任务(3)</a></li>
                <li><a href="#" onfocus="this.blur()"><em class="ic03"></em>未读公告(2)</a></li>
                <li><a href="#" onfocus="this.blur()"><em class="ic04"></em>查看留言(1)</a></li>
                <li><a href="#" onfocus="this.blur()"><em class="ic05"></em>整理桌面</a></li>
                <li><a href="#" onfocus="this.blur()"><em class="ic06"></em>退出系统</a></li>
            </ul>
            <span class="mcright"></span>
        </div>
        <div class="left">
            <!-- left start-->
            <%=LoadDiv %>
        </div>
        <!-- left stop -->
        <div class="right" id="mainRight">
            <!-- right start -->
            <%--<iframe id="mainFrm" name="mainFrm" src="#" scrolling="yes" frameborder="0"></iframe>--%>
            <div position="center" id="framecenter"> 
                <div tabid="home" title="我的主页"> 
                    <iframe frameborder="0" name="home" id="home" src="Welcome.aspx"></iframe>
                </div> 
            </div> 
        </div>
        <!-- right stop -->
    </div>
</body>
</html>
