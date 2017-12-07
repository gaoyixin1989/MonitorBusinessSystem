﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Web;
public partial class DataUser_Do : System.Web.UI.Page
{
    #region 常用变量.
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public string FK_Node
    {
        get
        {
            return this.Request.QueryString["FK_Node"];
        }
    }
    public Int64 WorkID
    {
        get
        {
            return Int64.Parse( this.Request.QueryString["WorkID"]);
        }
    }
    public Int64 OID
    {
        get
        {
            return Int64.Parse(this.Request.QueryString["OID"]);
        }
    }
    #endregion 常用变量.

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 输出系统url.
        //string s = "";
        //foreach (string key   in this.Request.QueryString.AllKeys)
        //{
        //    s += " , " + key + "=" + this.Request.QueryString[key];
        //}
        //this.Response.Write(s);
        //Log.DefaultLogWriteLineError(s);
        //return;
        #endregion 输出系统url.

        try
        {
            switch (this.DoType)
            {
                case "SetHeJi":
                    string sql = "UPDATE ND101 SET HeJi=(SELECT SUM(XiaoJi) FROM ND101Dtl1 WHERE RefPK=" + this.OID + ") WHERE OID=" + this.OID;
                    BP.DA.DBAccess.RunSQL(sql);
                    //把合计转化成大写.
                    float hj = BP.DA.DBAccess.RunSQLReturnValFloat("SELECT HeJi FROM ND101 WHERE OID=" + this.OID,0);
                    sql = "UPDATE ND101 SET DaXie='" + BP.DA.DataType.ParseFloatToCash(hj) + "' WHERE OID=" + this.OID;
                    BP.DA.DBAccess.RunSQL(sql);
                    break;
                case "OutOK":
                    /*在这是里处理您的业务过程。*/
                    return;
                default:
                    break;
            }
        }
        catch(Exception ex)
        {
            string info = "error:" + ex.StackTrace + " message:" + ex.Message;
            //info = System.Text.Encoding.UTF8.GetString(info);
            this.Response.Write(info);
        }
    }
     
}