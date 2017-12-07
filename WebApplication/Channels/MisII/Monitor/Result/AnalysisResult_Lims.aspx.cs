using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using System.Data.SqlClient;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;

/// <summary>
/// 功能描述：分析结果录入 仪器数据 清远
/// 创建日期：2013-10-17
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Mis_Monitor_Result_ZZ_AnalysisResult_Lims : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strID = "";
        if (Request["strid"] != null)
        {
            strID = this.Request["strid"].ToString();
        }

        //获取指定动态属性类别的动态属性数据
        if (Request["type"] != null && Request["type"].ToString() == "GetInfo")
        {
            GetInfo();
        }
        //初始化加载工作曲线图
        if (Request["type"] != null && Request["type"].ToString() == "getinitchartdata")
        {
            //Response.Write("{\"title\": {\"text\": \"\u52A8\u6001\u5206\u6790\",\"style\": \"{color:#505050;font-size:20px;font-family:Microsoft YaHei,Verdana; }\"},\"x_axis\": {\"labels\": {\"labels\": [{\"text\": \"\",\"size\": 12,\"visible\": true},{\"text\": \"\",\"size\": 12,\"visible\": true},{\"text\": \"\",\"size\": 12,\"visible\": true},{\"text\": \"\",\"size\": 12,\"visible\": true},{\"text\": \"\",\"size\": 12,\"visible\": true},{\"text\": \"\",\"size\": 12,\"visible\": true},{\"text\": \"\",\"size\": 12,\"visible\": true}]},\"colour\": \"#505050\",\"grid-colour\": \"#FFFFFF\",\"3d\": 0,\"offset\": true},\"y_axis\": {\"tick-length\": 0,\"labels\": {\"colour\": \"#505050\"},\"colour\": \"#505050\",\"grid-colour\": \"#C1C1C1\",\"steps\": 20,\"min\": 20,\"max\": 100,\"3d\": 0,\"offset\": false},\"elements\": [],\"x_legend\": {\"text\": \"\u65F6\u95F4\",\"style\": \"{font-family:Microsoft YaHei,Verdana;font-size:18px;text-align:center;color:#5B7993}\"},\"y_legend\": {\"text\": \"\u58F0\u7EA7(dB)\",\"style\": \"{font-family:Microsoft YaHei,Verdana;font-size:18px;text-align:center;color:#5B7993}\"},\"bg_colour\": \"#F1FFFF\"}");
            GetInitChartData();
        }

        // 获取标准曲线列表
        if (Request["type"] != null && Request["type"].ToString() == "showLineTable")
        {
            showLineTable();
        }
        
    }

    /// <summary>
    /// 获取初始化图表数据 Create By weilin
    /// </summary>
    /// <returns></returns>
    public void GetInitChartData()
    {
        string strTime = "";

        if (Request["TIME"] != null)
        {
            strTime = Request["TIME"].ToString();
        }
        else
        {
            strTime= "01 27 2015  4:51PM";
        }

        string strJson = "";
        //定义标题
        OpenFlashChart.OpenFlashChart objChart = new OpenFlashChart.OpenFlashChart();
        SetFlashChartTitle(objChart, "工作曲线图");

        // 获取坐标值
        string strConnection = ConfigurationManager.ConnectionStrings["i3OracleConnect"].ToString();

        DataTable objTable = new DataTable();
        string strLimsSql = "select * from T_DC_ONLINE_MOM_RST" + " where TIME=" + "'"+ strTime + "'";
        objTable = ExecuteDataTableEx(strLimsSql, strConnection);


        List<double> objTaList = new List<double>();  //气温Y轴数据对象

        OpenFlashChart.XAxisLabels objXList = new OpenFlashChart.XAxisLabels(); //定义X轴数据对象

        for (int i = 0; i < objTable.Rows.Count-1 && objTable.Rows.Count % 2 ==0; i=i+2)
        {
            if ((Convert.ToDouble(objTable.Rows[i + 1]["VALUE"]) - 0.0) > 0.000001)
            {
                objTaList.Add(Convert.ToDouble( objTable.Rows[i+1]["VALUE"]));

                OpenFlashChart.AxisLabel objXLable = new OpenFlashChart.AxisLabel();
                objXLable.Text = (objTable.Rows[i]["VALUE"]).ToString();
                objXList.Add(objXLable);
            }
        }

        AddFlashChartLine(objChart, objTaList, "", OpenFlashChartColor[0], "#val#");

        //设置图表X轴对象
        SetFlashChartXAxis(objChart, objXList);

        //设置图表Y轴对象
        SetFlashChartYAxis(objChart, 0, 100, 10);
        ////设置图表Y轴对象
        //List<string> objYList = new List<string>();
        //objYList.Add("1");
        //objYList.Add("10");
        //objYList.Add("20");
        //objYList.Add("30");
        //objYList.Add("50");
        //objChart.Y_Axis.Labels.SetLabels(objYList);
        //添加X轴名称
        AddFlashChartXLegend(objChart, "浓度值X");
        //添加Y轴名称
        AddFlashChartYLegend(objChart, "吸收值Y=a+bX");
        strJson = objChart.ToPrettyString();

        Response.Write(strJson);
        Response.End();
    }

    private void showLineTable()
    {
        // 获取坐标值
        string strConnection = ConfigurationManager.ConnectionStrings["i3OracleConnect"].ToString();

        DataTable objTable = new DataTable();
        string strLimsSql = "select distinct(TIME) from T_DC_ONLINE_MOM_RST";
        objTable = ExecuteDataTableEx(strLimsSql, strConnection);

        string strJson;// = DataTableToJson(objTable);
        strJson = CreateToJson(objTable, objTable.Rows.Count);
        Response.Write(strJson);
        Response.End();
    }

    private void GetInfo()
    {
        string strJson = "[]";

        if (Request["ResultID"] == null)
        {
            Response.Write(strJson);
            Response.End();
            return;
        }

        //获取点位及样品信息
        string strResultID = Request["ResultID"].ToString();
        TMisMonitorResultVo objResult = new TMisMonitorResultLogic().Details(strResultID);
        string strSrcID = objResult.REMARK_4;
        string strSrcTable = objResult.REMARK_5;
        if (strSrcID.Length == 0)
        {
            Response.Write(strJson);
            Response.End();
            return;
        }

        string strConnection = ConfigurationManager.ConnectionStrings["Lims"].ToString();

        DataTable objTable = new DataTable();
        string strLimsSql = "select * from " + strSrcTable + " where ID=" + strSrcID;
        objTable = ExecuteDataTableEx(strLimsSql, strConnection);

        if (objTable.Rows.Count > 0)
        {
            objTable.Columns.Add("TableName");
            objTable.Rows[0]["TableName"] = strSrcTable;

            strJson = DataTableToJson(objTable);
        }

        Response.Write(strJson);
        Response.End();
    }

    private DataTable ExecuteDataTableEx(string strCommandText, string strConnectionStatic)
    {
        SqlCommand objCommand = new SqlCommand(strCommandText);
        objCommand.CommandType = CommandType.Text;
        SqlConnection objConnection = new SqlConnection(strConnectionStatic);
        SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
        DataTable objTable = new DataTable();

        try
        {
            PrepareCommand(objCommand, objConnection, null, CommandType.Text, strCommandText);

            objAdapter.Fill(objTable);
        }
        catch (Exception ex)
        {
            //Tips.AppendLine(ex.Message);
        }
        finally
        {
            objAdapter.Dispose();
            objCommand.Dispose();
            objConnection.Close();
            objConnection.Dispose();
        }

        return objTable;
    }



    /// <summary>
    /// 命令准备
    /// </summary>
    /// <param name="objCommand">命令</param>
    /// <param name="objConnection">连接</param>
    /// <param name="objTransaction">事务</param>
    /// <param name="strCommandType">命令类型</param>
    /// <param name="strCommandText">命令明细</param>
    private void PrepareCommand(SqlCommand objCommand, SqlConnection objConnection, SqlTransaction objTransaction, CommandType strCommandType, string strCommandText)
    {
        if (objConnection.State != ConnectionState.Open)
        {
            objConnection.Open();
        }

        objCommand.Connection = objConnection;
        objCommand.CommandText = strCommandText;

        if (objTransaction != null)
        {
            objCommand.Transaction = objTransaction;
        }

        objCommand.CommandType = strCommandType;
    }
}