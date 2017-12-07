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
public partial class Channels_Mis_Monitor_Result_QY_AnalysisResult_Lims : PageBase
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