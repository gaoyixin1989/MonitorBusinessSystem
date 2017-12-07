<%@ WebHandler Language="C#" Class="select" %>

using System;
using System.Web;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Web.Script.Serialization;

using i3.BusinessLogic.Channels.Base.MonitorType;

/// <summary>
/// 前台下拉框数据入口
/// 所以下拉框通用函数，传入view=表名，idfield=下拉框的value，textfield=下拉框的text，distict：是否distinct，where：查询条件，格式如（字段名|内容-字段名2-内容2）
/// </summary>
public class select : IHttpHandler
{ 
    public void ProcessRequest (HttpContext context) { 
        context.Response.ContentType = "text/plain";

        string strview = (context.Request.QueryString["view"] != null) ? context.Request.QueryString["view"] : "";
        string stridfield = (context.Request.QueryString["idfield"] != null) ? context.Request.QueryString["idfield"] : "";
        string strtextfield = (context.Request.QueryString["textfield"] != null) ? context.Request.QueryString["textfield"] : "";
        string strdistinct = (context.Request.QueryString["distinct"] != null) ? context.Request.QueryString["distinct"] : "";
        string strwhere = (context.Request.QueryString["where"] != null) ? context.Request.QueryString["where"] : "";
        string strOrder = (context.Request.QueryString["Order"] != null) ? context.Request.QueryString["Order"] : "";

        DataTable dt = new TBaseMonitorTypeInfoLogic().SelectByTable(strview, strwhere, stridfield, strtextfield, strdistinct, strOrder);

        string json = DataTableToJson(dt);
        
        context.Response.Write(json);
        context.Response.End();   
    } 
    
    public bool IsReusable {
        get {
            return false;
        }
    }

    /// <summary>
    /// DataTable 转换为 Json 字符串
    /// </summary>
    /// <param name="dtSource">数据集</param>
    /// <returns>JSON 字符串</returns>
    private  string DataTableToJson(DataTable dtSource)
    {
        if (dtSource == null) return "";
        List<object> listRows = new List<object>();
        foreach (DataRow dr in dtSource.Rows)
        {
            Dictionary<string, object> diRow = new Dictionary<string, object>();
            foreach (DataColumn dc in dtSource.Columns)
            {
                diRow.Add(dc.ColumnName, dr[dc].ToString());
            }
            listRows.Add(diRow);
        }
        return ToJson(listRows);
    }

    /// <summary>
    /// 对象转换为 Json
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns>JSON字符串</returns>
    private string ToJson(object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        return serializer.Serialize(obj);
    }
}