<%@ Page Language="C#" AutoEventWireup="true" Inherits="Portal_Welcome" Codebehind="Welcome.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <!--Jquery 基础文件-->
    <script src="../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <!--自动完成-->
    <script src="../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <!--LigerUI-->
    <script src="../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/Date.js"></script>
    <link href="../CSS/welcome.css" rel="stylesheet" type="text/css" />
    <script>
        function TurnTo(url, title, index) {
            top.f_addTab("newWindow" + index, title, url);
        }
    </script>
</head>
<body style="overflow-x: hidden; overflow-y: auto;">
    <div class="RightFrm_Content">
        <div class="RightFrm_ContentInner">
            <!-- 首页 start -->
            <h4 class="welcomeInfo">
                <span>
                    <%=base.LogInfo.UserInfo.REAL_NAME %>
                </span>，
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <script language="javascript" type="text/javascript">
                    document.write(GetSayHelloMsg())</script>
            </h4>
            <ul class="toDoList">
                <li class="toDoType01">任务信息：共<span><%=strTaskHasCount %></span>项<a onclick="TurnTo('../Sys/WF/WfTaskListForJS.aspx','任务办理','01')"
                    href="#">待办任务</a>，其中<span><%=strTaskBackCount %></span>项<a>退回任务</a>； 今日待办<span><%=strTodayTaskHasCount%></span>项；<span><%=strTaskFinishCount%></span>项<a>已办任务</a></li>
                <li class="toDoType03">日志信息：共<span><%=strSampleCount %></span>项<a onclick="TurnTo('../Channels/Mis/Monitor/sampling/SamplingList.aspx','采样任务列表','02')"
                    href="#">采样任务</a>；共<span><%=strAnalysisCount %></span>项<a onclick="TurnTo('../Channels/Mis/Monitor/Result/AnalysisTaskAllocation.aspx','分析任务分配','03')"
                        href="#">分析任务</a></li>
            </ul>
            <ul class="workType">
                <li class="green" onclick=""><span>业务办理</span>
                    <p>
                        办理各类业务</p>
                </li>
                <li onclick="TurnTo('../Sys/WF/WFStartPage.aspx?action=|type=true&WF_ID=WT_FLOW','新增常规类委托书','04')"
                    runat="server" id="linkNormalContractInfo"><span>新增委托书</span>
                    <p>
                        签订新委托书</p>
                </li>
                <li onclick="TurnTo('../Channels/Mis/Report/ReportList.aspx','报告办理','05')" runat="server"
                    id="linkJDContractInfo"><span>报告办理</span>
                    <p>
                        进行报告业务办理</p>
                </li>
                <li onclick="TurnTo('../Channels/Mis/Report/ReportManager.aspx','报告领取','06')" runat="server"
                    id="li3"><span>报告领取</span>
                    <p>
                        进行报告领取</p>
                </li>
            </ul>
            <ul class="workType">
                <li class="green" onclick=""><span>采样</span>
                    <p>
                        办理采样业务</p>
                </li>
                <li onclick="TurnTo('../Channels/Mis/Monitor/sampling/SamplingList.aspx','采样任务列表','07')"
                    runat="server" id="linkContractWFStepView"><span>采样任务</span>
                    <p>
                        办理采样任务</p>
                </li>
                <li onclick="TurnTo('../Channels/Mis/MonitoringPlan/PlanCalendar.aspx','采样预约','08')"
                    runat="server" id="linkEntrustContractInfo"><span>采样预约</span>
                    <p>
                        采样预约和办理</p>
                </li>
                <li onclick="TurnTo('../Channels/Mis/Monitor/sampling/SamplingAllocationSheet.aspx','采样交接单','09')"
                    runat="server" id="li4"><span>采样交接单</span>
                    <p>
                        采样交接单打印</p>
                </li>
            </ul>
            <ul class="workType">
                <li class="green" onclick=""><span>监测分析</span>
                    <p>
                        办理分析业务</p>
                </li>
                <li onclick="TurnTo('../Channels/Mis/Monitor/Result/AnalysisTaskAllocation.aspx','分析任务分配','10')"
                    runat="server" id="linkContractView"><span>分析任务分配</span>
                    <p>
                        进行分析任务分配</p>
                </li>
                <li onclick="TurnTo('../Channels/Mis/Monitor/Result/AnalysisResult.aspx','监测分析','11')"
                    runat="server" id="linkSampleView"><span>监测分析</span>
                    <p>
                        监测分析办理</p>
                </li>
                <li onclick="TurnTo('../Channels/Mis/Monitor/Result/AnalysisResultCheck.aspx','分析结果校核','12')"
                    runat="server" id="linkReportView"><span>分析结果校核</span>
                    <p>
                        分析结果校核</p>
                </li>
                <li onclick="TurnTo('../Channels/Mis/Monitor/Result/AnalysisResultQcCheck.aspx','分析结果质控审核','13')"
                    runat="server" id="li1"><span>分析结果质控审核</span>
                    <p>
                        分析结果质控审核</p>
                </li>
            </ul>
            <ul class="workType">
                <li class="green" onclick=""><span>资料查询</span>
                    <p>
                        查询相关基础信息</p>
                </li>
                <li onclick="TurnTo('../Channels/Base/Search/ItemSearch.aspx','监测项目查询','14')" id="linkItemInfoList"
                    runat="server"><span>监测项目</span>
                    <p>
                        查询监测项目</p>
                </li>
                <li onclick="TurnTo('../Channels/Base/Search/MethodSearch.aspx','方法依据查询','15')" id="linkItemMethodByList"
                    runat="server"><span>方法依据</span>
                    <p>
                        查询监测方法依据</p>
                </li>
                <li onclick="TurnTo('../Channels/Base/Search/AnalysisSearch.aspx','评价标准查询','16')"
                    runat="server" id="linkEvaluteStandInfoList"><span>评价标准</span>
                    <p>
                        查询评价标准</p>
                </li>
                <li onclick="TurnTo('../Channels/Base/Search/ApparatusSearch.aspx','仪器设备查询','17')"
                    runat="server" id="li2"><span>仪器设备</span>
                    <p>
                        查询仪器设备信息</p>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
