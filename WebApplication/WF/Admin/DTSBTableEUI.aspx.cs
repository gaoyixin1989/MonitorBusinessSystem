using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Sys;
using BP.WF;
using BP.DA;
using System.Data;

namespace CCFlow.WF.Admin
{
    public partial class DTSBTableEUI : System.Web.UI.Page
    {
        #region  参数
        public string FK_Flow
        {
            get
            {
                return getUTF8ToString("FK_Flow");
            }
        }
        public string getUTF8ToString(string param)
        {
            return HttpUtility.UrlDecode(Request[param], System.Text.Encoding.UTF8);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string method = string.Empty;

            string s_responsetext = string.Empty;
            if (string.IsNullOrEmpty(Request["method"]))
                return;

            method = Request["method"].ToString();
            switch (method)
            {
                case "checkData":
                    s_responsetext = checkData();
                    break;
                case "loadData":
                    s_responsetext = loadData();
                    break;
            }
            if (string.IsNullOrEmpty(s_responsetext))
                s_responsetext = "";

            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "text/html";
            Response.Expires = 0;
            Response.Write(s_responsetext);
            Response.End();
        }
        /// <summary>
        /// 所有检查操作都应该在此编写,没有错误返回null.
        /// </summary>
        /// <returns></returns>
        public string checkData()
        {
            Flow fl = new Flow();
            fl.No = this.FK_Flow;
            fl.RetrieveFromDBSources();

            if (fl.DTSWay== BP.WF.Template.FlowDTSWay.None)
                return "配置无效，请更改数据\"同步方式\"!";

            if (string.IsNullOrEmpty(fl.DTSBTable) == true)
                return "配置无效，\"业务表名\"不可以为空!";

            //检查表是否存在.
            string isExitSql = "select count(*) from " + fl.DTSBTable;
            try
            {
                DBAccess.RunSQLReturnValInt(isExitSql,0);
            }
            catch (Exception)
            {
                return "@配置无效，您配置业务数据表[" + fl.DTSBTable + "]库中不存在此表!";
            }
            return "";//检测完毕表明没有错误，开始加载数据.
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string loadData()
        {
            string FK_Flow = getUTF8ToString("FK_Flow");
            string rpt = "ND" + int.Parse(FK_Flow) + "Rpt";
            DataTable rptDt = DBAccess.GetTableSchema(rpt);
            rptDt.Columns.Add("");

            MapAttrs attrs = new MapAttrs(rpt);

            DataTable dt = new DataTable();
            dt.Columns.Add("ZD", typeof(string));
            dt.Columns.Add("ZDMC", typeof(string));
            dt.Columns.Add("LX", typeof(string));
            foreach (MapAttr attr in attrs)
            {
                dt.Rows.Add(attr.Name, attr.KeyOfEn, attr.MyDataTypeStr);
            }

            return DataTableConvertJson.DataTable2Json(dt, dt.Rows.Count);
        }
    }
}