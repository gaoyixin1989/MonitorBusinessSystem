<%@ Page Language="C#" AutoEventWireup="True" Inherits="Portal_Welcome_New" Codebehind="Welcome_New.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../App_Themes/default/styles/index2013.css" rel="stylesheet" type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../App_Themes/default/styles/welcomebody.css" rel="stylesheet" type="text/css" />
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
    <div class="pdt15">
    <div class="shortcut pt4 mgtl" id="divTopMenu">
    </div>
    </div>
    <!--快捷按钮 end-->
    <!--内容 start-->
    <div class="layoutmin mg10">
        <div class="listborder">
            <div class="listh2">
                <h2>
                    消息公告</h2>
                <span><a href="#" onclick="CheckNoticeAll()">更多>></a></span></div>
            <div id="focus">
            </div>
        </div>
       
    </div>
    <!--内容 end-->
    <!--right end-->
</body>
</html>
