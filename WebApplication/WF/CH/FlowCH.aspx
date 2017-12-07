<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowCH.aspx.cs" Inherits="CCFlow.WF.CH1234.FlowCH" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程考核</title>
    <link href="../Scripts/jquery/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/jquery/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../Comm/Charts/css/style_3.css" rel="stylesheet" type="text/css" />
    <link href="../Comm/Charts/css/prettify.css" rel="stylesheet" type="text/css" />
    <link href="../Comm/Charts/css/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Comm/Charts/js/json2_3.js" type="text/javascript"></script>
    <script src="../Comm/Charts/js/FusionCharts.js" type="text/javascript"></script>
    <link href="css/flowch.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function queryData(param, callback, scope, method, showErrMsg) {
            if (!method) method = 'GET';
            $.ajax({
                type: method,
                dataType: "text",
                contentType: "application/json; charset=utf-8",
                url: "FlowCH.aspx",
                data: param,
                async: false,
                cache: false,
                complete: function () { },
                error: function (XMLHttpRequest, errorThrown) {
                    callback(XMLHttpRequest);
                },
                success: function (msg) {
                    var data = msg;
                    callback(data, scope);
                }
            });
        }
        $(function () {
            //加载图形
            loadEmpChart();
            loadDeptChart();
            loadAllDeptChart();
        });
        //加载我的工作第一个图形
        function loadEmpChart() {
            var params = {
                method: "empChart"
            };
            queryData(params, function (js, scope) {
                $("#pageloading").hide();
                if (js == "") js = "[]";
                if (js.status && js.status == 500) {
                    $("body").html("<b>访问页面出错，请联系管理员。<b>");
                    return;
                }

                var pushData = eval('(' + js + ')');
                var chart = new FusionCharts("../Comm/Charts/MSLine.swf", "CharZ", '850', '350', '0', '0');
                chart.setDataXML(pushData.set_XML[0]);
                chart.render("empChart");
            }, this);
        }
        //我部门
        function loadDeptChart() {
            var params = {
                method: "deptChart"
            };
            queryData(params, function (js, scope) {
                $("#pageloading").hide();
                if (js == "") js = "[]";
                if (js.status && js.status == 500) {
                    $("body").html("<b>访问页面出错，请联系管理员。<b>");
                    return;
                }

                var pushData = eval('(' + js + ')');
                var chart = new FusionCharts("../Comm/Charts/MSLine.swf", "CharZ", '850', '350', '0', '0');
                chart.setDataXML(pushData.set_XML[0]);
                chart.render("deptChart");
            }, this);
        }
        //全单位
        function loadAllDeptChart() {
            var params = {
                method: "allDeptChart"
            };
            queryData(params, function (js, scope) {
                $("#pageloading").hide();
                if (js == "") js = "[]";
                if (js.status && js.status == 500) {
                    $("body").html("<b>访问页面出错，请联系管理员。<b>");
                    return;
                }

                var pushData = eval('(' + js + ')');
                var chart = new FusionCharts("../Comm/Charts/MSLine.swf", "CharZ", '850', '350', '0', '0');
                chart.setDataXML(pushData.set_XML[0]);
                chart.render("allDeptChart");
            }, this);
        }
    </script>
