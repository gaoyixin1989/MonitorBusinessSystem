<%@ Page Language="C#" AutoEventWireup="True" Inherits="Portal_IndexNew" Codebehind="IndexNew.aspx.cs" %>

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
        
        /* 顶部 */ 
        .l-topmenu{ margin:0; padding:0; height:24px; line-height:24px; background:#1B3160 url('../css/index/top.jpg') repeat-x bottom;  position:relative; border-top:1px solid #7C96AF; border-bottom:2px solid #4E7194;}
        .l-topmenu-logo{ color:#070A0C;  padding-left:30px; background:url('../images/index/topicon.gif') no-repeat 10px 4px;}
        .l-topmenu-welcome{  position:absolute; height:24px; right:30px; top:0px;color:#070A0C;}
        .l-topmenu-welcome a{ color:#070A0C; text-decoration:underline}
        .l-topmenu-username{ color:#070A0C; font-weight:bold;} 


        /* 菜单列表 */
        .menulist{ margin-left:2px; margin-right:2px; margin-top:2px;text-align:left; color:#000; }
        .menulist li{ height:24px; line-height:24px; padding-left:24px; display:block; position:relative; cursor:pointer; text-align:left;background:url('../css/index/menulist2013.gif') no-repeat;}
        .menulist li img{ position:absolute; left:4px; top:4px; width:16px; height:16px;}
        .menulist li.over{ background:url('../css/index/menuitem.gif') repeat-x 0px 0px; margin-bottom:4px;}
        .menulist li.over .menuitem-l{background:url('../css/index/menuitem.gif') repeat-x 0px -24px; width:15px; height:24px; position:absolute; left:0; top:0;}
        .menulist li.over .menuitem-r{background:url('../css/index/menuitem.gif') no-repeat -15px -24px ; width:1px; height:24px; position:absolute; right:0; top:0;}
    </style>
    <!--CSS样式加载-->
    <!--<link href="../App_Themes/default/styles/main.css" rel="stylesheet" type="text/css" />-->
   

    <link href="../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" />  
     <link href="../App_Themes/default/styles/index2013.css" rel="stylesheet" type="text/css" />
    <!--Script样式加载-->
    <script src="../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-cookie/jquery.cookie.js" type="text/javascript"></script>

    <script src="../Controls/ligerui/lib/ligerUI/js/ligerui.min.js" type="text/javascript"></script>  
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#username").focus();
            //            if ($.cookie("skin") == null) {
            //                $.cookie("skin", "default", { path: '/' });
            //            }
            //            $("#switchSkin").attr("href", "../App_Themes/" + $.cookie("skin") + "/styles/main.css");

            $("#div_logout").click(function () {
                if (confirm("是否要退出系统？")) {
                    //先清除session 再跳转 胡方扬 2013-02-21
                                        $.get("../portal/login.aspx?logout=1", function () {
                                            window.location = "../portal/login.aspx";
                                        });
                }
            });

        })
    </script>
    <script type="text/javascript">
        //几个布局的对象
        var layout, tab, accordion;
        //tabid计数器，保证tabid不会重复
        var tabidcounter = 0;
        
        //窗口改变时的处理函数
        function f_heightChanged(options) {
            if (tab)
                tab.addHeight(options.diff);
            if (accordion && options.middleHeight - 24 > 0)
                accordion.setHeight(options.middleHeight - 24);
        }
        //增加tab项的函数
        function f_addTab(tabid, text, url) {
            if (!tab) return;

            if (!tabid) {
                tabidcounter++;
                tabid = "tabid" + tabidcounter;
            }

            if (tab.isTabItemExist(tabid)) {

                tab.removeTabItem(tabid);
            }

            tab.addTabItem({ tabid: tabid, text: text, url: url });
        }

        //重载tab项的函数
        function f_overTab(text, url) {
            if (!tab) return;
            if (!url || url.length == 0) return;
            tab.overrideSelectedTabItem({ text: text, url: url });
        }

        //关闭当前打开的窗口
        function f_removeSelectedTabs() {
            if (!tab) return;
            tab.removeSelectedTabItem();
        }

        //重载tab项的函数
        function f_overTabovId(tabid, text, url) {
            if (!tab) return;
            if (!url || url.length == 0) return;
            tab.overrideSelectedTabItem({ tabid: tabid, text: text, url: url });
        }

        $(document).ready(function () {
            //菜单初始化
            $("ul.menulist li").live('click', function () {
                var jitem = $(this);
                var tabid = jitem.attr("tabid");
                var url = jitem.attr("url");
                if (!url) return;
                if (!tabid) {
                    tabidcounter++;
                    tabid = "tabid" + tabidcounter;
                    jitem.attr("tabid", tabid);

                    //给url附加menuno
                    if (url.indexOf('?') > -1) url += "&";
                    else url += "?";
                    url += "MenuNo=" + jitem.attr("menuno");
                    jitem.attr("url", url);
                }
                f_addTab(tabid, $("span:first", jitem).html(), url);
            }).live('mouseover', function () {
                var jitem = $(this);
                jitem.addClass("over");
            }).live('mouseout', function () {
                var jitem = $(this);
                jitem.removeClass("over");
            });

            //布局初始化 
            //layout
            layout = $("#mainbody").ligerLayout({ height: '100%', heightDiff: -3, leftWidth: 180, onHeightChanged: f_heightChanged, minLeftWidth: 120 });
            var bodyHeight = $(".l-layout-center:first").height();
            //Tab
            tab = $("#framecenter").ligerTab({ height: bodyHeight, contextmenu: true, onAfterSelectTabItem: function (tabid) {
                tab.reload(tabid);
                }, onAfterRemoveTabItem: function (tabid) {
                    var selectTabID = tab.getSelectedTabItemID();
                    tab.reload(selectTabID);
                }
            });
//            tab = $("#framecenter").ligerTab({ height: bodyHeight, contextmenu: true});

            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: "IndexNew.aspx?type=getMenu&localUserID=" + $("#localUserID").val(),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (menus, textStatus) {
                    var mainmenu = $("#mainmenu");
                    $(menus).each(function (i, menu) {
                        var item = $('<div title="' + menu.MenuName + '"><ul class="menulist"></ul></div>');

                        $(menu.children).each(function (j, submenu) {
                            var subitem = $('<li><span></span><div class="menuitem-l"></div><div class="menuitem-r"></div></li>');
                            subitem.attr({
                                url: submenu.MenuUrl,
                                menuno: submenu.MenuNo
                            });
                            $("span", subitem).html(submenu.MenuName || submenu.text);

                            $("ul:first", item).append(subitem);
                        });
                        mainmenu.append(item);

                    });

                    //Accordion
                    accordion = $("#mainmenu").ligerAccordion({ height: bodyHeight - 24, speed: null });
                }
            });
        });
    </script>
</head>
<body>

    <div class="header"  id="headBody">
<div class="headertop">
<a href="#" class="logo2013">环境监测业务管理系统logo</a>
</div>
<div class="headeright"><a href="javascript:">设置</a><span></span><a href="javascript:" id="div_logout" class="esc" >退出</a></div>
 <p class="headewelcome">欢迎您，<asp:Label runat="server" ID="lbWecomeUserName" Visible="false"></asp:Label> <%=LogInfo.UserInfo.REAL_NAME %></p>
</div>


    <div id="mainbody" style="width:100%;">
        <!--左边开始-->
        <div position="left"  title="系统菜单" id="accordion1"> 
            <div position="left"  id="mainmenu"></div>  
        </div>
        <!--左边结束-->
        
        <!--右边开始-->

        <div position="center" id="framecenter"  > 
            <%--<div  id="mainRight">--%>
                <!-- right start -->
                <div tabid="home" title="我的主页"> 
                    <iframe frameborder="0" name="home" id="home" src="Welcome_ZZ_New.aspx"></iframe> 
                </div> 
            <%--</div>--%>
        </div>
        <input type="text" id="localUserID" runat="server" style="display:none" />
        <!--右边结束-->
    </div>
</body>
</html>
