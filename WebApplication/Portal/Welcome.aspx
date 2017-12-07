<%@ Page Language="C#" AutoEventWireup="True" Inherits="n22.Portal_Welcome" Codebehind="Welcome.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../App_Themes/default/styles/index2013.css" rel="stylesheet" type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <!--Jquery 基础文件-->
    <script src="../Scripts/news.js" type="text/javascript"></script>
    <script src="../Scripts/tab.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function ($) {

            //上下滑动选项卡切换
            $("#move-animate-top").tabso({
                cntSelect: "#topcon",
                tabEvent: "mouseover",
                tabStyle: "move-animate",
                direction: "top"
            });

            //左右滑动选项卡切换
            $("#move-animate-left").tabso({
                cntSelect: "#leftcon",
                tabEvent: "mouseover",
                tabStyle: "move-animate",
                direction: "left"
            });

            //淡隐淡现选项卡切换
            $("#fadetab").tabso({
                cntSelect: "#fadecon",
                tabEvent: "mouseover",
                tabStyle: "fade"
            });

            //默认选项卡切换
            $("#normaltab").tabso({
                cntSelect: "#normalcon",
                tabEvent: "mouseover",
                tabStyle: "normal"
            });

            //获取当前登录用户的所有菜单信息
            var url = "Welcome.aspx"
            var str = "";
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: url + "/getMenuInfo",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    var arr = jQuery.parseJSON(data.d);
                    for (var i = 0; i < arr.length; i++) {
                        var strMenuName = arr[i].MENU_TEXT;
                        var strUrl = arr[i].MENU_URL.toLocaleLowerCase();
                        //委托书列表
                        if (strUrl == "../channels/mis/contract/contractlist.aspx?wf_id=wt_flow|wf_a|sample_wt") {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut1\"></span>" + strMenuName + "</a>";
                        }
                        //委托书缴费
                        if (strUrl == "../channels/mis/contractfee/contractfee.aspx") {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut2\"></span>" + strMenuName + "</a>";
                        }
                        //采样预约
                        if (strUrl == "../channels/mis/monitoringplan/plancalendar.aspx") {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut3\"></span>" + strMenuName + "</a>";
                        }
                        //自送样预约
                        if (strUrl == "../channels/mis/sampleplan/sampleplanlist.aspx") {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut4\"></span>" + strMenuName + "</a>";
                        }
                        //采样任务列表
                        if (strUrl.indexOf("samplinglist.aspx") >= 0) {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut6\"></span>" + strMenuName + "</a>";
                        }
                        //报告办理
                        if (strUrl == "../channels/mis/report/reportlist.aspx") {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut8\"></span>" + strMenuName + "</a>";
                        }
                        //样品交接
                        if (strUrl == "../channels/mis/monitor/sampling/qhd/samplingallocationsheet.aspx") {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut9\"></span>" + strMenuName + "</a>";
                        }
                        //分析任务分配
                        if (strUrl == "../channels/mis/monitor/result/qhd/analysistaskallocation.aspx") {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut10\"></span>" + strMenuName + "</a>";
                        }
                        //监测分析
                        if (strUrl == "../channels/mis/monitor/result/qhd/analysisresult.aspx") {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut11\"></span>" + strMenuName + "</a>";
                        }
                        //分析室主任审核
                        if (strUrl == "../channels/mis/monitor/result/qhd/analysisresultcheck.aspx") {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut12\"></span>" + strMenuName + "</a>";
                        }
                        //技术室主任审核
                        if (strUrl == "../channels/mis/monitor/result/qhd/analysisresultqcCheck.aspx") {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut13\"></span>" + strMenuName + "</a>";
                        }
                    }
                }
            });
            $("#divTopMenu").html(str);
        });
        function CheckNoticeAll() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/OA/Notice/NoticeListView.aspx";
            top.f_addTab(tabid, "公告列表", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
    </script>
</head>
<body>
    <!--right start-->
    <!--快捷按钮 start-->
    <div class="shortcut pt4 mg10" id="divTopMenu">
    </div>
    <!--快捷按钮 end-->
    <!--内容 start-->
    <div class="layoutright mg10">
        <div class="listborder">
            <div class="listh2">
                <h2>
                    消息公告</h2>
                <span><a href="#" onclick="CheckNoticeAll()">更多>></a></span></div>
            <div id="focus">
            </div>
        </div>
        <!--消息公告 end-->
        <!--任务办理 start-->
        <div class="demo">
            <!--tabbtn start-->
            <ul class="tabbtn" id="move-animate-top">
                <li class="current"><a href="#">任务办理</a></li>
                <li><a href="#">公文列表</a></li>
                <li><a href="#">申购列表</a></li>
            </ul>
            <!--tabbtn end-->
            <div class="tabcon" id="topcon">
                <!--tabcon start-->
                <div class="subbox">
                    <!--subbox start-->
                    <div class="sublist">
                        <!--tabcon 待办任务 start-->
                        <div class="listtop">
                            <span>任务信息</span> 共<strong><%=strTaskHasCount %></strong>项 <a onclick="TurnTo('../Sys/WF/WfTaskListForJS.aspx','任务办理','01')"
                                href="#">待办任务</a>， 其中<strong><%=strTaskBackCount %></strong>项<a>退回任务</a>； 今日待办<strong><%=strTodayTaskHasCount%></strong>项；
                            <strong>
                                <%=strTaskFinishCount%></strong>项<a>已办任务</a>
                        </div>
                        <ul class="listul">
                            <li><strong>1</strong> <a href="#">广东金玉兰包装机械有限公司2013年度定期委托监测 <span class="right">2013-02-28</span></a>
                                <span><a href="#">立即办理</a></span> </li>
                            <li><strong>2</strong> <a href="#">专业技术人员(超级管理员)2013年度考核登记表<span class="right">2013-02-28</span></a>
                                <span><a href="#">立即办理</a></span> </li>
                            <li><strong>3</strong> <a href="#">事业单位人员(超级管理员)2013年度考核登记表<span class="right">2013-02-28</span></a>
                                <span><a href="#">立即办理</a></span> </li>
                            <li><strong>4</strong> <a href="#">2013年度培训登记表<span class="right">2013-02-28</span></a>
                                <span><a href="#" class="gray">已经办理</a></span> </li>
                            <li><strong>5</strong> <a href="#">广东金玉兰包装机械有限公司2013年度定期委托监测<span class="right">2013-02-28</span></a>
                                <span><a href="#" class="gray">已经办理</a></span> </li>
                        </ul>
                        <p class="listul_more">
                            <a href="#">查看更多</a></p>
                    </div>
                    <!--tabcon 待办任务 end-->
                    <div class="sublist">
                        <!--tabcon 公文列表 start-->
                        <ul>
                            <li><span>▪</span> <a href="#" target="_blank">荥阳市永烨商贸有限公司100万t/a建筑石料用灰岩矿资源开发利用项目环境影响报告书受理公示<span>2013-02-28</span></a>
                            </li>
                            <li><span>▪</span> <a href="#" target="_blank">郑州市南环公园（原市青铜器公园）项目环境影响报告书受理公示<span>2013-02-28</span></a>
                            </li>
                            <li><span>▪</span> <a href="#" target="_blank">省环保厅陈新贵副厅长到郑州站检查PM2.5监测工作<span>2013-02-28</span></a>
                            </li>
                            <li><span>▪</span> <a href="#" target="_blank">郑州市召开第七次环境保护大会<span>2013-02-28</span></a>
                            </li>
                            <li><span>▪</span> <a href="#" target="_blank">市人大白红战主任一行到我局视察十大实事环保项目建设情况<span>2013-02-28</span></a>
                            </li>
                            <li><span>▪</span> <a href="#" target="_blank">郑州日报：空气质量近日污染严重 建议老人减少户外活动<span>2013-02-28</span></a>
                            </li>
                        </ul>
                        <p class="listul_more">
                            <a href="#">查看更多</a></p>
                    </div>
                    <!--tabcon 公文列表 end-->
                    <div class="sublist">
                        <!--tabcon 申购列表 start-->
                        <ul>
                            <li><span>▪</span> <a href="#" target="_blank">荥阳市永烨商贸有限公司100万t/a建筑石料用灰岩矿资源开发利用项目环境影响报告书受理公示<span>2013-02-28</span></a>
                            </li>
                            <li><span>▪</span> <a href="#" target="_blank">郑州市南环公园（原市青铜器公园）项目环境影响报告书受理公示<span>2013-02-28</span></a>
                            </li>
                            <li><span>▪</span> <a href="#" target="_blank">省环保厅陈新贵副厅长到郑州站检查PM2.5监测工作<span>2013-02-28</span></a>
                            </li>
                            <li><span>▪</span> <a href="#" target="_blank">郑州市召开第七次环境保护大会<span>2013-02-28</span></a>
                            </li>
                            <li><span>▪</span> <a href="#" target="_blank">市人大白红战主任一行到我局视察十大实事环保项目建设情况<span>2013-02-28</span></a>
                            </li>
                            <li><span>▪</span> <a href="#" target="_blank">郑州日报：空气质量近日污染严重 建议老人减少户外活动<span>2013-02-28</span></a>
                            </li>
                        </ul>
                        <p class="listul_more">
                            <a href="#">查看更多</a></p>
                    </div>
                    <!--tabcon 申购列表 end-->
                </div>
                <!--subbox end-->
            </div>
            <!--tabcon end-->
        </div>
        <!--任务办理 end-->
        <!--统计报表 start-->
        <!--
    <div class=" layoutright_min  listborder">
        <h4 class="listh4">统计报表</h4>

        <p class="leader">领导专用</p> 
        <div class="listbutton">
            <a href="#">污染源监测统计</a>
            <a href="#">费用统计</a>
            <a href="#">环境质量监测统计</a>
            <a href="#">污染源超标统计</a>
            <a href="#">环境质量超标统计</a>
            <a href="#" class="gray">质控超标统计</a>
            <a href="#" class="gray">及时率统计</a>
            <a href="#" class="gray">质控超标统计</a>
        </div>

        <p class="environment">环境质量报表</p> 
        <div class="listbutton">
            <a href="#">污染源监测统计</a>
            <a href="#">费用统计</a>
            <a href="#">环境质量监测统计</a>
            <a href="#">污染源超标统计</a>
            <a href="#">环境质量超标统计</a>
            <a href="#">质控超标统计</a>
        </div>

        <p class="quality">质控报表</p> 
        <div class="listbutton">
            <a href="#">质控数据统计表</a>
            <a href="#">质控报表</a>
            <a href="#">监测数据统计表</a>
            <a href="#">质量控制结果统计</a>
        </div>

        <p class="business">业务查询</p>  
        <div class="listbutton">  
            <a href="#">综合查询</a>
            <a href="#">监测项目</a>
            <a href="#">方法依据</a>
            <a href="#">评价标准</a>
            <a href="#">仪器设备</a>
            <a href="#">企业信息</a>
            <a href="#">点位信息</a>
            <a href="#">企业信息</a>
        </div>
    </div>
    -->
        <div class="rrow layoutright_min">
            <div id="login" class="login">
                <dl class="qlogin ">
                </dl>
                <ul>
                    <!--环保报表 start-->
                    <h2>
                        环保报表<span><a class="w"></a></span></h2>
                    <li id="btypepan01"><span class="bzzx" id="yhzc">地表水</span><span class="bzzx">环境噪声</span>
                        <div class="block">
                            <a href="#">江河水</a> <a href="#">饮用水</a> <a href="#">河流底泥</a> <a href="#">地表水重金属</a>
                            <a href="#">底泥重金属</a> <a href="#">蓝藻水华</a>
                        </div>
                        <div>
                            <a href="#">区域环境噪声</a> <a href="#">道路交通噪声</a> <a href="#">功能区噪声</a>
                        </div>
                    </li>
                    <li id="btypepan02"><span class="bzzx">海洋</span><span class="bzzx">其他</span>
                        <div>
                            <a href="#">入海河口</a> <a href="#">近岸海域</a> <a href="#">直排入海</a> <a href="#">入海河口</a>
                        </div>
                        <div class="block">
                            <a href="#">环境空气</a> <a href="#">降水</a> <a href="#">降尘</a>
                        </div>
                    </li>
                    <!--环保报表 end-->
                    <!--管理报表 start-->
                    <h2>
                        管理报表<span><a class="w"></a></span></h2>
                    <li id="btypepan03"><span class="bzzx">污染源</span><span class="bzzx">环境质量</span>
                        <div class="block">
                            <a href="#">污染源监测统计</a> <a href="#">污染源超标统计</a>
                        </div>
                        <div>
                            <a href="#">环境质量监测统计</a> <a href="">环境质量超标统计</a>
                        </div>
                    </li>
                    <li id="btypepan04"><span class="bzzx">业务统计</span>
                        <div>
                            <a href="#">费用统计</a> <a href="#">及时率</a> <a href="#">质控超标统计</a>
                        </div>
                    </li>
                    <!--管理报表 end-->
                    <!--质控统计 start-->
                    <h2>
                        质控统计<span><a class="w"></a></span></h2>
                    <li id="btypepan05"><span class="bzzx">质控数据统计表</span><span class="bzzx">监测数据统计表</span>
                    </li>
                    <li id="btypepan06"><span class="bzzx">质量控制结果统计</span> </li>
                    <!--质控统计 end-->
                    <!--站务统计 start-->
                    <h2>
                        站务统计<span><a class="w"></a></span></h2>
                    <li id="btypepan07"><span class="bzzx">公文统计</span><span class="bzzx">人员统计</span></li>
                    <li id="btypepan08"><span class="bzzx">培训统计</span><span class="bzzx">档案查询</span></li>
                    <li id="btypepan09"><span class="bzzx">采购统计</span><span class="bzzx">物料统计</span></li>
                    <li id="btypepan10"><span class="bzzx">服务商统计</span></li>
                    </li>
                    <!--站务统计 end-->
                    <!--任务管理 start-->
                    <h2>
                        任务管理<span><a class="w"></a></span></h2>
                    <li id="btypepan11"><span class="bzzx">任务追踪</span><span class="bzzx">任务清除</span></li>
                    <li id="btypepan12"><span class="bzzx">任务回收站</span><span class="bzzx">删除委托</span></li>
                    <li id="btypepan13"><span class="bzzx">删除预约</span></li>
                    <!--任务管理 end-->
                    <!--业务查询 start-->
                    <h2>
                        业务查询<span><a class="w"></a></span></h2>
                    <li id="btypepan14"><span class="bzzx">综合查询</span><span class="bzzx">监测项目</span></li>
                    <li id="btypepan15"><span class="bzzx">方法依据</span><span class="bzzx">评价标准</span></li>
                    <!--业务查询 end-->
                </ul>
            </div>
            <script src="../Scripts/tabright.js" type="text/javascript"></script>
        </div>
        <!--统计报表 end-->
    </div>
    <!--内容 end-->
    <!--right end-->
</body>
</html>