</head>
<body>
    <div class="main">
        <div class="main_top_left">
            <div class="main_top_left_info">
                我的总体工作效率
            </div>
            <ul>
                <%
                    string sql = "";
                    System.Data.DataTable dt = new System.Data.DataTable();

                    BP.WF.Data.CH ch = new BP.WF.Data.CH();


                    //总数.
                    int totalGzzs = BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM WF_CH WHERE FK_Emp='" + BP.Web.WebUser.No + "'");
                    //按时完成/
                    int totalAswc = BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM WF_CH WHERE FK_Emp='" + BP.Web.WebUser.No + "' AND (CHSta=0 OR CHSta=1)");
                    //超时完成(逾期和超期之和)
                    int totalCswc = BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM WF_CH WHERE FK_Emp='" + BP.Web.WebUser.No + "' AND (CHSta=2 OR CHSta=3)");

                    //求按时完率.
                    double totalAswcl = 0;
                    string totalAswclStr = "";
                    if (totalGzzs != 0)
                    {
                        totalAswcl = (double)totalAswc / totalGzzs * 100;//按时完成率
                        totalAswclStr = totalAswcl.ToString("00.00");
                    }
                    
                    sql = "select distinct FK_Emp from WF_CH";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

                    //根据按时(及时和按期)完成率计算排名，考虑按时完成率相同
                    int totalCount = dt.Rows.Count;//总排名
                    int myTotalPm = 1;//默认为排名第一位
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        if (dr["FK_Emp"].ToString() == BP.Web.WebUser.No)
                            continue;

                        double otherTotalAswcl;
                        sql = "select * from WF_CH where FK_Emp='" + dr["FK_Emp"].ToString() + "' AND CHSta='0' AND CHSta='1'";

                        string sumSql = "select * from WF_CH where FK_Emp='" + dr["FK_Emp"].ToString() + "'";
                        try
                        {
                            otherTotalAswcl = (BP.DA.DBAccess.RunSQLReturnCOUNT(sql) / BP.DA.DBAccess.RunSQLReturnCOUNT(sumSql)) * 100;
                        }
                        catch (Exception)
                        {
                            otherTotalAswcl = 0;
                        }

                        if (totalAswcl < otherTotalAswcl)//总体排名
                            myTotalPm += 1;
                    }

                    //OverMinutes小于0表明提前 
                    sql = "select sum(OverMinutes) from wf_ch where fk_emp='" + BP.Web.WebUser.No + "' and OverMinutes <0";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

                    int totalZttq = 0;//总体提前
                    try
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                            totalZttq = -int.Parse(dt.Rows[0][0].ToString());
                    }
                    catch (Exception)
                    {
                    }

                    //OverMinutes大于0表明逾期
                    int totalZtyq = 0;
                    sql = "select sum(OverMinutes) from wf_ch where fk_emp='" + BP.Web.WebUser.No + "' and OverMinutes >0";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

                    try
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                            totalZtyq = int.Parse(dt.Rows[0][0].ToString());
                    }
                    catch (Exception)
                    {
                    }

                    int totalJswc = 0;
                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "0");

                    totalJswc = qo.GetCount();

                    int totalAqwc = 0;
                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "1");

                    totalAqwc = qo.GetCount();

                    int totalYqwc = 0;
                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "2");

                    totalYqwc = qo.GetCount();

                    int totalCqwc = 0;
                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "3");

                    totalCqwc = qo.GetCount();
                %>
                <li>工作总数<%=totalGzzs%>,按时完成<font class="greenFont"><%=totalAswc%></font>个,超时完成<font class="redFont"><%=totalCswc%></font>个,按时完成率:<%=totalAswclStr%>%</li>
                </br>
                <li>总体排名:第<font class="redFont"><%=myTotalPm %></font>名</li>
                <li>总体提前:<font class="greenFont"><%=totalZttq %></font>分钟</li>
                <li>总体逾期:<font class="redFont" ><%=totalZtyq%></font>分钟</li>
                <li>及时完成<%=totalJswc %>条</li>
                <li>按期完成<%=totalAqwc%>条</li>
                <li>逾期完成<%=totalYqwc%>条</li>
                <li>超期完成<font style="font-size: 25px; color: Red; font-family: Vijaya"><%=totalCqwc%></font>条</li>`
            </ul>
        </div>
        <div class="main_top_right">
            <div class="main_top_right_info">
                我的上周工作效率
            </div>
            <ul>
                <%
                    string firstDayOflastWeek = CalculateFirstDateOfWeek(DateTime.Now.AddDays(-7)).ToString("yyyy-MM-dd");//上周第一天
                    string endDayOfLastWeek = CalculateLastDateOfWeek(DateTime.Now.AddDays(-7)).ToString("yyyy-MM-dd");//上周最后一天
                    sql = "";
                    dt = new System.Data.DataTable();

                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.DTFrom, ">=", firstDayOflastWeek);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.DTTo, "<=", endDayOfLastWeek);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);

                    int lastWeekGzzs = qo.GetCount(); //上周工作总数

                    qo.addAnd();
                    qo.AddWhere("(CHSta='0' or CHSta='1')");

                    int lastWeekAswc = qo.GetCount();//按时完成

                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.DTFrom, ">=", firstDayOflastWeek);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.DTTo, "<=", endDayOfLastWeek);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere("(CHSta='2' or CHSta='3')");

                    int lastWeekCswc = qo.GetCount();//超时完成

                    double lastWeekAswcl = 0;//按时完成率
                    string lastWeekAswclStr = "";
                    if (lastWeekGzzs != 0)
                    {
                        lastWeekAswcl = (double)lastWeekAswc / lastWeekGzzs * 100;
                        lastWeekAswclStr = lastWeekAswcl.ToString("00.00");
                    }
                    qo = new BP.En.QueryObject(ch);

                    //我的按期完成率
                    sql = "select distinct FK_Emp from WF_CH where"
                        + " DTFrom>='" + firstDayOflastWeek + "' and DTTo<='" + endDayOfLastWeek + "'";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

                    //根据按时(及时,按期之和)完成率计算排名，考虑按时完成率相同
                    int lastWeekCount = dt.Rows.Count;//总排名
                    int lastWeekMypm = 1;//默认为排名第一位

                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        if (dr["FK_Emp"].ToString() == BP.Web.WebUser.No)
                            continue;

                        double lastWeekOtherAswcl;
                        sql = "select * from WF_CH where FK_Emp='" + dr["FK_Emp"].ToString() +
                            "' AND CHSta='0' AND CHSta='1' and  DTFrom>='" + firstDayOflastWeek + "' and DTTo<='" + endDayOfLastWeek + "'";

                        string sumSql = "select * from WF_CH where FK_Emp='" + dr["FK_Emp"].ToString()
                            + "' and DTFrom>='" + firstDayOflastWeek + "' and DTTo<='" + endDayOfLastWeek + "'";

                        try
                        {
                            lastWeekOtherAswcl = (BP.DA.DBAccess.RunSQLReturnCOUNT(sql) / BP.DA.DBAccess.RunSQLReturnCOUNT(sumSql)) * 100;
                        }
                        catch (Exception)
                        {
                            lastWeekOtherAswcl = 0;
                        }

                        if (lastWeekAswcl < lastWeekOtherAswcl)//总体排名
                            lastWeekMypm += 1;
                    }

                    int lastWeekZttq = 0; //上周总体提前
                    sql = "select  sum(OverMinutes) from WF_CH where FK_Emp='" + BP.Web.WebUser.No
                    + "' and DTFrom>='" + firstDayOflastWeek + "' and DTTo<='" + endDayOfLastWeek + "' and OverMinutes<0";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

                    try
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                            lastWeekZttq = -int.Parse(dt.Rows[0][0].ToString());
                    }
                    catch (Exception)
                    {
                    }


                    int lastWeekZtyq = 0;//上周总体逾期
                    sql = "select  sum(OverMinutes) from WF_CH where FK_Emp='" + BP.Web.WebUser.No
                   + "' and DTFrom>='" + firstDayOflastWeek + "' and DTTo<='" + endDayOfLastWeek + "' and OverMinutes>0";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

                    try
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                            lastWeekZtyq = int.Parse(dt.Rows[0][0].ToString());
                    }
                    catch (Exception)
                    {
                    }


                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.DTFrom, ">=", firstDayOflastWeek);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.DTTo, "<=", endDayOfLastWeek);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "0");

                    int lastWeekJswc = qo.GetCount();//及时完成

                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.DTFrom, ">=", firstDayOflastWeek);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.DTTo, "<=", endDayOfLastWeek);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "1");

                    int lastWeekAqwc = qo.GetCount();//按期完成

                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.DTFrom, ">=", firstDayOflastWeek);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.DTTo, "<=", endDayOfLastWeek);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "2");

                    int lastWeekYqwc = qo.GetCount();//逾期完成

                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.DTFrom, ">=", firstDayOflastWeek);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.DTTo, "<=", endDayOfLastWeek);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "2");

                    int lastWeekCqwc = qo.GetCount();//超期完成
                %>
                <li>工作总数<%=lastWeekGzzs%>,按时完成<font class="greenFont"><%=lastWeekAswc%></font>个 ,超时完成<font class="redFont"><%=lastWeekCswc%></font>个,按时完成率:<%=lastWeekAswclStr%>%</li>
                </br>
                <li>总体排名:第<font class="redFont"><%=lastWeekMypm%></font>名</li>
                <li>提前完成时间:<font style="font-size: 25px; color: Green; font-family: Vijaya"><%=lastWeekZttq %></font>分钟</li>
                <li>逾期完成时间:<font style="font-size: 25px; color: Red; font-family: Vijaya"><%=lastWeekZtyq %></font>分钟</li>
                <li>及时完成<%=lastWeekJswc %>条</li>
                <li>按期完成<%=lastWeekAqwc%>条</li>
                <li>逾期完成<%=lastWeekYqwc%>条</li>
                <li>超期完成<font style="font-size: 25px; color: Red; font-family: Vijaya;"><%=lastWeekCqwc%></font>条</li>`
            </ul>
        </div>
        <div class="main_center_left">
            <div class="main_center_left_info">
                我的上月工作效率
            </div>
            <ul>
                <%
                    DateTime dTime = DateTime.Now;

                    //上个月最后一天  
                    DateTime lastMouthFirstDay = dTime.AddMonths(-1).AddDays(-dTime.Day + 1);//当前时间的上一月的第一天
                    DateTime lastMouthLastDay = dTime.AddDays(-dTime.Day);//...最后一天

                    ch = new BP.WF.Data.CH();

                    qo = new BP.En.QueryObject(ch);

                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_NY, lastMouthFirstDay.ToString("yyyy-MM"));

                    int lastMouthGzzs = qo.GetCount();//上月总的工作

                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "0");
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "1");

                    int lastMouthAswc = qo.GetCount();//按时完成

                    int lastMouthCswc = 0;//超时完成
                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_NY, lastMouthFirstDay.ToString("yyyy-MM"));
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "2");
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "3");


                    int jswcSum = 0;
                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_NY, lastMouthFirstDay.ToString("yyyy-MM"));
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "0");
                    jswcSum = qo.GetCount();

                    double lastMouthAswcl = 0;
                    string lastMouthAswclStr = "";
                    try
                    {
                        lastMouthAswcl = (double)lastMouthAswc / lastMouthGzzs * 100;
                        lastMouthAswclStr = lastMouthAswcl.ToString("00.00");
                    }
                    catch (Exception)
                    {
                        lastMouthAswclStr = "0";
                    }


                    //计算总体排名
                    sql = "select distinct FK_Emp from WF_CH where "
                      + " FK_NY='" + lastMouthFirstDay.ToString("yyyy-MM") + "'";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

                    int lastMouthMypm = 1;//默认为第一名

                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        if (dr["FK_Emp"].ToString() == BP.Web.WebUser.No)
                            continue;

                        double lastMouthOtherAswcl;
                        sql = "select * from WF_CH where FK_Emp='" + dr["FK_Emp"].ToString() +
                            "' AND CHSta='0' AND CHSta='1' and  FK_NY='" + lastMouthFirstDay.ToString("yyyy-MM") + "'";

                        string sumSql = "select * from WF_CH where FK_Emp='" + dr["FK_Emp"].ToString()
                            + "' and FK_NY='" + lastMouthFirstDay.ToString("yyyy-MM") + "'";

                        try
                        {
                            lastMouthOtherAswcl = (BP.DA.DBAccess.RunSQLReturnCOUNT(sql) / BP.DA.DBAccess.RunSQLReturnCOUNT(sumSql)) * 100;
                        }
                        catch (Exception)
                        {
                            lastMouthOtherAswcl = 0;
                        }

                        if (lastMouthAswcl < lastMouthOtherAswcl)//并列排名
                            lastMouthMypm += 1;
                    }

                    sql = "select  sum(OverMinutes) from WF_CH where FK_Emp='" + BP.Web.WebUser.No
                        + "' and FK_NY='" + lastMouthFirstDay.ToString("yyyy-MM") + "' and OverMinutes<0";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

                    int lastMouthZttq = 0;//上月提前完成
                    try
                    {
                        lastMouthZttq = -int.Parse(dt.Rows[0][0].ToString());
                    }
                    catch (Exception)
                    {
                    }

                    sql = "select  sum(OverMinutes) from WF_CH where FK_Emp='" + BP.Web.WebUser.No
                       + "' and FK_NY='" + lastMouthFirstDay.ToString("yyyy-MM") + "' and OverMinutes>0";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

                    int lastMouthZtyq = 0; //上月逾期完成

                    try
                    {
                        lastMouthZtyq = int.Parse(dt.Rows[0][0].ToString());
                    }
                    catch (Exception)
                    {
                    }

                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_NY, lastMouthFirstDay.ToString("yyyy-MM"));
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "0");

                    int lastMouthJswc = qo.GetCount();//上月及时完成

                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_NY, lastMouthFirstDay.ToString("yyyy-MM"));
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "1");

                    int lastMouthAqwc = qo.GetCount();//上月按期完成

                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_NY, lastMouthFirstDay.ToString("yyyy-MM"));
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "2");

                    int lastMouthYqwc = qo.GetCount();//上月逾期完成

                    qo = new BP.En.QueryObject(ch);
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, BP.Web.WebUser.No);
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.FK_NY, lastMouthFirstDay.ToString("yyyy-MM"));
                    qo.addAnd();
                    qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "3");

                    int lastMouthCqwc = qo.GetCount();//上月超期完成
                %>
                <li>工作总数<%=lastMouthGzzs %>,按时完成<%=lastMouthAswc %>个, 超时完成<%=lastMouthCswc%>个, 按时完成率:<%=lastMouthAswclStr%>%
                </li>
                </br>
                <li>总体排名:第<%=lastMouthMypm%>名</li>
                <li>提前完成时间:<font style="font-size: 25px; color: Green; font-family: Vijaya"><%=lastMouthZttq%></font>分钟</li>
                <li>逾期完成时间:<font style="font-size: 25px; color: Red; font-family: Vijaya"><%=lastMouthZtyq%></font>分钟</li>
                <li>及时完成<%=lastMouthJswc%>条</li>
                <li>按期完成<%=lastMouthAqwc%>条</li>
                <li>逾期完成<%=lastMouthYqwc%>条</li>
                <li>超期完成<font style="font-size: 25px; color: Red; font-family: Vijaya;"><%=lastMouthCqwc%></font>条</li>
            </ul>
        </div>
        <div class="main_center_right">
            <div class="main_center_right_info">
                按期完成率总体排名
            </div>
            <ul>
                <%
                    sql = "select distinct(fk_emp) from wf_ch";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

                    System.Data.DataTable pmDt = new System.Data.DataTable();

                    pmDt.Columns.Add("empNo", typeof(string));
                    pmDt.Columns.Add("fDu", typeof(float));

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i]["FK_Emp"].ToString()))
                            continue;

                        qo = new BP.En.QueryObject(ch);

                        qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, dt.Rows[i]["FK_Emp"].ToString());

                        int sum = qo.GetCount();

                        qo.addAnd();
                        qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "1");

                        int count = qo.GetCount();

                        float pm = 0;

                        try
                        {
                            if (count != 0 && sum != 0)
                                pm = count / sum;
                        }
                        catch (Exception)
                        {
                        }

                        pmDt.Rows.Add();
                        pmDt.Rows[i]["empNo"] = dt.Rows[i]["FK_Emp"].ToString();
                        pmDt.Rows[i]["fDu"] = Math.Abs(pm);
                    }

                    System.Data.DataView dv = pmDt.DefaultView;
                    dv.Sort = " fDu desc ";
                    pmDt = dv.ToTable();

                    int rowCount = 1;
                    if (pmDt.Rows.Count >= 8)
                    {
                    }
                    else
                    {
                        rowCount = 9 - pmDt.Rows.Count;
                    }

                    string totalHtml = "";
                    for (int i = 0; i < pmDt.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(pmDt.Rows[i]["empNo"].ToString()))
                        {
                            rowCount += 1;
                            continue;
                        }

                        BP.GPM.Emp e = new BP.GPM.Emp(pmDt.Rows[i]["empNo"].ToString());

                        int num = i + 1;
                        if (i == 8)
                            break;
                        if (num <= 3)
                        {
                            totalHtml += "<li>第<font style='font-size:25px;color:Red;font-family:Vijaya;'>" + num + "</font>名:" + e.Name + "</li>";
                        }
                        else
                        {
                            totalHtml += "<li>第" + num + "名:" + e.Name + "</li>";
                        }
                    }

                    for (int i = 1; i < rowCount; i++)
                    {
                        totalHtml += "<li></li>";
                    }

                %>
                <%=totalHtml %>
            </ul>
        </div>
        <div class="main_bottom_left">
            <div class="main_bottom_left_info">
                按期完成率上周总体排名
            </div>
            <ul>
                <%
                    string thisStartWeek = CalculateFirstDateOfWeek(DateTime.Now).ToString("yyyy-MM-dd");//本周第一天
                    string thisEndWeek = CalculateLastDateOfWeek(DateTime.Now).ToString("yyyy-MM-dd");//本周最后一天

                    string startWeek = CalculateFirstDateOfWeek(DateTime.Now.AddDays(-7)).ToString("yyyy-MM-dd");//上周第一天
                    string endWeek = CalculateLastDateOfWeek(DateTime.Now.AddDays(-7)).ToString("yyyy-MM-dd");//上周最后一天

                    sql = "select distinct(fk_emp) from wf_ch";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

                    System.Data.DataTable fDudt = new System.Data.DataTable();

                    fDudt.Columns.Add("empNo", typeof(string));
                    fDudt.Columns.Add("thisWeekWcl", typeof(float));
                    fDudt.Columns.Add("fDu", typeof(float));
                    fDudt.Columns.Add("zFu", typeof(string));


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i]["FK_Emp"].ToString()))
                            continue;
                        fDudt.Rows.Add();
                        qo = new BP.En.QueryObject(ch);

                        qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, dt.Rows[i]["FK_Emp"].ToString());
                        qo.addAnd();
                        qo.AddWhere(BP.WF.Data.CHAttr.DTFrom, ">=", thisStartWeek);
                        qo.addAnd();
                        qo.AddWhere(BP.WF.Data.CHAttr.DTFrom, "<=", thisEndWeek);

                        int sum = qo.GetCount();

                        qo.addAnd();
                        qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "1");

                        int count = qo.GetCount();

                        float thisWeekWcl;
                        try
                        {
                            thisWeekWcl = count / sum * 100;
                        }
                        catch (Exception)
                        {
                            thisWeekWcl = 0;
                        }

                        fDudt.Rows[i]["thisWeekWcl"] = thisWeekWcl;

                        int lastcount = 0;
                        qo = new BP.En.QueryObject(ch);

                        qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, dt.Rows[i]["FK_Emp"].ToString());
                        qo.addAnd();
                        qo.AddWhere(BP.WF.Data.CHAttr.DTFrom, ">=", startWeek);
                        qo.addAnd();
                        qo.AddWhere(BP.WF.Data.CHAttr.DTFrom, "<=", endWeek);

                        sum = qo.GetCount();

                        qo.addAnd();
                        qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "1");

                        lastcount = qo.GetCount();

                        float lastbl = 0;
                        try
                        {
                            if (count - lastcount == 0 || sum == 0)
                            {
                                lastbl = (count - lastcount) / sum * 100;//sum为0的处理,上一期没有
                                lastbl = float.Parse(lastbl.ToString("00.00"));
                            }
                            else
                            {
                                lastbl = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //lastbl = 100;
                            continue;
                        }


                        fDudt.Rows[i]["empNo"] = dt.Rows[i]["FK_Emp"].ToString();
                        fDudt.Rows[i]["fDu"] = Math.Abs(lastbl);

                        string zFu = "0";
                        if (lastbl >= 0)
                        {
                            zFu = "1";
                        }

                        fDudt.Rows[i]["zFu"] = zFu;
                    }

                    dv = fDudt.DefaultView;
                    dv.Sort = " zFu desc ";
                    fDudt = dv.ToTable();

                    rowCount = 0;

                    string lastWeekHtml = "";
                    foreach (System.Data.DataRow dr in fDudt.Rows)
                    {
                        if (string.IsNullOrEmpty(dr["empNo"].ToString()))
                            continue;

                        if (rowCount == 8)
                            break;

                        BP.GPM.Emp e = new BP.GPM.Emp(dr["empNo"].ToString());


                        string str = "<img style='margin:0px 10px;' src='images/down.png' />";//下降
                        if (int.Parse(dr["zFu"].ToString()) > 0)//上升
                            str = "<img style='margin:0px 10px;' src='images/up.png' />";

                        if (dr["fDu"].ToString() == "0")
                        {
                            lastWeekHtml += "<li></li>";
                            rowCount += 1;
                        }
                        else
                        {
                            lastWeekHtml += "<li>" + e.Name + "-" + dr["thisMouthWcl"].ToString() + "%-同比" + str + float.Parse(dr["fDu"].ToString()) + "%</li>";
                            rowCount += 1;
                        }
                    }

                    for (int i = 1; i <= 8 - rowCount; i++)
                    {
                        lastWeekHtml += "<li></li>";
                    }
                %>
                <%=lastWeekHtml%>
            </ul>
        </div>
        <div class="main_bottom_right">
            <div class="main_bottom_right_info">
                按期完成率上月总体排名
            </div>
            <ul>
                <%
                    dTime = DateTime.Now;

                    DateTime lastDt = dTime.AddMonths(-1);//上一月

                    sql = "select distinct(fk_emp) from wf_ch";
                    dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

                    fDudt = new System.Data.DataTable();

                    fDudt.Columns.Add("empNo", typeof(string));
                    fDudt.Columns.Add("thisMouthWcl", typeof(float));
                    fDudt.Columns.Add("fDu", typeof(float));
                    fDudt.Columns.Add("zFu", typeof(string));


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i]["FK_Emp"].ToString()))
                            continue;

                        fDudt.Rows.Add();
                        qo = new BP.En.QueryObject(ch);

                        qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, dt.Rows[i]["FK_Emp"].ToString());
                        qo.addAnd();
                        qo.AddWhere(BP.WF.Data.CHAttr.FK_NY, dTime.ToString("yyyy-MM"));

                        int sum = qo.GetCount();

                        qo.addAnd();
                        qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "1");

                        int count = qo.GetCount();

                        float thisWeekWcl;
                        try
                        {
                            if (sum == 0 || count == 0)
                            {
                                thisWeekWcl = 0;
                            }
                            else
                            {
                                thisWeekWcl = (float)count / sum * 100;
                                thisWeekWcl = float.Parse(thisWeekWcl.ToString("00.00"));
                            }
                        }
                        catch (Exception)
                        {
                            thisWeekWcl = 0;
                        }

                        fDudt.Rows[i]["thisMouthWcl"] = thisWeekWcl;

                        int lastcount = 0;
                        qo = new BP.En.QueryObject(ch);

                        qo.AddWhere(BP.WF.Data.CHAttr.FK_Emp, dt.Rows[i]["FK_Emp"].ToString());
                        qo.addAnd();
                        qo.AddWhere(BP.WF.Data.CHAttr.FK_NY, lastDt.ToString("yyyy-MM"));

                        sum = qo.GetCount();

                        qo.addAnd();
                        qo.AddWhere(BP.WF.Data.CHAttr.CHSta, "1");

                        lastcount = qo.GetCount();

                        float lastbl = 0;
                        try
                        {
                            if (count - lastcount == 0 || sum == 0)
                            {
                                lastbl = 0;
                            }
                            {
                                lastbl = (float)(count - lastcount) / sum * 100;//sum为0的处理,上一期没有
                                lastbl = float.Parse(lastbl.ToString("00.00"));
                            }
                        }
                        catch (Exception)
                        {
                        }


                        fDudt.Rows[i]["empNo"] = dt.Rows[i]["FK_Emp"].ToString();
                        fDudt.Rows[i]["fDu"] = Math.Abs(lastbl);

                        string zFu = "0";
                        if (lastbl >= 0)
                        {
                            zFu = "1";
                        }

                        fDudt.Rows[i]["zFu"] = zFu;
                    }

                    dv = fDudt.DefaultView;
                    dv.Sort = " fDu desc ";
                    fDudt = dv.ToTable();


                    rowCount = 0;


                    string lastMouthHtml = "";
                    foreach (System.Data.DataRow dr in fDudt.Rows)
                    {
                        if (string.IsNullOrEmpty(dr["empNo"].ToString()))
                            continue;

                        if (rowCount == 8)
                            break;

                        BP.GPM.Emp e = new BP.GPM.Emp(dr["empNo"].ToString());

                        string str = "<img style='margin:0px 10px;' src='images/down.png' />";//下降
                        if (int.Parse(dr["zFu"].ToString()) > 0)//上升
                            str = "<img style='margin:0px 10px;' src='images/up.png' />";

                        if (dr["fDu"].ToString() == "0")
                        {
                            lastMouthHtml += "<li></li>";
                            rowCount += 1;
                        }
                        else
                        {
                            lastMouthHtml += "<li>" + e.Name + "-" + dr["thisMouthWcl"].ToString() + "%-同比" + str + float.Parse(dr["fDu"].ToString()) + "%</li>";
                            rowCount += 1;
                        }
                    }

                    for (int i = 1; i <= 8 - rowCount; i++)
                    {
                        lastMouthHtml += "<li></li>";
                    }
                %>
                <%=lastMouthHtml%>
            </ul>
        </div>
        <div class="myworkbgPho">
            <div class="myworkmaintitle">
                我的工作(最近一个月)
            </div>
        </div>
        <div class="mywork_top">
            <div class="mywork_top_info">
                趋势图(按数量)
            </div>
            <div class="mywork_top_chart" id="empChart">
            </div>
        </div>
        <div class="deptworkbgPho">
            <div class="deptworktitle">
                我部门(最近一个月)
            </div>
        </div>
        <div class="deptwork_top">
            <div class="deptwork_top_info">
                趋势图(按数量)
            </div>
            <div class="deptwork_top_chart" id="deptChart">
            </div>
        </div>
        <div class="alldeptbgPho">
            <div class="alldeptworktitle">
                全单位(最近一个月)
            </div>
        </div>
        <div class="alldeptwork_top">
            <div class="alldeptwork_top_info">
                趋势图(按数量)
            </div>
            <div class="alldeptwork_top_chart" id="allDeptChart">
            </div>
        </div>
    </div>
</body>
</html>
