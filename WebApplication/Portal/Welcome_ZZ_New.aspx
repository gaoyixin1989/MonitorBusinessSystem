<%@ Page Language="C#" AutoEventWireup="True" Inherits="Portal_Welcome_ZZ_New" CodeBehind="Welcome_ZZ_New.aspx.cs" %>

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
    <style type="text/css">
        .toDoList
        {
            font-size: 14px;
            clear: both;
            padding-bottom: 10px;
        }
        .toDoList h3
        {
            margin-top: 6px;
        }
        .toDoList li
        {
            clear: both;
        }
        .toDoList li div
        {
            float: left;
            overflow: hidden;
            margin-left: 1em;
        }
        .toDoList span
        {
            color: #e95800;
            font-weight: bold;
            padding: 3px;
        }
        .toDoList a
        {
            color: #036;
            padding: 3px;
            margin: 0 3px;
        }
        
        .toDoList a:hover
        {
            color: #ff6600;
            text-decoration: underline;
        }
        .div2013
        {
            overflow: auto;
            clear: both;
        }
        .div2013 div
        {
            float: left;
            margin-left: 1em;
        }
    </style>
    <script src="../Scripts/news.js" type="text/javascript"></script>
    <script src="../Scripts/tab.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function ($) {
            if ($("#Hidden1").val() != null && $("#Hidden1").val() == "Welcome_QY") {
                $("#divInfoMenu").css("display", "block");
            }
            else {
                $("#divInfoMenu").css("display", "none");
            }
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
                    var iTaskPlan = false, iTaskDoPlan = false, iAllocationSheet = false, iTaskAllocation = false, iMasterQcCheck = false, iResultQcCheck = false, iSampleQcCheck = false;
                    var iTaskPlanJD = false;
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
                        if (strUrl == "../channels/mis/monitoringplan/taskplanlist.aspx?type=qy") {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut3\"></span>" + strMenuName + "</a>";
                            iTaskPlan = true;
                        }
                        //监督性任务预约
                        if (strUrl == "../channels/mis/monitoringplan/planadd_qy.aspx") {
                            iTaskPlanJD = true;
                        }
                        //预约办理
                        if (strUrl == "../channels/mis/monitoringplan/taskdoplanlist.aspx?type=qy") {
                            iTaskDoPlan = true;
                        }
                        //样品交接
                        if (strUrl == "../channels/mis/monitor/sampling/qy/samplingallocationsheet.aspx") {
                            iAllocationSheet = true;
                        }
                        //样品分发
                        if (strUrl == "../channels/mis/monitor/result/qy/analysistaskallocation.aspx") {
                            iTaskAllocation = true;
                        }
                        //分析室主任审核
                        if (strUrl == "../channels/mis/monitor/result/qy/analysismasterqccheck.aspx") {
                            iMasterQcCheck = true;
                        }
                        //质控审核
                        if (strUrl == "../channels/mis/monitor/result/qy/analysisresultqccheck.aspx") {
                            iResultQcCheck = true;
                        }
                        //现场室主任审核
                        if (strUrl == "../channels/mis/monitor/result/qy/sampleresultqcchecklist.aspx") {
                            iSampleQcCheck = true;
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
                        //技术室主任审核
                        if (strUrl == "../channels/mis/monitor/result/qhd/analysisresultqcCheck.aspx") {
                            str += " <a href=\"" + strUrl + "\"><span class=\"shortcut13\"></span>" + strMenuName + "</a>";
                        }
                    }
                    if (!iTaskPlan)
                        $("#TaskPlan").attr("style", "display:none");
                    if (!iTaskPlanJD) {
                        $("#TaskPlan_10").attr("style", "display:none");
                        $("#TaskPlan_11").attr("style", "display:none");
                        $("#TaskPlan_12").attr("style", "display:none");
                        $("#TaskPlan_13").attr("style", "display:none");
                        $("#TaskPlan_14").attr("style", "display:none");
                    }
                    if (!iTaskDoPlan)
                        $("#TaskDoPlan").attr("style", "display:none");
                    if (!iAllocationSheet)
                        $("#AllocationSheet").attr("style", "display:none");
                    if (!iTaskAllocation)
                        $("#TaskAllocation").attr("style", "display:none");
                    if (!iMasterQcCheck)
                        $("#MasterQcCheck").attr("style", "display:none");
                    if (!iResultQcCheck)
                        $("#ResultQcCheck").attr("style", "display:none");
                    if (!iSampleQcCheck)
                        $("#SampleQcCheck").attr("style", "display:none");
                }
            });
            $("#divTopMenu").html(str);

            getCCFlow();
            getCCFlowBatch();
            getCCFlowCC();
        });


        function getCCFlow() {

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "Welcome_ZZ_New.aspx?GetCCFlow=GetCCFlow",
                dataType: "json",
                success: function (data, textStatus) {

                    var html = "";

                    $(data).each(function (index, item) {

                        html += '<div>' +
                                    '共<span>' + item.Total + '</span>项<a href="#" onclick="toCCFlow(\'' + item.FK_Node + '\',\'' + item.NodeName + '\')">' + item.NodeName + '任务</a>;' +
                                '</div>'

                    });


                    $('#RX').append(html);

                }
            });
        }

        function getCCFlowBatch() {

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "Welcome_ZZ_New.aspx?GetCCFlow=GetCCFlowBatch",
                dataType: "json",
                success: function (data, textStatus) {

                    var html = "";

                    $(data).each(function (index, item) {

                        html += '<div>' +
                        //                                    '<a href="#" onclick="toCCFlowBatch(\'' + item.FK_Node + '\',\'' + item.NodeName + '\')">' + item.NodeName + '(' + item.NUM + ')' + '</a>;' +
                                     '共<span>' + item.NUM + '</span>项<a href="#" onclick="toCCFlowBatch(\'' + item.FK_Node + '\',\'' + item.NodeName + '\')">' + item.NodeName + '任务</a>;' +
                                '</div>'

                    });


                    $('#RX2').append(html);

                }
            });
        }

        function getCCFlowCC() {

            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "Welcome_ZZ_New.aspx?GetCCFlow=GetCCFlowCC",
                dataType: "json",
                success: function (data, textStatus) {

                    var html = "";

                    $(data).each(function (index, item) {

                        if (item.IsToDo) {
                            html += '<div>' +

                                     '共<span>' + item.Total + '</span>项<a href="#" onclick="toCCFlow(\'' + item.FK_Node + '\',\'' + item.NodeName + '\')">' + item.NodeName + '任务</a>;' +
                                '</div>';
                        }
                        else {

                            html += '<div>' +

                                     '共<span>' + item.Total + '</span>项<a href="#" onclick="toCCFlowCC(\'' + item.FK_Node + '\',\'' + item.NodeName + '\')">' + item.NodeName + '任务</a>;' +
                                '</div>';
                        }

                    });


                    $('#RX3').append(html);

                }
            });
        }

        function toCCFlow(fk_node, NodeName) {

            var tabid = "tabid" + fk_node;
            var url = "../ccflow/ToDoWorkList.aspx?FK_Node=" + fk_node;
            top.f_addTab(tabid, "待办-" + NodeName, url);
        }

        function toCCFlowBatch(fk_node, NodeName) {

            var tabid = "tabid" + fk_node;
            var url = "../WF/Batch.aspx?FK_Node=" + fk_node;
            top.f_addTab(tabid, "批处理-" + NodeName, url);
        }

        function toCCFlowCC(fk_node, NodeName) {

            var tabid = "tabid" + fk_node;
            var url = "../ccflow/CCWorkList.aspx?FK_Node=" + fk_node;
            top.f_addTab(tabid, "抄送-" + NodeName, url);

        }



        function CheckNoticeAll() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/OA/Notice/NoticeListView.aspx";
            top.f_addTab(tabid, "公告列表", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //待办任务  WfTaskListCFMForJS   WfTaskListForJS
        function strTask() {
            var tabid = "tabid" + "Notice";
            var url = "../Sys/WF/WfTaskListForJS.aspx?waiting=2A";
            top.f_addTab(tabid, "待办任务", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //退回任务
        function strTaskBack() {
            var tabid = "tabid" + "Notice";
            var url = "../Sys/WF/WfTaskListForJS.aspx?Return=00";
            top.f_addTab(tabid, "任务退回", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //报告办理
        function ReportManage() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/Report/ReportList.aspx?type=QY";
            top.f_addTab(tabid, "报告办理", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //收文办理
        function strSwT() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/OA/SW/ZZ/SWHandleList.aspx";
            top.f_addTab(tabid, "收文办理", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //采样预约
        function TaskPlan() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/MonitoringPlan/TaskPlanList.aspx?type=QY";
            top.f_addTab(tabid, "采样预约", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //监督性任务预约
        function TaskPlanJD() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/MonitoringPlan/PlanAdd_QY.aspx";
            top.f_addTab(tabid, "年度监测预约", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //预约办理
        function TaskDoPlan() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/MonitoringPlan/TaskDoPlanList.aspx?type=QY";
            top.f_addTab(tabid, "预约办理", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //任务分配
        function PlanList() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/MonitoringPlan/TaskDoPlanList.aspx";
            top.f_addTab(tabid, "任务分配", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //采样任务
        function Sampling() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/Monitor/sampling/QY/SamplingList.aspx";
            top.f_addTab(tabid, "采样任务列表", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //样品交接
        function AllocationSheet() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/Monitor/sampling/QY/SamplingAllocationSheet.aspx";
            top.f_addTab(tabid, "样品交接", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //样品分发
        function TaskAllocation() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/Monitor/Result/QY/AnalysisTaskAllocation.aspx";
            top.f_addTab(tabid, "样品分发", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //监测分析
        function Analysis() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/Monitor/Result/QY/AnalysisResult.aspx";
            top.f_addTab(tabid, "监测分析", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //分析结果复核
        function ResultCheck() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/Monitor/Result/QY/AnalysisResultCheck.aspx";
            top.f_addTab(tabid, "分析结果复核", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //分析主任审核
        function MasterQcCheck() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/Monitor/Result/QY/AnalysisMasterQcCheck.aspx";
            top.f_addTab(tabid, "分析主任审核", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //质控审核
        function ResultQcCheck() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/Monitor/Result/QY/AnalysisResultQcCheck.aspx";
            top.f_addTab(tabid, "质控审核", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //分析类现场项目结果核录
        function SampleAnalysisCheck() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/Monitor/Result/QY/SampleAnalysisResultCheck.aspx";
            top.f_addTab(tabid, "分析类现场项目结果核录", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //现场结果复核
        function SampleCheck() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/Monitor/Result/QY/SampleResultCheckList.aspx";
            top.f_addTab(tabid, "现场监测结果复核", url);
            if (top.tab.isTabItemExist(tabid)) {
                top.tab.reload(tabid);
            }
        }
        //现场结果审核
        function SampleQcCheck() {
            var tabid = "tabid" + "Notice";
            var url = "../Channels/Mis/Monitor/Result/QY/SampleResultQcCheckList.aspx";
            top.f_addTab(tabid, "现场室主任审核", url);
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
        <div class="shortcut pt4 mgtl" id="divInfoMenu">
            <div class="toDoList">
                <div id="RX" class="div2013">
                    <h3>
                        待办信息</h3>
                    <div id="strTask" runat="server" style="display: none;">
                        共<span><%=strTaskHasCount %></span>项<a href="#" onclick="strTask()">待办任务</a>;
                    </div>
                    <div id="strTaskBack" runat="server" style="display: none;">
                        共<span><%=strTaskBackCount %></span>项<a href="#" onclick="strTaskBack()">退回任务</a>;
                    </div>
                    <div id="ReportManage" runat="server" style="display: none;">
                        共<span><%=ReportCount%></span>项<a href="#" onclick="ReportManage()">报告办理任务</a>;
                    </div>
                </div>
                <div id="RX2" class="div2013">
                    <h3>
                        批处理信息</h3>
                </div>
                <div id="RX3" class="div2013">
                    <h3>
                        抄送信息</h3>
                </div>
                <%--<div  id="GX"  class="div2013">
              <h3>公文信息</h3>
                                    <div id="strSwT" runat="server"> 共<span><%=strSwTaskAllCount%></span>项<a href="#" onclick="strSwT()">收文办理</a>; </div>
                        </div>--%>
                <div id="CX">
                    <h3>
                        监测任务</h3>
                    <ul>
                        <li>
                            <div id="TaskPlan" runat="server">
                                共<span><%=TaskPlanCount%></span>项<a href="#" onclick="TaskPlan()">采样预约任务</a>;</div>
                            <div id="TaskPlan_10" runat="server">
                                共<span><%=TaskPlan10Count%></span>项<a href="#" onclick="TaskPlanJD()">监督性(国控)预约任务</a>;</div>
                            <div id="TaskPlan_11" runat="server">
                                共<span><%=TaskPlan11Count%></span>项<a href="#" onclick="TaskPlanJD()">监督性(省控)预约任务</a>;</div>
                            <div id="TaskPlan_12" runat="server">
                                共<span><%=TaskPlan12Count%></span>项<a href="#" onclick="TaskPlanJD()">监督性(重金属)预约任务</a>;</div>
                            <div id="TaskPlan_13" runat="server">
                                共<span><%=TaskPlan13Count%></span>项<a href="#" onclick="TaskPlanJD()">监督性预约任务</a>;</div>
                            <div id="TaskPlan_14" runat="server">
                                共<span><%=TaskPlan14Count%></span>项<a href="#" onclick="TaskPlanJD()">年度委托预约任务</a>;</div>
                            <div id="TaskDoPlan" runat="server">
                                共<span><%=TaskDoPlanCount%></span>项<a href="#" onclick="TaskDoPlan()">预约办理任务</a>;</div>
                            <div id="Sampling" runat="server">
                                共<span><%=SamplingCount%></span>项<a href="#" onclick="Sampling()">采样任务</a>;</div>
                        </li>
                        <li>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                            <div id="AllocationSheet" runat="server">
                                共<span><%=AllocationSheetCount%></span>项<a href="#" onclick="AllocationSheet()">样品交接任务</a>;</div>
                            <div id="TaskAllocation" runat="server">
                                共<span><%=TaskAllocationCount%></span>项<a href="#" onclick="TaskAllocation()">样品分发任务</a>;</div>
                            <div id="Analysis" runat="server">
                                共<span><%=AnalysisCount%></span>项<a href="#" onclick="Analysis()">监测分析任务</a>;
                            </div>
                            <div id="ResultCheck" runat="server">
                                共<span><%=ResultCheckCount%></span>项<a href="#" onclick="ResultCheck()">分析结果复核任务</a>;</div>
                            <div id="MasterQcCheck" runat="server">
                                共<span><%=MasterQcCheckCount%></span>项<a href="#" onclick="MasterQcCheck()">分析主任审核任务</a>;</div>
                        </li>
                        <li>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                            <div id="ResultQcCheck" runat="server">
                                共<span><%=ResultQcCheckCount%></span>项<a href="#" onclick="ResultQcCheck()">质控审核任务</a>;</div>
                            <div id="SampleAnalysisCheck" runat="server">
                                共<span><%=SampleAnalysisCheckCount%></span>项<a href="#" onclick="SampleAnalysisCheck()">现场结果核录任务</a>;</div>
                            <div id="SampleCheck" runat="server">
                                共<span><%=SampleCheckCount%></span>项<a href="#" onclick="SampleCheck()">现场结果复核</a>;</div>
                            <div id="SampleQcCheck" runat="server">
                                共<span><%=SampleQcCheckCount%></span>项<a href="#" onclick="SampleQcCheck()">现场结果审核</a>;</div>
                        </li>
                    </ul>
                </div>
            </div>
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
    <input id="Hidden1" type="hidden" runat="server" />
    <!--内容 end-->
    <!--right end-->
</body>
</html>
